open System
open System.IO

// 11.1: Partial Function Application
// i: partially applied functions are one of the most 
// powerful parts of the functional system in F#

// Listing 11.1
// -> Passing arguments with and without brackets...
// Tupled function:
let tupledAdd (a, b) = a + b
let answer = tupledAdd (12, 4)
// Curried function:
let curriedAdd a b = a + b
let answer1 = curriedAdd 12 4

// TUPLED FUNCTIONS force you to supply all the arguments at once.
// F# considers all the arguments as a single object.
//
// CURRIED FUNCTIONS allow you to supply only some of the arguments to
// a function and get back a new function that expects the remaining arguments.

// Creating a function in curried form:
let add first second = first + second
// Partially applying "add" to get back a new function. Notice its signature:
let addFive = add 5
// Calling addFive:
let fifteen = addFive 10

let buildDt year month day = DateTime(year, month, day)

// Listing 11.3
// -> Explicitly creating wrapper functions in F#:
let buildDtThisYear month day = buildDt DateTime.UtcNow.Year month day
let buildDtThisMoth day = buildDtThisYear DateTime.UtcNow.Month day
//
// VS.
//
// Listing 11.4
// -> Creating wrapper functions by currying:
let buildDtThisYear1 = buildDt DateTime.UtcNow.Year
let buildDtThisMonth1 = buildDtThisYear1 DateTime.UtcNow.Month
// i: This is identical to Listing 11.3, except that you don't have to pass in
//    the extraarguments to the right hand side. F# does that for you.

// i: Partially applied functions work from left to right!!! You partially apply
//    arguments starting from the left side and then work your way in.

// NOW YOU TRY:
let writeToFile (date:DateTime) fileName text =
    let dateAsText = date.ToString "yyMMdd"
    let fileName = sprintf "%s-%s.txt" dateAsText fileName
    let path = sprintf "C:\Users\d3m1r\OneDrive\GitHub\FSharpDemos\GetProgrammingWithFSharp\Unit_03\\%s" fileName

    System.IO.File.WriteAllText (path, text)

writeToFile DateTime.UtcNow "Creative File Name" "Woooooooooooo, weee!"

// Listing 11.6: 
// -> Creating constrained functions:
// Creating constrained version of the fuinction to print with a specific date:
let writeToToday = writeToFile DateTime.UtcNow.Date
let writeToTomorrow = writeToFile (DateTime.UtcNow.Date.AddDays 1.)
// Creating a more constrained version to print with a specific filename:
let writeToTodayHelloWorld = writeToFile DateTime.UtcNow.Date "merhaba-world"

// Calling a constrained version to create a file with a specific date and "first-file":
writeToToday "first-file" "Lorem ipsum dolor sit amet, consectetur adipiscing elit,"
writeToTomorrow "second-file" "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
// Calling the more-constrained version. Only the final argument is required!
writeToTodayHelloWorld "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."


// 11.2.1 - Pipelines
let checkCreation (dateTime:DateTime) =
    match dateTime with
    | dt when (dateTime.AddDays 7.) > DateTime.UtcNow -> "New"
    | _ -> "Old"

// let currentDirectory = Directory.GetCurrentDirectory()
// let creationTime = Directory.GetCreationTimeUtc currentDirectory

// let result = checkCreation creationTime

// Listing 11.7
// -> Calling functions arbitrarily:
let time =
    // Temporary value to store the directiory:
    let directory = Directory.GetCurrentDirectory()
    // Using the temporary value in a subsequent method call:
    Directory.GetCreationTime directory
checkCreation time
// i: Using temporary variables like this become a liability when
//    our functions get bigger and bigger

// Listing 11.8
// -> Simplistic chaining of functions:
// Explicitly nesting method callls:
checkCreation(
    Directory.GetCreationTime(
        Directory.GetCurrentDirectory()))
// i: This is less code and a cleaner approach, but now you read the
//    code opposite of the order of operation. You don't want this.

// Listing 11.9
// -> Chaining three functions together using the pipeline operator:
// Directory.GetCurrentDirectory()  //-> Returns a string.
|> Directory.GetCreationTime     //-> Takes in a string, returns DateTime.
|> checkCreation                 //-> Takes in a DateTime, prints to console.

// Listing 11.11
// -> Review of existing petrol sample
let drive distance petrol =
    match distance with
    | "far" -> petrol - 30.0
    | "medium" -> petrol - 15.0
    | "short" -> petrol - 5.0
    | _ -> failwith "Unknown command"

let startingPetrol = 100.0

let petrol1 = drive "far", startingPetrol
let petrol2 = drive "medium", petrol1
let petrol3 = drive "short", petrol2

// Listing 11.12
// -> Using pipelines to implicitly pass chained state:
startingPetrol
|> drive "far"
|> drive "medium"
|> drive "short"
|> printfn "%.2f"

// Listing 11.3
// -> Automatically composing functions:
// Create a function by composing a set of functions together:
let checkCurrentDirectoryAge =
    Directory.GetCurrentDirectory
    >> Directory.GetCreationTime
    >> checkCreation
// Call the newly created composed function:
let description = checkCurrentDirectoryAge