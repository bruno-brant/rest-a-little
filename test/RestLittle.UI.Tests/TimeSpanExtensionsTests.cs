// Copyright (c) Bruno Brant. All rights reserved.

using System;
using AutoFixture.Xunit2;
using RestLittle.UI.Plumbing;
using Xunit;

namespace RestLittle.UI.Tests
{
	public class TimeSpanExtensionsTests
	{
		[Theory]
		[AutoData]
		public void ToFriendlyString_NegativeTimeSpan_Throws(int minutes, int seconds)
		{
			var ts = new TimeSpan(0, -Math.Abs(minutes), -Math.Abs(seconds));

			Assert.ThrowsAny<Exception>(() => TimeSpanExtensions.ToFriendlyString(ts));
		}

		[Theory]
		[InlineData(0, "0 seconds")]
		[InlineData(1, "1 second")]
		[InlineData(2, "2 seconds")]
		[InlineData(10, "10 seconds")]
		public void ToFriendlyString_WhenLessThanOneMinute_ShowAsSeconds(int seconds, string expected)
		{
			var ts = TimeSpan.FromSeconds(seconds);

			var actual = TimeSpanExtensions.ToFriendlyString(ts);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(0, "0 seconds")]
		[InlineData(1, "1 minute")]
		[InlineData(2, "2 minutes")]
		[InlineData(10, "10 minutes")]
		public void ToFriendlyString_WhenWholeMinutesLessThanOneHour_ShowAsMinutesAndSeconds(int minutes, string expected)
		{
			var ts = TimeSpan.FromMinutes(minutes);

			var actual = TimeSpanExtensions.ToFriendlyString(ts);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(2d, 30d, "2 minutes 30 seconds")]
		[InlineData(1d, 30d, "1 minute 30 seconds")]
		[InlineData(1d, 1d, "1 minute 1 second")]
		public void ToFriendlyString_MinutesAndSecondsLessThanOneHour_ShowAsMinutesAndSeconds(int minutes, int seconds, string expected)
		{
			var ts = TimeSpan.FromMinutes(minutes) + TimeSpan.FromSeconds(seconds);

			var actual = TimeSpanExtensions.ToFriendlyString(ts);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(1d, "1 hour")]
		[InlineData(2d, "2 hours")]
		[InlineData(10d, "10 hours")]
		public void ToFriendlyString_WholeHours_ShowAsMinutesAndSeconds(int hours, string expected)
		{
			var ts = TimeSpan.FromHours(hours);

			var actual = TimeSpanExtensions.ToFriendlyString(ts);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(1d, 1d, "1 hour 1 minute")]
		[InlineData(2d, 30d, "2 hours 30 minutes")]
		[InlineData(10d, 1d, "10 hours 1 minute")]
		public void ToFriendlyString_HoursAndMinutes_ShowAsMinutesAndSeconds(int hours, int minutes, string expected)
		{
			var ts = new TimeSpan(hours, minutes, 0);

			var actual = TimeSpanExtensions.ToFriendlyString(ts);

			Assert.Equal(expected, actual);
		}
	}
}
