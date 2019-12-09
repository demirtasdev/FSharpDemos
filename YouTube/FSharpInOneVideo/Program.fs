open System

module Funcs =
    let doFuncs() =
        let getSum (x:int, y:int) : int = x + y
        printfn "5 + 7 = %i" (getSum(5, 7))

[<EntryPoint>]
let main argv =

    Funcs.doFuncs()
    Console.ReadKey |> ignore
    
    0 // return an integer exit code
