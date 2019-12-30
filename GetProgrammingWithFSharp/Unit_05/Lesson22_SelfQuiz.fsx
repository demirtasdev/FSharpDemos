open System

// Q1: Declare an option type
let myAge = Some 21

// Q2: Explain the risk of using .Value on an option type
// A2: When we use .Value, we are trying to get the value
// without the foreknowledge that the value exists. This 
// may throw an exception when done on an option of None.

// Q3: Pattern Match against an optionk
match myAge with
| Some age -> printfn "Age stated: %d" age
| None -> printfn "Age not stated."

// Q4: Do an option mapping
let something = myAge |> Option.map (fun x -> sprintf "%d" x)

// Q5: Do an option binding
let objectToOption option =
    Some (sprintf "%A" option)

let someOtherThing = myAge |> Option.bind objectToOption

// Q6: Filter an option
let myAgeFiltered = myAge |> Option.filter (fun x -> x > 24)