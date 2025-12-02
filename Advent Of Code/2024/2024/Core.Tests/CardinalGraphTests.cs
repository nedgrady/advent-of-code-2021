using FluentAssertions;
using MathNet.Numerics.LinearAlgebra;


namespace Core.Tests
{
    public class CardinalGraphTests
    {

        private readonly CardinalGraph<int> _graphUnderTest = new(
            new int[3, 4]
            {
               { 00, 01, 02, 03 },
               { 04, 05, 06, 07 },
               { 08, 09, 10, 11 }
            });


        [Test]
        public void WestToEast()
        {
            var westToEast = _graphUnderTest.WestToEast.GetEnumerator();
            westToEast.MoveNext();
            westToEast.Current.Should().BeEquivalentTo(new int[] { 00, 01, 02, 03 });

            westToEast.MoveNext();
            westToEast.Current.Should().BeEquivalentTo(new int[] { 04, 05, 06, 07 });

            westToEast.MoveNext();
            westToEast.Current.Should().BeEquivalentTo(new int[] { 08, 09, 10, 11 });

            westToEast.MoveNext().Should().BeFalse();
        }

        [Test]
        public void EastToWest()
        {
            var eastToWest = _graphUnderTest.WestToEast.GetEnumerator();
            eastToWest.MoveNext();
            eastToWest.Current.Should().BeEquivalentTo(new int[] { 03, 02, 01, 00 });

            eastToWest.MoveNext();
            eastToWest.Current.Should().BeEquivalentTo(new int[] { 07, 06, 05, 04 });

            eastToWest.MoveNext();
            eastToWest.Current.Should().BeEquivalentTo(new int[] {11, 10, 09, 08 });

            eastToWest.MoveNext().Should().BeFalse();
        }


        [Test]
        public void NorthToSouth()
        {
            var northToSouth = _graphUnderTest.NorthToSouth.GetEnumerator();
            northToSouth.MoveNext();
            northToSouth.Current.Should().BeEquivalentTo(new int[] { 0, 4, 8 });

            northToSouth.MoveNext();
            northToSouth.Current.Should().BeEquivalentTo(new int[] { 1, 5, 9 });

            northToSouth.MoveNext();
            northToSouth.Current.Should().BeEquivalentTo(new int[] { 2, 6, 10 });

            northToSouth.MoveNext();
            northToSouth.Current.Should().BeEquivalentTo(new int[] { 3, 7, 11 });

            northToSouth.MoveNext().Should().BeFalse();
        }


        //[Test]
        //public void NorthWestToSouthEastDiagonals()
        //{
        //    var northToSouth = _graphUnderTest.NorthWestToSouthEastDiagonals.GetEnumerator();
        //    northToSouth.MoveNext();
        //    northToSouth.Current.Should().BeEquivalentTo(new int[] { 08 });

        //    northToSouth.MoveNext();
        //    northToSouth.Current.Should().BeEquivalentTo(new int[] { 04, 09 });

        //    northToSouth.MoveNext();
        //    northToSouth.Current.Should().BeEquivalentTo(new int[] { 00, 05, 10 });

        //    northToSouth.MoveNext();
        //    northToSouth.Current.Should().BeEquivalentTo(new int[] { 01, 06, 11 });

        //    northToSouth.MoveNext();
        //    northToSouth.Current.Should().BeEquivalentTo(new int[] { 02, 07 });

        //    northToSouth.MoveNext();
        //    northToSouth.Current.Should().BeEquivalentTo(new int[] { 03 });

        //    northToSouth.MoveNext().Should().BeFalse();
        //}

        [Test]
        public void NorthEastToSouthWest()
        {
            var northToSouth = _graphUnderTest.NorthWestToSouthEastDiagonals.GetEnumerator();
            northToSouth.MoveNext();
            northToSouth.Current.Should().BeEquivalentTo(new int[] { 08 });

            northToSouth.MoveNext();
            northToSouth.Current.Should().BeEquivalentTo(new int[] { 04, 09 });

            northToSouth.MoveNext();
            northToSouth.Current.Should().BeEquivalentTo(new int[] { 00, 05, 10 });

            northToSouth.MoveNext();
            northToSouth.Current.Should().BeEquivalentTo(new int[] { 01, 06, 11 });

            northToSouth.MoveNext();
            northToSouth.Current.Should().BeEquivalentTo(new int[] { 02, 07 });

            northToSouth.MoveNext();
            northToSouth.Current.Should().BeEquivalentTo(new int[] { 03 });

            northToSouth.MoveNext().Should().BeFalse();
        }

        [Test]
        public void Thing()
        {

            var matrix = Matrix<double>.Build.DenseOfColumnArrays(
               new double[] { 00, 01, 02, 03 },
               new double[] { 04, 05, 06, 07 },
               new double[] { 08, 09, 10, 11 });

            var ds = GetDiagonals(matrix);

            foreach (var item in ds)
            {
                Console.WriteLine(string.Join(',', item));
            }

            foreach (var item in GetTopRightToBottomLeftDiagonals(matrix))
            {
                Console.WriteLine(string.Join(',', item));
            }
        }

        public static IEnumerable<double[]> GetDiagonals(Matrix<double> matrix)
        {
            int rows = matrix.RowCount;
            int cols = matrix.ColumnCount;

            // Top-left to bottom-right diagonals
            for (int k = 0; k < rows + cols - 1; k++)
            {
                var diagonal = new List<double>();

                for (int j = 0; j < cols; j++)
                {
                    int i = k - j;
                    if (i >= 0 && i < rows)
                    {
                        diagonal.Add(matrix[i, j]);
                    }
                }

                yield return diagonal.ToArray();
            }
        }
        public static IEnumerable<double[]> GetTopRightToBottomLeftDiagonals(Matrix<double> matrix)
        {
            int rows = matrix.RowCount;
            int cols = matrix.ColumnCount;

            // Top-right to bottom-left diagonals
            for (int k = 0; k < rows + cols - 1; k++)
            {
                var diagonal = new List<double>();

                for (int j = cols - 1; j >= 0; j--)
                {
                    int i = k - (cols - 1 - j);
                    if (i >= 0 && i < rows)
                    {
                        diagonal.Add(matrix[i, j]);
                    }
                }

                if (diagonal.Count > 0)
                    yield return diagonal.ToArray();
            }
        }
    }



}