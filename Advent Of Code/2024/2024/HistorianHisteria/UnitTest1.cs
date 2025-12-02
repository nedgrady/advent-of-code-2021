using NUnit.Framework.Constraints;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace HistorianHisteria
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var lists =
                File.ReadLines("input.txt");

            List<int> l1 = new();
            List<int> l2 = new();


            foreach (var line in lists)
            {
                var tokens = line.Split();
                int a = int.Parse(tokens[0]);
                int b = int.Parse(tokens[3]);

                l1.Add(a);
                l2.Add(b);
            }

            l1.Sort();
            l2.Sort();

            Console.WriteLine(Enumerable.Zip(l1, l2).Sum((pair) => Math.Abs(pair.First - pair.Second)));

            var similarityScore = l1.Sum(n1 => n1 * (l2.Where(n2 => n1 == n2).Count()));
            Console.WriteLine(similarityScore);
        }

        [Test]
        public void Test2() 
        {
            var reportLines =
                   File.ReadLines("input2.txt");

            int safeReportsCount = 0;
            int safeReportsWithDampenerCount = 0;
            foreach (var reportLine in reportLines)
            {
                var report = new Report(reportLine.Split().Select(int.Parse).ToArray());
                

                if (report.IsSafe) safeReportsCount++;

                if (report.IsSafeWithDampener()) safeReportsWithDampenerCount++;

            }

            Console.WriteLine(safeReportsCount);
            Console.WriteLine(safeReportsWithDampenerCount);
        }


    }

    public class Report
    {
        private readonly int[] _reportValues;
        private readonly bool _hasBeenDampened;

        public Report(int[] reportValues, bool hasBeenDampened = false)
        {
            _reportValues = reportValues;
            _hasBeenDampened = hasBeenDampened;
        }

        private Direction ReportDirection => _reportValues[0] > _reportValues[1] ? Direction.Down : Direction.Up;

        public bool IsSafe
        {
            get
            {
                var deltas = _reportValues.OverlappingPairs().Select((pair) => pair.second - pair.first);

                bool IsSafeUpDelta(int delta) =>  delta == 1 || delta == 2 || delta == 3;
                bool IsSafeDownDelta(int delta) =>  delta == -1 || delta == -2 || delta == -3;

                var targetSafeDeltaCounts = new[] { deltas.Count() };
                var safeUpDeltas = deltas.Count(IsSafeUpDelta);
                var safeDownDeltas = deltas.Count(IsSafeDownDelta);

                return targetSafeDeltaCounts.Contains(safeUpDeltas) || targetSafeDeltaCounts.Contains(safeDownDeltas);
            }
        }

        public bool IsSafeWithDampener()
        {
            if (IsSafe) return true;

            for (int i = 0; i < _reportValues.Length; i++)
            {
                var valuesToCheckWithRemoval = _reportValues.ToList();
                valuesToCheckWithRemoval.RemoveAt(i);

                if (new Report(valuesToCheckWithRemoval.ToArray()).IsSafe) return true;
            }

            return false;
        }
    }



    abstract public class Direction
    {
        public abstract bool IsPairSafe(int first, int next);


        protected Direction() { }

        public readonly static Direction Up = new Up();
        public readonly static Direction Down = new Down();
    }

    public class Down : Direction
    {
 

        public override bool IsPairSafe(int first, int next)
        {
            var allowedNext = new[] { first - 1, first -2, first - 3};
            return allowedNext.Contains(next);
        }
    }

    public class Up : Direction
    {        public override bool IsPairSafe(int first, int next)
        {
            var allowedNext = new[] { first + 1, first + 2, first + 3 };
            return allowedNext.Contains(next);
        }
    }


    public static class Extensions
    {
        public static IEnumerable<(T first, T second, int index)> OverlappingPairs<T>(this T[] values)
        {
            if (values.Length < 1) yield break;

            for (var i = 0;  i < values.Length - 1; i++)
            {
                yield return (values[i], values[i + 1], i);
            }
        }


    }

}