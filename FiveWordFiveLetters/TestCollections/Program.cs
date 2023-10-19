using TestCollections.Collections;

namespace TestCollections;
public class TestCollections
{
    public static void Test(string[] input)
    {
        var tests = new CollectionTester<int>(input, x => int.Parse(x), x => x);
        tests.Add(new ArrayCollection<int>());
        tests.RunAllTest();
    }
}