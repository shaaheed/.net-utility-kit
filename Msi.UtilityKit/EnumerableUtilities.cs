using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Msi.UtilityKit
{
    public static class EnumerableUtilities
    {

        public static void ForEach<T>(this IEnumerable<T> _this, Action<T> callback)
        {
            foreach (T entry in _this)
            {
                callback(entry);
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var hash = new HashSet<TKey>();
            return source.Where(p => hash.Add(keySelector(p)));
        }

        public static int Count(this IEnumerable source)
        {
            var col = source as ICollection;
            int c = 0;
            if (col != null)
            {
                c = col.Count;
            }
            else
            {
                var e = source.GetEnumerator();
                e.DynamicUsing(() =>
                {
                    while (e.MoveNext())
                        c++;
                });
            }
            return c;
        }

        public static List<T> CreateEmptyList<T>(this T type, int? count = null)
        {
            return CreateEmptyList<T>();
        }

        public static List<T> CreateEmptyList<T>(int? count = null)
        {
            return count.HasValue ? new List<T>(count.Value) : new List<T>();
        }

        public static string Join<T>(this IEnumerable<T> items, string separator)
        {
            return items.Select(i => i.ToString()).Aggregate((acc, next) => string.Concat(acc, separator, next));
        }

        public static T Rand<T>(this IEnumerable<T> items)
        {
            IList<T> list;
            if (items is IList<T>)
                list = (IList<T>)items;
            else list = items.ToList();
            return list[new Random().Next(list.Count)];
        }

        // ref: http://stackoverflow.com/questions/375351/most-efficient-way-to-randomly-sort-shuffle-a-list-of-integers-in-c
        public static IList<T> Shuffle<T>(this IList<T> array)
        {
            T[] shuffleArray = new T[array.Count];
            array.CopyTo(shuffleArray, 0);

            Random random = new Random();
            for (int i = 0; i < array.Count; i += 1)
            {
                int swapIndex = random.Next(i, array.Count);
                if (swapIndex != i)
                {
                    T temp = shuffleArray[i];
                    shuffleArray[i] = shuffleArray[swapIndex];
                    shuffleArray[swapIndex] = temp;
                }
            }
            return shuffleArray;
        }

    }
}
