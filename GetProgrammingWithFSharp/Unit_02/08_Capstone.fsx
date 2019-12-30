


type Destination =
| Stadium
| Home
| Office
| GasStation    

let drive petrol (destination:Destination) =

    let remaining =
        match destination with
        | GasStation -> petrol - 10.0
        | Home | Stadium -> petrol - 25.0
        | Office -> petrol - 50.0

    
    match remaining, destination with
    | remaining, _ when remaining < 0.0 -> "Trip has failed. Insufficient petrol." , petrol
    | remaining, destination when destination = GasStation -> "Trip succeded. Tank filled", remaining + 50.0
    | _ -> "Trip succeded.", remaining


let initialPetrol = 100.0
let destination = Stadium

let msg, result = drive initialPetrol Stadium
let msg2, result2 = drive result Stadium
let msg3, result3 = drive result2 Home
let msg4, result4 = drive result3 Office
let msg5, result5 = drive result3 GasStation