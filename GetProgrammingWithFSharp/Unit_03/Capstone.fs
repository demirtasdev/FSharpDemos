open System

type Customer =
    { FullName:string }

type Account =
    { ID:Guid 
      Balance:decimal
      Owner:Customer }

// transaction loop keeps running until the user types in "exit"
let rec transactionLoop ( account:Account ) =

    // attempt withdrawal function
    let withdraw ( account:Account ) =
        printfn "How much would you like to withdraw?"
        let amount = decimal <| Console.ReadLine()
        
        // check whether the customer has sufficient funds
        match amount with
        | a when ( a <= account.Balance ) ->

            // bind the new balance to a keyword
            let newBalance = account.Balance - amount

            printfn "Deposit successful. Current Balance: %.2f" newBalance
            printfn "Could we help with anything else?" 
            transactionLoop { ID = account.ID ; 
                              Balance = newBalance ; 
                              Owner = account.Owner }
        | _ ->  
            printfn "Withdrawal failed. Insufficient funds."
            transactionLoop { ID = account.ID ; 
                              Balance = account.Balance ; 
                              Owner = account.Owner }


    // deposit money function
    let deposit ( account:Account ) =
        printfn "How much would you like to deposit?"
        let amount = decimal <| Console.ReadLine()

        let newBalance = account.Balance + amount

        printfn "Deposit successful. Current Balance: %.2f" newBalance
        printfn "Could we help with anything else?" 
        transactionLoop { ID = account.ID ;
                          Balance = newBalance ;
                          Owner = account.Owner }                  

    // finalize function is called when the user types in "exit"
    let finalize ( account:Account ) =
        printfn "Your final balance is: %.2f" account.Balance
        printfn "Thank you for choosing us!"


    // ask what the user would like to do
    printfn "Would you like to \"deposit\", \"withdraw\", or \"exit\"?"
    let response = Console.ReadLine()

    // pattern match to call the corresponding function
    match response with
    | "deposit" -> (deposit account)
    | "withdraw"-> withdraw account
    | "exit" -> finalize account
    | _ ->  printfn "Faulty response. Please type in \"withdraw\" or \"response\""
            transactionLoop account

[<EntryPoint>]
let main argv =
    // send a demo account into the transaction loop    
    transactionLoop { ID = Guid.NewGuid() ; 
                      Owner = { FullName = "Isaac A." } ; 
                      Balance = 100M }

    // keep the console open for the thank you message
    printfn "Press any key to continue..."
    Console.Read() |> ignore
    0 // return an integer exit cod