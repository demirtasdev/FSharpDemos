// Listing 9.3:
// -> Returning arbitrary data pairs in F#
let parseName (name:string) =
    let parts = name.Split(' ')
    let forename = parts.[0]
    let surname = parts.[1]
    //return a tuple of forename and surname:
    forename, surname
// i: note the dot that we have used before the intexer of the array

//calling a function that returns a tuple:
let name = parseName "Isaac Abraham"
//deconstruction a tuple into meaningful values
let forename, surname = name
//deconstructing a tuple directly from a function call
let fname, sname = parseName "Isaac Abraham"

// Notes:
// 1. seperate values with a comma to create a tuple
// 2. deconstruct the tuple by assigning them to seperate values using a comma
// 3. tuples can be of arbitrary length and a mixture of types



// NOW YOU TRY
let parse (person:string) =
    let parts = person.Split(' ')
    let player = parts.[0]
    let game = parts.[1]
    let score = int parts.[2]
    player, game, score

let (p, g, s) = parse "Alican ProblemSolving 9"

let name21, age21 = "Alican", 21
printfn "%s" name21

// Listing 9.4:
// -> Returning more-complex arbitrary data pairs in F#
//creating a nested tuple:
let nameAndAge = ("Joe", "Bloggs"), 20
//deconstructing a tuple
let name1, age1 = nameAndAge
//deconstructing a tuple including its nested components
let (forename1, surname1), theAge1 = nameAndAge

// Listing 9.5:
// -> Using wildcards with tuples
let nameAndAge1 = "Jane", "Smith", 25
let forename2, surname2, _ = nameAndAge1



// Listing 9.6:
// -> Type inference with tuples:
let explicit : int * int = 10, 5
let implicit = 10, 5
let addNumbers arguments =
    let a, b = arguments
    a + b

// Listing 9.7
// -> Genericized functions with tuples
let addNumbers1 arguments =
    //deconstructing a four-part tuple
    let a, b, c, _ = arguments
    a + b

// Notes:
// 1. when the size of a tuple is getting larger than 2, better to think about other 
// ways of grouping, because they get increasingly harder to manage
// 2. think about naming intelligently the functions that return tuples

// Listing 9.9
// -> Intelligently nameing functions
// let a, b = getData() // <-- Poor Naming
// let a, b = getBankDetails() <-- Improved Naming
// let a, b = getSortCodeAndAccountNumber <-- Better Naming

// TRY THIS:

