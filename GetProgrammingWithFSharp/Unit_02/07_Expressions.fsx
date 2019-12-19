open System

let describeAge age =
    let ageDescription =
        if age < 18 then "Child!"
        elif age < 65 then "Adult"
        else "OAP"

    let greeting = "Hello"
    Console.WriteLine("{0}! You are a '{1}'.", greeting, ageDescription)

// NOW YOU TRY
let un = ()
let ageDesc = describeAge 21

let res = ( un = ageDesc )
