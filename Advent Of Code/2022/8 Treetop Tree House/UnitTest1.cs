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

            var treeData = new int[width, height];

            var treeGraph = new SquareGraphNode<int>[width, height];

            for (int rowIndex = 0; rowIndex < lines.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < lines[rowIndex].Length; columnIndex++)
                {
                    var treeHeight = int.Parse(lines[rowIndex][columnIndex].ToString());
                    treeData[rowIndex, columnIndex] = height;
                    treeGraph[rowIndex, columnIndex] = new SquareGraphNode<int>(treeGraph, rowIndex, columnIndex, treeHeight);
                }
            }

            var visibleTreesCount = treeGraph
                .Cast<SquareGraphNode<int>>()
                .Count(tree => tree.IsVisibleInAnyDirection());
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

        public SquareGraphNode<TNode> Left => _xCoordinate == 0 ? null : _treeData[_xCoordinate - 1, _yCoordinate];
        public SquareGraphNode<TNode> Up => _yCoordinate == 0 ? null : _treeData[_xCoordinate, _yCoordinate - 1];
        public SquareGraphNode<TNode> Right => _xCoordinate == _treeData.GetLength(1) - 1 ? null : _treeData[_xCoordinate + 1, _yCoordinate];
        public SquareGraphNode<TNode> Down => _yCoordinate == _treeData.GetLength(0) - 1 ? null : _treeData[_xCoordinate, _yCoordinate + 1];

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
    }

}