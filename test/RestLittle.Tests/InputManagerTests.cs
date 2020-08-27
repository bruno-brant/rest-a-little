using System;
using Xunit;

namespace RestLittle.Tests
{
	public class InputManagerTests
	{
		[Fact]
		public void GetLastInputTime_ReturnsValidDateTime()
		{
			var sut = new InputManager();
			var time = sut.GetLastInputTime();

			// BUG: this may fail when running in CI
			Assert.InRange(time, DateTime.Now.AddDays(-1), DateTime.Now);
		}
	}
}
