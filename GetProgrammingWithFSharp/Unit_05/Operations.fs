namespace Capstone4

open System
open Capstone4.Domain

module Operations =
    let classifyAccount account =
        if account.Balance >= 0M then (InCredit(CreditAccount account))
        else Overdrawn account

    /// Withdraws an amount of an account (if there are sufficient funds)
    let withdraw amount (CreditAccount account) =
        { account with Balance = account.Balance - amount }
        |> classifyAccount

    let withdrawSafe amount ratedAccount =
        match ratedAccount with
        | InCredit account -> account |> withdraw amount
        | Overdrawn _ ->
            printfn "Your account is overdrawn - withdrawal rejected!"    
            ratedAccount

    /// Deposits an amount into an account
    let deposit amount account =
        let account =
            match account with
            | InCredit (CreditAccount account) -> account
            | Overdrawn account -> account
        { account with Balance = account.Balance + amount }
        |> classifyAccount

    /// Runs some account operation such as withdraw or deposit with auditing.
    let auditAs operationName audit operation amount account =
        let updatedAccount = operation amount account
        
        let accountIsUnchanged = (updatedAccount = account)

        let transaction =
            let transaction = { Operation = operationName; Amount = amount; Timestamp = DateTime.UtcNow; Accepted = true }
            if accountIsUnchanged then { transaction with Accepted = false }
            else transaction

        audit account.AccountId account.Owner.Name transaction
        updatedAccount

    /// Creates an account from a historical set of transactions
    let loadAccount (owner, accountId, transactions) =
        let openingAccount = 
            match { AccountId = accountId; Balance = 0M; Owner = { Name = owner } } with
            | InCredit -> CreditAccount account
            | account -> 



        transactions
        |> Seq.sortBy(fun txn -> txn.Timestamp)
        |> Seq.fold(fun account txn ->
            if txn.Operation = "withdraw" then account |> withdraw txn.Amount
            else account |> deposit txn.Amount) openingAccount

    // let tryParseCommand c =
    //     match c with
    //     | 'w' -> Some Withdraw
    //     | 'd' -> Some Deposit
    //     | 'x' -> Some Exit
    //     | _ -> None

    let tryGetBankOperation cmd =
       

        match cmd with
        | Exit -> None
        | AccountCommand op -> Some op