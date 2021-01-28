// Copyright (c) Bruno Brant. All rights reserved.

using System;
using NSubstitute;
using Xunit;

namespace RestLittle.Tests
{
	public class UserIdleMonitorTests
	{
		[Theory, AutoSubstituteData]
		public void Update_WhenLastTimeIsLessThanTimeToIdle_StatusIsBusy(IUserIdleMonitorConfiguration configuration)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			var returnThis = DateTime.Now - (configuration.TimeToIdle - TimeSpan.FromMinutes(1));
			var inputManager = Substitute.For<IInputManager>();

			inputManager.GetLastInputTime()
				.Returns(returnThis);

			var sut = new UserIdleMonitor(configuration, inputManager);

			var actual = sut.GetStatus();

			Assert.Equal(InteractionStatus.Busy, actual);
		}

		[Theory, AutoSubstituteData]
		public void Update_WhenLastTimeIsLargerThanMaxBusyTime_StatusIsIdle(IUserIdleMonitorConfiguration configuration)
		{
			if (configuration is null)
			{
				throw new ArgumentNullException(nameof(configuration));
			}

			var returnThis = DateTime.Now - (configuration.TimeToIdle + TimeSpan.FromMinutes(1));

			var inputManager = Substitute.For<IInputManager>();

			inputManager
				.GetLastInputTime()
				.Returns(returnThis);

			var sut = new UserIdleMonitor(configuration, inputManager);

			var actual = sut.GetStatus();

			Assert.Equal(InteractionStatus.Idle, actual);
		}
	}
}
