using NUnit.Framework.Constraints;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace HistorianHisteria
{
    public class Tests2
    {

        [TestCase("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))", ExpectedResult = 161)]
        [TestCase("mul(100)asda897t", ExpectedResult = 0)]
        [TestCase("==o-'#mul(100,100)asda897t", ExpectedResult = 10000)]
        [TestCase("==o-'#mul(1,2)\n==o-'#mul(3,1)", ExpectedResult = 5)]
        [TestCase("", ExpectedResult = 0)]
        public int Test(string input)
        {
            return SumMuls(input);
        }


        [TestCase("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))", ExpectedResult = 161)]
        [TestCase("mul(100)asda897t", ExpectedResult = 0)]
        [TestCase("==o-'#mul(100,100)asda897t", ExpectedResult = 10000)]
        [TestCase("==o-'#mul(1,2)\n==o-'#mul(3,1)", ExpectedResult = 5)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))", ExpectedResult = 48)]
        [TestCase("don't()==o-'#mul(1,2)\n==o-'#mul(3,1)", ExpectedResult = 0)]
        [TestCase("don't()don't()don't()do()==o-'#mul(100,100)asda897txmul(2,4)", ExpectedResult = 10008)]
        [TestCase("do()do()do()don't()==o-'#mul(100,100)asda897txmul(2,4)", ExpectedResult = 0)]
        public int Test2(string input)
        {
            return SumMuls2(input);
        }

        [Test]
        public void Part1()
        {
            var input = File.ReadAllText("input3.txt");

            Console.WriteLine(SumMuls(input));
        }


        [Test]
        public void Part2()
        {
            var input = File.ReadAllText("input3.txt");

            Console.WriteLine(SumMuls2(input));
        }

        public int SumMuls(string input)
        {
            var oneToThreeDigits = "[0-9]{1,3}";
            var open = "\\(";
            var comma = "\\,";
            var close = "\\)";

            var regexString = "mul" + open + '(' + oneToThreeDigits + ')' + comma + '(' +  oneToThreeDigits + ')' + close;
            //var regex = new Regex(regexString);

            var matches = Regex.Matches(input, regexString);

            return matches.Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
        }

        public int SumMuls2(string input)
        {
            var oneToThreeDigits = "[0-9]{1,3}";
            var open = "\\(";
            var comma = "\\,";
            var close = "\\)";

            var mulRegex = "mul" + open + '(' + oneToThreeDigits + ')' + comma + '(' + oneToThreeDigits + ')' + close;

            var doRegex = "do" + open + close;
            var dontRegex = "don't" + open + close;
            var toggleRegex = '(' + doRegex + '|' + dontRegex + ')';

            var matches = Regex.Matches(input, mulRegex + '|' + toggleRegex);

            var coefiiciant = 1;
            var sum = 0;


            foreach(var match in matches.ToArray())
            {
                if (match.Value == "do()")
                {
                    coefiiciant = 1;
                    continue;
                }

                if (match.Value == "don't()")
                {
                    coefiiciant = 0;
                    continue;
                }

                if (match.Value.StartsWith("mul"))
                {
                    sum += (int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value) * coefiiciant);
                }
            }

            return sum;
        }
    }
}