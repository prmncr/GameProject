using System;
using System.Collections.Generic;

namespace GameProject.GameObjects
{
	public static class EnumerableExtensions
	{
		public static void ForAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate,
			Action<TSource> action)
		{
			if (source == null) throw new ArgumentException($"{nameof(source)} must be not null");
			if (predicate == null) throw new ArgumentException($"{nameof(predicate)} must be not null");
			foreach (var element in source)
				if (predicate(element))
					action(element);
		}
	}
}