// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle
{
	/// <inheritdoc/>
	public class UserIdleMonitor : IUserIdleMonitor
	{
		/// <summary>
		/// After this interval of idleness, the user is considered idle.
		/// </summary>
		private readonly TimeSpan _minTimeToIdle;

		/// <summary>
		/// Dependency. Used to obtain the last time the user interacted with the computer.
		/// </summary>
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
			_minTimeToIdle = configuration?.MinTimeToIdle ?? throw new ArgumentNullException(nameof(configuration));
			_inputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
		}

		/// <inheritdoc/>
		public UserStatus GetStatus()
		{
			var lastTime = _inputManager.GetLastInputTime();

			var timeSinceLastInput = DateTime.Now - lastTime;

			return timeSinceLastInput > _minTimeToIdle
				? UserStatus.Idle
				: UserStatus.Busy;
		}
	}
}
