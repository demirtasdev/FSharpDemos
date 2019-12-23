module Capstone.Domain

open System

type Customer = { FullName : string }
type Account = { ID : Guid; Owner : Customer; Balance : decimal }
type Transaction = { Timestamp : DateTime; Operation : char; Amount : decimal; Accepted : bool }

module Transactions =
    
    let serialize transaction =
        sprintf "%O***%c***%M***%b" transaction.Timestamp transaction.Operation transaction.Amount transaction.Accepted

    /// Deserializes a transaction
    let deserialize (fileContents:string) =
        let parts = fileContents.Split([|"***"|], StringSplitOptions.None)
        { Timestamp = DateTime.Parse parts.[0]
          Operation = parts.[1] |> char
          Amount = Decimal.Parse parts.[2]
          Accepted = Boolean.Parse parts.[3] }

    