open System

// Lesson 21: Relationships

// Q1: Write a Record Composition
type Customer =
    { ID: int
      FullName: string }
type Item =
    { Name: string
      Price: decimal }
type Order = 
    { Customer: Customer
      Content: Item list }

let order = 
    { Customer = 
        { ID = 21
          FullName = "Alican Demirtas"}
      Content =
        [ { Name = "Fender Strat 2019"
            Price = 700M };
          { Name = "Bulleit 70cl"
            Price = 25M } ] }



// Q2: Write a Discriminated Union
type Beverage =
| Bourbon of Brand: string * Size: float
| Scotch of Brand: string * Size: float
| IrishWhiskey of Brand: string * Size: float
| Guiness
| Other



// Q3: Write a function that has a DU as parameter
// Q4: Pattern Match against a DU
let isDrinkable (beverage:Beverage) =
    match beverage with
    | Bourbon _ 
    | Scotch _ 
    | IrishWhiskey _ -> printfn "Bottoms up!"
    | Guiness -> printfn "Don't mind..."
    | _ -> printfn "Pass."


// Q5: Write a Nested DU
type ProgrammingStyle =
| ObjectOriented
| Functional
| Other

type ProgrammingLanguage =
| Language of Style: ProgrammingStyle * Name:string

let fsharp = Language(Functional, "F#")


// Q5: Write Records with Shared Fields
type SharedFields =
    { Country: string
      NativeLanguage: string
      Above21: bool }
type Record =
    { ID: int 
      Shared: SharedFields }


// Q6: Write an Enum
type SockSmell =
| Roses = 0
| MildlySmelly = 1
| CheeseDoritos = 2
| Horrific = 3  

let mySockSmell = SockSmell.Horrific

printfn "%A" mySockSmell