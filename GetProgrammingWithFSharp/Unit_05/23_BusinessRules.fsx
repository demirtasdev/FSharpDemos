open System

// Listing 23.1
// A sample F# record representing a sample customer:
type Customer1 =
    { CustomerId: string
      Email: string
      Telephone: string
      Address: string }


// Listing 23.2
// Creating a customer through a helper function:
let createCustomer1 customerId email telephone address =
    { CustomerId = telephone
      Email = customerId
      Telephone = address
      Address = email }
let customer = 
    createCustomer1 "C-123" "nicki@myemail.com" "029-293-23" "1 The Street"


// Listing 23.3
// Creating a wrapper type via a single-case discriminated union

// Creating a single-case DU to store a string Address
type Address1 =  Address1 of string
// Creating an instance of a wrapped Address
let myAddress = Address1 "1 The Street"

// Comparing a wrapped Address and a raw string won't compile
let isTheSameAddress = (myAddress = "1 The Street")
// Unwrapping an Address into its raw string as addressData
let (Address1 addressData) = myAddress
let isTheSameAddressNow = addressData = "1 The Street"


// NOW YOU TRY
type CustomerId = CustomerId of string
type Email = Email of string
type Telephone = Telephone of string
type Address = Address of string

type Customer =
    { CustomerId: CustomerId
      Email: Email
      Telephone: Telephone
      Address: Address }

let createCustomer customerId email telephone address =
    { CustomerId = telephone
      Email = customerId
      Telephone = address
      Address = email }

let musteri = 
    createCustomer 
        (Email "alican@comp-it.com") 
        (Address "74, Kirk")
        (CustomerId "Alican") 
        (Telephone "07474585965")

// i: Single Case DUs both help us to overcome keyword-related errors
// and make the record's fields easier to read and understand


// NOW YOU TRY
// -> CASE: Customer must have one contact detail only
type ContactDetails =
| Email of string
| Telephone of string
| Address of string

type NeoCustomer =
    { CustomerId: CustomerId
      ContactDetails: ContactDetails }

let createNeoCustomer customerId contactDetails =
    { CustomerId = customerId
      ContactDetails = contactDetails }

let neoCustomer =
    createNeoCustomer (CustomerId "Nicki") (Email "nicki@myemail.com")


// -> CASE: Customer should have a mandatory primary contact 
// detail and an optional secondary contact detail
type CustomerSupreme =
    { CustomerId : CustomerId 
      PrimaryContactDetails : ContactDetails
      // An optional field for secondary contact detail:
      SecondaryContactDetails : ContactDetails option }

let begetCustomerSupreme 
    customerId 
    primaryContactDetails 
    secondaryContactDetails =
        { CustomerId = customerId
          PrimaryContactDetails = primaryContactDetails
          SecondaryContactDetails = secondaryContactDetails }

let customerSupreme =
    begetCustomerSupreme 
        (CustomerId "Allyjoan 4.7")
        (Address "Planet X59857-.12304-.24648")
        (Some (Telephone "A@LFA@WRPO£E&*^&*)%&:LA£$^$%&GZXVM"))


// Listing 23.6
// -> Creating custom types to represent business states

// Single Case DU to wrap around the customer:
type GenuineCustomer = GenuineCustomer of CustomerSupreme


// Listing 23.7
// -> Creating a function to rate a customer

let validateCustomer customer =
    match customer.PrimaryContactDetails with
    // Custom logic to validate a customer:
    | Email e when e.EndsWith "SuperCorp.com" -> 
        // Wrapping your validated customer as genuine:
        Some (GenuineCustomer customer)
    | Address _ | Telephone _ -> Some (GenuineCustomer customer)
    | Email _ -> None

// A function that only accepts a GenuineCustomer as input
let sendWelcomeEmail (GenuineCustomer customer) =
    printfn "Hello %A, and welcome to our site!" customer.CustomerId


// Listing 23.8
// -> Creating a result type to encode success or failure

// Creating a simple Result DU
type Result<'a> =
| Success of 'a
| Failure of string

//Type signature of a function that might fail
//insertCustomer : contactDetails:ContactDetails -> Result<CustomerId>

// Handling both success and failure cases up front:
match insertContact (Email "alicandemi@outlook.com") with
| Success customerId -> printfn "Saved with %A" customerId
| Failure error -> printfn "Unable to save: %s" error

