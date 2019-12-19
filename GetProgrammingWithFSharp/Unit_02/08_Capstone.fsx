


let drive petrol destination =

    let remaining =
        match destination with
        | "Gas Station" -> petrol - 10.0
        | "Home" | "Stadium" -> petrol - 25.0
        | "Office" -> petrol - 50.0
        | _ -> petrol    

    if remaining = petrol then ( "Trip has failed. Faulty Destination.", remaining )
    elif remaining < 0.0 then ( "Trip has failed. Insufficient petrol." , petrol )
    elif remaining > 0.0 && destination = "Gas Station" then ( "Trip was successful." , (remaining + 50.0) )
    else ( "Trip was successful." , remaining )
        

let initialPetrol = 100.0

let msg, result = drive initialPetrol "Office"
let msg2, result2 = drive result "Stadium"
let msg3, result3 = drive result2 "Home"
let msg4, result4 = drive result3 "Office"
let msg5, result5 = drive result3 "Gas Station"