open System

[<AutoOpen>]
module Domain =
// DU for Suir
    type Suit =
        | Clubs
        | Hearts
        | Diamonds
        | Spades

    // DU for Ranks
    type Number = Two | Three | Four | Five | Six | Seven | Eight | Nine | Ten
    type Rank =
        | Ace
        | King
        | Queen
        | Jack
        | Number of Number

    // Records for Cards, and Hand which is a list of Cards =>
    type Card = { Suit: Suit; Rank: Rank }
    // Wrapper types for "Hand" and "Deck" Card lists
    type Hand =
        | Hand of Card list
        member this.Cards = match this with Hand cardList -> cardList
    type Deck = 
        | Deck of Card list
        member this.Cards = match this with Deck cardList -> cardList

    // Record for User =>
    type User = { Name: string }
    // DU for player =>
    type Player = { User: User; Hand: Hand }
    // Record representing game state =>
    type GameState = {Players: Player list; Deck: Deck }
    // Single case DU representing possibleScores
    type PossibleScores = 
        | PossibleScores of int list
        member this.List = match this with PossibleScores scores -> scores

    let allRanks = 
        [ Ace; King; Queen; Jack; Number(Two)
          Number(Three); Number(Four); Number(Five) 
          Number(Six); Number(Seven); Number(Eight) 
          Number(Nine);Number(Ten) ]


[<AutoOpen>]
module Deal =
    // Generate a deck of cards populated with every possible combination =>
    let generateDeck =
        Deck 
            [ for suit in [ Clubs; Diamonds; Hearts; Spades ] do
                for rank in allRanks do
                    { Suit = suit; Rank = rank } ]

    let random = Random() // Random type for shuffling
    let shuffle cards = cards |> List.sortBy (fun x -> random.Next())

    // Take a deck and return one card and the rest of the cards from it as option
    let tryDrawCard (deck: Deck) =
        match deck.Cards with
        | [] -> None
        | cards -> Some (cards.Head, Deck cards.Tail)

    // Take a deck and return two cards, and the rest of the cards from it as option
    let tryDealHand (deck: Deck) =
        match (deck |> tryDrawCard) with 
        | Some(firstDraw, theRestAfterFirstDraw) ->
            match (theRestAfterFirstDraw |> tryDrawCard) with
            | Some (secondDraw, theRestAfterSecondDraw) ->
                Some (Hand [ firstDraw; secondDraw ], theRestAfterSecondDraw)
            | None -> None        
        | None -> None    

    // Take the number of players (N) and created a UserList with the length N
    let setUsers numberOfPlayers = 
        [ for x = 1 to numberOfPlayers do { Name = sprintf "P%d" x } ]


    // FOLDER function for dealing hands
    let dealHands gameState user =
        let players, deck = 
            match gameState with
            { Players = players; Deck = deck } -> players, deck

        match deck |> tryDealHand with
        | Some(hand, theRest) -> 
            match players with
            | playerList ->
                { Players = { User = user; Hand = hand } :: playerList
                  Deck = theRest }
        | None -> gameState

    let hitPlayer player deck =
        let newCard, newDeck =
            match deck |> tryDrawCard with
            | Some (drawnCard, theRest) -> drawnCard, theRest
            | None -> failwith "TODO: FIX THIS PM TO RETURN A PROPER VALUE"

        let user, currentHand = match player with { User = user; Hand = cards } -> user, cards.Cards
        let newHand = Hand (newCard :: currentHand)
        
        { User = user; Hand = newHand }, newDeck

    // TODO: Delete this if you don't need it
    let tryServePlayer {User = user; Hand = hand} (Deck deck) =    
        match deck with
        | nextCard :: remainingCards ->
            let newPlayer = {User = user; Hand = Hand (nextCard :: hand.Cards) } 
            let newDeck = Deck remainingCards
            Some (newPlayer, newDeck)
        | [] -> None    


[<AutoOpen>]
module Calculations =
    
    let getScore card = 
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

    let addCardScore possibleScores card =
        let scoreList = match possibleScores with PossibleScores(scores) -> scores

        let newScores =
            match card |> getScore with
            | [ single ] -> [ for s in scoreList do s + single ]                    
            | multiple ->
                [ for s in scoreList do
                    for p in multiple do
                        s + p ]

        PossibleScores newScores


    let playerScore (player: Player) =
        [ match player with 
            | { User = _; Hand = hand } -> 
                for card in hand.Cards do 
                    yield! card |> getScore ]


    let scoreHand (Hand cards) = []


    let isPlayerBust {User = _; Hand = hand} =
        hand |> scoreHand |> List.forall (fun s -> s > 21)


    let endGameStats (players: Player list) =
        let playersByScore = 
            players 
            |> List.sortBy (fun p -> p |> playerScore)

        [ for i = 0 to playersByScore.Length - 1 do
            {| Standing = sprintf "%d" i ; Name = playersByScore.[i] |} ]

    // Check if all players but one busted
    let onePlayerLeft (players: Player list) =
        if players.Length = 1 then true, sprintf "%s Wins!" players.[0].User.Name
        else false, ""
   

// Start the game for Two people
let readyState =
    let setState = { Players = []; Deck = generateDeck }
    let users = [ { Name = "P1" }; { Name = "P2" } ]
    users |> Seq.fold dealHands setState
