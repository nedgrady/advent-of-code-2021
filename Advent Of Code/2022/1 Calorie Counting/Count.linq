<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
	var inventory = await File.ReadAllLinesAsync("C:\\Code\\advent-of-code-2021\\Advent Of Code\\2022\\1 Calorie Counting\\Input.txt");
	
	var caloriesPerElf = new Dictionary<int, int>();
	
	var currentElfIndex = 0;
	var currentCalorieCount = 0;
	
	foreach(var calorieCountOrEmpty in inventory)
	{
		if(string.IsNullOrWhiteSpace(calorieCountOrEmpty))
		{
			caloriesPerElf[currentElfIndex] = currentCalorieCount;
			currentElfIndex += 1;
			currentCalorieCount = 0;
		} else {
			currentCalorieCount += int.Parse(calorieCountOrEmpty);
		}			
	}

	Console.WriteLine($"Part 1, elf with the most calories has {caloriesPerElf.Values.Max()}");
	
	var topThreeCount = caloriesPerElf.Values.OrderByDescending(cals => cals).Take(3).Sum();
	Console.WriteLine($"Part 2, top 3 elves with the most calories have {topThreeCount}");
}

// You can define other methods, fields, classes and namespaces here
