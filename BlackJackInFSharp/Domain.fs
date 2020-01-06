module BlackJack.Domain

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

type Player = 
    { Name: string
      Hand: Card list }

type GameState = 
    { Players: Player list
      Deck: Card list
      DuePlayerIndex: int }

type State =
    | SetUp
    | Active of GameState      

type Response =
    | Draw
    | Pass
    | EndGame