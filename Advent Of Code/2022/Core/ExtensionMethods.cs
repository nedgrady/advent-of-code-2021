namespace Core
{
    public static class ExtensionMethods
    {
        public static T[][] ToJaggedArray<T>(this T[,] twoDimensionalArray)
        {
            int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
            int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
            int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
                }
            }
            return jaggedArray;
        }

        public static IEnumerable<T> AsSingletonEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static bool InclusivelyBetween(this int testee, int boundAlpha, int boundbravo)
        {
            return testee < Math.Max(boundAlpha, boundbravo) && testee > Math.Min(boundAlpha, boundbravo);
        }

        public static bool InclusivelyBetween(this char testee, char boundAlpha, char boundbravo)
        {
            return (testee <= Math.Max(boundAlpha, boundbravo)) && (testee >= Math.Min(boundAlpha, boundbravo));
        }
    }
}