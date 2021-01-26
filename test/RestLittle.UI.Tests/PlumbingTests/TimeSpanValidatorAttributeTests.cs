// Copyright (c) Bruno Brant. All rights reserved.

using System;
using RestLittle.UI.Plumbing;
using Xunit;

namespace RestLittle.UI.Tests.PlumbingTests
{
	public class TimeSpanValidatorAttributeTests
	{
		[Fact]
		public void IsValid_WhenObjectIsNull_ReturnsTrue()
		{
			var sut = new TimeSpanValidatorAttribute();
			var actual = sut.IsValid(null);

			Assert.True(actual);
		}

		[Fact]
		public void IsValid_WhenObjectNotTimeSpan_ReturnsFalse()
		{
			var sut = new TimeSpanValidatorAttribute();
			var actual = sut.IsValid(new object());

			Assert.False(actual);
		}

		[Fact]
		public void IsValid_WhenOnlyMinValueSet_ThenAllowsAnyValueAboveIt()
		{
			var sut = new TimeSpanValidatorAttribute
			{
				MinValue = "00:00:01",
			};

			Assert.True(sut.IsValid(TimeSpan.FromSeconds(2)));
			Assert.True(sut.IsValid(TimeSpan.MaxValue));
			Assert.False(sut.IsValid(TimeSpan.FromSeconds(0)));
		}

		[Fact]
		public void IsValid_WhenOnlyMaxValueSet_ThenAllowsAnyValueBelowIt()
		{
			var sut = new TimeSpanValidatorAttribute
			{
				MinValue = "00:00:01",
			};

			Assert.True(sut.IsValid(TimeSpan.FromSeconds(2)));
			Assert.True(sut.IsValid(TimeSpan.MaxValue));
			Assert.False(sut.IsValid(TimeSpan.FromSeconds(0)));
		}

		[Fact]
		public void IsValid_WhenTimeSpanInRange_ReturnsTrue()
		{
			var sut = new TimeSpanValidatorAttribute
			{
				MinValue = TimeSpan.FromSeconds(1).ToString(),
				MaxValue = TimeSpan.FromSeconds(5).ToString(),
			};

			var actual = sut.IsValid(TimeSpan.FromSeconds(3));

			Assert.True(actual);
		}

		[Fact]
		public void IsValid_WhenTimeSpanOutsideRange_ReturnsFalse()
		{
			var sut = new TimeSpanValidatorAttribute
			{
				MinValue = TimeSpan.FromSeconds(1).ToString(),
				MaxValue = TimeSpan.FromSeconds(5).ToString(),
			};

			var actual = sut.IsValid(TimeSpan.FromSeconds(6));

			Assert.False(actual);
		}
	}
}
