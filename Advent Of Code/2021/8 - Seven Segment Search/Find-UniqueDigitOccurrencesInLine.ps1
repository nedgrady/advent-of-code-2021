function Find-UniqueDigitOccurrencesInLine ([string] $inputLine) {
    $occurences = ($inputLine  -Replace ".*\|", "").Trim() -split " " | Where-Object {@(2,3,4,7).contains($_.Length)}
    $occurences.Count
}
