using AutoFixture.Xunit2;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RestLittle.Tests
{
	public class RestingMonitorTests
	{
		private readonly IUserIdleMonitor _userIdleMonitor = Substitute.For<IUserIdleMonitor>();

		[Theory, AutoData]
		public void Update_StatusUnchanged_TotalTimeEqualsElapsed(RestingMonitorConfiguration configuration, TimeSpan elapsed)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			_userIdleMonitor.GetStatus().Returns(configuration.InitialStatus);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			sut.Update(elapsed);

			Assert.Equal(configuration.InitialStatus, sut.LastStatus);
			Assert.Equal(elapsed, sut.TimeSinceLastStatus);
		}

		[Theory, AutoData]
		public void Update_StatusChanged_TotalTimeEqualsZero(RestingMonitorConfiguration configuration, TimeSpan elapsed)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			var otherStatus = configuration.InitialStatus switch
			{
				UserStatus.Busy => UserStatus.Idle,
				UserStatus.Idle => UserStatus.Busy,
				_ => throw new NotImplementedException()
			};

			_userIdleMonitor.GetStatus().Returns(otherStatus);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			sut.Update(elapsed);

			Assert.Equal(otherStatus, sut.LastStatus);
			Assert.Equal(TimeSpan.Zero, sut.TimeSinceLastStatus);
		}

		[Theory, AutoData]
		public void Update_StatusChanged_TotalTimeEqualsSum(RestingMonitorConfiguration configuration, TimeSpan[] elapseds)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			if (elapseds is null)
			{
				throw new ArgumentNullException(nameof(elapseds));
			}

			var otherStatus = configuration.InitialStatus switch
			{
				UserStatus.Busy => UserStatus.Idle,
				UserStatus.Idle => UserStatus.Busy,
				_ => throw new NotImplementedException()
			};

			_userIdleMonitor.GetStatus().Returns(otherStatus);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			foreach (var elapsed in elapseds)
			{
				sut.Update(elapsed);
			}

			Assert.Equal(otherStatus, sut.LastStatus);
			// Skip the first one when the time zeroes out.
			Assert.Equal(elapseds.Skip(1).Aggregate(TimeSpan.Zero, (acc, cur) => acc + cur), sut.TimeSinceLastStatus);
		}

		[Theory, AutoData]
		public void Update_WhenInitialStatusIsBusyAndThenBusyAgain_TotalBusyTimeIsElapsed(RestingMonitorConfiguration configuration, TimeSpan elapsed)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			configuration.InitialStatus = UserStatus.Busy;
			_userIdleMonitor.GetStatus().Returns(UserStatus.Busy);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			sut.Update(elapsed);

			Assert.Equal(elapsed, sut.TotalBusyTimeSinceRested);
		}

		[Theory, AutoData]
		public void Update_WhenInitialStatusIsIdleAndThenIdleAgain_TotalIdleTimeIsElapsed(RestingMonitorConfiguration configuration, TimeSpan elapsed)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			configuration.InitialStatus = UserStatus.Idle;
			_userIdleMonitor.GetStatus().Returns(UserStatus.Idle);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			sut.Update(elapsed);

			Assert.Equal(TimeSpan.Zero, sut.TotalIdleTimeSinceRested);
		}

		[Fact]
		public void Update_WhenTotalIdleTimeLargerThanRestTimePerBusyTime_TotalBusyTimeIsZero()
		{
			var configuration = new RestingMonitorConfiguration
			{
				InitialStatus = UserStatus.Busy,
				MaxBusyTime = TimeSpan.MaxValue,
				RestTimePerBusyTime = TimeSpan.FromSeconds(3),
			};

			var busyTime = TimeSpan.FromSeconds(30);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			// Adds lots of busy time
			_userIdleMonitor.GetStatus().Returns(UserStatus.Busy);
			sut.Update(busyTime);

			// now rest for a while
			foreach (var _ in Enumerable.Range(0, 10))
			{
				_userIdleMonitor.GetStatus().Returns(UserStatus.Idle);
				sut.Update(TimeSpan.FromSeconds(1));
			}

			Assert.Equal(UserStatus.Idle, sut.LastStatus);
			Assert.Equal(TimeSpan.Zero, sut.TotalBusyTimeSinceRested);
			Assert.Equal(TimeSpan.Zero, sut.TotalIdleTimeSinceRested);
		}

		[Fact]
		public void Update_WhenTotalBusyIsZeroAndTotalIdleNotZeroAndUserIsNowBusy_TotalIdleIsZero()
		{
			var configuration = new RestingMonitorConfiguration
			{
				InitialStatus = UserStatus.Busy,
				MaxBusyTime = TimeSpan.MaxValue,
				RestTimePerBusyTime = TimeSpan.FromSeconds(3),
			};

			var busyTime = TimeSpan.FromSeconds(30);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			// Adds lots of busy time
			_userIdleMonitor.GetStatus().Returns(UserStatus.Busy);
			sut.Update(busyTime);

			// now rest for a while
			_userIdleMonitor.GetStatus().Returns(UserStatus.Idle);
			sut.Update(TimeSpan.FromSeconds(10));

			// and now get busy
			_userIdleMonitor.GetStatus().Returns(UserStatus.Busy);
			sut.Update(TimeSpan.FromSeconds(1));

			Assert.Equal(UserStatus.Busy, sut.LastStatus);
			Assert.Equal(TimeSpan.Zero, sut.TotalIdleTimeSinceRested);
		}

		[Theory, AutoData]
		public void Update_WhenTotalBusyLargerThanMaxBusyTime_ThenMustRestIsTrue(RestingMonitorConfiguration configuration)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			_userIdleMonitor.GetStatus().Returns(UserStatus.Busy);
			configuration.InitialStatus = UserStatus.Busy;

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			// Adds lots of busy time
			sut.Update(configuration.MaxBusyTime + TimeSpan.FromSeconds(1));

			Assert.True(sut.MustRest);
		}

		[Theory, AutoData]
		public void Update_WhenTotalBusyLesserThanMaxBusyTime_ThenMustRestIsFalse(RestingMonitorConfiguration configuration)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			_userIdleMonitor.GetStatus().Returns(UserStatus.Busy);
			configuration.InitialStatus = UserStatus.Busy;

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			// Adds lots of busy time
			sut.Update(configuration.MaxBusyTime + new TimeSpan(-1));

			Assert.False(sut.MustRest);
		}
	}
}
