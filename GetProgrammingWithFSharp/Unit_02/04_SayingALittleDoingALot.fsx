// Listing 4.1: Sample 'let' bindings
let age = 35
let website = System.Uri "http://fsharp.org"
let add (first, second) = first + second

// NOW YOU TRY:
let myName = "Alican"
let random = System.Random
let next (ran:System.Random) = ran.Next()

// Listing 4.2: Reusing 'let' bindings (Shadowing)
let foo() =
    let x = 10                   
    printfn "%d" (x + 20)        
    let x = "test"               
    let x = 50.0
    x + 200.0

// QUICK CHECK 4.1:
// 
// Q1. Give at least two examples of values that can be bound to symbols with 'let'.
// A1. Primitive values, values of custom types, functions.
//
// Q2. What’s the difference between let and var?
// A2. Let is an immutable binding of a symbol. var represents a pointer to a specific mutable object. 
//
// Q3. Is F# a static or dynamic language?
// A3. F# is a statically typed language.

// Listing 4.4: Scopin in F#
let doStuffWithTwoNumbers(first, second) =
    let added = first + second
    Console.WriteLine("{0} + {1} = {2}", first, second, added)
    let doubled = added * 2
    doubled

// Listing 4.5. Unmanaged scope
let year1 = System.DateTime.UtcNow.Year
let age1 = year1 - 1979
let estimatedAge = sprintf "You are about %d years old!" age1

// Listing 4.6. Tightly bound scope
let estimatedAge2 =                             // Top level scope
    let age2 =                                  // Nested scope
        let year2 = System.DateTime.UtcNow.Year // Value of year visible only within scope of "age" value
        year2 - 1979
    sprintf "You are about %d years old" age2   // Can't access "year" value

printfn "%s" estimatedAge

// Listing 4.7. Nested (inner) functions
let estimateAges(familyName, year1, year2, year3) =
    let calculateAge yearOfBirth =              // A nested function that is used as a helper within the outer scope.
        let year = System.DateTime.Now.Year
        year - yearOfBirth

    let estimatedAge1 = calculateAge year1
    let estimatedAge2 = calculateAge year2
    let estimatedAge3 = calculateAge year3

    let averageAge = (estimatedAge1 + estimatedAge2 + estimatedAge3) / 3
    sprintf "Average age for family %s is %d" familyName averageAge


// TRY THIS
type User = {
    Name:string;
    Password:string;
}

let validate user =
    let validateUsername name =
        let nameResult =
            match name with
            | "Alican" -> true
            | _ -> false
        
        let randomNumResult =
            match System.Random().Next(100) with
            | x -> x < 100
            | x -> x > 100

        match (nameResult, randomNumResult) with
        | (true, true) -> true
        | _ -> false

    let validatePassword pw =
        match pw with
        | "VeniVidiVici" -> true
        | _ -> false

    let usernameIsValid = validateUsername user.Name
    let passwordIsValid = validatePassword user.Password

    (usernameIsValid && passwordIsValid)

