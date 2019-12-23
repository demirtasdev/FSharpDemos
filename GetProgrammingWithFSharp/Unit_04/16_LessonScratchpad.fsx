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

