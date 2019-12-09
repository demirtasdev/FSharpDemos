open System


let sum (x:int, y:int) : int = x + y

let rec factorial x =
    if x < 1 then 1
    else x * factorial (x - 1)



[<EntryPoint>]
let main argv =

    // `sum` function call
    printfn "5 + 7 = %i" (sum(5, 7))

    // `factorial` function call
    printfn "Factorial 4 : %i" (factorial 4)


    Console.ReadKey() |> ignore
    0 // return an integer exit code
