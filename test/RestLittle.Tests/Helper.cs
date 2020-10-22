// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Collections.Generic;

namespace RestLittle.Tests
{
	public static class Helper
	{
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			if (collection is null)
			{
				throw new ArgumentNullException(nameof(collection));
			}

			foreach (var item in collection)
			{
				action(item);
			}
		}
	}
}
