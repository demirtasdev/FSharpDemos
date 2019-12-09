open System

// takes two int and returns their sum
let sum (x:int, y:int) : int = x + y

// a recursive function keeps calling itself until it returns a value
let rec factorial x =
    // in this case it returns a value when x is smaller than 1
    if x < 1 then 1
    // ..and until then it keeps calling itself with x's previous value - 1
    else x * factorial (x - 1)

[<EntryPoint>]
let main argv =

    // `sum` function call
    printfn "5 + 7 = %i" (sum(5, 7))

    // `factorial` function call
    printfn "Factorial 4 : %i" (factorial 4)

    // keep the console open
    Console.ReadKey() |> ignore
    0 // return an integer exit code
