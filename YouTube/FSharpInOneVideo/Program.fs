open System

// 01_FIRSTFUNCTION:
// takes two int and returns their sum
let sum (x:int, y:int) : int = x + y


// 02_RECURSIVEFUNCTIONS:
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


// 03_LAMBDAEXPRESSIONS
// Create a list of two integers
let randList = [1;2;3]
// Use a lambda expression to multiply each value by 2 (notice "fun x ->..."):
let randListTwo = List.map (fun x -> x * 2) randList
// map function that exists in the List module maps through each item of the list


// Lambda function using the pipeline operator
let filterAndMultiply() =
    [5;6;7;8]
    // List.filter returns a new list containing only the numbers that meets the expression's criteria
    |> List.filter (fun v -> (v % 2) = 0)
    // Then we can map through the list as before and multiply each value by 2
    |> List.map (fun x -> x * 2)
    // And then print the values:
    |> printfn "Even Doubles: %A"


[<EntryPoint>]
let main argv =

    filterAndMultiply()

    // keep the console open
    Console.ReadKey() |> ignore
    0 // return an integer exit code
