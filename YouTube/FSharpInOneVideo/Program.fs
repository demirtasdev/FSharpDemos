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

randList
    |> List.map (fun x -> x * 2)
    |> List.filter (fun x -> x % 2 = 0)
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
    // IF, ELIF and ELSE operators:
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
    // OR operator:
    printfn "College Grant: %b" ((gpa >= 3.8) || income <= 12000)
    // NOT operator:
    printfn "Not true: %b" (not true)
    
    let grade2 yearsOld =
        // MATCH operator:
        match age with
        | age when age < 5 -> "Preschool"
        | 5 -> "Kindergarten"
        | age when ((age > 5) && (age <= 18)) -> (age - 5).ToString()
        // user UNDERSCORE to define default condition
        | _ -> "College"    
    printfn "Grade2: %s" (grade2 8)
    // notice that when matching an exact value we don't need 'exp' when 'exp'
    // but we do need it while applying '<', '>', etc



// ___09_LISTS___
let lists() =

    // LIST_OPERATORS:
    // LIST.ITER to iterate over list and print out each value:
    let list1 = [1;2;3;4]
    list1 |> List.iter (printfn "Num: %i")

    // %A to print an INTERNAL REPRESENTATION of a list:
    printfn "list1: %A" list1

    // CONS operators to join several values:
    let list2 = 5::6::7::[]
    printfn "list2: %A" list2
    
    // RANGE to declare a list of numbers:
    let list3 = [1..5]
    printfn "list3: %A" list3

    // RANGE to declare a list of characters:
    let list4 = ['a'..'g']
    printfn "list4: %A" list4
    
    // LIST.INIT to generate a list with 5 indexes
    // (the boolean function multiplies each index by two):
    let list5 = List.init 5 (fun i -> i * 2)  
    printfn "list5: %A" list5

    // YIELD to generate a list by multiplying index values of a range of numbers by themselves:
    let list6 = [ for a in 1..5 do yield (a * a) ]
    printfn "list6: %A" list6
    // YIELD to create an even list out of a range:
    let list7 = [ for a in 1..20 do if a % 2 = 0 then yield a]
    printfn "list7: %A" list7
    // YIELD BANG to create a list for each item, eventually merging them into a final list:
    let list8 = [ for a in 1..3 do yield! [ a .. a + 2 ] ]
    printfn "list8: %A" list8

    // .LENGTH of a list:
    printfn "Length : %i" list8.Length
    // .ISEMPTY to check whether the list contains any members:
    printfn "Empty : %b" list8.IsEmpty
    // .INDEX to get a member at a certain index:
    printfn "Index 2 : %c" ( list4.[2] )
    // .HEAD to get the first element of a list:
    printfn "Head : %c" ( list4.Head )
    // .TAIL to get all the elements except the first one:
    printfn "Tail : %A" ( list5.Tail )
    // .FILTER to filter a list's members matching a criterion:
    let list9 = list3 |> List.filter ( fun x -> x % 2 = 0 )
    // .MAP to return a new list whose elements are the result 
    // of applying the predicate function to an existing list:
    let list10 = list9 |> List.map (fun x -> (x * x))
    // .SORT a list in ascending order:
    printfn "Sorted : %A" ( List.sort [5;4;3] )
    // .FOLD a list, iterating over each value, applying a function
    // to it and adding it up to the eventual final result:
    printfn "Sum : %i" (List.fold (fun sum elem -> sum + elem) 0 [1;2;3] )



// ___10_ENUMS___
// define an enum type
type Emotion =
| Joy = 0
| Fear = 1
| Anger = 2

let enums() =
    // declare an enum expression
    let myFeeling = Emotion.Anger

    // MATCH to find which Emotion is myFeeling
    match myFeeling with
    | Emotion.Joy -> printfn "I'm Joyful"
    | Emotion.Fear -> printfn "I'm fearful"
    | Emotion.Anger -> printfn "I'm angry"



// ___11_OPTIONS___
let options() =
    // MATCH to work out which option to return:
    let divide x y =
        match y with
        | 0 -> None //..NONE means there is no return value
        | _ -> Some(x / y) //..SOME means there is a return value (see inside parentheses)

    // .ISSOME to check whether the option returned is Some.
    // notice we call the function again to get the actual value:
    if (divide 5 0).IsSome then
        printfn "5 / 0 = %A" ( (divide 5 0).Value )
    // .ISNONE to check whether the option returned is None. 
    elif (divide 5 0).IsNone then
        printfn "Can't divide by zero"
    // if none of these is returned, something probable went wrong:    
    else
        printfn "Something happened"    



// ___12_TUPLES___
// i: A tuple is a grouping of unnamed but ordered values, possibly of different types.
// Tuples can either be reference types or structs.
let tuples() =
    let avg (w, x, y, z) : float =
        let sum = w + x + y + z
        sum / 4.0

    // TUPLE as an argument for the above method
    printfn "Avg : %f" (avg (1.0, 2.0, 3.0, 4.0))

    // TUPLE OF DIFFERENT TYPES
    let myData = ("Derek", 42, 6.25)

    // instantiating a tuple of variables by a tuple of
    // UNDERSCORE indicates we don't import the third value of the tuple passed in
    let (name, age, _) = myData

    printfn "Name : %s" name



// ___13_RECORDS___
// i: Records are a list of key-value pairs for creating custom types.
// define a record:
type Customer = { Name:string ; Balance:float }

let records() =
    // initialize a record:
    let bob = { Name = "Bob Smith"; Balance = 101.50 }
    // see result:
    printfn "%s owes us %.2f" bob.Name bob.Balance



// ___14_SEQUENCES___
// i: Sequences allow us to create INFINITE DATA STRUCTURES 
// that aren't defined nor populated until they are needed.
let sequences() =

    // basic sequence from a range:
    let seq1 = seq { 1 .. 100 }
    // generate even numbers from 0 to 50 (middle value defines how much to increment each time):
    let seq2 = seq { 0 .. 2 .. 50}
    // define a descending sequence sequence
    let seq3 = seq { 50 .. 1 }

    // experiment printing their values:
    printfn "%A" seq2
    // it prints only the first four values because sequences 
    // aren't generated until they're actually required

    // .TOLIST to convert a seq to a list so that we can print all the values
    Seq.toList seq2 |> List.iter (printfn "Num : %i")

    // a recursive function checking whether the value passed in is a prime number
    let isPrime n =
        let rec check i =
            // return bool depending on whether the number is prime:
            i > n/2 || (n % i <> 0 && check (i + 1))
        check 2

    // call isPrime for each number from 1 to 500 and add it to the list if it's prime:
    let primeSeq = seq { for n in 1..500 do if isPrime n then yield n }    

    // print every value as done before:
    Seq.toList primeSeq |> List.iter (printfn "Num : %i")



// ___15_MAPS___
let maps()=
// i: Maps also are collections of key-value pairs like records
    // MAP.EMPTY to define an empty map
    let customers =
        Map.empty.
            // .ADD to add members (notice the DOTs)
            Add("Bob Smith", 100.50).
            Add("Sally Marks", 50.25)

    // .COUNT to get the number of members
    printfn "# of Customers : %i" customers.Count

    // .TRYFIND to find a member by a key (returns Some(x) or None)
    let customer = customers.TryFind "Bob Smith"
    // MATCH/WITH to determine whether any values return
    match customer with
    | Some x -> printfn "Balance : %.2f" x
    | None -> printfn "Not found."

    // %A to print an internal representation of a map
    printfn "Customers : %A" customers

    // .CONTAINSKEY to determine if a key exists
    if customers.ContainsKey "Bob Smith" then
        printfn "Bob Smith was Found"

    // .[KEY] to access a member
    printfn "Bob's Balance : %.2f" customers.["Bob Smith"]        

    // .REMOVE to remove a member by a key
    let customers2 = Map.remove "Sally Marks" customers
    printfn "# of Customers : %i" customers2.Count



// ___16_GENERICS___
// i: Generics allows us to use any data type in a function
let addStuff<'T> x y =
    printfn "%A" (x + y)

let generics() =
    // declare the type you're using in angle brackets
    addStuff<int> 5 2
    // once we do this, the function only accepts the declared type



// ___17_EXCEPTION_HANDLING___
let exceptionHandling() =
    let divide x y =
        // TRY/WITH to check for and handle exceptions
        try
            // RAISE to raise an exception manually. we also pass a message to it
            if y = 0 then raise(DivideByZeroException "Can't Divide by 0")
            else printfn "%i / %i = %i" x y (x/y)
        with
            // | :? after WITH to check for a specific exception
            | :? System.DivideByZeroException -> printfn "Can't Divide by Zero"  

    divide 5 0



// ___18_STRUCTS___
// i: Structs allow us to create our own data type.

// TYPE/=STRUCT to start a struct declaration
type Rectangle = struct
    // VAL to define it's fields
    val Length : float
    val Width : float

    // NEW (PARAM1, PARAM2) to declare a constructor
    new (length, width) =
        {Length = length ; Width = width}
// END to finish struct declaration
end    

let structs() =
    // define a function that takes in a Rectangle type
    let area(shape:Rectangle) =
        shape.Length * shape.Width
    // instantiate a new Rectangle type
    let rect = Rectangle(5.0, 6.0)
    // define a function that is the new rectangle type passed into the area function
    let rectArea = area rect
    printfn "Area : %.2f" rectArea



// ___19_CLASSES___
// i: Classes model a real world object using fields and functions

// TYPE/=CLASS to start a class declaration
type Animal = class
    // VAL to define properties
    val Name : string
    val Height : float
    val Weight : float

    // NEW to define a constructor
    new (name, height, weight) = { Name = name ; Height = height; Weight = weight }

    // MEMBER to define a method
    member x.Run =
        printfn "%s Runs" x.Name
// END to finish declaration    
end

// define a class that inherits Animal
type Dog (name, height, weight) =
    // INHERIT to define what class to inherit
    inherit Animal(name, height, weight)

    // after inheriting all the methods and properties of a class
    // we can then go ahead and define our own methods and properties
    member x.Bark = 
        printfn "%s Barks" x.Name

let classes() =
    let spot = Animal("Spot", 20.5, 40.5)
    spot.Run

    let bowser = Dog("Bowser", 20.5, 40.5)
    bowser.Run
    bowser.Bark



[<EntryPoint>]
let main argv =

    classes()

    // keep the console open
    Console.ReadKey() |> ignore
    0 // return an integer exit code