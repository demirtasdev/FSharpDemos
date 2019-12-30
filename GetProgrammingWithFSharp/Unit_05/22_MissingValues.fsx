open System

// I HAVE SKIPPED THE LISTINGS FOR JSON and C# CODE AND
// WENT RIGHT INTO OPTION TYPE SECTION..

// Listing 22.5
// -> Sample code to calculate a premium

let aNumber : int = 10
// Creating an optional number:
let maybeANumber : int option = Some 10

let calculateAnnualPremiumUsd score =
    match score with
    // Handling a safety score of (Some 0):
    | Some 0 -> 250
    | Some score when score < 0 -> 400
    | Some score when score > 0 -> 150
    // Handling the case when no safety score is found:
    | None ->
        printfn "No score supplied! Using temporary premium."
        300
    | _ -> failwith "ERROR!"    
// Calculating the premium with a wrapped score of (Some 250)
let result = calculateAnnualPremiumUsd (Some 250)
// Calculating the premium with None
let result1 = calculateAnnualPremiumUsd None



// NOW YOU TRY
type Customer =
    { Name : string
      SafetyScore : Option<int>
      YearPassed : int }

let listOfCustomers =
    [ { Name = "Fred Smith"
        SafetyScore = Some 550
        YearPassed = 1980  }
      { Name = "Jane Dunn"
        SafetyScore = None
        YearPassed = 1980 } ]

let calculatePremium (customer:Customer) =
    match customer.SafetyScore with
    | Some 0 -> 250
    | Some score when score < 0 -> 400
    | Some score when score > 0 -> 150
    // Handling the case when no safety score is found:
    | None ->
        printfn "No score supplied! Using temporary premium."
        300
    | _ -> failwith "ERROR!"

// !!-> If you use .Value on an option there's the risk of the compiler
// throwing a null reference exception if the value is None.

let customer = 
    { Name = "Alican";
      SafetyScore = Some 999;
      YearPassed = 1998 } 

let describe score =
    match score with
    | score when score > 500 -> "Low Risk"
    | score when score <= 500 -> "High Risk"
    | _ -> failwith "DESCRIBE FUNCTION ERROR"


// Listing 22.6
// -> Matching and mapping
let description =
    // A standard match over an input
    match customer.SafetyScore with
    | Some score -> Some (describe score)
    | None -> None
// Using Option.map to act on the Some case
let descriptionTwo =
    customer.SafetyScore
    |> Option.map (fun score -> describe score)
// Shorthand to avoid having to explicitly 
// supply arguments to describe in Option.map:
let shorthand = customer.SafetyScore |> Option.map describe
// Creating a new function that safely 
// executes describe over optional values:
let optionalDescribe = Option.map describe

// i: Option.map applies the function if the input is Some
// and does nothing if it is None


// Listing 22.7
// -> Chaining functions that return an option with Option.bind
// Two functions that each return an optional value:
let tryFindCustomer cId = 
    if cId = 10 then Some listOfCustomers.[0] else None
let getSafetyScore customer = customer.SafetyScore
// Binding both functions together:
let score = tryFindCustomer 10 |> Option.bind getSafetyScore
    // attempt withdrawal function
    let withdraw ( account:Account ) =
        printfn "How much would you like to withdraw?"
        let amount = decimal <| Console.ReadLine()
        
        // check whether the customer has sufficient funds
        match amount with
        | a when ( a <= account.Balance ) ->

            // bind the new balance to a keyword
            let newBalance = account.Balance - amount

            printfn "Deposit successful. Current Balance: %.2f" newBalance
            printfn "Could we help with anything else?" 
            transactionLoop { ID = account.ID ; 
                              Balance = newBalance ; 
                              Owner = account.Owner }
        | _ ->  
            printfn "Withdrawal failed. Insufficient funds."
            transactionLoop { ID = account.ID ; 
                              Balance = account.Balance ; 
                              Owner = account.Owner }
let customerIds = [ 0 .. 10 ]   

customerIds |> List.choose tryLoadCustomer