using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bingo
{
    public class ThingToReadTheFile
    {
        private IFileSystem _fileSystem;

        public ThingToReadTheFile(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IEnumerable<int> ReadSequenceOfNumbers()
        {
            var firstLine = _fileSystem.File.ReadLines("input.txt").First();

            return firstLine.Split(",").Select(numberAsString => Convert.ToInt32(numberAsString));
        }

        public IEnumerable<int[,]> ReadTheBoards()
        {
            var rows = _fileSystem.File
                .ReadLines("input.txt")
                .Where(line => !string.IsNullOrWhiteSpace(line)).Skip(1)
                .Select(line => Regex.Split(line.Trim(), @"\s+").Select(numberOnBoardAsString => Convert.ToInt32(numberOnBoardAsString)));

            var rowEnumerator = rows.GetEnumerator();
            var board = new int[5, 5];
            for (var rowIndex = 0; rowEnumerator.MoveNext(); rowIndex++)
            {
                rowIndex %= 5;
                var columnEnumerator = rowEnumerator.Current.GetEnumerator();
                for (var columnIndex = 0; columnEnumerator.MoveNext(); columnIndex++)
                {
                    board[rowIndex, columnIndex] = columnEnumerator.Current;
                }

                if(rowIndex == 4)
                {
                    yield return board;
                    board = new int[5, 5];
                }
            }
        }
    }
}