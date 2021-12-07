using System;
using System.IO.Abstractions;
using System.Linq;

namespace Bingo
{
    class Program
    {
        static void Main(string[] args)
        {
            var thing = new ThingToReadTheFile(new FileSystem());

            var sequenceOfNumbers = thing.ReadSequenceOfNumbers();

            var boards = thing.ReadTheBoards()
                .Select(boardFromFile => new BingoBoard(boardFromFile)).ToList();

            var alreadyReportedFirstWinner = false;

            foreach (var numberToReceive in sequenceOfNumbers)
            {
                foreach(var board in boards)
                {
                    var boardsRemainingToBeWon = boards.Where(board => !board.IsWinning);
                    BingoBoard lastWinningBoard = null;
                    if (boardsRemainingToBeWon.Count() == 1)
                    {
                        lastWinningBoard = boardsRemainingToBeWon.Single();
                    }

                    board.ReceiveNumber(numberToReceive);

                    if (lastWinningBoard?.IsWinning ?? false)
                    {
                        Console.WriteLine($"Part 2 - Last winner on board with Number={numberToReceive} Sum={lastWinningBoard.SumOfUnmarkedNumbers} Board Score={numberToReceive * lastWinningBoard.SumOfUnmarkedNumbers}");
                    }

                    if(board.IsWinning && !alreadyReportedFirstWinner)
                    {
                        Console.WriteLine($"Part 1 - First winner on board with Number={numberToReceive} Sum={board.SumOfUnmarkedNumbers} Board Score={numberToReceive * board.SumOfUnmarkedNumbers}");
                        alreadyReportedFirstWinner = true;
                    }

                }
            }

        }
    }
}
