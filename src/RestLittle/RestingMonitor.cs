// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle
{
	/// <summary>
	/// Checks whether the user is properly rested.
	/// </summary>
	public class RestingMonitor
	{
		private readonly RestingMonitorConfiguration _configuration;
		private readonly IUserIdleMonitor _userIdleMonitor;

		/// <summary>
		///     Initializes a new instance of the <see cref="RestingMonitor"/> class.
		/// </summary>
		/// <param name="configuration">
		///     Configuration of this service.
		/// </param>
		/// <param name="userIdleMonitor">
		///     Used to check whether the user is currently idle or is busy.
		/// </param>
		public RestingMonitor(RestingMonitorConfiguration configuration, IUserIdleMonitor userIdleMonitor)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_userIdleMonitor = userIdleMonitor ?? throw new ArgumentNullException(nameof(userIdleMonitor));

			LastStatus = configuration.InitialStatus;
		}

		/// <summary>
		/// Gets current status of the user.
		/// </summary>
		public UserStatus LastStatus { get; private set; }

		/// <summary>
		/// Gets time passed since the last status is the current.
		/// </summary>
		public TimeSpan TimeSinceLastStatus { get; private set; }

		/// <summary>
		/// Gets the accumulated RESTING time since the user was last considered rested.
		/// </summary>
		/// <remarks>
		/// User is considered rested once he's idle for at least
		/// <see cref="RestingMonitorConfiguration.RestTimePerBusyTime"/>.
		/// </remarks>
		public TimeSpan TotalIdleTimeSinceRested { get; private set; }

		/// <summary>
		/// Gets the accumulated WORKING time since the user was last considered rested.
		/// </summary>
		/// <remarks>
		/// User is considered rested once he's idle for at least
		/// <see cref="RestingMonitorConfiguration.RestTimePerBusyTime"/>.
		/// </remarks>
		public TimeSpan TotalBusyTimeSinceRested { get; private set; }

		/// <summary>
		/// Gets a value indicating whether the user must rest.
		/// </summary>
		public bool MustRest => TotalBusyTimeSinceRested > _configuration.MaxBusyTime;

		/// <summary>
		///     Updates the current status.
		/// </summary>
		/// <param name="elapsed">
		///     How much time elapsed since the last update.
		/// </param>
		public void Update(TimeSpan elapsed)
		{
			var currentStatus = _userIdleMonitor.GetStatus();

			if (currentStatus == LastStatus)
			{
				TimeSinceLastStatus += elapsed;

				// since the user is on the same status, increments it properly
				IncrementStatus(currentStatus, elapsed);
			}
			else
			{
				TimeSinceLastStatus = TimeSpan.Zero;
			}

			UpdateAccumulators();

			LastStatus = currentStatus;
		}

		private void UpdateAccumulators()
		{
			if (TotalIdleTimeSinceRested >= _configuration.RestTimePerBusyTime)
			{
				TotalBusyTimeSinceRested = TimeSpan.Zero;
				TotalIdleTimeSinceRested = TimeSpan.Zero;
			}
		}

		/// <summary>
		///     Increments the timer accordingly to the provided status.
		/// </summary>
		/// <param name="status">
		///     The current status of the user.
		/// </param>
		/// <param name="elapsed">
		///     The amount of time the user has elapsed on this status.
		/// </param>
		private void IncrementStatus(UserStatus status, TimeSpan elapsed)
		{
			switch (status)
			{
				case UserStatus.Idle:
					TotalIdleTimeSinceRested += elapsed;
					break;

				case UserStatus.Busy:
					TotalBusyTimeSinceRested += elapsed;
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(elapsed), elapsed, "Unknown status");
			}
		}
	}
}
