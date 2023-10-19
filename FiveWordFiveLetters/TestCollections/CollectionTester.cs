using System.Text;

namespace TestCollections;
internal class CollectionTester<T>
{
    private readonly string[] _inputStrings;
    readonly Func<string, T> _stringToT;
    readonly Func<T, T> _comparer;
    private readonly List<BaseCollection<T>> _collection = new();
    public CollectionTester(string[] input, Func<string, T> stringToT, Func<T, T> comparer)
    {
        _inputStrings = input;
        _stringToT = stringToT;
        _comparer = comparer;
    }

    public void Add(BaseCollection<T> collection)
    {
        _collection.Add(collection);
    }
    public int Count()
    {
        return _collection.Count;
    }

    private long RunTest(BaseCollection<T> collection)
    {
        Console.WriteLine($"Test {collection.GetType()}.");
        Console.WriteLine($"Fill collection in {collection.FillCollection(_inputStrings, _stringToT)} ms");
        Console.WriteLine($"Sort collection in {collection.SortCollection(_comparer)} ms.");
        using (var writer = new StreamWriter("dump.txt", false, Encoding.UTF8))
        {
            Console.WriteLine($"Print collection in {collection.PrintCollection(writer)} ms.");
        }
        Console.WriteLine($"Total time {collection.TotalElapsedMiliseconds} ms.");
        Console.WriteLine($"For {collection.Count()} objects, first object: {collection.GetFirstObject()}, last object: {collection.LastObject()}");

        Console.WriteLine();
        return 0;
    }

    public void RunAllTest()
    {
        foreach (BaseCollection<T> collection in _collection)
        {
            RunTest(collection);
        }
    }
}
