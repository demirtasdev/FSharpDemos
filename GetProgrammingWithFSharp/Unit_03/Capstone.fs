open System

// Domain
type Customer = Customer of string
type Account = { ID: Guid; Balance: decimal; Owner: Customer }

// Pure functions:
let withdraw amount account =
    if amount < account.Balance then 
        { account with Account.Balance = account.Balance - amount }, true
    else account, false

let deposit amount account =
    { account with Account.Balance = account.Balance + amount}

// Impure function:
let rec transactionLoop outputFunc inputFunc account =    
    outputFunc (sprintf "Current Balance: %.2f" account.Balance)

    outputFunc "Would you like to [d]eposit, [w]ithdraw, or e[x]it?"
    let operation = inputFunc()
    if operation = "x" then account 
    else
        outputFunc "Enter amount:"
        let amount = inputFunc() |> decimal

        match operation with
        | "d" ->
            account |> deposit amount |> transactionLoop outputFunc inputFunc
        | "w"->
            let account, isSuccess = account |> withdraw amount
            match isSuccess with
            | true ->
                outputFunc "Withdraw Successful."
                account |> transactionLoop outputFunc inputFunc
            | false ->
                printfn "Withdraw Failed."
                account |> transactionLoop outputFunc inputFunc
        | _ -> failwith "Something went wrong"

[<EntryPoint>]
let main argv =
    // Function calls:
    let myAccount = { ID = Guid.NewGuid(); Balance = 0M; Owner = Customer "Alican" }
    let finalAccount = myAccount |> transactionLoop (printfn "%s") (Console.ReadLine)
    printfn "%A" finalAccount
    
    printfn "Press any key to continue..."
    Console.Read() |> ignore
    0 // return an integer exit cod