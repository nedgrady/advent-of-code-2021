BeforeAll {
    . .\Find-UniqueDigitOccurrencesInLine.ps1
}

Describe 'Count-UniqueDigitOccurrencesInLine' {
    It "For the input string '<inputLine>' returns '<uniqueDigitOccurrences>'" -ForEach @(
        @{ InputLine = "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe"; UniqueDigitOccurrences = 2 }
        @{ InputLine = "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb"; UniqueDigitOccurrences = 4 }
        @{ InputLine = "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef"; UniqueDigitOccurrences = 1 }
    ) {
        $cnt = Find-UniqueDigitOccurrencesInLine -InputLine $inputLine
        $cnt | Should -Be $uniqueDigitOccurrences
    }
}
