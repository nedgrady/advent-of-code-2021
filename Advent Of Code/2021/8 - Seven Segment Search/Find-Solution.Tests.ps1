BeforeAll {
    . .\Find-Solution.ps1
}

Describe 'Count-UniqueDigitOccurrencesInLine' {
    It "Returns the correct thing" {
        Mock Get-Content {
            $content = @(
                "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe"
                "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb"
                "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef")
            return $content
        } -ParameterFilter { $Path -like "*input.txt" }

        Find-Solution | Should -Be 7
    }
}

Describe 'Count-UniqueDigitOccurrencesInLine' {
    It "Returns the correct thing for this sample too" {
        Mock Get-Content {
            $content = @("fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb"
            "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe"
            "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef"
            "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb"
            "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce")
            return $content
        } -ParameterFilter { $Path -like "*input.txt" }

        Find-Solution | Should -Be 14
    }
}







