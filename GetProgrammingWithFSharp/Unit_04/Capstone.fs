open System

type Customer =
    { FullName:string }

type Account =
    { ID:Guid 
      Balance:decimal
      Owner:Customer }

type Transaction =
    { Timestamp:DateTime
      Operation:char
      Amount:decimal
      Accepted:bool }

let consoleCommands = seq {
    while true do
        printfn "(d)eposit, (w)ithdraw or e(x)it : "
        yield Console.ReadKey().KeyChar }

// check whether the user input is valid
let isValidCommand input =
    match input with
    | 'd' | 'w' | 'x' -> true
    | _ -> false

let isStopCommand input =
    match input with
    | 'x' -> true
    | _ -> false

let getAmount input =
    if input = 'x' then ('x', 0M)
    else
        printf "%sEnter Amount: " Environment.NewLine
        let amount = Console.ReadLine() |> decimal
        printfn "%s" Environment.NewLine
        (input, amount)

let processCommand (account:Account) (cmd, amount) =
    match cmd with
    | 'd' ->
        printfn "Account %s: deposit of %.2f (approved: true)" 
            account.Owner.FullName amount

        { ID = account.ID
          Balance = account.Balance + amount
          Owner = account.Owner }
    | 'w' ->
        if amount <= account.Balance then
            printfn "Account %s: withdrawal of %.2f (approved: true)" 
                account.Owner.FullName amount

            { ID = account.ID
              Balance = account.Balance - amount
              Owner = account.Owner }
        else
            printfn "Account %s: withdrawal of %.2f (approved: false)" 
                account.Owner.FullName amount
            account              
    | _ -> account

let serialized transaction =
    sprintf "%O***%c***%M***%b"
        transaction.Timestamp
        transaction.Operation
        transaction.Amount
        transaction.Accepted


// bind the result after all operations to
// a variable as the final state of the account



[<EntryPoint>]
let main argv =
    printf "Please enter your name: "
    let name = Console.ReadLine()

    let openingAccount = 
        { Owner = { FullName = name }
          ID = Guid.Empty; 
          Balance = 0M }

          
    printfn "Current balance is £%.2f" openingAccount.Balance
    
    let finalAccount =
        consoleCommands
        |> Seq.filter isValidCommand
        |> Seq.takeWhile (not << isStopCommand)
        |> Seq.map getAmount
        |> Seq.fold processCommand openingAccount



    // let account =
    //     let commands = [ 'd'; 'w'; 'z'; 'f'; 'd'; 'x'; 'w' ]
        
    //     commands
        // |> Seq.filter isValidCommand
        // |> Seq.takeWhile (not << isStopCommand)
        // |> Seq.map getAmount
        // |> Seq.fold processCommand openingAccount

    0 // return an integer exit code
