// Copyright (c) Bruno Brant. All rights reserved.

using System;
using AutoFixture.Xunit2;
using Xunit;

namespace RestLittle.UI.Tests
{
	public class StringExtensionsTests
	{
		[Theory]
		[AutoData]
		public void Truncate_LengthIsNegative_Throws(string input)
		{
			Assert.ThrowsAny<ArgumentOutOfRangeException>(() => input.Truncate(-1));
		}

		[Fact]
		public void Truncate_InputNull_Throws()
		{
			Assert.ThrowsAny<ArgumentNullException>(() => StringExtensions.Truncate(null, 1));
		}

		[Theory]
		[AutoData]
		public void Truncate_LenghtIsZero_ReturnEmptyString(string input)
		{
			Assert.Empty(input.Truncate(0));
		}

		[Fact]
		public void Truncate_SizeEqualsLength_OriginalStringIsReturned()
		{
			var expected = "Large string";

			var actual = expected.Truncate(expected.Length);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Truncate_LengthIsGreaterThanString_ReturnsTruncatedString()
		{
			const string input = "Large String";

			var actual = input.Truncate(input.Length + 1);

			Assert.Equal(input, actual);
		}

		[Fact]
		public void Truncate_LengthIsLessThanString_ReturnsTruncatedString()
		{
			const string expected = "Large";

			const string input = "Large String";

			var actual = input.Truncate(expected.Length);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Truncate_LengthIsLessThanStringAndEllipsisIsTrue_ReturnsTruncatedString()
		{
			const string expected = "La...";

			const string input = "Large String";

			var actual = input.Truncate(expected.Length, true);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Truncate_LengthIsLessThanStringAndEllipsisIsTrue_ReturnsTruncatedString2()
		{
			const string expected = "...";

			const string input = "Large String";

			var actual = input.Truncate(expected.Length, true);

			Assert.Equal(expected, actual);
		}

		[Theory, AutoData]
		public void Truncate_WhenEllipsisTrueAndLengthLessThan3_Throws(string input)
		{
			Assert.ThrowsAny<ArgumentException>(() => input.Truncate(2, true));
			Assert.ThrowsAny<ArgumentException>(() => input.Truncate(1, true));
		}
	}
}
