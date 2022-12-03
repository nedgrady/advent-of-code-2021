<Query Kind="Program">
  <NuGetReference>Ardalis.SmartEnum</NuGetReference>
  <Namespace>Ardalis.SmartEnum</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	var encryptedStrategyGuide = await File.ReadAllLinesAsync("C:\\Code\\advent-of-code-2021\\Advent Of Code\\2022\\2 Rock Paper Scissors\\Input.txt");
	
	var runningScoreTotal = 0;
	
	foreach(var round in encryptedStrategyGuide)
	{
		var list = MyShape.List	;
		var myChoice = MyShape.FromValue(round[2]);
		var theirChoice = TheirShape.FromValue(round[0]);
		
		runningScoreTotal += myChoice.Score;
		runningScoreTotal += myChoice.Versus(theirChoice).ScoreForResult;
	}

	Console.WriteLine($"Part 1: final total score is {runningScoreTotal}");
}

class RoundResult : SmartEnum<RoundResult, int>
{
	public static readonly RoundResult Win = new RoundResult(nameof(Win), 6);
	public static readonly RoundResult Draw = new RoundResult(nameof(Draw), 3);
	public static readonly RoundResult Loss = new RoundResult(nameof(Loss), 0);

	public int ScoreForResult => Value;

	private RoundResult(string name, int score) : base(name, score)
	{
	}
}

abstract class MyShape : SmartEnum<MyShape, char>
{
	public static readonly MyShape MyRock = new MyRock();
	public static readonly MyShape MyPaper = new MyPaper();
	public static readonly MyShape MyScissors = new MyScissors();

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
	public MyRock() : base(nameof(MyRock), 'X', 1)
	{
		
	}

	public override RoundResult Versus(TheirShape theirShape)
	{
		if(theirShape == TheirShape.Rock) return RoundResult.Draw;
		if(theirShape == TheirShape.Paper) return RoundResult.Loss;
		if(theirShape == TheirShape.Scissors) return RoundResult.Win;
		
		throw new Exception();
	}
}

class MyPaper : MyShape
{
	public MyPaper() : base(nameof(MyPaper), 'Y', 2)
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
	public MyScissors() : base(nameof(MyScissors), 'Z', 3)
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

class TheirShape : SmartEnum<TheirShape, char>
{
	public static readonly TheirShape Rock = new TheirShape(nameof(Rock), 'A', 1);
	public static readonly TheirShape Paper = new TheirShape(nameof(Paper), 'B', 2);
	public static readonly TheirShape Scissors = new TheirShape(nameof(Scissors), 'C', 3);

	public char EncodedCharacter => Value;

	public int Score { get; }

	private TheirShape(string name, char codedLetter, int score) : base(name, codedLetter)
	{
		Score = score;
	}
}