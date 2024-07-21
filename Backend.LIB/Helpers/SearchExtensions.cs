using global::System;

namespace Backend.LIB.Helpers;

public static class IEnumerableExtensions
{
    public static int BinarySearch<T>(this IEnumerable<T> enumerable, T value) where T : IComparable<T>
    {
        var array = enumerable.ToArray();
        int min = 0;
        int max = array.Length - 1;
        while (min <= max)
        {
            int mid = (min + max) / 2;
            if (array[mid].CompareTo(value) == 0)
            {
                return mid;
            }
            else if (array[mid].CompareTo(value) < 0)
            {
                min = mid + 1;
            }
            else
            {
                max = mid - 1;
            }
        }
        return -1;
    }
}