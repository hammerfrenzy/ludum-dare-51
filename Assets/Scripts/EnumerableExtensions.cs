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
}