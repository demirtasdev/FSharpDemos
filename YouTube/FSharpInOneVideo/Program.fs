open System

// ___01_FIRSTFUNCTION___
// takes two int and returns their sum
let sum (x:int, y:int) : int = x + y


// ___02_RECURSIVEFUNCTIONS___
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


// ___03_LAMBDAEXPRESSIONS___
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


// ___04_MULTIPLEFUNCTIONSATONCE___
let multiplyNumber x = x * 3
let addNumber x = x + 3
// compose two functions together using ">>" or "<<"
let multiplyAndAddNumber = multiplyNumber >> addNumber
// direction of the arrows determines the flow of functions
let addAndMultiplyNumber = multiplyNumber << addNumber


// ___05_MATHFUNCTIONS___
let doMath() =
    printfn "5 + 4 = %i" (5 + 4)
    printfn "5 - 4 = %i" (5 - 4)
    printfn "5 * 4 = %i" (5 * 4)
    printfn "5 / 4 = %i" (5 / 4)
    // to print '%' you need to type two of them back to back
    printfn "5 %% 4 = %i" (5 % 4)
    // "**" returns the number on the left squared by the number on the right as float
    printfn "5 ** 4 = %.1f" (5.0 ** 4.0)

    // you can get the type of a number as such:
    let number = 2;
    printfn "Type : %A" (number.GetType())

    // and you can cast one type into another:
    printfn "A Float : %.2f" (float number)
    printfn "An Int : %i" (int 3.14)

    // some more basic math functions:
    printfn "abs -1 : %i" (abs -1)
    printfn "ceil 4.5 : %f" (ceil 4.5)
    printfn "floor 4.5 : %f" (floor 4.5)
    printfn "log 2.71828 : %f" (log 2.71828)
    printfn "log10 1000 : %f" (log10 1000.0)
    printfn "sqrt 25 : %f" (sqrt 25.0)




[<EntryPoint>]
let main argv =

    doMath()

    // keep the console open
    Console.ReadKey() |> ignore
    0 // return an integer exit code
