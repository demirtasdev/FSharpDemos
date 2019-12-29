open System

// Lesson 20: PROGRAM FLOW

// Q1: Write both types of for loops
let listA = [ 0 .. 10 ]
for i in listA do printfn "%d" i
for i = 1 to 10 do printfn "%d" i

// Q2: Use Comprehension
let listB = [ for i = 1 to 10 do i * i ]
let arrayA = [| for i = 1 to 10 do i |> float |]
let seqA = seq { for i = 1 to 10 do sprintf "%d" i }

// Q3: Write if/then logic
let a = 21
if a < 21 then printfn "The value is smaller than 21."
elif a > 21 then printfn "The value is greater than 21."
else printfn "The value is 21"

// Q4: Write Pattern Matching logic
match a with
| 21 -> printfn "The value is 21"
| _ -> printfn "The value is not 21"

// Q5: Use the When Guard Clause
match a with
| a when a < 21 -> printfn "The value is smaller than 21."
| a when a > 21 -> printfn "The value is greater than 21."
| _ -> printfn "The value is 21"

// Q6: Use Nested Pattern Matching
match a with
| a when a >= 21 ->
    match a with
    | a when a <= 100 ->
        printfn "The value is between 21 and 100."
    | _ -> printfn "The value is greater than 100."
| _ -> printfn "The value is smaller than 21"

// Q7: Pattern Match against a List
let listC = [ "Alican"; "Constantine"; "Demirtas" ]
match listC with
| [] -> printfn "This is an Empty List."
| [ only ] -> printfn "This list has one member."
| [ first; second ] -> printfn "This list has two members."
| _ -> printfn "This list has more than two members"

// Q8: Pattern Match against a Record
type Person =
    { Age : int 
      Name : string
      BirthYear : int }
let me = { Age = 21; Name = "Alican"; BirthYear = 1998 }

match me with
| { Name = "Isaac" } -> printfn "He's Isaac"
| { BirthYear = 19650 } -> printfn "He's a child of the 80s"
| { Age = age } when age < 24 -> printfn "He's a Man-child!"
| _ -> printfn "I dunno what the hell he is..."

// Q9: Pattern Match against a record list:
let people = 
    [ { Name = "Sevasti"
        Age = 21
        BirthYear = 1998 }
      { Name = "Sovatsna" 
        Age = 29
        BirthYear = 1990 } ]

match people with
| [ { Age = age }; _ ] when age < 24 -> 
    printfn "There is at least one man/woman-child in this list."
| [ { Name = "Sevasti" }; _ ] -> printfn "Sevasti is in this list!"
| _ -> printfn "I don't like this list"