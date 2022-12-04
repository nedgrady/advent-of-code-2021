<Query Kind="Program">
  <NuGetReference>Ardalis.SmartEnum</NuGetReference>
  <Namespace>Ardalis.SmartEnum</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var rucksacks = File.ReadAllLines("C:\\Code\\advent-of-code-2021\\Advent Of Code\\2022\\3 Rucksack Reorganization\\Input.txt");
	var runningTotalPriority = 0;

	foreach (var rucksack in rucksacks)
	{
		var middleIndex = rucksack.Length / 2;
		var leftItems = rucksack[0..middleIndex];
		var rightItems = rucksack[middleIndex..];
		var distinctItems = leftItems.Distinct().Concat(rightItems.Distinct());
		
		var matchingItem = distinctItems
			.GroupBy(item => item)
			.Single(group => group.Count() >= 2).Key;

		runningTotalPriority += Priority(matchingItem);
	}
	
	Console.WriteLine($"Part 1: total priority is {runningTotalPriority}");
}

int Priority(char item)
{
	if(item <= 'Z')
		return item - 'A' + 27;
	
	return item - 'a' + 1;
}