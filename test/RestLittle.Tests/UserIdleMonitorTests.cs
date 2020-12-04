// Copyright (c) Bruno Brant. All rights reserved.

using System;
using AutoFixture.Xunit2;
using NSubstitute;
using Xunit;

namespace RestLittle.Tests
{
	public class UserIdleMonitorTests
	{
		[Theory, AutoData]
		public void Update_WhenLastTimeIsLessThanTimeToIdle_StatusIsBusy(IUserIdleMonitorConfiguration configuration)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			var inputManager = Substitute.For<IInputManager>();
			inputManager.GetLastInputTime()
				.Returns(DateTime.Now - (configuration.TimeToIdle - TimeSpan.FromMinutes(1)));

			var sut = new UserIdleMonitor(configuration, inputManager);

			var actual = sut.GetStatus();

			Assert.Equal(UserStatus.Busy, actual);
		}

		[Theory, AutoData]
		public void Update_WhenLastTimeIsLargerThanMaxBusyTime_StatusIsIdle(IUserIdleMonitorConfiguration configuration)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			var inputManager = Substitute.For<IInputManager>();
			inputManager.GetLastInputTime()
				.Returns(DateTime.Now - (configuration.TimeToIdle + TimeSpan.FromMinutes(1)));

			var sut = new UserIdleMonitor(configuration, inputManager);

			var actual = sut.GetStatus();

			Assert.Equal(UserStatus.Idle, actual);
		}
	}
}
