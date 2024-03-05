namespace AulasAI.Search;

public static partial class Search
{
    public static int BinarySearch<T>(List<T> collection, T value, int begin = 0, int end = -1)
    {
        end = end == -1 ? collection.Count - 1 : end;
        var mid = begin + (end - begin) / 2;
        var midValue = collection[mid];
        var comparer = Comparer<T>.Default;

        do
        {
            if (comparer.Compare(midValue, value) < 0)
                begin = mid + 1;
            if (comparer.Compare(midValue, value) > 0)
                end = mid;

            mid = begin + (end - begin) / 2;
            midValue = collection[mid];

            if (comparer.Compare(midValue, value) == 0)
                return mid;
        } while (end > begin);

        return -1;
    }
}