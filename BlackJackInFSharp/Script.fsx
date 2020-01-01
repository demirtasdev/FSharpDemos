open System

// Enum type for suits =>
type Suits =
| Clubs = 1
| Hearts = 2
| Diamonds = 3
| Spades = 4

// Enum type for numbers =>
type Numbers =
| Ace = 1 | Two = 2 | Three = 3 | Four = 4 | Five = 5 | Six = 6 | Seven = 7 
| Eight = 8 | Nine = 9 | Ten = 10 | Jack = 11 | Queen = 12 | King = 13

// Records for Cards, and Hand which is a list of Cards =>
type Card = { Suit: Suits; Number: Numbers }
type CardCollection = { Cards: Card list }

type Hand = Hand of CardCollection
type Deck = Deck of CardCollection

// Record for User =>
type User = { Name: string }
// DU for player =>
type Player = Player of User: User * Hand: Hand



// Generate a deck of cards populated with every possible combination =>
let generateDeck =
    Deck { Cards = 
        [ for x = 1 to 4 do 
            for y = 1 to 13 do
                { Suit = enum<Suits>(x); Number = enum<Numbers>(y) } ] }
        
// Shuffle the deck, putting the cards in a random order =>
let rec shuffleDeck (indexer: int option) (pack: Card list) =
    // Set an incrementor according to the indexer passed (or not) =>
    let incrementor =
        match indexer with
        | Some x -> x + 1
        | None -> 0

    // Take action according to the incrementor =>
    match incrementor with
    | inc when inc < 0 -> None
    | inc when inc <= 51 ->
        let currentCard = pack.[incrementor]
        let randomCard = pack.[ Random().Next(0, 51) ]
        let pack = 
            [ for card in pack do 
                match card with
                | card when card = currentCard -> randomCard
                | card when card = randomCard -> currentCard
                | _ -> card ]
        pack |> shuffleDeck (Some incrementor)
    | _ -> Some pack

// Take a deck and return one card and the rest of the cards from it as option
let drawCard (deck: Card list) =
    match deck with
    | [] -> None
    | (firstCard :: theRest) -> 
        Some (firstCard, theRest)

// Take a deck and return two cards, and the rest of the cards from it as option
let dealHand (deck: Card list) =
    match (deck |> drawCard) with 
    | Some(firstDraw, rest) ->
        match (rest |> drawCard) with
        | Some (secondDraw, rest) ->
            Some ((firstDraw, secondDraw), rest)
        | None -> None        
    | None -> None

// Take the number of players (N) and created a UserList with the length N
let setPlayers numberOfPlayers = 
    [ for x = 1 to numberOfPlayers do { Name = sprintf "P%d" x } ]

// takes a list of users and gives each user 2 cards and returns them as a list of players 
// let rec dealHands (deck: Card list option) (hands: Hand list option) (users: User list) =
//     match deck with
//     | Some remaining ->
//         for user in users do
//             match deck |> dealHand with
//             | Some (hand, rest) ->
//                 match hands with
//                 | Some hands -> 
//                     dealHands((Some rest), (hands |> List.append hand), user)
//                 | None ->
//                     dealHands ((Some generateDeck), None, users)
//             | None -> None
//     | None -> 
//          dealHands ((Some generateDeck), None, users)

let rec startGame userList cardList (deck: Deck option) =
    let currentDeck =
        match deck with
        | Some deck -> deck
        | None -> generateDeck

    match userList with
    | None | Some [] -> None
    | Some list when list.Length < 2 -> None
    | Some list -> [ for user in list do Player(user, hand) ] 

// type Player = Player of User: User * Hand: Hand

// takes a list of users and gives each user 2 cards and returns them as a list of players
// let dealHands : User list -> Card list -> Person list = 
//     fun users cards ->
//         users |> List.fold  
