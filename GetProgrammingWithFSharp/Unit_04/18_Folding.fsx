open System

// Listing 18.1
// -> Example aggreagation signatures:
type Sum        = int   seq -> int 
type Average    = float seq -> float
type Count<'T>  = 'T    seq -> int
// -> Notice that they all take a collection of
// things and return a single thing.

// Listing 18.2
// -> Imperative implementation of sum
let sum inputs =
    // Empty accumulator:
    let mutable accumulator = 0
    // Go through every item:
    for input in inputs do
        // Apply aggregation onto accumulator
        accumulator <- accumulator + input
    // Return accumulator    
    accumulator
//->Notice function signature. "inputs" is determined to be a collection 
//  based on the for loop, and an int based on the accumulator.


// NOW YOU TRY:
// -> Function that gets the length of a list:
let length li =
    let mutable accumulator = 0
    for _ in li do accumulator <- accumulator + 1
    accumulator
// -> Check if works:
let lengthOfList =
    [ 0 .. 99 ]
    |> length

// -> Function that returns the highest value in a list:
let max li =
    let mutable highest = 0
    for x in li do
        if x > highest then highest <- x
    highest    
// -> Check if works:
let maximum = 
    [ 0 .. 2 .. 50 ]
    |> max


// 18.2: Saying hello to fold
// -> Fold is a higher order function that allows you to supply an input 
// collection you want to aggregate, a start state for the accumulator and 
// a function that says how to accumulate data.

// -> Signature for Seq.Fold:
// folder: ('State -> 'T -> 'State) -> state:'State -> source:seq<'T> -> 'State
// 1. Folder -> A function that's passed into fold that handles the accumulation
// 2. State  -> Initial start state
// 3. Source -> The input collection

// Listing 18.3
// -> Implementing sum through fold
let sumWithFold inputs =
    Seq.fold
        // Folder function to sum the accumulator and input:
        (fun state input -> state + input)
        // Initial state:
        0
        // Input collection:
        inputs

// Listing 18.4
// -> Fold without logging:
let sumWithFoldAndLogging inputs =
    Seq.fold
        (fun state input ->
            // Creating the new state:
            let newState = state + input
            printfn
                "Current state is %d, input is %d, new state value is %d"
                state
                input
                // Debug message:
                newState
            // Returning the new state:            
            newState)
        0
        inputs        
// -> Check:
[ 1 .. 5 ] |> sumWithFoldAndLogging


// NOW YOU TRY
// -> Implement a length function using fold
let lengthWithFold li =
    Seq.fold
        (fun state li->
            let newState = state + 1
            newState)
        0
        li        
// -> Check:
[ 0 .. 99 ] |> lengthWithFold

// -> Implement a max function using fold
let maxWithFold li =
    Seq.fold
        (fun state li ->
            let newState =
                if li > state then li
                else state
            newState )
        0
        li
// -> Check
[ 12; 18; 3; 45; 0; 2 ] |> maxWithFold 


// Listing 18.5
// -> Making fold read in a more logical way
// Seq.fold (fun state input -> state + input) 0 inputs
let inputs = [ 12; 18; 3; 45; 0; 2 ]
// Using pipeline to move "inputs" to the left side:
inputs |> Seq.fold (fun state input -> state + input) 0
// Using the double pipeline to move both the initial 
// state and "inputs" to the left side:
(0, inputs) ||> Seq.fold (fun state input -> state + input)


// 18.2.2: Related FOLD functions
// FOLDBACK -> Same as FOLD but goes backwards from the last element of collection
// MAPFOLD  -> MAP and FOLD combined. Emits a sequence of mapped results and a final state.
// REDUCE   -> FOLD simplified. First element of the collection is the initial state.
// SCAN     -> Similar to FOLD but also generates an intermediate result.
// UNFOLD   -> Generates a sequence from a single starting state. Similar to YIELD.


// Listing 18.6:
// -> Accumulating through a while loop
open System.IO
// Initial State:
let mutable totalChars = 0
// Opening a stream to a file
let sr = new StreamReader(File.OpenRead "book.txt")
// Stopping condition:
while (not sr.EndOfStream) do 
    let line = sr.ReadLine()
    // Accumulation function:
    totalChars <- totalChars + line.ToCharArray().Length


// Listing 18.7:
// Simulating a collection through sequence expressions
let lines : string seq =
    // Sequence expression:
    seq {
        use sr = new StreamReader(File.OpenRead @"book.txt")
        while not sr.EndOfStream do
            // Yielding a row from the StreamReader:
            yield sr.ReadLine() }
// A standard fold:        
(0, lines) ||> Seq.fold (fun total line -> total + line.Length)


// A TYPE ALIAS:

// TYPE ALIASES:
// In the above block the line that starts with "type Rule.." is a TYPE ALIAS.
// 1->It lets you define a signature that you can use in place of another one.
// 2->It is not a new type.   
// 3->The definition it aliases is interchangeable with it and it's erased in runtime.


// Listing 18.9:
// -> Manually building a super rule
let validateManual (rules: Rule list) word =
    // Testing the first rule:
    let passed, error = rules.[0] word
    // Checking whether the rule failed:
    if not passed then false, error
    else
        // Rinse and repeat for all remaining rules:
        let passed, error = rules.[1] word
        if not passed then false, error
        else
            let passed, error = rules.[2] word
            if not passed then false, error
            else true, ""
// i: This solution doesn't scale particularyly well.

open System
type Rule = string -> bool * string

// Listing 18.10
// -> Composing a list of rules by using reduce:
let buildValidator (rules: Rule list) =
    rules
    |> List.reduce(fun firstRule secondRule ->
        // Higher order function:
        fun word ->
            // Run first rule:
            let passed, error = firstRule word
            // Passed, move on to next rule:
            if passed then
                let passed, error = secondRule word
                if passed then true, "Success!" else false, error
            // Failed, return error:      
            else false, error)

// NOW YOU TRY:
module Rules =
    let threeWordsMin (text: string) =
        printfn "Running the minimum three words rule..."
        (text.Split ' ').Length = 3, "Must be three words!"

    let thirtyCharactersMax (text: string) =
        printfn "Running the thirty characters max rule..."
        text.Length <= 30, "Max length is 30 characters!"

    let allCapitals (text: string) =
        printfn "Running the all capitals rule..."
        text
        |> Seq.filter Char.IsLetter
        |> Seq.forall Char.IsUpper, "All letters must be capitals!"

    let noDigits (text: string) =
        let digitFound = text |> Seq.exists Char.IsDigit
        if digitFound then false, "No digits allowed!"
        else true, ""

let rules: Rule list = 
    [ Rules.threeWordsMin;
      Rules.thirtyCharactersMax;
      Rules.allCapitals;
      Rules.noDigits ]

let validate = rules |> buildValidator

let word = "HELLO FROM F#"

validate word


    
let mylist = [ 1 .. 5 .. 1000 ]

mylist
|> List.filter (fun x -> x % 2 = 0)
|> List.iter (fun x -> printfn "%i" x)
