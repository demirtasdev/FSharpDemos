namespace Capstone4.Program

open System
open Capstone4.Domain
open Capstone4.Operations
open Capstone4.Auditing
open Capstone4.FileRepository

// [<AutoOpen>]
// module CommandParsing =
//     let isValidCommand cmd = [ 'd'; 'w'; 'x' ] |> List.contains cmd
//     let isStopCommand = (=) Exit

[<AutoOpen>]
module UserInput =
    let commands = seq {
        while true do
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            yield Console.ReadKey().KeyChar
            Console.WriteLine() }
    
    // let getAmount command =
    //     Console.WriteLine()
    //     Console.Write "Enter Amount: "
    //     command, Console.ReadLine() |> Decimal.Parse

    let tryGetAmount command =
        Console.WriteLine()
        printf "Enter Amount: "
        let amount = Console.ReadLine() |> Decimal.TryParse
        match amount with
        | true, amount -> Some(command, amount)
        | false, _ -> None


module AppStart =

    let loadAccountOptional = Option.map loadAccount

    let withdrawWithAudit = auditAs "withdraw" composedLogger withdraw
    let depositWithAudit = auditAs "deposit" composedLogger deposit

    // let loadAccountFromDisk = Capstone4.FileRepository.findTransactionsOnDisk >> loadAccount
    let tryLoadAccountFromDisk = tryFindTransactionsOnDisk >> loadAccountOptional

    [<EntryPoint>]
    let main _ =
        let openingAccount =
            Console.Write "Please enter your name: "
            // Console.ReadLine() |> loadAccountFromDisk
            let owner = Console.ReadLine()

            match (tryLoadAccountFromDisk owner) with
            | Some account -> account
            | None ->
                { Balance = 0M
                  AccountId = Guid.NewGuid()
                  Owner = {Name = owner}}
        
        printfn "Current balance is £%M" openingAccount.Balance

        let processCommand account (command, amount) =
            printfn ""
            let account =
                match command with
                | Deposit -> account |> depositWithAudit amount
                | Withdraw -> account |> withdrawWithAudit amount
                | _ -> { 
                    AccountId = Guid.Empty
                    Owner = { Name = "ERROR: INVALID INPUT" }
                    Balance = 0M }
            
            printfn "Current balance is £%M" account.Balance
            account

        let closingAccount =
            commands
            |> Seq.choose tryGetBankOperation
            |> Seq.takeWhile (fun x -> x <> Exit)
            |> Seq.choose tryGetAmount
            |> Seq.fold processCommand openingAccount
        
        printfn ""
        printfn "Closing Balance:\r\n %A" closingAccount
        Console.ReadKey() |> ignore

        0