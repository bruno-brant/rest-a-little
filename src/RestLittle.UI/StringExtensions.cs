// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle.UI
{
	/// <summary>
	/// Extensions for the <see cref="string"/> object.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		///     Truncates the string to the given size, producing a new string.
		/// </summary>
		/// <param name="input">The string that will be truncated.</param>
		/// <param name="length">The maximum length of the new string.</param>
		/// <param name="ellipsis">Whether to add ellipsis (...) to the string.</param>
		/// <returns>A new string with at most <paramref name="length"/> characters.</returns>
		/// <remarks>
		///     If <paramref name="ellipsis"/> is true, length must be at least 3 since
		///     we need three characters to represent the ellipsis.
		/// </remarks>
		public static string Truncate(this string input, int length, bool ellipsis = false)
		{
			if (input is null)
			{
				throw new ArgumentNullException(nameof(input));
			}

			if (length < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(length), length, "Length can't be negative");
			}

			if (length == 0)
			{
				return string.Empty;
			}

			if (length > input.Length)
			{
				return input;
			}

			if (ellipsis)
			{
				if (length < 3)
				{
					throw new ArgumentException("When ellipsis is true, length must be larger than 3", nameof(length));
				}

				return input.Substring(0, length - 3) + "...";
			}

			return input.Substring(0, length);
		}
	}
}
