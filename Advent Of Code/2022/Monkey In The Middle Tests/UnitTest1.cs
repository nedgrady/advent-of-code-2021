using Antlr4.Runtime;
using NUnit.Framework;
using FluentAssertions;
using System.Linq;

namespace Monkey_In_The_Middle_Tests
{
    public class Tests
    {
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(9)]
        public void MonkeyHeader(int monkeyNumber)
        {
            AntlrInputStream inputStream = new($"Monkey {monkeyNumber}:");
            MonkeyLexer monkeyLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(monkeyLexer);
            MonkeyParser monkeyParser = new(commonTokenStream);

            var monkeyHeaderContext = monkeyParser.monkeyHeader();
            
            monkeyHeaderContext.monkeyNumber().GetText().Should().Be(monkeyNumber.ToString());
        }

        [Test]
        public void StartingTtems()
        {
            AntlrInputStream inputStream = new("Starting items: 72, 64, 51, 57, 93, 97, 68");
            MonkeyLexer monkeyLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(monkeyLexer);
            MonkeyParser monkeyParser = new(commonTokenStream);

            var monkeyStartingItemsContext = monkeyParser.monkeyStartingItems();

            monkeyStartingItemsContext.items().GetText().Should().Be("72,64,51,57,93,97,68");

            monkeyStartingItemsContext.items().item().Select(item => int.Parse(item.GetText())).Should().BeEquivalentTo(new int[] { 72, 64, 51, 57, 93, 97, 68 });
        }

        [Test]
        public void MonkeyOperation()
        {
            AntlrInputStream inputStream = new("Operation: new = old * 19");
            MonkeyLexer monkeyLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(monkeyLexer);
            MonkeyParser monkeyParser = new(commonTokenStream);

            var monkeyOperationContext = monkeyParser.monkeyOperation();

            monkeyOperationContext.monkeyOperand(0).GetText().Should().Be("old");
            monkeyOperationContext.monkeyOperand(1).GetText().Should().Be("19");
            monkeyOperationContext.OPERATOR().GetText().Should().Be("*");
        }

        [Test]
        public void MonkeyOperation2()
        {
            AntlrInputStream inputStream = new("Operation: new = 20 * old");
            MonkeyLexer monkeyLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(monkeyLexer);
            MonkeyParser monkeyParser = new(commonTokenStream);

            var monkeyOperationContext = monkeyParser.monkeyOperation();

            monkeyOperationContext.monkeyOperand(0).GetText().Should().Be("20");
            monkeyOperationContext.monkeyOperand(1).GetText().Should().Be("old");
            monkeyOperationContext.OPERATOR().GetText().Should().Be("*");
        }

        [Test]
        public void MonkeyOperation3()
        {
            AntlrInputStream inputStream = new("Operation: new = old + old");
            MonkeyLexer monkeyLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(monkeyLexer);
            MonkeyParser monkeyParser = new(commonTokenStream);

            var monkeyOperationContext = monkeyParser.monkeyOperation();

            monkeyOperationContext.monkeyOperand(0).GetText().Should().Be("old");
            monkeyOperationContext.monkeyOperand(1).GetText().Should().Be("old");
            monkeyOperationContext.OPERATOR().GetText().Should().Be("+");
        }

        [TestCase(0)]
        [TestCase(3)]
        [TestCase(17)]
        public void MonkeyTestCondition(int divisor)
        {
            AntlrInputStream inputStream = new($"Test: divisible by {divisor}");
            MonkeyLexer monkeyLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(monkeyLexer);
            MonkeyParser monkeyParser = new(commonTokenStream);

            var monkeyTestConditionContext = monkeyParser.monkeyTestCondition();

            monkeyTestConditionContext.monkeyTestConditionDivisor().GetText().Should().Be(divisor.ToString());
        }

        [TestCase(0)]
        [TestCase(3)]
        [TestCase(17)]
        public void MonkeyTestTrueCondition(int monkeyNumber)
        {
            AntlrInputStream inputStream = new($"If true: throw to monkey {monkeyNumber}");
            MonkeyLexer monkeyLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(monkeyLexer);
            MonkeyParser monkeyParser = new(commonTokenStream);

            var monkeyTrueActionContext = monkeyParser.trueAction();

            monkeyTrueActionContext.monkeyNumber().GetText().Should().Be(monkeyNumber.ToString());
        }

        [TestCase(0)]
        [TestCase(3)]
        [TestCase(17)]
        public void MonkeyTestFalseCondition(int monkeyNumber)
        {
            AntlrInputStream inputStream = new($"If false: throw to monkey {monkeyNumber}");
            MonkeyLexer monkeyLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(monkeyLexer);
            MonkeyParser monkeyParser = new(commonTokenStream);

            var monkeyFalseActionContext = monkeyParser.falseAction();

            monkeyFalseActionContext.monkeyNumber().GetText().Should().Be(monkeyNumber.ToString());
        }

        [Test]
        public void MonkeyTest()
        {
            AntlrInputStream inputStream = new($@"Test: divisible by 17
    If true: throw to monkey 4
    If false: throw to monkey 7");
            MonkeyLexer monkeyLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(monkeyLexer);
            MonkeyParser monkeyParser = new(commonTokenStream);

            var monkeyTestContext = monkeyParser.monkeyTest();

            monkeyTestContext.monkeyTestCondition().monkeyTestConditionDivisor().GetText().Should().Be("17");
            monkeyTestContext.trueAction().monkeyNumber().GetText().Should().Be("4");
            monkeyTestContext.falseAction().monkeyNumber().GetText().Should().Be("7");

        }

        [Test]
        public void MonkeyDefinition()
        {
            AntlrInputStream inputStream = new($@"Monkey 2:
     Starting items: 57, 94, 69, 79, 72
     Operation: new = old + 6
     Test: divisible by 19
        If true: throw to monkey 0
        If false: throw to monkey 4
");
            MonkeyLexer monkeyLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(monkeyLexer);
            MonkeyParser monkeyParser = new(commonTokenStream);

            var monkeyDefinitionContext = monkeyParser.monkeyDefinition();

            monkeyDefinitionContext.monkeyHeader().monkeyNumber().GetText().Should().Be("2");
            monkeyDefinitionContext.monkeyStartingItems().items().item().Select(item => int.Parse(item.GetText())).Should().BeEquivalentTo(new int[] { 57, 94, 69, 79, 72 });
            monkeyDefinitionContext.monkeyOperation().monkeyOperand(0).GetText().Should().Be("old");
            monkeyDefinitionContext.monkeyOperation().monkeyOperand(1).GetText().Should().Be("6");
            monkeyDefinitionContext.monkeyOperation().OPERATOR().GetText().Should().Be("+");
            monkeyDefinitionContext.monkeyTest().monkeyTestCondition().monkeyTestConditionDivisor().GetText().Should().Be("19");
            monkeyDefinitionContext.monkeyTest().trueAction().monkeyNumber().GetText().Should().Be("0");
            monkeyDefinitionContext.monkeyTest().falseAction().monkeyNumber().GetText().Should().Be("4");
        }

        [Test]
        public void MonkeyDefinitions()
        {
            AntlrInputStream inputStream = new($@"Monkey 5:
  Starting items: 57, 95, 81, 61
  Operation: new = old * old
  Test: divisible by 5
    If true: throw to monkey 1
    If false: throw to monkey 6

Monkey 6:
  Starting items: 79, 99
  Operation: new = old + 2
  Test: divisible by 11
    If true: throw to monkey 3
    If false: throw to monkey 1

Monkey 7:
  Starting items: 68, 98, 62
  Operation: new = old + 3
  Test: divisible by 13
    If true: throw to monkey 5
    If false: throw to monkey 6
");
            MonkeyLexer monkeyLexer = new(inputStream);
            CommonTokenStream commonTokenStream = new(monkeyLexer);
            MonkeyParser monkeyParser = new(commonTokenStream);

            var monkeyStartingItemsContext = monkeyParser.monkeyDefinitions().monkeyDefinition();

            var monkeyDefinitionContext0 = monkeyStartingItemsContext[0];

            monkeyDefinitionContext0.monkeyHeader().monkeyNumber().GetText().Should().Be("5");
            monkeyDefinitionContext0.monkeyStartingItems().items().item().Select(item => int.Parse(item.GetText())).Should().BeEquivalentTo(new int[] { 57, 95, 81, 61 });
            monkeyDefinitionContext0.monkeyOperation().monkeyOperand(0).GetText().Should().Be("old");
            monkeyDefinitionContext0.monkeyOperation().monkeyOperand(1).GetText().Should().Be("old");
            monkeyDefinitionContext0.monkeyOperation().OPERATOR().GetText().Should().Be("*");
            monkeyDefinitionContext0.monkeyTest().monkeyTestCondition().monkeyTestConditionDivisor().GetText().Should().Be("5");
            monkeyDefinitionContext0.monkeyTest().trueAction().monkeyNumber().GetText().Should().Be("1");
            monkeyDefinitionContext0.monkeyTest().falseAction().monkeyNumber().GetText().Should().Be("6");

            var monkeyDefinitionContext2 = monkeyStartingItemsContext[2];

            monkeyDefinitionContext2.monkeyHeader().monkeyNumber().GetText().Should().Be("7");
            monkeyDefinitionContext2.monkeyStartingItems().items().item().Select(item => int.Parse(item.GetText())).Should().BeEquivalentTo(new int[] { 68, 98, 62 });
            monkeyDefinitionContext2.monkeyOperation().monkeyOperand(0).GetText().Should().Be("old");
            monkeyDefinitionContext2.monkeyOperation().monkeyOperand(1).GetText().Should().Be("3");
            monkeyDefinitionContext2.monkeyOperation().OPERATOR().GetText().Should().Be("+");
            monkeyDefinitionContext2.monkeyTest().monkeyTestCondition().monkeyTestConditionDivisor().GetText().Should().Be("13");
            monkeyDefinitionContext2.monkeyTest().trueAction().monkeyNumber().GetText().Should().Be("5");
            monkeyDefinitionContext2.monkeyTest().falseAction().monkeyNumber().GetText().Should().Be("6");
        }
    }
}