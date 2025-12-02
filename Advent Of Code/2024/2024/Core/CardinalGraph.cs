using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CardinalGraph<TNode>
    {
        private readonly TNode[][] _nodes;
        private readonly TNode[,] _jagged;
        private readonly int _width;
        private readonly int _height;

        public CardinalGraph(TNode[,] nodes)
        {
            _nodes = nodes.ToJaggedArray();
            _width = nodes.GetLength(1);
            _height = nodes.GetLength(0);
            _jagged = (TNode[,])nodes.Clone();
        }

        public IEnumerable<IEnumerable<TNode>> WestToEast
        {
            get
            {
                foreach(var row in _nodes)
                {
                    yield return row;
                }
            }
        }

        public IEnumerable<IEnumerable<TNode>> EastToWest
        {
            get
            {
                foreach (var row in _nodes)
                {
                    yield return row.Reverse();
                }
            }
        }

        public IEnumerable<IEnumerable<TNode>> SouthToNorth
        {
            get
            {
                for (var currentColumnIndex = 0; currentColumnIndex < _width; currentColumnIndex++)
                {
                    var column = new List<TNode>();
                    for (var currentRowIndex = _height - 1; currentRowIndex >= 0; currentRowIndex--)
                    {
                        column.Add(_jagged[currentRowIndex, currentColumnIndex]);
                    }
                    yield return column;
                }
            }
        }

        public IEnumerable<IEnumerable<TNode>> NorthToSouth
        {
            get
            {
                for (var currentColumnIndex = 0; currentColumnIndex < _width; currentColumnIndex++)
                {
                    var column = new List<TNode>();
                    for (var currentRowIndex = 0; currentRowIndex < _height; currentRowIndex++)
                    {
                        column.Add(_jagged[currentRowIndex, currentColumnIndex]);
                    }
                    yield return column;
                }
            }
        }

        public IEnumerable<IEnumerable<TNode>> NorthWestToSouthEastDiagonals
        {
            get
            {
                for (var currentColumnIndex = 0; currentColumnIndex < _width; currentColumnIndex++)
                {
                    var column = new List<TNode>();
                    for (var currentRowIndex = 0; currentRowIndex < _height; currentRowIndex++)
                    {
                        column.Add(_jagged[currentRowIndex, currentColumnIndex]);
                    }
                    yield return column;
                }
            }
        }
    }
}
