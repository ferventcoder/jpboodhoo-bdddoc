using System;
using System.Collections.Generic;

namespace bdddoc.utility
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> one_at_a_time<T>(this IEnumerable<T> items)
        {
            foreach (var t in items) yield return t;
        }

        public static void each<T>(this IEnumerable<T> items, Action<T> work)
        {
            foreach (var t in items) work(t);
        }
    }
}