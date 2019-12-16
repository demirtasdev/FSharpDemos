// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    
    printfn "Passed in %d items: %A" argv.Length argv

    Console.Read() |> ignore  // keep the console open
    0  // return an integer exit code


// F# PLACEHOLDERS:
//
// %d - int
// %f - float
// %b - Boolean
// %s - string
// %O - The .ToString() representation of the argument
// %A - An F# pretty-print representation of the argument that falls back to %O if none exists


// QUICK CHECK 2.1:
//
// Q1: What are the two basic project types for F# shipped with Visual Studio?
// A1: Console and Class Library
//
// Q2: What is the [<EntryPoint>] attribute for?
// A2: Marking the entry function to a console application
//
// Q3: What are the two types of F# files in a project?
// A3: .fs (compiled file) and .fsx (script file) 
