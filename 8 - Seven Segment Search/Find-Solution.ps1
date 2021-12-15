function Find-Solution() {
    . .\Find-UniqueDigitOccurrencesInLine.ps1
    $cnt = 0
    foreach ($line in (Get-Content .\input.txt)) {
        $cnt += Find-UniqueDigitOccurrencesInLine -InputLine $line
    }
    $cnt
}