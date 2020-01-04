open System

[<AutoOpen>]
module Domain =

    type Suit =
        | Clubs
        | Hearts
        | Diamonds
        | Spades

    type Number = 
        | Two 
        | Three 
        | Four 
        | Five 
        | Six 
        | Seven 
        | Eight 
        | Nine 
        | Ten

    type Rank =
        | Ace
        | King
        | Queen
        | Jack
        | Number of Number

    type Card = 
        { Suit: Suit
          Rank: Rank }
   
    type User = 
        { Name: string }

    type Player = 
        { User: User
          Hand: Card list
          PossibleScores: int list }

    type GameState = 
        { Players: Player list
          Deck: Card list
          DuePlayerIndex: int }

    type Response =
        | Draw
        | Pass
        | EndGame

[<AutoOpen>]
module SetUp =

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

    let drawTwoCards deck =
        match (deck |> drawACard) with 
        | Some(firstDraw, theRestAfterFirstDraw) ->

            match (theRestAfterFirstDraw |> drawACard) with
            | Some (secondDraw, theRestAfterSecondDraw) ->
                Some ([ firstDraw; secondDraw ], theRestAfterSecondDraw)
            | None -> None

        | None -> None    

    let setUpGameForNPlayers numberOfPlayers = 
        { Players = 
            [ for x = 1 to numberOfPlayers do 
                { User = 
                    { Name = sprintf "P%d" x } 
                  Hand = []
                  PossibleScores = [] } ]
          Deck = fullDeck
          DuePlayerIndex = 0 }

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
                    (* PossibleScores = hand |> getPossibleScores *) 
                    } ]

        let remainingCards =
            playerCardsGroups
            |> Map.find None
            |> List.map snd

        playerHands, remainingCards

    let dealOneToPlayer player deck =
        let newCard, newDeck =
            match deck |> drawACard with
            | Some (drawnCard, theRest) -> drawnCard, theRest
            | None -> failwith "TODO: FIX THIS PM TO RETURN A PROPER VALUE"

        let user, currentHand = match player with { User = user; Hand = cards } -> user, cards
        let newHand = newCard :: currentHand
        
        { User = user; Hand = newHand; PossibleScores = [] }, newDeck


[<AutoOpen>]
module Calculations =

    let getCardScore card = 
        match card.Rank with
        | Ace -> [1; 11]
        | King | Queen | Jack -> [10]
        | Number num ->
            match num with 
            | Two -> [2] 
            | Three -> [3] 
            | Four -> [4]
            | Five -> [5]
            | Six -> [6]
            | Seven -> [7]
            | Eight -> [8]
            | Nine -> [9]
            | Ten -> [10]

    let getPossibleScores (cards: Card list) =
        
        let scoresInitial = [ for c in cards do c |> getCardScore ]

        let aceScores = 
            [ for scores in scoresInitial do
                if scores.Length = 2 then scores ]
            |> List.concat            

        let otherScoresInitial =
            [ for score in scoresInitial do
                if score.Length = 1 then k
                    match score with 
                    | [ score ] -> score 
                    | _ -> failwith "SCOFERASDOFA" ]
        
        let otherScoresFinal =
            if otherScoresInitial.IsEmpty then [ 0 ]
            else otherScoresInitial

        if aceScores.IsEmpty then otherScoresFinal
        else
            otherScoresFinal
            |> List.replicate (aceScores.Length)
            |> List.sort
            |> List.indexed
            |> List.map (fun result ->
                match result with 
                | i, scores ->
                    match scores with
                    | [ first; _ ] ->
                        if i % 2 = 0 || i = 0 then
                            first + 1
                        else
                            first + 11
                    | _ -> 0 )

    [ { Rank = Number(Two) ; Suit = Clubs }; { Rank = Number(Five) ; Suit = Clubs }; { Rank = Ace ; Suit = Clubs } ] 
    |> getPossibleScores

    let playerScore (player: Player) =
        [ match player with 
            | { User = _; Hand = hand } -> 
                for card in hand do 
                    yield! card |> getCardScore ]

    let isPlayerBust { User = _; Hand = hand } =
        hand |> getPossibleScores |> List.forall (fun s -> s > 21)


    let endGameWithStats (players: Player list) =
        let playersByScore = 
            players  
            |> List.sortBy (fun p -> p |> playerScore)

        [ for i = 0 to playersByScore.Length - 1 do
            {| Standing = sprintf "%d" i ; Name = playersByScore.[i] |} ]

    // Check if all players but one busted
    let onePlayerLeft (players: Player list) =
        if players.Length = 1 then true, sprintf "%s Wins!" players.[0].User.Name
        else false, ""

let tryParseResponse response =
    match response with
    | 'D' | 'd' -> Some Draw
    | 'P' | 'p' -> Some Pass
    | _ -> None

let actOnResponse gameState response =
    match response with
    | Pass -> { gameState with DuePlayerIndex = gameState.DuePlayerIndex + 1}
    | Draw ->   
        let newPlayer, newDeck =
            gameState.Deck
            |> dealOneToPlayer gameState.Players.[gameState.DuePlayerIndex]

        let newPlayerList =            
            gameState.Players
            |> List.map (fun player -> 
                if player = gameState.Players.[gameState.DuePlayerIndex] then newPlayer
                else player )            
        
        { Players = newPlayerList
          Deck = newDeck 
          DuePlayerIndex = gameState.DuePlayerIndex + 1 }      
    | EndGame -> gameState   

let responses =
    seq {
        printfn "[D]raw, [P]ass? or [E]nd the game: "
        Console.ReadKey().KeyChar }

let isValidResponse response =
    match response with    
    | Some _ -> true
    | None -> false

module Main =
    let beginningGameState = 2 |> setUpGameForNPlayers

    let rec gameLoop gameState =
        
        printfn "[D]raw, [P]ass? or [E]nd the game: "

        let response = 
            Console.ReadKey().KeyChar 
            |> tryParseResponse

        match response with
        | Some response -> 
            response |> actOnResponse gameState
        | None -> gameState |> gameLoop



    




    

    