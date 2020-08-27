// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle
{
	/// <summary>
	/// Configuration for <see cref="UserIdleMonitor"/>.
	/// </summary>
	public class RestingMonitorConfiguration
	{
		/// <summary>
		/// Gets or sets maximum amount of time using the computer without rest.
		/// </summary>
		public TimeSpan MaxBusyTime { get; set; }

		/// <summary>
		/// Gets or sets how long the user must rest in a interval of maxBusyTime.
		/// </summary>
		public TimeSpan RestTimePerBusyTime { get; set; }

		/// <summary>
		/// Gets or sets the initial status of the <see cref="RestingMonitor"/>.
		/// </summary>
		public UserStatus InitialStatus { get; set; }
	}
}
