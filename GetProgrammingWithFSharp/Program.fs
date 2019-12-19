// Learn more about F# at http://fsharp.org

open System

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


let rec loop petrol =
    
    printfn "Where would you like to travel?"
    
    let destination = Console.ReadLine()

    let msg , newPetrol =  drive petrol destination
    
    printfn "%s Remaining petrol: %.2f" msg newPetrol
    loop newPetrol


[<EntryPoint>]
let main argv =
    loop 100.0
    0 // return an integer exit code
