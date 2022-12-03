using System;
using System.Collections.Generic;
using System.Drawing;

namespace HydrothermalVenture
{
    public class VentLine
    {
        private Point _start;
        private Point _end;

        public VentLine(Point start, Point end)
        {
            _start = start;
            _end = end;
        }

        public IEnumerable<Point> ComponentCoordinates
        {
            get
            {
                var xDirectionIncrement = _start.X > _end.X ? -1 : 1;
                var yDirectionIncrement = _start.Y > _end.Y ? -1 : 1;


                yield return _start;
                var currentPoint = _start;
                while (currentPoint != _end)
                {
                    if (currentPoint.X != _end.X)
                        currentPoint.X += xDirectionIncrement;

                    if (currentPoint.Y != _end.Y)
                        currentPoint.Y += yDirectionIncrement;

                    yield return currentPoint;
                }
            }
        }

    }
}
