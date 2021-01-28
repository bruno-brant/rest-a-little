// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle
{
	/// <inheritdoc/>
	public class UserIdleMonitor : IUserIdleMonitor
	{
		/// <summary>
		/// Configurations for this service.
		/// </summary>
		private readonly IUserIdleMonitorConfiguration _configuration;

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
		public UserIdleMonitor(IUserIdleMonitorConfiguration configuration, IInputManager inputManager)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_inputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
		}

		/// <inheritdoc/>
		public InteractionStatus GetStatus()
		{
			var lastTime = _inputManager.GetLastInputTime();

			var timeSinceLastInput = DateTime.Now - lastTime;

			return timeSinceLastInput > _configuration.TimeToIdle
				? InteractionStatus.Idle
				: InteractionStatus.Busy;
		}
	}
}
