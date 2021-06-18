using System;
using System.Collections.Generic;
using System.Linq;

namespace NavySpade.Utils
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            var random = new ThreadSafeRandom();
            var list = enumerable as IList<T> ?? enumerable.ToList();
            return list.ElementAt(random.Next(0, list.Count()));
        }
    }
}