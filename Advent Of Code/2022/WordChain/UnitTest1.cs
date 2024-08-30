using QuikGraph;
using QuikGraph.Algorithms;
using FluentAssertions;
using System.Globalization;

namespace WordChain
{
    public class Tests
    {
        [Test]
        public void CreatesGraphWithCorrectVertexCount()
        {
            string[] sourceWords = new[] { "abc", "abd" };

            var createWordGraph = new CreateWordGraph(sourceWords);

            var wordGraph = createWordGraph.CreateGraph();

            wordGraph.Vertices.Should().HaveCount(2);
        }

        public static readonly string[] Words = new[] { "xxx", "xxy" };

        [TestCaseSource(nameof(Words))]
        public void CreatesGraphWithCorrectVertices(string wordInSourceWords)
        {
            var createWordGraph = new CreateWordGraph(Words);
            var wordGraph = createWordGraph.CreateGraph();

            wordGraph.Vertices.Should().Contain(wordInSourceWords);
        }

        public static readonly EdgeTestCase[] EdgeTestCases = new[] 
        {
            new EdgeTestCase()
            {
                SourceWords = {  "abc", "abd", "xxx"  },
                ExpectedTransitions =
                {
                    ("abc", "abd"),
                    ("abd", "abc"),
                }
            },
            new EdgeTestCase()
            {
                SourceWords = {  "zzz", "yyy", "xxx"  },
                ExpectedTransitions =
                {

                }
            },
            new EdgeTestCase()
            {
                SourceWords = {  "catting", "patting", "potting", "chicken", "lhicken"  },
                ExpectedTransitions =
                {
                    ("catting", "patting"),
                    ("patting", "catting"),
                    ("potting", "patting"),
                    ("patting", "potting"),
                    ("chicken", "lhicken"),
                    ("lhicken", "chicken")
                }
            }
        };

        [TestCaseSource(nameof(EdgeTestCases))]
        public void CreatesEdgesCorrectly(EdgeTestCase edgeTestCase)
        {
            var createWordGraph = new CreateWordGraph(edgeTestCase.SourceWords);
            var wordGraph = createWordGraph.CreateGraph();

            var receivedTransitions = edgeTestCase.ExpectedTransitions.Select(transition => new Edge<string>(transition.source, transition.target));

            wordGraph.Edges.Should().BeEquivalentTo(receivedTransitions);
        }

        [Test]
        public void SolvesWordChain()
        {
            WordChainTestCase testCase = new()
            {
                SourceWords =
                {
                    "aaa",
                    "baa",
                    "bba",
                    "bbb",
                    "caa",
                    "cca",
                    "ccc",
                    "ccb",
                    "cbb"
                },
                StartWord = "aaa",
                EndWord = "bbb",
                ExpectedPath =
                {
                    "aaa",
                    "baa",
                    "bba",
                    "bbb"
                }
            };

            var wordChainSolver = new WordChainSolver(testCase.SourceWords);

            var receivedShortestPath = wordChainSolver.ShortestPathBetween(testCase.StartWord, testCase.EndWord).Single();

            receivedShortestPath.Should().BeEquivalentTo(testCase.ExpectedPath);
        }
    }


    public class EdgeTestCase
    {
        public List<string> SourceWords { get; init; } = new List<string>();
        public List<(string source, string target)> ExpectedTransitions { get; init; } = new List<(string source, string target)>();
    }

    public class WordChainTestCase
    {
        public List<string> SourceWords { get; init; } = new List<string>();

        public string StartWord { get; init; }
        public string EndWord { get; init; }
        public List<string> ExpectedPath { get; init; } = new List<string>();
    }

    public static class StringExtensions
    {
        public static bool IsCloseTo(this string word, string otherWord)
        {
            if(word.Length != otherWord.Length) return false;

            var nonMatchingPairs = word.Zip(otherWord).Where(pair => pair.First != pair.Second).Count();

            return nonMatchingPairs == 1;
        }
    }
}