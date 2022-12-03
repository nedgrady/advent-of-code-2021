using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace HydrothermalVenture.Tests
{
    public class ReadingInputFromTheFile
    {
        [Fact]
        public void ReadsTheSequenceOfNumbers()
        {
            var mockFileContents =
@"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

            var mockFiles = new Dictionary<string, MockFileData>
            {
                ["input.txt"] = mockFileContents
            };

            IFileSystem mockFileSystem = new MockFileSystem(mockFiles);

            var thingToReadTheFile = new ThingToReadTheFile(mockFileSystem);

            IEnumerable<(Point start, Point end)> pointsReadFromFile = thingToReadTheFile.ReadSequenceOfVentLines();

            pointsReadFromFile.Should().BeEquivalentTo(new[]
            {
                (new Point(0,9), new Point(5,9)),
                (new Point(8,0), new Point(0,8)),
                (new Point(9,4), new Point(3,4)),
                (new Point(2,2), new Point(2,1)),
                (new Point(7,0), new Point(7,4)),
                (new Point(6,4), new Point(2,0)),
                (new Point(0,9), new Point(2,9)),
                (new Point(3,4), new Point(1,4)),
                (new Point(0,0), new Point(8,8)),
                (new Point(5,5), new Point(8,2))
            });

        }
    }
}
