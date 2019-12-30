open System


// Q1 Write a Wrapper Type
type Man = Male of string
type Woman = Female of string

let man = Male "Alican"
let woman = Female "Asena"

// Q2 Compare Wrapped Records
let wrappedVsNonWrapper = man = "Alican"
let wrappedVsWrapped = man = Male "Alican"

// Q3 Unwrap a value
let (Male name) = man

// Q4: Benefits of Wrappers
// Wrappers are useful tools for type-safety and allow
// us hints on fieldnames while binding values to keywords.

// Q5: Represending Business States w/ Wrappers
type Transaction = { Amount: decimal }
type Successful = Successful of Transaction
type Failed = Failed of Transaction * errorMessage:string
type Pending = Pending of Transaction

let successOrFailure transaction =
    match transaction with
    | Failed({Amount = amount}, errorMessage) ->
        printfn "Failed to draw %M. %s" amount errorMessage
    | _ -> printfn "Blabablabablaa..."    

// Q6: Restrict a Function via a Wrapper
let drawMoney (transaction:Successful) =
    printfn "Transaction successful"

type Person = { Name:string; Age:int; Gender:string }

let me = { Name = "Alican"; Age = 22; Gender = "Male" }


// Q7: Use a Result Type
let isOlderThan21 person =
    match person with
    | p when p.Age > 21 -> Ok person
    | _ -> Error "Failure"

me |> isOlderThan21

// Q8 Use an optional field
type Robot =
    { ChipCode: int option
      Name: string }

let robot1 = { ChipCode = None; Name = "Alican" }  
let robot2 = { ChipCode = Some 21; Name = "Konney"}


// Q9 Use a One of Which case with DU combinations
type PaymentOption =
| Card of Decimal
| Cash of Decimal
| HitAndRun

type Payment = { PaymentOption: PaymentOption;}

let paid = { PaymentOption = Card 25M }