// See https://aka.ms/new-console-template for more information
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

var consoleInputOutput = File.ReadAllText("input.txt");

AntlrInputStream inputStream = new AntlrInputStream(consoleInputOutput);
ConsoleInputOutputLexer speakLexer = new(inputStream);
CommonTokenStream commonTokenStream = new(speakLexer);

foreach(var thing in commonTokenStream.GetTokens())
{
    Console.WriteLine(thing);
}

ConsoleInputOutputParser consoleInputOutputParser = new(commonTokenStream);

ConsoleInputOutputParser.ProgramContext programContext = consoleInputOutputParser.program();
BasicConsoleInputOutputBaseVisitor visitor = new();
var root = visitor.Visit(programContext);

var totalSize = root.AllDescendants.Where(directory => directory.Size <= 100000).Sum(directory => directory.Size);

Console.WriteLine($"Part 1, total size of all directories of size at most 100000 is {totalSize}");

public class BasicConsoleInputOutputBaseVisitor : ConsoleInputOutputBaseVisitor<Directory>
{
    private static readonly Directory Root = new();
    private Directory _cwd;

    public BasicConsoleInputOutputBaseVisitor()
    {
        _cwd = Root;
    }

    public override Directory VisitCdDown(ConsoleInputOutputParser.CdDownContext context)
    {
        var directoryName = context.directoryName().GetText();

        Directory directory = new()
        {
            Name = directoryName,
            Parent = _cwd
        };

        _cwd.Children.Add(directory);

        _cwd = directory;
        return VisitChildren(context);
    }

    public override Directory VisitCdUp([NotNull] ConsoleInputOutputParser.CdUpContext context)
    {
        _cwd = _cwd.Parent;
        return VisitChildren(context);
    }

    public override Directory VisitFileSizeCommand([NotNull] ConsoleInputOutputParser.FileSizeCommandContext context)
    {
        var fileSize = int.Parse(context.fileSize().GetText());
        var fileDescriptor = context.fileDescriptor().GetText();
        FileReal currentFile = new()
        {
            Name = fileDescriptor,
            Size = fileSize
        };
        _cwd.Files.Add(currentFile);
        
        return VisitChildren(context);
    }

    public override Directory VisitProgram([NotNull] ConsoleInputOutputParser.ProgramContext context)
    {
        VisitChildren(context);
        // Ignore the fake root :~)
        return Root.Children.Single();
    }
}

public class Directory
{
    private readonly static Directory UninitializedDirectory = new Directory();

    public string Name { get; init; } = "Uninitialized";

    public ICollection<FileReal> Files { get; } = new List<FileReal>();

    public ICollection<Directory> Children { get; } = new List<Directory>();

    public Directory Parent { get; init; } = UninitializedDirectory;

    public int Size => Files.Sum(file => file.Size) + Children.Sum(child => child.Size);

    public IEnumerable<Directory> AllDescendants => Children.SelectMany(child => child.AllDescendants).Concat(new[] { this });

    public override string ToString()
    {
        return $"Name={Name} Size={Size}";
    }
}

public class FileReal
{
    public string Name { get; init; } = "Uninitialized";

    public int Size { get; init; } = 0;

    public override string ToString()
    {
        return $"Name={Name} Size={Size}";
    }
}