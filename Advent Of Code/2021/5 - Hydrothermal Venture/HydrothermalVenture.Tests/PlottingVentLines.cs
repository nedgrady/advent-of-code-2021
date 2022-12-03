using FluentAssertions;
using System.Drawing;
using Xunit;

namespace HydrothermalVenture.Tests
{
    public class PlottingVentLines
    {
        [Fact]
        public void ComponentCoordinatesOfAVentLineGoingVerticallyAreCorrect()
        {
            var ventLineUnderTest = new VentLine(new Point(0, 0), new Point(0, 5));

            ventLineUnderTest.ComponentCoordinates.Should().BeEquivalentTo(new[]
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(0, 2),
                new Point(0, 3),
                new Point(0, 4),
                new Point(0, 5)
            });
        }

        [Fact]
        public void ComponentCoordinatesOfAVentLineGoingHoriontallyAreCorrect()
        {
            var ventLineUnderTest = new VentLine(new Point(6, 2), new Point(1, 2));

            ventLineUnderTest.ComponentCoordinates.Should().BeEquivalentTo(new[]
            {
                new Point(6, 2),
                new Point(5, 2),
                new Point(4, 2),
                new Point(3, 2),
                new Point(2, 2),
                new Point(1, 2)
            });
        }

        [Fact]
        public void ComponentCoordinatesOfAVentLineGoingDiagonallyAreCorrect()
        {
            var ventLineUnderTest = new VentLine(new Point(3, 3), new Point(7, 7));

            ventLineUnderTest.ComponentCoordinates.Should().BeEquivalentTo(new[]
            {
                new Point(3, 3),
                new Point(4, 4),
                new Point(5, 5),
                new Point(6, 6),
                new Point(7, 7)
            });
        }

        [Fact]
        public void ComponentCoordinatesOfAVentLineGoingDiagonallyAreCorrectWhereCoordinatesAreNotSame()
        {
            var ventLineUnderTest = new VentLine(new Point(15, 10), new Point(10, 5));

            ventLineUnderTest.ComponentCoordinates.Should().BeEquivalentTo(new[]
            {
                new Point(15, 10),
                new Point(14, 9),
                new Point(13, 8),
                new Point(12, 7),
                new Point(11, 6),
                new Point(10, 5)
            });
        }

        [Fact]
        public void ComponentCoordinatesOfAVentLineGoingDiagonallyAreCorrectWhereCoordinatesAreNotSameTrickyExample()
        {
            var ventLineUnderTest = new VentLine(new Point(8, 0), new Point(0, 8));

            ventLineUnderTest.ComponentCoordinates.Should().BeEquivalentTo(new[]
            {
                new Point(0, 8),
                new Point(1, 7),
                new Point(2, 6),
                new Point(3, 5),
                new Point(4, 4),
                new Point(5, 3),
                new Point(6, 2),
                new Point(7, 1),
                new Point(8, 0)
            });
        }
    }
}
