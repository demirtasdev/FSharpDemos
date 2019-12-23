module Capstone.Domain

open System

type Customer = { Name : string }
type Transaction = { Timestamp : DateTime; Operation : string; Amount : decimal; Accepted : bool }
type Account = { AccountId : Guid; Owner : Customer; Balance : decimal }
type CreditAccount = CreditAccount of Account
type RatedAccount =
    | InCredit of CreditAccount
    | Overdrawn of Account
    member this.GetField getter =
        match this with
        | InCredit (CreditAccount account) -> getter account
        | Overdrawn account -> getter account

type BankOperation = Deposit | Withdraw
type Command = AccountCommand of BankOperation | Exit

module Transactions =
    /// Serializes a transaction
    let serialize transaction =
        sprintf "%O***%s***%M***%b" transaction.Timestamp transaction.Operation transaction.Amount transaction.Accepted
    
    /// Deserializes a transaction
    let deserialize (fileContents:string) =
        let parts = fileContents.Split([|"***"|], StringSplitOptions.None)
        { Timestamp = DateTime.Parse parts.[0]
          Operation = parts.[1]
          Amount = Decimal.Parse parts.[2]
          Accepted = Boolean.Parse parts.[3] }