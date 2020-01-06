module BlackJack.Main

open System
open BlackJack.Operations
open BlackJack.Domain
open BlackJack.SetUp

[<EntryPoint>]
let main argv =
    let rec setUp (gso: GameState option) =
        match gso with
        | None -> 
            printfn "How many players would you like to set up the game for?"
            match Console.ReadLine() |> Int32.TryParse with
            | true, i -> i | false, _ -> 0
            |> setUpGameForNPlayers
            |> setUp
        | Some gameState ->
            match gameState.Deck |> dealTwoCardsToEachPlayer gameState.Players with
            | players, deck -> 
                { gameState with 
                    Players = players
                    Deck = deck }

    let finalState =
        let initialGameState = setUpGameForNPlayers 2 |> Option.get
        responses
        |> Seq.filter isValidResponse
        |> Seq.map parseResponse
        |> Seq.takeWhile ((<>) EndGame)
        |> Seq.fold actOnResponse initialGameState

    Console.ReadLine() |> ignore
    0

