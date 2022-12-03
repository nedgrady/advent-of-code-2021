<Query Kind="Statements">
  <NuGetReference>Ardalis.SmartEnum</NuGetReference>
  <Namespace>Ardalis.SmartEnum</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var encryptedStrategyGuide = await File.ReadAllLinesAsync("C:\\Code\\advent-of-code-2021\\Advent Of Code\\2022\\2 Rock Paper Scissors\\Input.txt");

var runningScoreTotal = 0;

foreach (var round in encryptedStrategyGuide)
{
	var list = MyShape.List;
	var theirChoice = TheirShape.FromValue(round[0]);
	var requiredResult = RoundResult.FromValue(round[2]);
	
	var myChoice = theirChoice.MyShapeRequiredFor(requiredResult);

	runningScoreTotal += myChoice.Score;
	runningScoreTotal += myChoice.Versus(theirChoice).ScoreForResult;
}

Console.WriteLine($"Part 1: final total score is {runningScoreTotal}");


class RoundResult : SmartEnum<RoundResult, char>
{
	public static readonly RoundResult Win = new RoundResult(nameof(Win), 'Z', 6);
	public static readonly RoundResult Draw = new RoundResult(nameof(Draw), 'Y', 3);
	public static readonly RoundResult Loss = new RoundResult(nameof(Loss), 'X', 0);

	public int ScoreForResult { get; }

	private RoundResult(string name, char encodedCharacter, int scoreForResult) : base(name, encodedCharacter)
	{
		ScoreForResult = scoreForResult;
	}
}

abstract class MyShape : SmartEnum<MyShape, char>
{
	public static readonly MyShape Rock = new MyRock();
	public static readonly MyShape Paper = new MyPaper();
	public static readonly MyShape Scissors = new MyScissors();

	public abstract RoundResult Versus(TheirShape theirShape);

	public char EncodedCharacter => Value;

	public int Score { get; }

	protected MyShape(string name, char codedLetter, int score) : base(name, codedLetter)
	{
		Score = score;
	}
}

class MyRock : MyShape
{
	public MyRock() : base(nameof(Rock), 'X', 1)
	{

	}

	public override RoundResult Versus(TheirShape theirShape)
	{
		if (theirShape == TheirShape.Rock) return RoundResult.Draw;
		if (theirShape == TheirShape.Paper) return RoundResult.Loss;
		if (theirShape == TheirShape.Scissors) return RoundResult.Win;

		throw new Exception();
	}
}

class MyPaper : MyShape
{
	public MyPaper() : base(nameof(Paper), 'Y', 2)
	{
	}

	public override RoundResult Versus(TheirShape theirShape)
	{
		if (theirShape == TheirShape.Rock) return RoundResult.Win;
		if (theirShape == TheirShape.Paper) return RoundResult.Draw;
		if (theirShape == TheirShape.Scissors) return RoundResult.Loss;

		throw new Exception();
	}
}

class MyScissors : MyShape
{
	public MyScissors() : base(nameof(Scissors), 'Z', 3)
	{
	}

	public override RoundResult Versus(TheirShape theirShape)
	{
		if (theirShape == TheirShape.Rock) return RoundResult.Loss;
		if (theirShape == TheirShape.Paper) return RoundResult.Win;
		if (theirShape == TheirShape.Scissors) return RoundResult.Draw;

		throw new Exception();
	}
}

abstract class TheirShape : SmartEnum<TheirShape, char>
{
	public static readonly TheirShape Rock = new TheirRock();
	public static readonly TheirShape Paper = new TheirPaper();
	public static readonly TheirShape Scissors = new TheirScissors();
	
	public abstract MyShape MyShapeRequiredFor(RoundResult roundResult);

	public char EncodedCharacter => Value;


	protected TheirShape(string name, char codedLetter) : base(name, codedLetter)
	{
	}
}

class TheirRock : TheirShape
{
	public TheirRock() : base(nameof(MyRock), 'A')
	{

	}

	public override MyShape MyShapeRequiredFor(RoundResult roundResult)
	{
		if(roundResult == RoundResult.Draw) return MyShape.Rock;
		if(roundResult == RoundResult.Win) return MyShape.Paper;
		if(roundResult == RoundResult.Loss) return MyShape.Scissors;
		
		throw new Exception();
	}
}

class TheirPaper : TheirShape
{
	public TheirPaper() : base(nameof(MyPaper), 'B')
	{

	}

	public override MyShape MyShapeRequiredFor(RoundResult roundResult)
	{
		if (roundResult == RoundResult.Draw) return MyShape.Paper;
		if (roundResult == RoundResult.Win) return MyShape.Scissors;
		if (roundResult == RoundResult.Loss) return MyShape.Rock;
		
		throw new Exception();
	}
}

class TheirScissors : TheirShape
{
	public TheirScissors() : base(nameof(TheirScissors), 'C')
	{

	}

	public override MyShape MyShapeRequiredFor(RoundResult roundResult)
	{
		if (roundResult == RoundResult.Draw) return MyShape.Scissors;
		if (roundResult == RoundResult.Win) return MyShape.Rock;
		if (roundResult == RoundResult.Loss) return MyShape.Paper;
		
		throw new Exception();
	}
}
