<Query Kind="Program">
  <NuGetReference>Ardalis.SmartEnum</NuGetReference>
  <Namespace>Ardalis.SmartEnum</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var initialStacksAndInstructions = File.ReadAllLines($@"{Path.GetDirectoryName(Util.CurrentQueryPath)}\Input.txt");
	SortedDictionary<int, Stack<char>> crateStacks = new();
	
	for(int crateStackNumber = 1; crateStackNumber <= 9; crateStackNumber++)
		crateStacks[crateStackNumber] = InitStack(crateStackNumber, initialStacksAndInstructions);
		
	var instructionsRegex = @"move (\d*) from (\d*) to (\d*)";
	foreach(var stackInstruction in initialStacksAndInstructions.Skip(10))
	{
		var matches = Regex.Match(stackInstruction, instructionsRegex);
		
		var cratesToMove = int.Parse(matches.Groups[1].Value);
		var source = int.Parse(matches.Groups[2].Value);
		var destination = int.Parse(matches.Groups[3].Value);
		
		var pickedUpCrates = crateStacks[source].PopMany(cratesToMove);
		
		crateStacks[destination].PushMany(pickedUpCrates.Reverse());
	}
	
	var finalTopConfiguration = string.Join("", crateStacks.Values.Select(stack => stack.Pop()));

	Console.Write($"Part 2 the final configuration of crates is {finalTopConfiguration}");
}

Stack<char> InitStack(int crateStackNumber, string[] initialStacksAndInstructions)
{
	Stack<char> stack = new();
	var offsetFromStartOfLine = (crateStackNumber * 4) - 3;

	for(var crateIndex = 7; crateIndex >= 0; crateIndex--)
	{
		var currentCrate = initialStacksAndInstructions[crateIndex][offsetFromStartOfLine];
		if(currentCrate != ' ')
			stack.Push(currentCrate);	
	}

	return stack;
}

public static class MyExtensions
{
	public static void PushMany<T>(this Stack<T> stack, IEnumerable<T> items)
	{
		foreach (var item in items)
			stack.Push(item);
	}

	public static T[] PopMany<T>(this Stack<T> stack, int popCount)
	{	
		T[] poppedItems = new T[popCount];
		
		for (int poppedCount = 0; poppedCount < popCount; poppedCount++)
		{
			poppedItems[poppedCount] = stack.Pop();
		}
		
		return poppedItems;
	}
}