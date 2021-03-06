module Capstone4.Operations

open System
open Capstone4.Domain

let declassifyAccount ratedAccount =
    match ratedAccount with
    | InCredit(CreditAccount account )
    | Overdrawn account -> account

let classifyAccount account =
    if account.Balance >= 0M then (InCredit(CreditAccount account))
    else Overdrawn account
/// Withdraws an amount of an account (if there are sufficient funds)
// let withdraw amount account =
//     if account.Balance < 0M then account
//     else { account with Balance = account.Balance - amount }
let withdraw amount (CreditAccount account) =
    { account with Balance = account.Balance - amount }
    |> classifyAccount

let withdrawSafe amount ratedAccount =
    match ratedAccount with
    | InCredit(CreditAccount account) -> 
        CreditAccount account |> withdraw amount
    | Overdrawn _ ->
        printfn "Your account is overdrawn - withdrawal rejected."
        ratedAccount // return input back out

let withdrawForAudit amount account =
    let ratedAccount = account |> classifyAccount

    match ratedAccount with
    | InCredit(CreditAccount acc) ->
        CreditAccount acc 
        |> withdraw amount 
        |> declassifyAccount
    | Overdrawn account ->
        printfn "Your account is overdrawn - withdrawal rejected."
        account

/// Deposits an amount into an account
let deposit amount ratedAccount =
    let account =
        match ratedAccount with
        | InCredit (CreditAccount account) -> account
        | Overdrawn account -> account
    { account with Balance = account.Balance + amount }    
    |> classifyAccount

let depositForAudit amount account =
    { account with Balance = account.Balance + amount }

/// Runs some account operation such as withdraw or deposit with auditing.
let auditAs operationName audit operation amount ratedAccount =
    let account : Account =
        match ratedAccount with
        | InCredit(CreditAccount acc) -> acc
        | Overdrawn acc -> acc

    let updatedAccount = operation amount account
    
    let accountIsUnchanged = (updatedAccount = account)

    let transaction =
        let transaction = { Operation = operationName; Amount = amount; Timestamp = DateTime.UtcNow; Accepted = true }
        if accountIsUnchanged then { transaction with Accepted = false }
        else transaction

    audit account.AccountId account.Owner.Name transaction
    updatedAccount

let tryParseSerializedOperation operation =
    match operation with
    | "withdraw" -> Some Withdraw
    | "deposit" -> Some Deposit
    | _ -> None

/// Creates an account from a historical set of transactions
let loadAccount (owner, accountId, transactions) =
    let openingAccount = classifyAccount { AccountId = accountId; Balance = 0M; Owner = { Name = owner } }

    transactions
    |> Seq.sortBy(fun txn -> txn.Timestamp)
    |> Seq.fold(fun account txn ->
        let operation = tryParseSerializedOperation txn.Operation
        match operation, account with
        | Some Deposit, _ -> account |> deposit txn.Amount
        | Some Withdraw, InCredit account -> account |> withdraw txn.Amount
        | Some Withdraw, Overdrawn _ -> account
        | None, _ -> account) openingAccount
        

//type BankOperation = | Withdraw | Deposit
//type Command = AccountCommand of BankOperation | Exit

// char -> Command option
// Command -> BankOption option
 
let tryParseCommand char =
    match char with
    | 'd' -> Some (AccountCommand Deposit)
    | 'w' -> Some (AccountCommand Withdraw)
    | 'x' -> Some Exit
    | _ -> None

let tryGetBankOperation cmd =
    match cmd with
    | Exit -> None
    | AccountCommand operation -> Some operation

// let loadAccountOptional value =
//     match value with
//     | Some value -> Some (loadAccount value)
//     | None -> None

let loadAccountOptional value = value |> Option.map loadAccount