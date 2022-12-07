<Query Kind="Program">
  <NuGetReference>Ardalis.SmartEnum</NuGetReference>
  <Namespace>Ardalis.SmartEnum</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var signal = File.ReadAllText($@"{Path.GetDirectoryName(Util.CurrentQueryPath)}\Input.txt");
	
	RingBuffer<char> ringBuffer = new(4);
	
	foreach(var @char in signal)
	{
		ringBuffer.Add(@char);
		
		if(ringBuffer.Distinct().Count() == 4)
		{
			Console.WriteLine($"First index is {signal.IndexOf(new String(ringBuffer.ToArray()))}");
			return;
		}
	}	
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