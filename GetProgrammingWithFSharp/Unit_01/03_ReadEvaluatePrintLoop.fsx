open System

let text = "Hello, world!"

text.Length

let greetPerson name age =
    sprintf "Hello, %s. You are %d years old." name age

let greeting = greetPerson "Fred" 25

// TRY THIS:
let countWords (text:string) =
        text.Split(" ")
        |> Array.length
    


// QUICK CHECK 3.2
//
// Q1: Do scripts need a project in order to run?
// A1: No - you can run scripts as standalone files.
//
// Q2: Give two reasons that you might use a script rather than coding
//     directly into FSI.
// A3: F# scripts have an improved development experience and are repeatable.