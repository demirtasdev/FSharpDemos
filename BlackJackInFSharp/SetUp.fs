module BlackJack.SetUp

open Domain
open Operations
open System

let allSuits = 
    [ Clubs
      Diamonds
      Hearts
      Spades ]

let allRanks = 
    [ Number(Two)
      Number(Three)
      Number(Four)
      Number(Five) 
      Number(Six)
      Number(Seven)
      Number(Eight)
      Number(Nine)
      Number(Ten) 
      Jack
      Queen
      King
      Ace ]

let random = Random() 

let fullDeck =    
    [ for suit in allSuits do
        for rank in allRanks do
            { Suit = suit; 
              Rank = rank } ]

let shuffle cards = 
    cards
    |> List.sortBy (fun x -> random.Next())

let drawACard (deck : Card list) =
    match deck with
    | [] -> None
    | first :: second -> Some (first, second)

let setUpGameForNPlayers numberOfPlayers = 
    if numberOfPlayers > 1 then
        Some 
            { Players = 
                [ for x = 1 to numberOfPlayers do 
                    { Name = sprintf "P%d" x 
                      Hand = [] } ]
              Deck = fullDeck |> shuffle
              DuePlayerIndex = 0 }
    else
        None

let dealOneToPlayer player deck =
    let newCard, newDeck =
        match deck |> drawACard with
        | Some (drawnCard, theRest) -> drawnCard, theRest
        | None -> failwith " TODO: FIX THIS PM TO RETURN A PROPER VALUE "


    { player with Hand = newCard :: player.Hand }, newDeck

let isValidResponse response =
    match response with
    | 'D' | 'd' | 'P' | 'p' | 'E' | 'e' -> true
    | _ -> false

let parseResponse response =
    match response with
    | 'D' | 'd' -> Draw
    | 'P' | 'p' -> Pass
    | 'E' | 'e' -> EndGame
    | _ -> failwith "WTF"

let getBestScore (scores: int list) =
    let bestScore = 
        [ for s in scores do
            if s < 21 then s ]

    if bestScore.IsEmpty then scores |> List.min
    else bestScore |> List.max

let isBusted player =
    (player.Hand |> getPossibleScores |> getBestScore) > 21

let actOnResponse gameState response =
    match response with
    | Pass -> 
        printfn "PASS AAAAAAAAAAAAAAAAAAA"
        { gameState with 
            DuePlayerIndex = 
                if gameState.DuePlayerIndex < gameState.Players.Length - 1 then gameState.DuePlayerIndex + 1
                else 0 }
    | Draw ->   
        let newPlayer, newDeck =
            gameState.Deck
            |> dealOneToPlayer gameState.Players.[gameState.DuePlayerIndex]

        let newPlayerList =            
            gameState.Players
            |> List.map (fun p -> 
                if p = gameState.Players.[gameState.DuePlayerIndex] then newPlayer
                else p )            
        
        printfn "%s draws a card." newPlayer.Name
        
        match newPlayer |> isBusted with
        | true -> 
            // Player busts
            printfn "It's a bust! He's out."
            // New list of players without the busted player
            let newPlayerList = 
                gameState.Players 
                |> List.filter ((<>) gameState.Players.[gameState.DuePlayerIndex])
            // If more than one player remains:
            if newPlayerList.Length > 1 then
                { gameState with 
                    Players = newPlayerList
                    Deck = newDeck }
            // One man standing:              
            else 
                printfn "%s Wins!" gameState.Players.[0].Name
                gameState
        | false -> 
            printfn "New score: %d" (newPlayer.Hand |> getPossibleScores |> getBestScore)
            
            { gameState with
                Players = newPlayerList
                Deck = newDeck
                DuePlayerIndex = 
                    if gameState.DuePlayerIndex < gameState.Players.Length - 1 then gameState.DuePlayerIndex + 1
                    else 0 }
    | EndGame -> 
        printfn "ENDGAME AAAAAAAAAAAAA"
        gameState   

let responses =
    seq {
        while true do
            printfn "[D]raw, [P]ass? or [E]nd the game: "
            yield Console.ReadKey().KeyChar 
            printfn "" }

let dealTwoCardsToEachPlayer (players: Player list) (cards: Card list) =
    let playersDuplicated =
        players 
        |> Seq.replicate 2
        |> Seq.concat
        |> Seq.toArray

    let playerCardsGroups = 
        [ for i, card in cards |> List.indexed do
            let player = playersDuplicated |> Array.tryItem i
            (player, card) ]
        |> List.groupBy fst
        |> Map

    let playerHands =
        [ for player in players do
            let hand =
                playerCardsGroups
                |> Map.find (Some player)
                |> List.map snd 
            { player with 
                Hand = hand;
                } ]

    let remainingCards =
        playerCardsGroups
        |> Map.find None
        |> List.map snd

    playerHands, remainingCards