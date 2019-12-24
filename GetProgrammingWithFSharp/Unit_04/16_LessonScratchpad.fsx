open System

// >>[16.1]>> #_MAP_#
// . the most common collection function you'll ever use
// . converts all items in a collection from one shape to another
// . always returns the exact number of items passed to it
// . signature: [ mapping:('T -> 'U) -> list:'T list -> 'U list]
//   1. mapping is a function that maps a single item from 'T to 'U
//   2. list is the input list of 'T that you want to convert
//   3. the output is a list of 'U that has been mapped

// input data:
let numbers = [ 1 .. 10 ]
// mapping function:
let timesTwo n = n * 2

// #1: manually constructing an output collection, iterating, and adding to output:
let outputImperative = ResizeArray()
for number in numbers do
    outputImperative.Add(number |> timesTwo)

// #2: using the List.map HOF to achieve the same output:
let outputFunctional = numbers |> List.map timesTwo



// >>|16.1.2|>> #_ITER_#
// . can think of it as a variant of map that can only return a unit "()"
// . useful as the end function of a pipeline (saving, printing, etc.)
// . this means any function that has side-effects

let names = ["Potato"; "Tuples"; "Keyboard"; "Sucuk"]
names
|> Seq.iter (fun name -> printfn "%s" name)
// Output: Unit
// Side effect: Printing the name of a collection of customers



// >>[16.1.3]>> #_COLLECT_#
// . collect is a useful for of map
// . collect has many names such as SelectMany, FlatMap, Flatten and Bind
// . it takes in a list of items, and a function that returns a new collection
// from each item in that collection, and merges them back all into a single list.

// signature: [ mapping:('T -> 'U list) -> list:'T list -> 'U list ] 

// example
type Customer = {
    FirstName:string
    Orders:string list}

// customers with string list as Orders
let cust1 = {FirstName = "Alican" ; Orders =["safd"; "saga"; "lkqw"] }
let cust2 = {FirstName = "Constantine" ; Orders =["oeea"; "dlaq"; "ptwx"] }

// a group of customers
let groupOfCust = [ cust1; cust2]

// a function that returns a list for a single value
let getOrders cust = cust.Orders

// using .collect to create a list from the lists that getOrders return
let collectList =
    groupOfCust
    |> List.collect getOrders

// ---- 24/12/2019 -----

// 16.1.4 - PAIRWISE
// i: Takes a list and returns a new list of tuple pairs of the original adjacent items.
let intList = [ 1; 2; 3; 4; 5 ]
let pairs = List.pairwise intList

// Listing 16.3
// -> Using pairwise within the context of a larger pipeline:
// List of dates:
[ DateTime(2010, 5, 1)
  DateTime(2010, 6, 1)
  DateTime(2010, 6, 12)
  DateTime(2010, 7, 3) ]
// Pairwise adjacent dates:
|> List.pairwise
// Substract the dates from one another as timespan, and
// return the total days between the two dates.
|> List.map ((fun (a, b) -> b - a) >> (fun time -> time.TotalDays))


// A type and a list to experiment with:
type OGs =
    { Name: string 
      Town: string}
let listOfOGs =  
    [ { Name = "Isaac"; Town = "London" } 
      { Name = "Sara"; Town = "Birmingham" }
      { Name = "Tim"; Town = "London" }
      { Name = "Michelle"; Town = "Manchester" } ]

// 16.2.1 -> GROUPBY
// i: Group the members of a list according to a certain function
// s: projection: ('T -> 'Key) -> list: 'T list -> ('Key * 'T list) list
let groupedList =
    listOfOGs
    |> List.groupBy (fun og -> og.Town)

// 16.2.2 -> COUNTBY
// i: Return a list of keys according to a function for each iteration of said key in the list. 
//    Make a new list of these keys and the amount of times they appear in the previous list.
// s: projection: ('T -> 'Key) -> list: 'T list -> ('Key * int) list
let countedBy =
    listOfOGs
    |> List.countBy (fun og -> og.Town)

// 16.2.3 -> PARTITION
// i: Slightly simpler version of groupBy. Supply a predicate and it returns two collections
//    partitioned based on the predicate.
// s: predicate: ('T -> bool) -> list: 'T list -> ('T list * 'T list)

// Listing 16.4
// -> Splitting a collection in two based on a predicate
// Decomposing the tuple result into two lists:
let londonCustomers, otherCustomers =
    // Precivate function to split the list:
    listOfOGs |> List.partition(fun c -> c.Town = "London")

// Listing 16.5
// -> Simple aggreagation functions in F#
// Aggregate functions take in a collection of items and merge them into a smaller collection.
// Build a list of 10 floats:
let floats = [ 1.0 .. 10.0 ]
// Execute a set of aggregate functions:
let total = floats |> List.sum
let average = floats |> List.average
let max = floats |> List.max
let min = floats |> List.min

// Listing 16.6
// -> Converting between collections
let numberOne =
    // Construct an int list:
    [ 1 .. 5 ]
    // Convert from an int list to an int array:
    |> List.toArray
    // Convert from an int array to an int sequence:
    |> Seq.ofArray
    |> Seq.head