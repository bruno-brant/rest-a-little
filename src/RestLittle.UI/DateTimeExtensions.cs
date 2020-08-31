using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RestLittle.UI
{
	/// <summary>
	/// Extensions for type <see cref="DateTime"/>.
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Gets the interval between <see cref="DateTime.Now"/> and <paramref name="previous"/>.
		/// </summary>
		/// <param name="previous">When to start counting the interval.</param>
		/// <returns>
		/// How long from the DateTime until now.
		/// </returns>
		public static TimeSpan UntilNow(this DateTime previous)
		{
			return DateTime.Now - previous;
		}
	}
}
