using FluentAssertions;
using NUnit.Framework;
using System;
using System.Text;

namespace Bingo.Tests
{
    class RespondingToDrawnNumbers
    {
        [Test]
        public void OneNumberKeepsWinningFalse()
        {
            var undelyingNumbers = new int[,]
            {
                {14, 21, 17, 24,  4 },
                {10, 16, 15,  9, 19 },
                {18,  8, 23, 26, 20 },
                {22, 11, 13,  6,  5 },
                { 2,  0, 12,  3,  7 }
            };

            BingoBoard boardUnderTest = new BingoBoard(undelyingNumbers);

            boardUnderTest.ReceiveNumber(14);

            boardUnderTest.IsWinning.Should().Be(false);
        }

        [Test]
        public void FourNumbersOfARowKeepsWinningFalse()
        {
            var undelyingNumbers = new int[,]
            {
                {14, 21, 17, 24,  4 },
                {10, 16, 15,  9, 19 },
                {18,  8, 23, 26, 20 },
                {22, 11, 13,  6,  5 },
                { 2,  0, 12,  3,  7 }
            };

            BingoBoard boardUnderTest = new BingoBoard(undelyingNumbers);

            boardUnderTest.ReceiveNumber(14);
            boardUnderTest.ReceiveNumber(21);
            boardUnderTest.ReceiveNumber(17);
            boardUnderTest.ReceiveNumber(24);

            boardUnderTest.IsWinning.Should().Be(false);
        }

        [Test]
        public void FiveNumbersOfARowFlipsWinningToTrue()
        {
            var undelyingNumbers = new int[,]
            {
                {14, 21, 17, 24,  4 },
                {10, 16, 15,  9, 19 },
                {18,  8, 23, 26, 20 },
                {22, 11, 13,  6,  5 },
                { 2,  0, 12,  3,  7 }
            };

            BingoBoard boardUnderTest = new BingoBoard(undelyingNumbers);

            boardUnderTest.ReceiveNumber(14);
            boardUnderTest.IsWinning.Should().Be(false);
            boardUnderTest.ReceiveNumber(21);
            boardUnderTest.IsWinning.Should().Be(false);
            boardUnderTest.ReceiveNumber(17);
            boardUnderTest.IsWinning.Should().Be(false);
            boardUnderTest.ReceiveNumber(24);
            boardUnderTest.IsWinning.Should().Be(false);
            boardUnderTest.ReceiveNumber(4);

            boardUnderTest.IsWinning.Should().Be(true);
        }

        [Test]
        public void FiveNumbersOfAColumnFlipsWinningToTrue()
        {
            var undelyingNumbers = new int[,]
            {
                {14, 21, 17, 24,  4 },
                {10, 16, 15,  9, 19 },
                {18,  8, 23, 26, 20 },
                {22, 11, 13,  6,  5 },
                { 2,  0, 12,  3,  7 }
            };

            BingoBoard boardUnderTest = new BingoBoard(undelyingNumbers);

            boardUnderTest.ReceiveNumber(4);
            boardUnderTest.IsWinning.Should().Be(false);
            boardUnderTest.ReceiveNumber(19);
            boardUnderTest.IsWinning.Should().Be(false);
            boardUnderTest.ReceiveNumber(20);
            boardUnderTest.IsWinning.Should().Be(false);
            boardUnderTest.ReceiveNumber(5);
            boardUnderTest.IsWinning.Should().Be(false);
            boardUnderTest.ReceiveNumber(7);

            boardUnderTest.IsWinning.Should().Be(true);
        }

        [Test]
        public void LotsOfJunkNumbersKeepWinningFalse()
        {
            var undelyingNumbers = new int[,]
            {
                {14, 21, 17, 24,  4 },
                {10, 16, 15,  9, 19 },
                {18,  8, 23, 26, 20 },
                {22, 11, 13,  6,  5 },
                { 2,  0, 12,  3,  7 }
            };

            BingoBoard boardUnderTest = new BingoBoard(undelyingNumbers);

            boardUnderTest.ReceiveNumber(4);
            boardUnderTest.ReceiveNumber(7);
            boardUnderTest.ReceiveNumber(2);
            boardUnderTest.ReceiveNumber(14);
            boardUnderTest.ReceiveNumber(999);
            boardUnderTest.ReceiveNumber(565);
            boardUnderTest.ReceiveNumber(88888);
            boardUnderTest.ReceiveNumber(23);


            boardUnderTest.IsWinning.Should().Be(false);
        }

        [Test]
        public void SumOfUnmarkedNumbersIsInitiallyTheTotalSum()
        {
            var undelyingNumbers = new int[,]
            {
                {14, 21, 17, 24,  4 },
                {10, 16, 15,  9, 19 },
                {18,  8, 23, 26, 20 },
                {22, 11, 13,  6,  5 },
                { 2,  0, 12,  3,  7 }
            };

            BingoBoard boardUnderTest = new BingoBoard(undelyingNumbers);

            boardUnderTest.SumOfUnmarkedNumbers.Should().Be(325);
        }

        [Test]
        public void ReceivingNumbersUpdatesTheSumOfUnmarkedNumbers()
        {
            var undelyingNumbers = new int[,]
            {
                {14, 21, 17, 24,  4 },
                {10, 16, 15,  9, 19 },
                {18,  8, 23, 26, 20 },
                {22, 11, 13,  6,  5 },
                { 2,  0, 12,  3,  7 }
            };

            BingoBoard boardUnderTest = new BingoBoard(undelyingNumbers);

            boardUnderTest.ReceiveNumber(4);
            boardUnderTest.ReceiveNumber(7);
            boardUnderTest.ReceiveNumber(2);
            boardUnderTest.ReceiveNumber(14);
            boardUnderTest.ReceiveNumber(999);
            boardUnderTest.ReceiveNumber(565);
            boardUnderTest.ReceiveNumber(88888);
            boardUnderTest.ReceiveNumber(23);


            boardUnderTest.SumOfUnmarkedNumbers.Should().Be(325 - 4 - 7 - 2 - 14 - 23);
        }
    }
}
