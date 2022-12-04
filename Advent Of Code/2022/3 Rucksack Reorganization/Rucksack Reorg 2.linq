<Query Kind="Statements" />


var rucksacks = File.ReadAllLines("C:\\Code\\advent-of-code-2021\\Advent Of Code\\2022\\3 Rucksack Reorganization\\Input.txt");
var runningTotalPriority = 0;

for(var ruckSackIndex = 0; ruckSackIndex < rucksacks.Length; ruckSackIndex += 3)
{
	var itemsAcrossRucksacks =
		rucksacks[ruckSackIndex].Distinct()
			.Concat(rucksacks[ruckSackIndex + 1].Distinct())
			.Concat(rucksacks[ruckSackIndex + 2].Distinct());

	var matchingItem = itemsAcrossRucksacks
		.GroupBy(item => item)
		.Single(group => group.Count() >= 3).Key;
		
	runningTotalPriority += Priority(matchingItem);
}

Console.WriteLine($"Part 2: total priority is {runningTotalPriority}");


int Priority(char item)
{
	if (item <= 'Z')
		return item - 'A' + 27;

	return item - 'a' + 1;
}
