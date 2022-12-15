// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var instructions = File.ReadAllLines("input.txt");

TailTrackingInstructionVisitor tailTrackingInstructionVisitor = new();

tailTrackingInstructionVisitor.Visit(instructions);

Console.WriteLine(tailTrackingInstructionVisitor.TotalTailVisits);


public class TailTrackingInstructionVisitor : InstructionVisitor
{
    public int TotalTailVisits => _visitedTailPositions.Count;
    private readonly HashSet<Position> _visitedTailPositions = new()
    {
        new Position { X = 0, Y = 0 },
    };
    protected override (Position head, Position tail) VisitDown((Position head, Position tail) position)
    {
        var (head, tail) = position;

        var currentDirection = head.RelativeDirectionTo(tail);

        switch (currentDirection)
        {
            case Direction.Same:
            case Direction.East:
            case Direction.West:
            case Direction.NorthEast:
            case Direction.NorthWest:
                head.Y--;
                break;
            case Direction.North:
                head.Y--;
                break;
            case Direction.SouthEast:
                head.Y--;
                tail.Y--;
                tail.X++;
                break;
            case Direction.SouthWest:
                head.Y--;
                tail.Y--;
                tail.X--;
                break;
            default:
                head.Y--;
                tail.Y--;
                break;
        }
        _visitedTailPositions.Add(tail);
        return (head, tail);
    }

    protected override (Position head, Position tail) VisitLeft((Position head, Position tail) position)
    {
        var (head, tail) = position;

        var currentDirection = head.RelativeDirectionTo(tail);

        switch (currentDirection)
        {
            case Direction.Same:
            case Direction.South:
            case Direction.East:
            case Direction.SouthEast:
            case Direction.NorthEast:
                head.X--;
                break;
            case Direction.North:
                head.X--;
                break;
            case Direction.NorthWest:
                head.X--;
                tail.X--;
                tail.Y++;
                break;
            case Direction.SouthWest:
                tail.Y--;
                tail.X--;
                head.X--;
                break;
            default:
                head.X--;
                tail.X--;
                break;
        }

        _visitedTailPositions.Add(tail);

        return (head, tail);
    }

    protected override (Position head, Position tail) VisitRight((Position head, Position tail) position)
    {
        var (head, tail) = position;

        var currentDirection = head.RelativeDirectionTo(tail);

        switch (currentDirection)
        {
            case Direction.Same:
            case Direction.West:
            case Direction.South:
            case Direction.North:
            case Direction.SouthWest:
            case Direction.NorthWest:
                head.X++;
                break;
            case Direction.NorthEast:
                head.X++;
                tail.Y++;
                tail.X++;
                break;
            case Direction.SouthEast:
                head.X++;
                tail.Y--;
                tail.X++;
                break;
            default:
                head.X++;
                tail.X++;
                break;
        }

        _visitedTailPositions.Add(tail);

        return (head, tail);
    }

    protected override (Position head, Position tail) VisitUp((Position head, Position tail) position)
    {
        var (head, tail) = position;

        var currentDirection = head.RelativeDirectionTo(tail);

        switch (currentDirection)
        {
            case Direction.Same:
            case Direction.South:
            case Direction.East:
            case Direction.West:
            case Direction.SouthWest:
            case Direction.SouthEast:
                head.Y++;
                break;
            case Direction.NorthEast:
                head.Y++;
                tail.Y++;
                tail.X++;
                break;
            case Direction.NorthWest:
                head.Y++;
                tail.Y++;
                tail.X--;
                break;
            default:
                head.Y++;
                tail.Y++;
                break;
        }

        _visitedTailPositions.Add(tail);

        return (head, tail);
    }
}

public abstract class InstructionVisitor
{
    public (Position head, Position tail) Visit(string[] instructions)
    {
        (Position head, Position tail) currentPosition = new();

        foreach (var instruction in instructions)
        {
            var direction = instruction[0];
            var distance = int.Parse(instruction.Split(" ")[1]);

            foreach (var i in Enumerable.Range(0, distance))
            {
                switch (instruction[0])
                {
                    case 'U':
                        currentPosition = VisitUp(currentPosition);
                        break;
                    case 'D':
                        currentPosition = VisitDown(currentPosition);
                        break;
                    case 'L':
                        currentPosition = VisitLeft(currentPosition);
                        break;
                    case 'R':
                        currentPosition = VisitRight(currentPosition);
                        break;
                    default: throw new NotImplementedException();
                }
            }
        }

        return currentPosition;
    }

    protected abstract (Position head, Position tail) VisitLeft((Position head, Position tail) postition);
    protected abstract (Position head, Position tail) VisitRight((Position head, Position tail) postition);
    protected abstract (Position head, Position tail) VisitDown((Position head, Position tail) postition);
    protected abstract (Position head, Position tail) VisitUp((Position head, Position tail) postition);
}

public struct Position
{
    public int X;
    public int Y;

    public override string ToString()
    {
        return $"({X},{Y})";
    }

    private static readonly IReadOnlyDictionary<(int xDifference, int yDifference), Direction> _direcitonMap = new Dictionary<(int xDifference, int yDifference), Direction>
    {
        [(0, 0)] = Direction.Same,
        [(0, 1)] = Direction.North,
        [(0, -1)] = Direction.South,
        [(1, 0)] = Direction.East,
        [(1, 1)] = Direction.NorthEast,
        [(1, -1)] = Direction.SouthEast,
        [(-1, 0)] = Direction.West,
        [(-1, 1)] = Direction.NorthWest,
        [(-1, -1)] = Direction.SouthWest,
    };

    public Direction RelativeDirectionTo(Position otherPosition)
    {
        var xDifference = X - otherPosition.X;
        var yDifference = Y - otherPosition.Y;

        return _direcitonMap[(xDifference, yDifference)];
    }
}

public enum Direction
{
    Same,

    North,
    South,
    East,
    West,

    NorthWest,
    NorthEast,

    SouthWest,
    SouthEast
}

public static class Extensions
{
    public static Position Pos(this (int x, int y) position) => new() { X = position.x, Y = position.y };
}