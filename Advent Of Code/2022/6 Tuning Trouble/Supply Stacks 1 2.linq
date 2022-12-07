<Query Kind="Program">
  <NuGetReference>Ardalis.SmartEnum</NuGetReference>
  <Namespace>Ardalis.SmartEnum</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>


void Main()
{
	var signal = File.ReadAllText($@"{Path.GetDirectoryName(Util.CurrentQueryPath)}\Input.txt");

	const int RepeatingCount = 14;
	
	RingBuffer<char> ringBuffer = new(RepeatingCount);
	
	foreach(var @char in ringBuffer.Take(RepeatingCount))
		ringBuffer.Add(@char);

	int signalsProcessed = RepeatingCount;

	var signalEnumerator = signal.Skip(RepeatingCount).GetEnumerator();
	
	signalEnumerator.MoveNext();
	
	do
	{
		if (ringBuffer.Distinct().Count() == RepeatingCount)
			break;

		signalsProcessed++;
		ringBuffer.Add(signalEnumerator.Current);


	} while (signalEnumerator.MoveNext());

	Console.WriteLine($"Found a repetition after {signalsProcessed} characters.");

}


class RingBuffer<TItem> : IEnumerable<TItem>
{
	private readonly TItem[] _items;

	int currentIndex = 0;


	public RingBuffer(int size)
	{
		_items = new TItem[size];
	}

	public void Add(TItem item)
	{
		_items[currentIndex++ % _items.Length] = item;
	}

	public IEnumerator<TItem> GetEnumerator()
	{
		return ((IEnumerable<TItem>)_items).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return _items.GetEnumerator();
	}
}

