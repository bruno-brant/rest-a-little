// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Collections.Generic;

namespace RestLittle
{
	/// <summary>
	/// Checks whether the user is properly rested.
	/// </summary>
	public class RestingMonitor
	{
		private readonly IRestingMonitorConfiguration _configuration;
		private readonly IUserIdleMonitor _userIdleMonitor;

		/// <summary>
		///     Holds the statuses for elapsed time.
		/// </summary>
		/// <remarks>
		///     While a dictionary is an overkill here, I used it to simplify my code
		///     by being able to call _elapsed[Status] instead of having to switch
		///     all the time.
		/// </remarks>
		private readonly Dictionary<UserStatus, TimeSpan> _elapsedTimeSinceRested = new Dictionary<UserStatus, TimeSpan>
		{
			[UserStatus.Busy] = TimeSpan.Zero,
			[UserStatus.Idle] = TimeSpan.Zero,
		};

		/// <summary>
		///     Initializes a new instance of the <see cref="RestingMonitor"/> class.
		/// </summary>
		/// <param name="configuration">
		///     Configuration of this service.
		/// </param>
		/// <param name="userIdleMonitor">
		///     Used to check whether the user is currently idle or is busy.
		/// </param>
		public RestingMonitor(IRestingMonitorConfiguration configuration, IUserIdleMonitor userIdleMonitor)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_userIdleMonitor = userIdleMonitor ?? throw new ArgumentNullException(nameof(userIdleMonitor));

			LastStatus = configuration.InitialStatus;
		}

		/// <summary>
		/// Gets the last status of the user.
		/// </summary>
		public UserStatus LastStatus { get; private set; }

		/// <summary>
		/// Gets time passed since the last status is the current.
		/// </summary>
		public TimeSpan TimeSinceLastStatus => _elapsedTimeSinceRested[LastStatus];

		/// <summary>
		/// Gets the accumulated RESTING time since the user was last considered rested.
		/// </summary>
		/// <remarks>
		/// User is considered rested once he's idle for at least
		/// <see cref="IRestingMonitorConfiguration.RestingTime"/>.
		/// </remarks>
		public TimeSpan TotalIdleTimeSinceRested => _elapsedTimeSinceRested[UserStatus.Idle];

		/// <summary>
		///     Gets the accumulated WORKING time since the user was last considered rested.
		/// </summary>
		/// <remarks>
		///     User is considered rested once he's idle for at least
		///     <see cref="IRestingMonitorConfiguration.RestingTime"/>.
		/// </remarks>
		public TimeSpan TotalBusyTimeSinceRested => _elapsedTimeSinceRested[UserStatus.Busy];

		/// <summary>
		/// Gets a value indicating whether the user must rest.
		/// </summary>
		public bool MustRest => TotalBusyTimeSinceRested > _configuration.MaxBusyTime;

		/// <summary>
		///     Updates the current status.
		/// </summary>
		/// <param name="elapsed">
		///     How much time elapsed since the last call to <see cref="Update(TimeSpan)"/>.
		/// </param>
		public void Update(TimeSpan elapsed)
		{
			var currentStatus = _userIdleMonitor.GetStatus();

			// skip updating the elapsed time if this is the first time we get this status
			// in this way, we avoid updating when the user was partially on the previous status
			if (currentStatus == LastStatus)
			{
				_elapsedTimeSinceRested[currentStatus] += elapsed;
			}

			if (_elapsedTimeSinceRested[UserStatus.Busy] == TimeSpan.Zero
				|| TotalIdleTimeSinceRested >= _configuration.RestingTime)
			{
				_elapsedTimeSinceRested[UserStatus.Busy] = TimeSpan.Zero;
				_elapsedTimeSinceRested[UserStatus.Idle] = TimeSpan.Zero;
			}

			LastStatus = currentStatus;
		}
	}
}
