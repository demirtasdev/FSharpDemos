namespace Capstone.Operations

open System
open Capstone.Domain

module Operations =
    let withdraw amount account =
        if amount > account.Balance then account
        else { account with Balance = account.Balance - amount }

    /// Deposits an amount into an account
    let deposit amount account =
        { account with Balance = account.Balance + amount }    

    let auditAs operationName audit operation amount account =
        let updatedAccount = operation amount account
        
        let accountIsUnchanged = (updatedAccount = account)

        let transaction =
            let transaction = { Operation = operationName; Amount = amount; Timestamp = DateTime.UtcNow; Accepted = true }
            if accountIsUnchanged then { transaction with Accepted = false }
            else transaction

        audit account.ID account.Owner.FullName transaction
        updatedAccount

    let loadAccount (owner, accountId, transactions) =
        let openingAccount = { ID = accountId; Balance = 0M; Owner = { FullName = owner } }

        transactions
        |> Seq.sortBy(fun txn -> txn.Timestamp)
        |> Seq.fold(fun account txn ->
            if txn.Operation = 'w' then account |> withdraw txn.Amount
            else account |> deposit txn.Amount) openingAccount

     