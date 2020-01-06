module BlackJack.Operations

open Domain

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

let addScores (cardScores: int list) (currentScores: int list) =
    [ for score in cardScores do
        for cs in currentScores do
            score + cs ]

let getPossibleScores (cards: Card list) =
    [ for c in cards do
        c |> getCardScore ]
    |> List.fold addScores [0]

let getPlayerScore (player: Player) =
    [ match player with 
        | { Name = _; Hand = hand } -> 
            for card in hand do 
                yield! card |> getCardScore ]

let isPlayerBust { Name = _; Hand = hand } =
    hand |> getPossibleScores |> List.forall (fun s -> s > 21)

let endGameWithStats (players: Player list) =
    let playersByScore = 
        players  
        |> List.sortBy (fun p -> p |> getPlayerScore)

    [ for i = 0 to playersByScore.Length - 1 do
        {| Standing = sprintf "%d" i ; Name = playersByScore.[i] |} ]

// Check if all players but one busted
let onePlayerLeft (players: Player list) =
    if players.Length = 1 then true, sprintf "%s Wins!" players.[0].Name
    else false, ""