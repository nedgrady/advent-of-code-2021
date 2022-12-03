using System.Collections.Generic;
using System.Linq;

namespace Bingo
{
    internal class BingoBoard
    {
        private IEnumerable<BingoSquare> AllSquares => _board.SelectMany(row => row);
        private readonly BingoSquare[][] _board;
        public BingoBoard(int[,] undelyingNumbers)
        {
            _board = new BingoSquare[5][];

            for(int rowIndex =0; rowIndex < 5; rowIndex++)
            {
                _board[rowIndex] = new BingoSquare[5];
                _board[rowIndex][0] = new BingoSquare(undelyingNumbers[rowIndex, 0]);
                _board[rowIndex][1] = new BingoSquare(undelyingNumbers[rowIndex, 1]);
                _board[rowIndex][2] = new BingoSquare(undelyingNumbers[rowIndex, 2]);
                _board[rowIndex][3] = new BingoSquare(undelyingNumbers[rowIndex, 3]);
                _board[rowIndex][4] = new BingoSquare(undelyingNumbers[rowIndex, 4]);
            }
        }

        public bool IsWinning
        {
            get
            {
                for(var columnIndex = 0; columnIndex < 5; columnIndex++)
                {
                    if (_board[0][columnIndex].HasBeenMarked
                        && _board[1][columnIndex].HasBeenMarked
                        && _board[2][columnIndex].HasBeenMarked
                        && _board[3][columnIndex].HasBeenMarked
                        && _board[4][columnIndex].HasBeenMarked)
                        return true;
                }

                return _board.Any(row => row.All(square => square.HasBeenMarked));
            }
        }

        public int SumOfUnmarkedNumbers => AllSquares.Sum(square => square.HasBeenMarked ? 0 : square.Number);

        internal void ReceiveNumber(int receivedNumber)
        {
            var matchedNumbers = AllSquares.Where(square => square.Number == receivedNumber);

            if (matchedNumbers.Any())
            {
                var matchedNUmber = matchedNumbers.Single();
                matchedNUmber.HasBeenMarked = true;
            }
        }

        class BingoSquare
        {
            public int Number {get;set;}
            public bool HasBeenMarked { get; set; }

            public BingoSquare(int underlyingNumber)
            {
                HasBeenMarked = false;
                Number = underlyingNumber;
            }
        }
    }
}
