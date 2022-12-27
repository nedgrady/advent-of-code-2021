using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

var monkeyRawInput = File.ReadAllText("input.txt");

AntlrInputStream inputStream = new(monkeyRawInput);

MonkeyLexer monkeyLexer = new(inputStream);
CommonTokenStream commonTokenStream = new(monkeyLexer);

foreach(var thing in commonTokenStream.GetTokens())
{
    Console.WriteLine(thing);
}

MonkeyParser monkeyParser = new(commonTokenStream);

var programContext = monkeyParser.monkeyProgram();
CreateMonkeyProgramVisitor visitor = new();

var monkeyProgram = visitor.Visit(programContext);

Console.WriteLine(monkeyProgram.Monkeys.Select(m => m.Divisor).Aggregate((total, next) => total * next));

checked
{
    foreach (var roundNumber in Enumerable.Range(0, 10000))
    {
        foreach (var monkey in monkeyProgram.Monkeys.ToList())
        {
            foreach (var item in monkey.Items.ToList())
            {
                monkey.Inspect(item);

                Monkey recipient;
                if (item.WorryLevel % monkey.Divisor == 0)
                {
                    //Console.WriteLine($"Current worry level is divisible by {monkey.Divisor}.");
                    recipient = monkeyProgram.Monkeys.Single(m => m.MonkeyNumber == monkey.TrueMonkeyNumber);
                }
                else
                {
                    //Console.WriteLine($"Current worry level is not divisible by {monkey.Divisor}.");
                    recipient = monkeyProgram.Monkeys.Single(m => m.MonkeyNumber == monkey.FalseMonkeyNumber);
                }

                monkey.Throw(item).To(recipient);
            }
        }
    }
}


var topTwo = monkeyProgram.Monkeys.OrderByDescending(monkey => monkey.InspectionCount).Take(2).ToList();

//Console.WriteLine(string.Join(",", monkeyProgram.Monkeys.Select(m => m.InspectionCount).OrderByDescending(i => i)));

Console.WriteLine($"Top two throwers were {topTwo[0].MonkeyNumber} ({topTwo[0].InspectionCount}) and {topTwo[1].MonkeyNumber} ({topTwo[1].InspectionCount}).");
Console.WriteLine($"Final result is {topTwo[0].InspectionCount * topTwo[1].InspectionCount}");

public class CreateMonkeyProgramVisitor : MonkeyBaseVisitor<MonkeyProgram>
{
    private Monkey.Builder _currentMonkeyBuilder = new();

    private readonly MonkeyProgram.Builder _programBuilder = new ();

    public override MonkeyProgram VisitMonkeyProgram([NotNull] MonkeyParser.MonkeyProgramContext context)
    {
        
        base.VisitMonkeyProgram(context);

        return _programBuilder.ToProgram();
    }

    public override MonkeyProgram VisitMonkeyDefinition([NotNull] MonkeyParser.MonkeyDefinitionContext context)
    {
        _currentMonkeyBuilder = new();

        var monkeyNumber = long.Parse(context.monkeyHeader().monkeyNumber().GetText());
        _currentMonkeyBuilder.MonkeyNumber = monkeyNumber;
        base.VisitMonkeyDefinition(context);
        _programBuilder.Monkeys.Add(_currentMonkeyBuilder.ToMonkey());
        return null;
    }

    public override MonkeyProgram VisitItem([NotNull] MonkeyParser.ItemContext context)
    {
        var worryLevel = long.Parse(context.NUMBER().GetText());
        _currentMonkeyBuilder.Items.Add(new Item { WorryLevel = worryLevel });
        return base.VisitItem(context);
    }

    public override MonkeyProgram VisitMonkeyOperation([NotNull] MonkeyParser.MonkeyOperationContext context)
    {
        (string operand1, string operand2) = (context.monkeyOperand(0).GetText(), context.monkeyOperand(1).GetText());

        string @operator = context.OPERATOR().GetText();

        Func<long, long> inspectionOperation = (worryLevel) =>
        {
            checked
            {
                long o1 = operand1 == "old" ? worryLevel : long.Parse(operand1);
                long o2 = operand2 == "old" ? worryLevel : long.Parse(operand2);

                if (@operator == "+")
                    return (o1 + o2);

                return (o1 * o2) % 9699690;
            }
        };

        _currentMonkeyBuilder.InspectionAction = inspectionOperation;

        return base.VisitMonkeyOperation(context);
    }

    public override MonkeyProgram VisitMonkeyTestConditionDivisor([NotNull] MonkeyParser.MonkeyTestConditionDivisorContext context)
    {
        _currentMonkeyBuilder.Divisor = long.Parse(context.GetText());
        return base.VisitMonkeyTestConditionDivisor(context);
    }

    public override MonkeyProgram VisitFalseAction([NotNull] MonkeyParser.FalseActionContext context)
    {
        _currentMonkeyBuilder.FalseMonkeyNumber = long.Parse(context.monkeyNumber().GetText());
        return base.VisitFalseAction(context);
    }

    public override MonkeyProgram VisitTrueAction([NotNull] MonkeyParser.TrueActionContext context)
    {
        _currentMonkeyBuilder.TrueMonkeyNumber = long.Parse(context.monkeyNumber().GetText());
        return base.VisitTrueAction(context);
    }
}




public class MonkeyProgram
{
    private readonly IList<Monkey> _monkeys;

    private MonkeyProgram(IList<Monkey> monkeys)
    {
        _monkeys = monkeys;
    }
    public IList<Monkey> Monkeys => _monkeys.ToList();

    public class Builder
    {
        public IList<Monkey> Monkeys { get; } = new List<Monkey>();

        public MonkeyProgram ToProgram() => new(Monkeys);
    }

    public override string ToString()
    {
        return string.Join("\n", _monkeys);
    }
}

public class Monkey
{
    private Monkey() { }

    public long MonkeyNumber { get; private set; }
    public ICollection<Item> Items { get; private set; } = new List<Item>();

    public long InspectionCount { get; private set; } = 0;

    public long Divisor { get; private init; }

    private Func<long, long> InspectionAction { get; init; } = _ => throw new NotImplementedException();
    internal void Inspect(Item item)
    {
        //Console.WriteLine($"Monkey {MonkeyNumber} inspects an item with a worry level of {item.WorryLevel}.");
        item.WorryLevel = InspectionAction(item.WorryLevel);
        //Console.WriteLine($"Worry level is now {item.WorryLevel}");
        //item.WorryLevel /= 3;

        //Console.WriteLine($"Monkey gets bored with item. Worry level is divided by 3 to {item.WorryLevel}");
        InspectionCount++;
    }

    public ThrowTo Throw(Item item)
    {
        Items.Remove(item);
        return new ThrowTo(item);
    }

    public long TrueMonkeyNumber { get; private init; }
    public long FalseMonkeyNumber { get; private init; }

    public override string ToString()
    {
        return @$"Monkey {MonkeyNumber}:
  Starting items: {string.Join(", ", Items)}
  Operation: new = (1) => {InspectionAction(1)}
  Test: divisible by {Divisor}
    If true: throw to monkey {TrueMonkeyNumber}
    If false: throw to monkey {FalseMonkeyNumber}";
    }


    public class Builder
    {
        public long MonkeyNumber { get; set; }
        public long Divisor { get; set; }
        public long TrueMonkeyNumber { get; set; }
        public long FalseMonkeyNumber { get; set; }

        public ICollection<Item> Items { get; set; } = new List<Item>();

        public Func<long, long> InspectionAction { get; set; } = _ => throw new NotImplementedException();

        internal Monkey ToMonkey()
        {
            return new Monkey
            {
                Items = Items,
                Divisor = Divisor,
                TrueMonkeyNumber = TrueMonkeyNumber,
                FalseMonkeyNumber = FalseMonkeyNumber,
                InspectionAction = InspectionAction,
                MonkeyNumber = MonkeyNumber
            };
        }
    }
}

public class ThrowTo
{
    private readonly Item projectile;

    public ThrowTo(Item projectile)
    {
        this.projectile = projectile;
    }

    public void To(Monkey monkey)
    {
        //Console.WriteLine($"Item with worry level {projectile.WorryLevel} is thrown to monkey {monkey.MonkeyNumber}.");
        monkey.Items.Add(projectile);
    }
}

public record Item
{
    public long WorryLevel { get; set; }
}