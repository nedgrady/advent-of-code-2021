using NUnit.Framework;
using System.IO;
using System.Linq;
using Core;
using System.Collections.Generic;

namespace _8_Treetop_Tree_House
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var lines = File.ReadAllLines("input.txt");
            var width = lines.First().Length;
            var height = lines.Length;

            var treeGraph = new SquareGraphNode<int>[width, height];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    var treeHeight = int.Parse(lines[y][x].ToString());
                    treeGraph[x, y] = new SquareGraphNode<int>(treeGraph, x, y, treeHeight);
                }
            }

            var visibleTreesCount = treeGraph
                .Cast<SquareGraphNode<int>>()
                .Count(tree => tree.IsVisibleInAnyDirection());

            var bestScenicScore = treeGraph
                .Cast<SquareGraphNode<int>>()
                .Max(tree => tree.ScenicScore());
        }
    }

    class SquareGraphNode<TNode>
    {
        private readonly SquareGraphNode<TNode>[,] _treeData;
        private readonly int _xCoordinate;
        private readonly int _yCoordinate;

        public SquareGraphNode(SquareGraphNode<TNode>[,] treeData, int xCoordinate, int yCoordinate, int value)
        {
            this._treeData = treeData;
            this._xCoordinate = xCoordinate;
            this._yCoordinate = yCoordinate;
            Value = value;
        }

        public SquareGraphNode<TNode>? Left => _xCoordinate == 0 ? null : _treeData[_xCoordinate - 1, _yCoordinate];
        public SquareGraphNode<TNode>? Up => _yCoordinate == 0 ? null : _treeData[_xCoordinate, _yCoordinate - 1];
        public SquareGraphNode<TNode>? Right => _xCoordinate == _treeData.GetLength(1) - 1 ? null : _treeData[_xCoordinate + 1, _yCoordinate];
        public SquareGraphNode<TNode>? Down => _yCoordinate == _treeData.GetLength(0) - 1 ? null : _treeData[_xCoordinate, _yCoordinate + 1];

        public IEnumerable<SquareGraphNode<TNode>> AllLeft
        {
            get
            {
                if(Left == null)
                    return Enumerable.Empty<SquareGraphNode<TNode>>();

                return Left.AsSingletonEnumerable().Concat(Left.AllLeft);
            }
        }

        public IEnumerable<SquareGraphNode<TNode>> AllRight
        {
            get
            {
                if (Right == null)
                    return Enumerable.Empty<SquareGraphNode<TNode>>();

                return Right.AsSingletonEnumerable().Concat(Right.AllRight);
            }
        }

        public IEnumerable<SquareGraphNode<TNode>> AllUp
        {
            get
            {
                if (Up == null)
                    return Enumerable.Empty<SquareGraphNode<TNode>>();

                return Up.AsSingletonEnumerable().Concat(Up.AllUp);
            }
        }

        public IEnumerable<SquareGraphNode<TNode>> AllDown
        {
            get
            {
                if (Down == null)
                    return Enumerable.Empty<SquareGraphNode<TNode>>();

                return Down.AsSingletonEnumerable().Concat(Down.AllDown);
            }
        }

        public int Value { get; private set; }

        public override string ToString()
        {
            return $"({_xCoordinate}, {_yCoordinate}) {Value}";
        }
    }

    static class SquareGraphNodeExtensions
    {
        public static bool IsVisibleInAnyDirection(this SquareGraphNode<int> squareGraphNode)
        {
            if (squareGraphNode.Left == null || squareGraphNode.Right == null || squareGraphNode.Up == null || squareGraphNode.Down == null)
                return true;

            return !(squareGraphNode.AllLeft.Any(tree => tree.Value >= squareGraphNode.Value) &&
                squareGraphNode.AllRight.Any(tree => tree.Value >= squareGraphNode.Value) &&
                squareGraphNode.AllDown.Any(tree => tree.Value >= squareGraphNode.Value) &&
                squareGraphNode.AllUp.Any(tree => tree.Value >= squareGraphNode.Value));
        }

        public static int ScenicScore(this SquareGraphNode<int> squareGraphNode)
        {
            return squareGraphNode.VisibleDown() * squareGraphNode.VisibleLeft() * squareGraphNode.VisibleRight() * squareGraphNode.VisibleUp();
        }

        public static int VisibleLeft(this SquareGraphNode<int> squareGraphNode)
        {
            if (squareGraphNode.Left == null)
                return 0;

            // Always one visible if we aren't on the edge
            int visibletrees = 0;

            foreach (var tree in squareGraphNode.AllLeft)
            {
                visibletrees++;
                if (squareGraphNode.Value <= tree.Value)
                    break;                
            }

            return visibletrees;
        }

        public static int VisibleRight(this SquareGraphNode<int> squareGraphNode)
        {
            if (squareGraphNode.Right == null)
                return 0;

            // Always one visible if we aren't on the edge
            int visibletrees = 0;

            foreach (var tree in squareGraphNode.AllRight)
            {
                visibletrees++;
                if (squareGraphNode.Value <= tree.Value)
                    break;
            }

            return visibletrees;
        }

        public static int VisibleUp(this SquareGraphNode<int> squareGraphNode)
        {
            if (squareGraphNode.Up == null)
                return 0;

            // Always one visible if we aren't on the edge
            int visibletrees = 0;

            foreach (var tree in squareGraphNode.AllUp)
            {
                visibletrees++;
                if (squareGraphNode.Value <= tree.Value)
                    break;
            }

            return visibletrees;
        }

        public static int VisibleDown(this SquareGraphNode<int> squareGraphNode)
        {
            if (squareGraphNode.Down == null)
                return 0;

            // Always one visible if we aren't on the edge
            int visibletrees = 0;

            foreach (var tree in squareGraphNode.AllDown)
            {
                visibletrees++;
                if (squareGraphNode.Value <= tree.Value)
                    break;
            }

            return visibletrees;
        }
    }
}