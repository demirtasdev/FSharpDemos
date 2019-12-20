// 15.1: A sample dataset of football results...
// record of input data
type FootballResult =
    { HomeTeam : string
      AwayTeam : string
      HomeGoals : int
      AwayGoals : int }

let create ( ht , hg ) ( at , ag ) = 
    { HomeTeam = ht ; HomeGoals = hg ; AwayTeam = at ; AwayGoals = ag }  

let results =
    [ create ( "Messiville", 1 ) ( "Ronaldo City", 2 ) 
      create ( "Messiville", 1 ) ("Bale Town", 3) 
      create ( "Bale Town", 3 )  ( "Ronaldo City", 1 ) 
      create ( "Bale Town", 2 )  ( "Messiville", 2 )
      create ( "Ronaldo City", 4 )  ( "Messiville", 2 )
      create ( "Ronaldo City", 1 )  ( "Bale Town", 2 ) ]


// #15.2: An imperative solution to a calculation over data...
// defining your output summary type:
open System.Collections.Generic
type TeamSummary = { Name : string; mutable AwayWins : int }
// accumulator for output: 
// (resize array is list in C#)
let summary = ResizeArray()

// core algorithm:
for result in results do
    if result.AwayGoals > result.HomeGoals then
        // flag to check whether it's a new entity
        let mutable found = false
        for entry in summary do
            if entry.Name = result.AwayTeam then
                found <- true
                entry.AwayWins <- entry.AwayWins + 1
            if not found then
                summary.Add { Name = result.AwayTeam ; AwayWins = 1 }

// custom icomparer for sorting based on away wins:
let comparer =
    { new IComparer<TeamSummary> with
        member this.Compare (x, y) =
           if x.AwayWins > y.AwayWins then -1
           elif x.AwayWins < y.AwayWins then 1
           else 0 }

// sort using the custom icomparer
summary.Sort(comparer)

// a standalone function to calculate whether a result is an away win
let isAwayWin result = result.AwayGoals > result.HomeGoals

results
// using isAwayWin within the List.filter higher order function
|> List.filter isAwayWin
// using countBy with an inline lambda expression to return the number of rows for each away team
|> List.countBy ( fun result -> result.AwayTeam )
|> List.sortByDescending ( fun ( _ , awayWins ) -> awayWins)

