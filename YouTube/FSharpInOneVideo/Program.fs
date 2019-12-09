open System


// takes two int and returns their sum
let sum (x:int, y:int) : int = x + y

// a recursive function keeps calling itself until it returns a value
let rec factorial x =
    // in this case it returns a value when x is smaller than 1
    if x < 1 then 1
    // ..and until then it keeps calling itself with x's previous value - 1
    else x * factorial (x - 1)

        // RESULT:
        // 1st : result = 4 * factorial(3) = 4 * 6 = 24
        // 2nd : result = 3 * factorial(2) = 3 * 2 = 6
        // 3rd : result = 2 * factorial(1) = 2 * 1 = 2
        // 4th : result = 1 * factorial(0) = 1 * 1 = 1


[<EntryPoint>]
let main argv =

    // `factorial` function call
    printfn "Factorial 4 : %i" (factorial 4)

    // keep the console open
    Console.ReadKey() |> ignore
    0 // return an integer exit code
