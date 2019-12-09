open System

let bindStuff() =
    let mutable weight = 175
    weight <- 170
    printfn "weight: %i" weight

    let changeMe = ref 10
    changeMe := 50
    printfn "Change: %i" ! changeMe


[<EntryPoint>]
let main argv =
    bindStuff()
    Console.ReadKey()
    0 // return an integer exit code
