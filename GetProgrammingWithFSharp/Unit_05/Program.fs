module Capstone4.Program

open System
open Capstone4.Domain
open Capstone4.Operations
open Capstone4.FileRepository

let withdrawWithAudit = auditAs "withdraw" Auditing.composedLogger withdrawForAudit
let depositWithAudit = auditAs "deposit" Auditing.composedLogger depositForAudit
let tryLoadAccountFromDisk = FileRepository.tryFindTransactionsOnDisk >> Operations.loadAccountOptional

[<AutoOpen>]
module UserInput =
    let commands = seq {
        while true do
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            yield Console.ReadKey().KeyChar
            Console.WriteLine() }
    
    let tryGetAmount command =
        Console.WriteLine()
        Console.Write "Enter Amount: "
        let amount = Console.ReadLine() |> Decimal.TryParse

        match amount with
        | true, amount -> Some (command, amount)
        | false, _ -> None    


[<EntryPoint>]
let main _ =
    let openingAccount =
        Console.Write "Please enter your name: "
        let owner = Console.ReadLine()

        match (tryLoadAccountFromDisk owner) with
        | Some account -> account
        | None ->
            { Balance = 0M 
              AccountId = Guid.NewGuid()
              Owner = { Name = owner }} |> classifyAccount

    let account =
        match openingAccount with
        | InCredit (CreditAccount acc) -> acc
        | Overdrawn acc -> acc

    printfn "Current balance is £%M" account.Balance

    //   let processCommand account (command, amount) =
    //     printfn ""
    //     let account =
    //         match command with
    //         | Deposit -> account |> depositWithAudit amount
    //         | Withdraw ->
    //             match account with
    //             | InCredit account -> account |> withdrawWithAudit amount
    //             | Overdrawn _ ->
    //                 printfn "You cannot withdraw funds as your account is overdrawn!"
    //                 account
    //     printfn "Current balance is £%M" (account.GetField(fun a -> a.Balance))
    //     match account with
    //     | InCredit _ -> ()
    //     | Overdrawn _ -> printfn "Your account is overdrawn!!"
    //     account

    let processCommand ratedAccount (command, amount) =
        printfn ""
        let returnAccount =
            match command with
            | Deposit -> 
                ratedAccount
                |> depositWithAudit amount
                |> classifyAccount
            | Withdraw -> 
                match ratedAccount with
                | InCredit (CreditAccount _) -> 
                    ratedAccount
                    |> withdrawWithAudit amount 
                    |> classifyAccount
                | Overdrawn _ ->
                    printfn "Withdraw failed. Account overdrawn."
                    ratedAccount

        match returnAccount with
        | InCredit (CreditAccount account)
        | Overdrawn account ->
            printfn "Current balance is £%M" account.Balance
                
        returnAccount

    

// folder: Account -> BankOperation -> Account

    let closingAccount =
        commands
        // Filtering invalid characters from the stream
        |> Seq.choose tryParseCommand
        |> Seq.takeWhile (fun (c: Command) -> c <>  Exit)
        |> Seq.choose tryGetBankOperation
        |> Seq.choose tryGetAmount
        |> Seq.fold processCommand openingAccount
        // |> Seq.fold processCommand openingAccount
    
    printfn ""
    printfn "Closing Balance:\r\n %A" closingAccount
    Console.ReadKey() |> ignore

    0