open System

type Customer =
    { Name:string }

type Account = { 
    ID:Guid 
    Balance:decimal 
    Owner:Customer }

// attempt withdrawal
let withdraw amount ( account:Account ) =
    // check whether the customer has sufficient funds
    match amount with
    | a when ( a <= account.Balance ) ->
        // bind the new balance to a keyword
        let newBalance = account.Balance - amount
        Some 
            ( { ID = account.ID
                Balance = newBalance
                Owner = account.Owner } )
    | _ -> None

// deposit money
let deposit amount (account:Account) =
    let newBalance = account.Balance + amount
    { ID = account.ID
      Balance = newBalance
      Owner = account.Owner }   

let finalize (account:Account) =
    printfn "Your final balance is: %.2f" account.Balance
    printfn "Thank you for choosing us!"

// transaction loop keeps running until the user types in "exit"
let rec transactionLoop ( account:Account ) =
    // ask what the user would like to do
    printfn "Would you like to \"deposit\", \"withdraw\", or \"exit\"?"
    let response = Console.ReadLine()
    // pattern match to call the corresponding function
    match response with
    | "deposit" -> 
        printfn "How much would you like to deposit?"
        let amount = decimal <| Console.ReadLine()
        let updatedAccount = account |> deposit amount
        transactionLoop updatedAccount
    | "withdraw"-> 
        printfn "How much would you like to withdraw?"
        let amount = decimal <| Console.ReadLine()
        let updatedAccount = account |> withdraw amount

        match updatedAccount with
        | Some x ->
            printfn "Deposit successful. Current Balance: %.2f" x.Balance
            printfn "Could we help with anything else?" 
        | None -> 
            printfn "Withdrawal failed. Insufficient funds."

        transactionLoop account
    | "exit" ->
        printfn "Press any key to continue..."
        Console.Read() |> ignore
        account
    | _ ->
        printfn "Faulty response. Please type in \"withdraw\" or \"response\"."
        transactionLoop account

// bind the result after all operations to
// a variable as the final state of the account
let finalAccount =
    // Start the loop with an example account
    transactionLoop 
        { ID = Guid.NewGuid()
          Owner = { Name = "Isaac A." }
          Balance = 100M }

    