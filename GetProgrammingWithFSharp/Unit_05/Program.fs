module Capstone

open System
open Capstone.Domain
open Capstone.Operations
open Capstone.Auditing
open Capstone.FileRepository

// [<AutoOpen>]
// module CommandParsing =
//     let isValidCommand cmd = [ 'd'; 'w'; 'x' ] |> List.contains cmd
//     let isStopCommand = (=) Exit
type Command = AccountCmd of BankOperation | Exit

[<AutoOpen>]
module CommandParsing =
    let tryParse cmd =
        match cmd with
        | 'd' -> Some (AccountCmd Deposit)
        | 'w' -> Some (AccountCmd Withdraw)
        | 'x' -> Some Exit
        | _ -> None

[<AutoOpen>]
module UserInput =
    let commands =
        Seq.initInfinite(fun _ ->
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            let output = Console.ReadKey().KeyChar
            Console.WriteLine()
            output)
    
    let getAmount command =
        let captureAmount _ =
            Console.Write "Enter Amount: "
            Console.ReadLine() |> Decimal.TryParse
        Seq.initInfinite captureAmount
        |> Seq.choose(fun amount ->
            match amount with
            | true, amount when amount <= 0M -> None
            | false, _ -> None
            | true, amount -> Some(command, amount))
        |> Seq.head


    let loadAccountOptional = Option.map loadAccount

    let withdrawWithAudit = auditAs "withdraw" composedLogger withdraw
    let depositWithAudit = auditAs "deposit" composedLogger deposit

    // let loadAccountFromDisk = Capstone4.FileRepository.findTransactionsOnDisk >> loadAccount
    let tryLoadAccountFromDisk = tryFindTransactionsOnDisk >> loadAccountOptional

[<EntryPoint>]
let main _ =
    let openingAccount =
        Console.Write "Please enter your name: "
        let owner = Console.ReadLine()
                
        match tryLoadAccountFromDisk owner with
        | Some account -> account
        | None ->
            InCredit(CreditAccount { AccountId = Guid.NewGuid()
                                     Balance = 0M
                                     Owner = { Name = owner } })
    
    printfn "Opening balance is £%M" (openingAccount.GetField(fun a -> a.Balance))

    let processCommand account (command, amount) =
        printfn ""
        let account =
            match command with
            | Deposit -> depositWithAudit amount account
            | Withdraw ->
                match account with
                | InCredit account -> account |> withdrawWithAudit amount
                | Overdrawn _ ->
                    printfn "You cannot withdraw funds as your account is overdrawn!"
                    account
        printfn "Current balance is £%M" (account.GetField(fun a -> a.Balance))
        match account with
        | InCredit _ -> ()
        | Overdrawn _ -> printfn "Your account is overdrawn!!"
        account

    let closingAccount =
        commands
        |> Seq.choose tryParse
        |> Seq.takeWhile ((<>) Exit)
        |> Seq.choose(fun cmd ->
            match cmd with
            | Exit -> None
            | AccountCmd cmd -> Some cmd)
        |> Seq.map getAmount
        |> Seq.fold processCommand openingAccount
    
    printfn ""
    printfn "Closing Balance:\r\n %A" closingAccount
    Console.ReadKey() |> ignore

    0