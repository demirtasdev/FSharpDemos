let text = "Hello, world!"

text.Length

let greetPerson name age =
    sprintf "Hello, %s. You are %d years old." name age

let greeting = greetPerson "Fred" 25


// QUICK CHECK 3.2
//
// Q: Do scripts need a project in order to run?
// A: No - you can run scripts as standalone files.
//
// Q: Give two reasons that you might use a script rather than coding
//    directly into FSI.
// A: F# scripts have an improved development experience and are repeatable.