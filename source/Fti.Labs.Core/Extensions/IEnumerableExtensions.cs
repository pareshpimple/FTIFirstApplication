namespace Fti.Labs.Core.Extensions
{
	using System;
	using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableExtensions
	{
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (var item in items)
			{
				action(item);
			}

			return items;
		}

        public static IEnumerable<T> DistinctBy<T, TKey>(
            this IEnumerable<T> value,
            Func<T, TKey> keySelector)
        {
            return value.DistinctBy(keySelector, null);
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(
            this IEnumerable<T> value,
            Func<T, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            if (value == null)
            {
                throw new ArgumentNullException("source");
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }

            return DistinctByImpl(value, keySelector, comparer);
        }

        private static IEnumerable<T> DistinctByImpl<T, TKey>(
            IEnumerable<T> value,
            Func<T, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            return value.GroupBy(keySelector, comparer).Select(g => g.First());
        }
    }
}
