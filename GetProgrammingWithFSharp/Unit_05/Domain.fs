namespace Capstone4.Domain

open System

type Customer = { Name : string }
type Transaction = { Timestamp : DateTime; Operation : string; Amount : decimal}

type Account = { AccountId : Guid; Owner : Customer; Balance : decimal }
// Marker type for an account in credit
type CreditAccount = CreditAccount of Account
// Categorization of Account
type RatedAccount =
| InCredit of CreditAccount
| Overdrawn of Account



type BankOperation = | Withdraw | Deposit
type Command = AccountCommand of BankOperation | Exit
    
module Transactions =
    /// Serializes a transaction
    let serialize transaction =
        sprintf "%O***%s***%M***%b" transaction.Timestamp transaction.Operation transaction.Amount
    
    /// Deserializes a transaction
    let deserialize (fileContents:string) =
        let parts = fileContents.Split([|"***"|], StringSplitOptions.None)
        { Timestamp = DateTime.Parse parts.[0]
          Operation = parts.[1]
          Amount = Decimal.Parse parts.[2] }

    let acc = { AccountId = Guid.NewGuid(); Owner = { Name = "Alican" }; Balance = 0M} 