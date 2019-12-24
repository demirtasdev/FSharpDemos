open System

// 10.1 Records in F#:
// i: simple to use objects designed to store, transfer 
// and access immutable data that have name fields

// Listing 10.4
// -> Immutable structural equality record in F#:
type Address =
    { Street: string
      Town: string 
      City: string }
// with this you get:
// 1. a constructor that requires all fields to be provided
// 2. public access for all fields (which are read-only)  
// 3. full structural equality, throughout the entire object graph

// Listing 10.5
// -> Constructing nested records in F#:
//declaring the Customer record type:
type Customer =
    { Forename: string 
      Surname: string
      Age: int
      Address: Address
      EmailAddress: string }
//creating a Customer with Address inline:
let customer =
    { Forename = "Joe" 
      Surname = "Bloggs" 
      Age = 30
      Address =
        { Street = "The Street"
          Town = "The Town"
          City = "The City" } 
      EmailAddress = "joe@bloggs.com"}
// i: notice you have defined the Customer's address inline.
// you could have done this seperately if you wanted to, using let.  
// i: you cannot define a Customer with an empty field. you are
// forced to populate all the fields when creating a record value


// NOW YOU TRY:
// 1:define a car record
type Car =
    { Model: string 
      Make: string
      Year: int
      EngineSize: decimal
      NumberOfDoors: int }
// 2:create an instance of it
let myCar =
    { Model = "F-Type" 
      Make = "Jaguar"
      Year = 2019
      EngineSize = 242M 
      NumberOfDoors = 4}
  
// Listing 10.6
// -> Providing explicit types for constructing records
// explicitly declaring the type of the address value:s
let address: Adress = ...
// explicitly declaring the type that the Street field belongs to:
let addressExplicit =
    { Address.Street = ... }

// i: when you prefix a field with the type, the compiler will
// immediately give you intellisense

// Listing 10.7
// -> Copy-and-update record syntax:
let updatedCustomer =
    //creating a new version of a record by using the WITH keywords
    { customer with
        Age = 31
        EmailAddress = "joe@bloggs.co.uk"}

// Listing 10.8
// -> Comparing two records in F#
//comaring two records by using the equals operator
let isSameAddress = (address = addressExplicit)

// NOW YOU TRY
// 1. define a record type
type Computer =
    { Make: string 
      OperatingSystem: string
      Memory: int
      CpuSpeed: float }
// 2. create two instances with same value
let pc1 =
    { Make = "Dell"
      OperatingSystem = "MS Windows"
      Memory = 32
      CpuSpeed = 3.5 }
let pc2 =
    { Make = "Dell"
      OperatingSystem = "MS Windows"
      Memory = 32
      CpuSpeed = 3.5 }
// 3. compare them using several different approaches
let comparison1 = pc1 = pc2
let comparison2 = pc1.Equals pc2
let comparison1 = Object.ReferenceEquals(pc1, pc2)

// 5. create a function that uses copy-and-update to return a
// copy of the passed in Customer object with a random age between 18 and 45
// after printing the old and new age on the console
let updateCustomer (customer:Customer) =
    let updatedCust =
        { customer with
            Age = Random().Next(18, 45) }
    printfn "previous age: %i" customer.Age
    printfn "new age: %i" updatedCust.Age
    updatedCust

let newCust = updateCustomer customer

// i: at runtime records compile into classes
// i: default type of equality checking for records is -> Structural Equality

// 10.3.1 Refactoring
// use power tools to generate fields automatically
// after you've only defined one of them
let custo = 
    { Forename = "Alican"
      Surname = failwith "Not Implemented"
      Age = failwith "Not Implemented"
      Address = failwith "Not Implemented"
      EmailAddress = failwith "Not Implemented" }

let shadowPC = { Make = "Dell"; OperatingSystem = "MS Windows"; Memory = 32; CpuSpeed = 3.5 }
let shadowPC = { shadowPC with OperatingSystem = "Ubuntu" } 
let shadowPC = { shadowPC with OperatingSystem =  "MintOS" }
// i: this isn't the same as mutating. this is to use the same symbol with a new value