// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Linq;
using AutoFixture.Xunit2;
using NSubstitute;
using Xunit;

namespace RestLittle.Tests
{
	public class RestingMonitorTests
	{
		private readonly IUserIdleMonitor _userIdleMonitor = Substitute.For<IUserIdleMonitor>();

		[Theory, AutoSubstituteData]
		public void Update_WhenStatusAlwaysBusy_TotalTimeEqualsElapsed(IRestingMonitorConfiguration configuration, TimeSpan elapsed)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			configuration.InitialStatus.Returns(InteractionStatus.Busy);
			configuration.MaxBusyTime.Returns(elapsed);
			configuration.RestingTime.Returns(TimeSpan.MaxValue);

			_userIdleMonitor.GetStatus().Returns(InteractionStatus.Busy);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			sut.Update(elapsed);

			Assert.Equal(configuration.InitialStatus, sut.LastStatus);
			Assert.Equal(elapsed, sut.TimeSinceLastStatus);
		}

		[Theory, AutoSubstituteData]
		public void Update_StatusChanged_TotalTimeEqualsZero(IRestingMonitorConfiguration configuration, TimeSpan elapsed)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			var otherStatus = configuration.InitialStatus switch
			{
				InteractionStatus.Busy => InteractionStatus.Idle,
				InteractionStatus.Idle => InteractionStatus.Busy,
				_ => throw new NotImplementedException()
			};

			_userIdleMonitor.GetStatus().Returns(otherStatus);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			sut.Update(elapsed);

			Assert.Equal(otherStatus, sut.LastStatus);
			Assert.Equal(TimeSpan.Zero, sut.TimeSinceLastStatus);
		}

		[Theory, AutoSubstituteData]
		public void Update_WhenStatusChanged_TotalTimeEqualsSum(IRestingMonitorConfiguration configuration, TimeSpan[] elapseds)
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
				InteractionStatus.Busy => InteractionStatus.Idle,
				InteractionStatus.Idle => InteractionStatus.Busy,
				_ => throw new NotImplementedException()
			};

			configuration.RestingTime.Returns(TimeSpan.MaxValue);

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

		[Theory, AutoSubstituteData]
		public void Update_WhenInitialStatusIsBusyAndThenBusyAgain_TotalBusyTimeIsElapsed(IRestingMonitorConfiguration configuration, TimeSpan elapsed)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			configuration.RestingTime.Returns(TimeSpan.MaxValue);
			configuration.InitialStatus.Returns(InteractionStatus.Busy);
			_userIdleMonitor.GetStatus().Returns(InteractionStatus.Busy);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			sut.Update(elapsed);

			Assert.Equal(elapsed, sut.TotalBusyTimeSinceRested);
		}

		[Theory, AutoSubstituteData]
		public void Update_WhenInitialStatusIsIdleAndThenIdleAgain_TotalIdleTimeIsZero(IRestingMonitorConfiguration configuration, TimeSpan elapsed)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			configuration.InitialStatus.Returns(InteractionStatus.Idle);
			_userIdleMonitor.GetStatus().Returns(InteractionStatus.Idle);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			sut.Update(elapsed);

			Assert.Equal(TimeSpan.Zero, sut.TotalIdleTimeSinceRested);
		}

		[Fact]
		public void Update_WhenTotalIdleTimeLargerThanRestTimePerBusyTime_TotalBusyTimeIsZero()
		{
			var configuration = Substitute.For<IRestingMonitorConfiguration>();

			configuration.InitialStatus.Returns(InteractionStatus.Busy);
			configuration.MaxBusyTime.Returns(TimeSpan.MaxValue);
			configuration.RestingTime.Returns(TimeSpan.FromSeconds(3));

			var busyTime = TimeSpan.FromSeconds(30);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			// Adds lots of busy time
			_userIdleMonitor.GetStatus().Returns(InteractionStatus.Busy);
			sut.Update(busyTime);

			// now rest for a while
			Enumerable
				.Range(0, 10)
				.ForEach(_ =>
				{
					_userIdleMonitor.GetStatus().Returns(InteractionStatus.Idle);
					sut.Update(TimeSpan.FromSeconds(1));
				});

			Assert.Equal(InteractionStatus.Idle, sut.LastStatus);
			Assert.Equal(TimeSpan.Zero, sut.TotalBusyTimeSinceRested);
			Assert.Equal(TimeSpan.Zero, sut.TotalIdleTimeSinceRested);
		}

		[Fact]
		public void Update_WhenTotalBusyIsZeroAndTotalIdleNotZeroAndUserIsNowBusy_TotalIdleIsZero()
		{
			var configuration = Substitute.For<IRestingMonitorConfiguration>();

			configuration.InitialStatus.Returns(InteractionStatus.Busy);
			configuration.MaxBusyTime.Returns(TimeSpan.MaxValue);
			configuration.RestingTime.Returns(TimeSpan.FromSeconds(3));

			var busyTime = TimeSpan.FromSeconds(30);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			// Adds lots of busy time
			_userIdleMonitor.GetStatus().Returns(InteractionStatus.Busy);
			sut.Update(busyTime);

			// now rest for a while
			_userIdleMonitor.GetStatus().Returns(InteractionStatus.Idle);
			sut.Update(TimeSpan.FromSeconds(10));

			// and now get busy
			_userIdleMonitor.GetStatus().Returns(InteractionStatus.Busy);
			sut.Update(TimeSpan.FromSeconds(1));

			Assert.Equal(InteractionStatus.Busy, sut.LastStatus);
			Assert.Equal(TimeSpan.Zero, sut.TotalIdleTimeSinceRested);
		}

		[Theory, AutoSubstituteData]
		public void Update_WhenTotalBusyLargerThanMaxBusyTime_ThenMustRestIsTrue(IRestingMonitorConfiguration configuration)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			configuration.InitialStatus.Returns(InteractionStatus.Busy);
			configuration.RestingTime.Returns(TimeSpan.MaxValue);

			_userIdleMonitor.GetStatus().Returns(InteractionStatus.Busy);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			// Adds lots of busy time
			sut.Update(configuration.MaxBusyTime + TimeSpan.FromSeconds(1));

			Assert.True(sut.MustRest);
		}

		[Theory, AutoSubstituteData]
		public void Update_WhenTotalBusyLesserThanMaxBusyTime_ThenMustRestIsFalse(IRestingMonitorConfiguration configuration)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			_userIdleMonitor.GetStatus().Returns(InteractionStatus.Busy);
			configuration.InitialStatus.Returns(InteractionStatus.Busy);

			var sut = new RestingMonitor(configuration, _userIdleMonitor);

			// Adds lots of busy time
			sut.Update(configuration.MaxBusyTime + new TimeSpan(-1));

			Assert.False(sut.MustRest);
		}
	}
}
