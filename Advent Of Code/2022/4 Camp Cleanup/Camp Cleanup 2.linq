<Query Kind="Program" />

void Main()
{
	var assignmentPairs = File.ReadAllLines("C:\\Code\\advent-of-code-2021\\Advent Of Code\\2022\\4 Camp Cleanup\\Input.txt");
	var assignmentPairRegex = @"(\d*)-(\d*),(\d*)-(\d*)";

	var partiallyOverlappingAssignments = 0;

	foreach (var assignmentPair in assignmentPairs)
	{
		var matches = Regex.Matches(assignmentPair, assignmentPairRegex, RegexOptions.None);

		(int start1, int end1) = (int.Parse(matches[0].Groups[1].Value), int.Parse(matches[0].Groups[2].Value));
		(int start2, int end2) = (int.Parse(matches[0].Groups[3].Value), int.Parse(matches[0].Groups[4].Value));

		var range1 = Enumerable.Range(start1, end1 - start1 + 1);
		var range2 = Enumerable.Range(start2, end2 - start2 + 1);

		var largestRangeLength = Math.Max(range1.Count(), range2.Count());

		if (range1.Concat(range2).Count() != range1.Union(range2).Count())
			partiallyOverlappingAssignments++;
	}

	Console.Write($"Part 1 there are {partiallyOverlappingAssignments} overlapping assingments");
}