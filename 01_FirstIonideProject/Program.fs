open System

module PigLatin =
    let toPigLatin (word: string) =
        //An inner function that takes in a char
        let isVowel (c: char) =
            match c with
            | 'a' | 'e' | 'i' | 'o' | 'u'
            | 'A' | 'E' | 'I' | 'O' | 'U' -> true
            |_ -> false
        //Calling the inner function to determine whether the initial letter of the
        //word that is passed into the outer function is a vowel
        if isVowel word.[0] then 
            //..if so, add "yay" to the end of the word
            word + "yay" 
        else
            //..if not, move the initial to the end of the word 
            //and add "ay" at the end of it
            word.[1..] + string(word.[0]) + "ay"

[<EntryPoint>]
let main argv =
    for name in argv do
        let newName = PigLatin.toPigLatin name
        printfn "%s in Pig Latin is %s" name newName

    0    



