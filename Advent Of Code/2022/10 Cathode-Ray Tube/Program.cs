using Core;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


var instructions = File.ReadAllLines("input.txt");

var instructionsReal = instructions.SelectMany(instruction =>
{

    switch (instruction)
    {
        case "noop": return 0.AsSingletonEnumerable();
        default: return new[] { 0, int.Parse(instruction[5..]) };
    }

    throw new NotImplementedException();
});

var first20 = instructionsReal.Take(20);


var registerAfter20 = first20.Sum() + 1;
var current40thCycle = 20;
var cumulativeSignalStrength = registerAfter20 * 20;
var currentRegisterValue = registerAfter20;

var rest = instructionsReal.Skip(20).Chunk(40).Take(5); // 40 - 220th cycle

foreach(var chunk in rest)
{
    current40thCycle += 40;
    var sumThisChunk = chunk.Take(39).Sum();

    currentRegisterValue += sumThisChunk;

    cumulativeSignalStrength += (currentRegisterValue * current40thCycle);

    currentRegisterValue += chunk.Last();
}

Console.WriteLine(cumulativeSignalStrength);



var screenChunks = instructionsReal.Chunk(40);

var rowBuffer = new string('.', 40).ToCharArray();

currentRegisterValue = 1;
int[] currentSpritePosition = SpriteRange(1);
int currentCrtPosition = 0;

foreach(var chunk in screenChunks)
{
    foreach(var instruction in chunk)
    {
        // During Instruction
        if (currentSpritePosition.Contains(currentCrtPosition))
        {
            rowBuffer[currentCrtPosition] = '#';
        }

        //After instruction
        if(instruction != 0)
        {
            currentRegisterValue += instruction;
            currentSpritePosition = SpriteRange(currentRegisterValue);
        }

        Console.Write(rowBuffer[currentCrtPosition]);
        currentCrtPosition++;
    }

    Console.WriteLine();

    currentCrtPosition = 0;
    rowBuffer = new string('.', 40).ToCharArray();
}

int[] SpriteRange(int middle) => new[] { middle - 1, middle, middle + 1 };