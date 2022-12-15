using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _9_Rope_Bridge.Tests
{
    public class Tests
    {
        public static readonly TestCase[] TestCases =
        {
            // Up Down
            new((0, 1), (0, 0), 1, "U 1"),
            new((0, 2),(0, 1), 2, "U 1", "U 1"),
            new((0, 4),(0, 3), 4, "U 4"),
            new((0, 3),(0, 3), 4, "U 4", "D 1"),
            new((0, 2),(0, 3), 4, "U 4", "D 2"),
            new((0, 10),(0, 9), 10, "U 4", "D 4", "U 10"),

            // Right Left
            new((1, 0), (0, 0), 1, "R 1"),
            new((2, 0),(1 , 0), 2, "R 1", "R 1"),
            new((4, 0),(3 , 0), 4, "R 4"),
            new((3 , 0),(3 , 0), 4, "R 4", "L 1"),
            new((2 , 0),(3 , 0), 4, "R 4", "L 2"),
            new((10 , 0),(9 , 0), 10, "R 4", "L 4", "R 10"),

            // Up Right
            new((1, 1), (0, 0), 1, "U 1", "R 1"),
            new((2, 4), (1, 4), 5, "U 4", "R 2"),
            new((4, 2), (4, 1), 5, "R 4", "U 2"),

            // Up Left
            new((-1, 1), (0, 0), 1, "U 1", "L 1"),
            new((-2, 4), (-1, 4), 5, "U 4", "L 2"),
            new((-4, 2), (-4, 1), 5, "L 4", "U 2"),

            // Down Right
            new((1, -1), (0, 0), 1, "D 1", "R 1"),
            new((2, -4), (1, -4), 5, "D 4", "R 2"),
            new((4, -2), (4, -1), 5, "R 4", "D 2"),

            // Down Left
            new((-1, -1), (0, 0), 1, "D 1", "L 1"),
            new((-2, -4), (-1, -4), 5, "D 4", "L 2"),
            new((-4, -2), (-4, -1), 5, "L 4", "D 2"),

            // 
            new ((-4, -1), (-4, -2), 9, "D 4", "L 4", "U 3"),

            // Example from website
            new((5,3),(4,3),10,"R 4","U 4","L 3","D 1", "R 4"),

            new((-1,0),(0,0),1,"L 1","D 1","U 1"),
            new((1,0),(0,0),1,"R 1","D 1","U 1"),

            new((0, 1),(0,0),1, "L 1", "U 1", "R 1"),
            new((0, 1),(0,0),1, "R 1", "U 1", "L 1"),

            new((1, 0),(0,0),1, "U 1", "R 1", "D 1"),
            new((-1, 0),(0,0),1, "U 1", "L 1", "D 1"),


            new((0, 2),(1,2),13,"R 4","U 4","L 3","D 1", "R 4", "D 1", "L 5")//,"R 2"
        };



        [TestCaseSource(nameof(TestCases))]
        public void FinalHeadPos(TestCase testCase)
        {
            TailTrackingInstructionVisitor visitor = new();

            var finalCoords = visitor.Visit(testCase.Instructions);

            finalCoords.head.Should().Be(testCase.FinalHeadPosition);
        }

        [TestCaseSource(nameof(TestCases))]
        public void FinalTailPos(TestCase testCase)
        {
            TailTrackingInstructionVisitor visitor = new();
            var finalCoords = visitor.Visit(testCase.Instructions);
            finalCoords.tail.Should().Be(testCase.FinalTailPosition);
        }


        [TestCaseSource(nameof(TestCases))]
        public void TailVisitedCount(TestCase testCase)
        {
            TailTrackingInstructionVisitor visitor = new();
            var finalCoords = visitor.Visit(testCase.Instructions);
            visitor.TotalTailVisits.Should().Be(testCase.ExpectedTailVisits);
        }


        public record TestCase
        {
            public TestCase((int x, int y) finalHeadPosition, (int x, int y) finalTailPosition, int expectedTailVisits, params string[] instructions)
            {
                FinalHeadPosition = finalHeadPosition.Pos();
                FinalTailPosition = finalTailPosition.Pos();
                ExpectedTailVisits = expectedTailVisits;
                Instructions = instructions;
            }

            public Position FinalHeadPosition { get; }
            public Position FinalTailPosition { get; }
            public int ExpectedTailVisits { get; }
            public string[] Instructions { get; }

            public override string ToString()
            {
                return string.Join(" ", Instructions);
            }
        }
    }
}