namespace global

// .NET modules
open System
open System.IO

// User-defined modules
open Auditing
open Operations
open Transactions
open FileRepository

[<AutoOpen>]
module CommandParsing =
    let consoleCommands = seq {
        while true do
            printfn "(d)eposit, (w)ithdraw or e(x)it : "
            yield Console.ReadKey().KeyChar }

    // check whether the user input is valid
    let isValidCommand input =
        match input with
        | 'd' | 'w' | 'x' -> true
        | _ -> false

    let isStopCommand input =
        match input with
        | 'x' -> true
        | _ -> false

    let getAmount input =
        if input = 'x' then ('x', 0M)
        else
            printf "%sEnter Amount: " Environment.NewLine
            let amount = Console.ReadLine() |> decimal
            printfn "%s" Environment.NewLine
            (input, amount)


[<AutoOpen>]
module FileRepository =
    let private accountsPath =
        let path = @"accounts"
        Directory.CreateDirectory path |> ignore
        path

    let private findAccountFolder owner =    
        let folders = Directory.EnumerateDirectories(accountsPath, sprintf "%s_*" owner)
        if Seq.isEmpty folders then ""
        else
            let folder = Seq.head folders
            DirectoryInfo(folder).Name
    
    let private buildPath(owner, accountId:Guid) = sprintf @"%s\%s_%O" accountsPath owner accountId

    let loadTransactions (folder:string) =
        let owner, accountId =
            let parts = folder.Split '_'
            parts.[0], Guid.Parse parts.[1]
        owner, accountId, buildPath(owner, accountId)
                          |> Directory.EnumerateFiles
                          |> Seq.map (File.ReadAllText >> deserialize)

    /// Finds all transactions from disk for specific owner.
    let findTransactionsOnDisk owner =
        let folder = findAccountFolder owner
        if String.IsNullOrEmpty folder then owner, Guid.NewGuid(), Seq.empty
        else loadTransactions folder

    /// Logs to the file system
    let writeTransaction accountId owner transaction =
        let path = buildPath(owner, accountId)    
        path |> Directory.CreateDirectory |> ignore
        let filePath = sprintf "%s/%d.txt" path (transaction.Timestamp.ToFileTimeUtc())
        let line = sprintf "%O***%c***%M***%b" transaction.Timestamp transaction.Operation transaction.Amount transaction.Accepted
        File.WriteAllText(filePath, line)


[<AutoOpen>]
module Auditing =
    let printTransaction _ accountId transaction =
        printfn "Account %O: %c of %M (approved: %b)" accountId transaction.Operation transaction.Amount transaction.Accepted

    // Logs to both console and file system
    let composedLogger = 
        let loggers =
            [ writeTransaction
              printTransaction ]
        fun accountId owner transaction ->
            loggers
            |> List.iter(fun logger -> logger accountId owner transaction)

[<AutoOpen>]
module UserInput =
    let commands = seq {
        while true do
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            yield Console.ReadKey().KeyChar
            Console.WriteLine() }
    
    let getAmount command =
        Console.WriteLine()
        Console.Write "Enter Amount: "
        command, Console.ReadLine() |> Decimal.Parse



module AppStart =
    [<EntryPoint>]
    let main argv =
        let withdrawWithAudit = auditAs 'w' composedLogger withdraw
        let depositWithAudit = auditAs 'd' composedLogger deposit
        let loadAccountFromDisk = findTransactionsOnDisk >> loadAccount

        let openingAccount =
            Console.Write "Please enter your name: "
            Console.ReadLine() |> loadAccountFromDisk
        
        printfn "Current balance is £%M" openingAccount.Balance

        let processCommand account (command, amount) =
            printfn ""
            let account =
                if command = 'd' then account |> depositWithAudit amount
                else account |> withdrawWithAudit amount
            printfn "Current balance is £%M" account.Balance
            account

        let closingAccount =
            commands
            |> Seq.filter isValidCommand
            |> Seq.takeWhile (not << isStopCommand)
            |> Seq.map getAmount
            |> Seq.fold processCommand openingAccount
        
        printfn ""
        printfn "Closing Balance:\r\n %A" closingAccount
        Console.ReadKey() |> ignore
        0 // return an integer exit code
