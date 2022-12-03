using System.Collections.Generic;
using System.Drawing;
using System.IO.Abstractions;
using System.Linq;
using System.Text.RegularExpressions;

namespace HydrothermalVenture
{
    public class ThingToReadTheFile
    {
        private readonly IFileSystem _fileSystem;

        public ThingToReadTheFile(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IEnumerable<(Point start, Point end)> ReadSequenceOfVentLines()
        {
            var fileLines = _fileSystem.File.ReadLines("input.txt");

            foreach (var fileLine in fileLines)
            {
                var fourBitsOfTheVentLine =
                    Regex.Split(fileLine, ",|->")
                        .Select(numberOfPoint => numberOfPoint.Trim())
                        .Select(int.Parse)
                        .ToArray();

                yield return (new Point(fourBitsOfTheVentLine[0], fourBitsOfTheVentLine[1]),
                    new Point(fourBitsOfTheVentLine[2], fourBitsOfTheVentLine[3]));
            }
        }
    }
}
