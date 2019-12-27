open System
open System.IO

// Listing 20.1
// -> for ... loops in F#
// upward counting for loop:
for number in 1 .. 10 do
    printfn "%d Hello!" number
// downward counting for loop:
for number in 10 .. -1 .. 1 do
    printfn "%d Heloo!" number

let customerIds = [ 45 .. 99 ]
// typical foreach-style loop
for customerId in customerIds do    
    printfn "%d bought something!" customerId
// Range with custom stepping
for even in 2 .. 2 .. 10 do
    printfn "%d is an even number!" even



// Listing 20.1.2:
// -> while loops in F#
// open a handle to the text file:
let reader = new StreamReader(File.OpenRead @"File.txt")
// while roop runs while the reader isn't at the end of the stream:
while (not reader.EndOfStream) do
    printfn "%s" (reader.ReadLine())

// i: In F# there is no "break" command. You can't exit out of a loop prematurely.
// Every branch must have an equivalent result.



// Listing 20.3
// -> Comprehensions in F#
// i: A powerful way of generating lists, arrays, 
// and sequences of daya based on for loop-style syntax.

// generating an array of the letters of the alphabet in uppercase
let arrayOfChars = [| for c in 'a' .. 'z' -> Char.ToUpper c |]
// generating the squares of the numbers 1 to 10
let listOfSquares = [ for i in 1 .. 10 -> i * i ]
// generating arbitrary strings based on every fourth member between 2 and 20
let seqOfStrings = seq { for i in 2 .. 4 .. 20 -> sprintf "Number %d" i }



// Listing 20.4
// -> if/then expression for complex logic
let limit score years =
    // A simple clause:
    if score = "medium" && years = 1 then 500
    // Complex clause (AND and OR combined)
    elif score = "good" && (years = 0 || years = 1) then 750
    elif score = "good" && years = 2 then 1000
    // Catchall for "good" customers
    elif score = "good" then 2000
    // Catchall for other customers
    else 250

let score = ""
let years = 0

let limitWithPM score years =
    // Implicitly matching on a tuple of rating and years
    match (score, years) with
    // Logic is exactly the same as Listing 20.4
    | "medium", 1 -> 500
    | "good", 0 | "good", 1 -> 750
    | "good", 2 -> 1000
    | "good", _ -> 2000
    | _ -> 250



// NOW YOU TRY:
let getCreditLimit customer =
    match customer with
    | _, 1 -> 520
    | "medium", 1 -> 500
    | "good", 0 | "good", 1 -> 750
    | "good", 2 -> 1000
    | "good", _ -> 2000
    | _, 0 -> 250

getCreditLimit ("bad", 1)

// i: Pattern matchin works top down.
// !: Always put the most specific patterns first and generals last.



// Listing 20.6
// -> Using the "when" guard clause
let getCreditLimitWhen customer =
    match customer with
    | "medium", 1 -> 500
    // Using the when guard to specify a custom pattern
    | "good", years when years < 2 -> 750
    | _ -> 0
// i: When we use the 'when' guard clause, the compiler won't
// be able to perform exhaustive pattern matching for us.



// Listing 20.7:
// -> Nesting matches inside one another
let getCreditLimitNested customer =
    match customer with
    | "medium", 1 -> 500
    // matching on "good" and binding years symbol
    | "good", years ->
        // a nested match on the value of years
        match years with
        // single value match
        | 0 | 1 -> 750
        | 2 -> 1000
        | _ -> 2000
    // global catchall    
    | _ -> 250    



// NOW YOU TRY #2:
type Customer =
    { Balance: int
      Name: string }

let handleCustomers (customers: Customer list) =  
    if customers.IsEmpty then failwith "No customers supplied!"
    elif customers.Length = 1 then printfn "%s" customers.[0].Name
    elif customers.Length = 2 then 
        printfn "%d" (customers.[0].Balance + customers.[1].Balance)
    else printfn "%d" customers.Length

// Listing 20.8
// Matchin against lists
let handleCustomersMatch customers =
    match customers with
    // Matching against an empty list:
    | [] -> failwith "No customers supplied."
    // Matching against a list of one customer:
    | [ customer ] -> printfn "%s" customer.Name
    // Matching against a list of two customers:
    | [ first; second ] -> 
        printfn "%d" (first.Balance + second.Balance)
    // Catchall for all others    
    | customers -> 
        printfn "Customers supplied: %d" customers.Length    

handleCustomersMatch []
handleCustomersMatch [ { Balance = 100; Name = "Jimmy" } ]



let getStatus customer =
    match customer with
    | { Balance = 0 } -> "Customer has empty balance."
    | { Name = "Isaac" } -> "This is a great customer!"
    | { Name = name; Balance = 50} -> 
        sprintf "%s has a large balance." name
    | { Name = name } -> sprintf "%s is a normal customer" name    

{ Balance = 50; Name = "Joe" } |> getStatus


// Listing 20.10
// -> Combining multiple patterns
match customer with
// Pattern matching agains a list of three items with specific fields
| [ { Name = "Tanya" }; { Balance = 21}; _ ] -> "It's a match..."
| _ -> "No match!"




// Listing 20.11
// -> When to use if/then over match
// if/then implicit default else branch
if customer.Name = "Isaac" then printfn "Hello!"
// Match with explicit default case:
match customer.NAme with
| "Isaac" -> printfn "Hello!"
| _ -> ()




// TRY THIS
let randomNumbers = [ 0 .. 5 .. 20 ]

match randomNumbers with
| [] -> printfn "No members found"
| numList when numList.Length = 500 -> printfn "500 Members found!"
| numList when numList.Head = 5 -> printfn "First element is 5."
| _ -> printfn "No criteria met."
