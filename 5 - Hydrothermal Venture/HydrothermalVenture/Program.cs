using System;
using System.IO.Abstractions;
using System.Linq;

namespace HydrothermalVenture
{
    class Program
    {
        static void Main(string[] args)
        {
            var thingToReadFiles = new ThingToReadTheFile(new FileSystem());

            var ventLines =
                thingToReadFiles
                    .ReadSequenceOfVentLines()
                    // Remove this line for part 2
                    .Where(points => points.end.X == points.start.X || points.end.Y == points.start.Y)
                    .Select(lineFromFile => new VentLine(lineFromFile.start, lineFromFile.end));

            var allCoordinates = ventLines.SelectMany(ventLine => ventLine.ComponentCoordinates);

            var countOfDangerousPoints = allCoordinates
                .GroupBy(coordinate => coordinate)
                .Where(group => group.Count() >= 2)
                .Count();


            Console.WriteLine($"Found {countOfDangerousPoints} dangerous points.");
        }
    }
}
