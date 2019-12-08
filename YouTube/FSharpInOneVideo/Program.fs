open System

let hello() =
    printf "Enter your name: "
    let name = Console.ReadLine()
    printfn "Hello %s" name

[<EntryPoint>]
let main argv =
    hello()
    Console.ReadKey() |> ignore
    0 // return an integer exit code
