﻿open System

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


// ___06_STRINGFUNCTIONS___
// EscapeCharacters:    newLine: \n    backSlash: \\    doubleQuote: \"    singleQuote: \'    percentSign: %%
let stringFuncs() =
    // declare a string function:
    let str1 = "This is a random string"
    // verbatum string:
    let str2 = @"I ignore backslashes. (\)"
    // three quote string:
    let str3 = """ "I ignore double quotes and backslashes" """
    // combine strings:
    let str4 = str1 + " " + str2
    // get the length of a string:
    printfn "Length : %i" (String.length str4)
    // access characters by index:
    printfn "%c" str1.[1]
    // get substring:
    printfn "1st word: %s" (str1.[0..3])
    // use String.collect to execute a function on each character of a string:
    let upperStr = String.collect (fun c -> sprintf"%c, " c) "commas"
    printfn "Commas : %s" upperStr
    // use String.exists and Char.IsUpper to check for uppercase characters:
    printfn "Any upper : %b" (String.exists (fun c -> Char.IsUpper(c)) str1)
    // use String.forall and Char.IsDigit to check whether ALL characters are digits:
    printfn "Number : %b" (String.forall (fun c -> Char.IsDigit(c)) "1234")
    // use String.init to iterate up to 10 and concatenate all the values:
    let string1 = String.init 10 (fun i -> i.ToString())
    printfn "Numbers : %s" string1
    // use String.iter to print each character of a string on a new line:
    String.iter(fun c -> printfn "%c" c) "Print Me"


// ___07_LOOPS___
// While Loop
let whileLoop() =
    let magicNum = "7"
    let mutable guess = ""

    // keep the user guessing while..
    while not (magicNum.Equals(guess)) do
        printf "Guess the number : "
        guess <- Console.ReadLine()

    printfn "You guessed the number!"

// For Loops
let forLoops() =
    // going up from 1 to 10:
    for i = 1 to 10 do
        printfn "%i" i

    // going down from 10 to 1:
    for i = 10 downto 1 do
        printfn "%i" i

    // loop over a 'range' (similar to foreach in C#):     
    for i in [1..10] do
        printfn "%i" i

// Some useful iterations:
let iterations() =
    // iteration by piping a range into a function:
    [1..10] |> List.iter (printfn "Num %i")
    // iterate over a whole range using List.reduce, get the sum of all its members:
    let sum = List.reduce (+) [1..10]
    printfn "Sum: %i" sum


// ___08_CONDITIONALS___
let conditionalOperators() =
    let age = 8
    // "if", "else if" and "else" operators
    if age < 5 then
        printfn "Preschool"
    elif age = 5 then
        printfn "Kindergarten"
    elif (age > 5) && (age <= 18) then
        let grade = age - 5
        printfn "Go to grade %i" grade
    else
        printfn "Go to College"

    let gpa = 3.9
    let income = 15000
    // "or" operator
    printfn "College Grant: %b" ((gpa >= 3.8) || income <= 12000)
    // "not" operator
    printfn "Not true: %b" (not true)
    // "match" operator
    let grade2 yearsOld =
        // start the operator as such (match 'exp' with)
        match age with
        | age when age < 5 -> "Preschool"
        | 5 -> "Kindergarten"
        | age when ((age > 5) && (age <= 18)) -> (age - 5).ToString()
        // "default" condition operator (_)
        | _ -> "College"    
    printfn "Grade2: %s" (grade2 8)
    // notice that when matching an exact value we don't need 'exp' when 'exp'
    // but we do need it while applying '<', '>', etc

    

 
[<EntryPoint>]
let main argv =

    conditionalOperators();

    // keep the console open
    Console.ReadKey() |> ignore
    0 // return an integer exit code
