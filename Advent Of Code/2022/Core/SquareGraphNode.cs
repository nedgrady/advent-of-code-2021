using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SquareGraphNode<TNode>
    {
        private readonly SquareGraphNode<TNode>[,] _treeData;
        private readonly int _xCoordinate;
        private readonly int _yCoordinate;

        public SquareGraphNode(SquareGraphNode<TNode>[,] treeData, int xCoordinate, int yCoordinate, TNode value)
        {
            this._treeData = treeData;
            this._xCoordinate = xCoordinate;
            this._yCoordinate = yCoordinate;
            Value = value;
        }

        public SquareGraphNode<TNode>? Left => _xCoordinate == 0 ? null : _treeData[_xCoordinate - 1, _yCoordinate];
        public SquareGraphNode<TNode>? Up => _yCoordinate == 0 ? null : _treeData[_xCoordinate, _yCoordinate - 1];
        public SquareGraphNode<TNode>? Right => _xCoordinate == _treeData.GetLength(0) - 1 ? null : _treeData[_xCoordinate + 1, _yCoordinate];
        public SquareGraphNode<TNode>? Down => _yCoordinate == _treeData.GetLength(1) - 1 ? null : _treeData[_xCoordinate, _yCoordinate + 1];

        public IEnumerable<SquareGraphNode<TNode>> Neighbours
        {
            get
            {
                if (Up != null)
                    yield return Up;
                if (Right != null)
                    yield return Right;
                if (Down != null)
                    yield return Down;
                if (Left != null)
                    yield return Left;
            }
        }

        public IEnumerable<SquareGraphNode<TNode>> AllLeft
        {
            get
            {
                if (Left == null)
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

        public TNode Value { get; private set; }

        public override string ToString()
        {
            return $"({_xCoordinate}, {_yCoordinate}) {Value}";
        }
    }
}
