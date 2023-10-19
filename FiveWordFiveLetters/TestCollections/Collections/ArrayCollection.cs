namespace TestCollections.Collections
{
    internal class ArrayCollection<T> : BaseCollection<T>
    {
        private T[]? _array;
        public override int Count()
        {
            if (_array is null) return 0;
            return _array.Length;
        }

        public override T GetFirstObject()
        {
            if (_array is null) throw new NullReferenceException();
            return _array[0];
        }

        public override T LastObject()
        {
            if (_array is null) throw new NullReferenceException();
            return _array[^1];
        }

        protected override void fillCollection(string[] input, Func<string, T> func)
        {
            //Add data to simulate data size
            _array = Array.Empty<T>();
            foreach (var item in input)
            {
                T[] addItem = new T[] { (func(item)) };
                int array1OriginalLength = _array.Length;
                Array.Resize<T>(ref _array, array1OriginalLength + addItem.Length);
                Array.Copy(addItem, 0, _array, array1OriginalLength, addItem.Length);
            }
        }

        protected override void printCollection(TextWriter writer)
        {
            if(_array is null) return;
            //print all data
            foreach (var item in _array)
            {
                writer.WriteLine(item);
            }
            writer.Flush();
        }

        protected override void sortCollection(Func<T, T> comparer)
        {
            // No sorting
        }
    }
}
