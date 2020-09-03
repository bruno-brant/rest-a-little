// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RestLittle.UI.Presenters
{
	/// <summary>
	/// Extensions for type <see cref="TimeSpan"/>.
	/// </summary>
	public static class TimeSpanExtensions
	{
		/// <summary>
		///     Returns a friendly description of the timespan.
		/// </summary>
		/// <param name="timeSpan">
		///     The timespan that will be formatted.
		/// </param>
		/// <returns>
		///     A friendly string.
		/// </returns>
		/// <remarks>
		///     When more than 59 minutes have passed, display as "hh:mm hours";
		///     When more than 59 seconds have passed, display as "mm minutes";
		///     Othewise, display as "ss seconds".
		/// </remarks>
		public static string ToFriendlyString(this TimeSpan timeSpan)
		{
			if (timeSpan.TotalHours >= 1)
			{
				return $"{timeSpan.ToString(@"hh\:mm", CultureInfo.InvariantCulture)} hours";
			}

			if (timeSpan.TotalMinutes >= 1)
			{
				return $"{(int)timeSpan.TotalMinutes} minutes";
			}

			return $"{(int)timeSpan.TotalSeconds} seconds";
		}
	}
}
