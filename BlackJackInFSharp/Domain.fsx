open System

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