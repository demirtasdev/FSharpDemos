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
