open System

type Suits =
| Clubs = 1
| Hearts = 2
| Diamonds = 3
| Spades = 4

type Numbers =
| Ace = 1 | Two = 2 | Three = 3 | Four = 4 | Five = 5 | Six = 6 | Seven = 7 
| Eight = 8 | Nine = 9 | Ten = 10 | Jack = 11 | Queen = 12 | King = 13

type Card = { Suit: Suits; Number: Numbers }
type Hand = { Cards: Card list }

type User = { Name: string }
type Player = Player of User: User * Hand: Hand

let generateDeck =
    [ for x = 1 to 4 do
        for y = 1 to 13 do
            { Suit = enum<Suits>(x); Number = enum<Numbers>(y) } ]

let rec shuffleDeck (indexer: int option) (pack: Card list) =
    let incrementor =
        match indexer with
        | Some x -> x + 1
        | None -> 0

    match incrementor with
    | inc when inc < 0 -> None
    | inc when inc <= 51 ->
        let currentCard = pack.[incrementor]
        let randomCard = pack.[Random().Next(0, 51)]
        let pack = 
            [ for card in pack do 
                match card with
                | card when card = currentCard -> randomCard
                | card when card = randomCard -> currentCard
                | _ -> card ]
        pack |> shuffleDeck (Some incrementor)
    | _ -> Some pack

let drawCard (deck: Card list) =
    match deck with
    | [] -> None
    | _ -> 
        let returnCard = deck.[Random().Next(0, deck.Length - 1)]
        let restOfTheDeck = 
            ( [ for card in deck do 
                if card <> returnCard then card ] )
        Some (returnCard, restOfTheDeck)                

let dealHand (deck: Card list) =
    match (deck |> drawCard) with
    | Some(firstDraw, rest) ->
        match (rest |> drawCard) with
        | Some (secondDraw, rest) ->
            Some ((firstDraw, secondDraw), rest)
        | None -> None        
    | None -> None

let deck = generateDeck
let hand, rest = 
    match (deck |> dealHand) with
    | Some result -> result
    | None -> failwith "Something went wrong"

let restLength = rest.Length
