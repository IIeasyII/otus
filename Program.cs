internal class Program
{
    private static async Task Main(string[] args)
    {
        var dictionary = new OtusDictionary(3);
        dictionary.Add(4, "Hello");
        dictionary.Add(5, "Otus");
        dictionary.Add(10, "Dictionary");
        dictionary.Add(11, "homework");

        var haveHello = dictionary.TryGetValue(4, out var result1);
        var haveOtus = dictionary.TryGetValue(5, out var result2);
        var haveDict = dictionary.TryGetValue(10, out var result3);
        var haveHomework = dictionary.TryGetValue(11, out var result4);

        var isEmpty = dictionary.TryGetValue(15, out var result5);

        var hello = dictionary[4];
        var otus = dictionary[5];
        var dict = dictionary[10];
        var homework = dictionary[11];

        var empty = dictionary[15];

        var newValue = dictionary[100] = "new value";
    }
}

internal class OtusDictionary
{
    private struct Entry
    {
        public Entry(int key, string value)
        {
            Key = key;
            Value = value;
        }

        public int HashCode { get; set; }
        public int Next { get; set; }
        public int Key { get; set; }
        public string Value { get; set; }
    }

    private int[] _buckets;
    private Entry[] _entries;

    private int _defaultCapacity = 32;
    private int _freeCount;
    private int _freeList;
    private int _count;
    private readonly IEqualityComparer<int> _comparer;

    public OtusDictionary() : this(32, null) { }

    public OtusDictionary(int capacity) : this(capacity, null) { }

    public OtusDictionary(IEqualityComparer<int> comparer) : this(32, comparer) { }

    public OtusDictionary(int capacity, IEqualityComparer<int>? comparer)
    {
        if (capacity < 0) throw new ArgumentOutOfRangeException();
        if (capacity > 0) Initialize(capacity);
        _comparer = comparer ?? EqualityComparer<int>.Default;
    }

    public void Add(int key, string value)
    {
        int hashCode = key & 0x7FFFFFFF;
        int targetBucket = hashCode % _buckets.Length;

        for (int i = _buckets[targetBucket]; i >= 0; i = _entries[i].Next)
        {
            if (_entries[i].HashCode == hashCode && _comparer.Equals(_entries[i].Key, key))
            {
                _entries[i].Value = value;
                return;
            }
        }

        int index;
        if (_freeCount > 0)
        {
            index = _freeList;
            _freeList = _entries[index].Next;
            _freeCount--;
        }
        else
        {
            if (_count == _entries.Length)
            {
                Resize(_buckets.Length * 2, false);
                targetBucket = hashCode % _buckets.Length;
            }
            index = _count;
            _count++;
        }

        _entries[index].HashCode = hashCode;
        _entries[index].Next = _buckets[targetBucket];
        _entries[index].Key = key;
        _entries[index].Value = value;
        _buckets[targetBucket] = index;
    }

    public bool TryGetValue(int key, out string? value)
    {
        int i = FindEntry(key);
        if (i >= 0)
        {
            value = _entries[i].Value;
            return true;
        }
        value = default(string);
        return false;
    }

    private int FindEntry(int key)
    {
        if (_buckets != null)
        {
            int hashCode = key;
            for (int i = _buckets[hashCode % _buckets.Length]; i >= 0; i = _entries[i].Next)
            {
                if (_entries[i].HashCode == hashCode && _comparer.Equals(_entries[i].Key, key))
                    return i;
            }
        }
        return -1;
    }

    private void Resize(int newSize, bool forceNewHashCodes)
    {
        int[] newBuckets = new int[newSize];
        for (int i = 0; i < newBuckets.Length; i++)
        {
            newBuckets[i] = -1;
        }

        Entry[] newEntries = new Entry[newSize];
        Array.Copy(_entries, 0, newEntries, 0, _count);
        if (forceNewHashCodes)
        {
            for (int i = 0; i < _count; i++)
            {
                if (newEntries[i].HashCode != -1)
                {
                    newEntries[i].HashCode = (_comparer.GetHashCode(newEntries[i].Key) & 0x7FFFFFFF);
                }
            }
        }
        for (int i = 0; i < _count; i++)
        {
            if (newEntries[i].HashCode >= 0)
            {
                int bucket = newEntries[i].HashCode % newSize;
                newEntries[i].Next = newBuckets[bucket];
                newBuckets[bucket] = i;
            }
        }
        _buckets = newBuckets;
        _entries = newEntries;
    }

    public string? this[int key]
    {
        get
        {
            int i = FindEntry(key);
            if (i >= 0)
                return _entries[i].Value;
            return default(string);
        }
        set
        {
            if (value is null) throw new NullReferenceException();
            Add(key, value);
        }
    }
    private void Initialize(int capacity)
    {
        _buckets = new int[capacity];
        for (int i = 0; i < _buckets.Length; i++)
        {
            _buckets[i] = -1;
        }
        _entries = new Entry[capacity];
        _freeList = -1;
    }
}