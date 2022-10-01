using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EnumerableExtension
{
    public static T PickRandom<T>(this IEnumerable<T> enumberable)
    {
        var index = Random.Range(0, enumberable.Count());
        return enumberable.ElementAt(index);
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}