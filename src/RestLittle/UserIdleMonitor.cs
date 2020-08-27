// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle
{
	/// <inheritdoc/>
	public class UserIdleMonitor : IUserIdleMonitor
	{
		private readonly UserIdleMonitorConfiguration _configuration;
		private readonly IInputManager _inputManager;

		/// <summary>
		///     Initializes a new instance of the <see cref="UserIdleMonitor"/> class.
		/// </summary>
		/// <param name="configuration">
		///     The configuration for the monitor.
		/// </param>
		/// <param name="inputManager">
		///     Allows to check the last time the user has used an input device.
		/// </param>
		public UserIdleMonitor(UserIdleMonitorConfiguration configuration, IInputManager inputManager)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_inputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
		}

		/// <inheritdoc/>
		public UserStatus GetStatus()
		{
			var lastTime = _inputManager.GetLastInputTime();

			var timeSinceLastInput = DateTime.Now - lastTime;

			if (timeSinceLastInput > _configuration.MinTimeToIdle)
			{
				return UserStatus.Idle;
			}
			else
			{
				return UserStatus.Busy;
			}
		}
	}
}
