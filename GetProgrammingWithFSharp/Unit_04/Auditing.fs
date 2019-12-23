namespace Capstone.Auditing

open Capstone.Domain
open Capstone.Operations
open Capstone.FileRepository

module Auditing =
    let printTransaction _ accountId transaction =
        printfn "Account %O: %c of %M (approved: %b)" accountId transaction.Operation transaction.Amount transaction.Accepted

    // Logs to both console and file system
    let composedLogger = 
        let loggers =
            [ FileRepository.writeTransaction
              printTransaction ]
        fun accountId owner transaction ->
            loggers
            |> List.iter(fun logger -> logger accountId owner transaction)