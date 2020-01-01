open System

// Enum type for suits =>
type Suits =
| Clubs = 1
| Hearts = 2
| Diamonds = 3
| Spades = 4

// Enum type for numbers
type Numbers =
| Ace = 1 | Two = 2 | Three = 3 | Four = 4 | Five = 5 | Six = 6 | Seven = 7 
| Eight = 8 | Nine = 9 | Ten = 10 | Jack = 11 | Queen = 12 | King = 13

// Records for Cards, and Hand which is a list of Cards =>
type Card = { Suit: Suits; Number: Numbers }
type CardCollection = { Cards: Card list }
// Wrapper types for "Hand" and "Deck" Card lists
type Hand = Hand of CardCollection
type Deck = Deck of CardCollection
// Record for User =>
type User = { Name: string }
// DU for player =>
type Player = Player of User: User * Hand: Hand

// ACTIVE PATTERNS
// Get the User of a Player
let (|UserOf|) player = match player with Player (user, _) -> user
// Get the Cards of a Player
let (|PlayerCards|) player = match player with Player (_, (Hand {Cards = cards})) -> cards
// Get the Cards of a Hand
let (|HandCards|) hand = match hand with Hand { Cards = cards } -> cards
// Get the Cards of a Deck if it has any.
let (|DeckCards|EmptyDeck|) deck = 
    match deck with 
    | Deck { Cards = cards } when cards |> List.isEmpty -> EmptyDeck
    | Deck { Cards = cards } -> DeckCards cards

// Generate a deck of cards populated with every possible combination =>
let generateDeck =
    Deck { Cards = 
        [ for x = 1 to 4 do 
            for y = 1 to 13 do
                { Suit = enum<Suits>(x); Number = enum<Numbers>(y) } ] }
        
// Shuffle the deck, putting the cards in a random order =>
let rec tryShuffleDeck (indexer: int option) (pack: Deck option) =
    // Set an incrementor according to the indexer passed (or not) =>
    let incrementor =
        match indexer with
        | Some x -> x + 1
        | None -> 0

    let cardList =
        match pack with
        | Some ( Deck cardCollection ) -> cardCollection.Cards
        | None -> []

    // Take action according to the incrementor =>
    match incrementor with
    | inc when inc < 0 -> None
    | inc when inc <= 51 ->
        let currentCard = cardList.[incrementor]
        let randomCard = cardList.[ Random().Next(0, 51) ]
        let deck = 
            Deck 
                { Cards = 
                    [ for card in cardList do 
                        if card = currentCard then randomCard
                        elif card = randomCard then currentCard
                        else card ] }
        Some deck |> tryShuffleDeck (Some incrementor)
    | _ -> Some (Deck { Cards = cardList })

// TRY SHUFFLING THE DECK
tryShuffleDeck None (Some generateDeck)

// Take a deck and return one card and the rest of the cards from it as option
let tryDrawCard (deck: Deck) =
    match deck with
    | Deck( { Cards = [] } ) -> None
    | Deck( { Cards = (firstCard :: theRest) } ) -> 
        Some (firstCard, Deck({ Cards = theRest }))

// try drawing a card
generateDeck |> tryDrawCard

// Take a deck and return two cards, and the rest of the cards from it as option
let tryDealHand (deck: Deck) =
    match (deck |> tryDrawCard) with 
    | Some(firstDraw, theRestAfterFirstDraw) ->
        match (theRestAfterFirstDraw |> tryDrawCard) with
        | Some (secondDraw, theRestAfterSecondDraw) ->
            Some (Hand { Cards = [ firstDraw; secondDraw ] }, theRestAfterSecondDraw)
        | None -> None        
    | None -> None

// try dealing a hand
generateDeck |> tryDealHand

// Take the number of players (N) and created a UserList with the length N
let setUsers numberOfPlayers = 
    [ for x = 1 to numberOfPlayers do { Name = sprintf "P%d" x } ]

// try setting 2 players
2 |> setUsers

let userList = Some (2 |> setUsers)
let deck : Deck option = None
let playerList : Player list option = None


// Start the game by dealing hands
let rec startGame 
    (deck: Deck option) 
    (playerList: Player list option) 
    (userList: User list option) =

    // find the current state of the deck
    let currentDeck = match deck with | Some deckInput -> deckInput | None -> generateDeck
    let currentPlayers = match playerList with | Some players -> players | None -> []
    let currentUsers = match userList with | Some users -> users | None -> []

    match currentUsers, currentPlayers with
    // uList has more members than pList -> ...
    | users, players 
        when users.Length >= 2  
        && players.Length < users.Length ->
            match (currentDeck |> tryDealHand) with
            // PM to deconstruct the result
            | Some(theHand, theRest) ->
                //I've bound some keywords below for readability's sake. Could be inline
                let user = users.[ players.Length ]
                let newPlayer = Player(user, theHand)
                let newPlayerList = players @ [ newPlayer ]
                match players, users with
                | pList, uList when pList.Length < uList.Length ->
                    (Some uList) 
                    |> startGame (Some theRest) (Some newPlayerList)
                | _ -> Some (players, theRest)
            | _ -> None
    | _ ->
        match (playerList, deck) with
        | Some pList, Some deck -> Some(pList, deck)
        | _ -> None

// Try starting game:
let users = 2 |> setUsers
let shuffledDeck = tryShuffleDeck None (Some generateDeck)
let gameOn = Some users |> startGame shuffledDeck None

let tryServePlayer (player: Player) (deck: Deck) =
    let user = match player with UserOf user -> user
    let cardList = match player with PlayerCards cards -> cards
    
    match deck with
    | DeckCards cards ->
        let newPlayer = Player(user, Hand {Cards = cards.Head :: cardList}) 
        let newDeck = Deck {Cards = cards.Tail}
        Some (newPlayer, newDeck)
    | EmptyDeck -> None

// TODO: decouple from 'gameOn' the player list and the deck, 
// send them to try ServePlayer to test the function