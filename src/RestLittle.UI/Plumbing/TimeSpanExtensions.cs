// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle.UI.Plumbing
{
	/// <summary>
	/// Extensions of <see cref="TimeSpan"/>.
	/// </summary>
	public static class TimeSpanExtensions
	{
		/// <summary>
		///     Prints a friendly elapsed time string.
		/// </summary>
		/// <param name="ts">
		///     The amount of time elapsed.
		/// </param>
		/// <returns>
		///     A friendly string describing the elapsed time.
		/// </returns>
		public static string ToFriendlyString(this TimeSpan ts)
		{
			if (ts < TimeSpan.Zero)
			{
				throw new Exception("TimeSpan can't be negative");
			}

			if (ts.TotalSeconds < 1)
			{
				return "0 seconds";
			}

			if (ts.TotalMinutes < 1)
			{
				var seconds = (int)ts.TotalSeconds;

				return ToFriendlyString(seconds);
			}

			if (ts.TotalHours < 1)
			{
				var remainderSeconds = ts.TotalSeconds % 60;
				var wholeMinutes = (ts.TotalSeconds - remainderSeconds) / 60;

				return ToFriendlyString((int)wholeMinutes, (int)remainderSeconds);
			}

			var remainderMinutes = (int)ts.TotalMinutes % 60;
			var wholeHours = (int)(ts.TotalMinutes - remainderMinutes) / 60;

			return ToFriendlyStringHours(wholeHours, remainderMinutes);
		}

		private static string ToFriendlyStringHours(int hours, int minutes = 0)
		{
			if (minutes == 0)
			{
				return $"{hours} hour{Plural(hours)}";
			}

			return $"{hours} hour{Plural(hours)} {minutes} minute{Plural(minutes)}";
		}

		private static string ToFriendlyString(int seconds)
		{
			return $"{seconds} second{Plural(seconds)}";
		}

		private static string ToFriendlyString(int minutes, int seconds = 0)
		{
			if (seconds == 0)
			{
				return $"{minutes} minute{Plural(minutes)}";
			}

			return $"{minutes} minute{Plural(minutes)} {seconds} second{Plural(seconds)}";
		}

		private static string Plural(int value)
		{
			if (value != 1)
			{
				return "s";
			}

			return string.Empty;
		}
	}
}
