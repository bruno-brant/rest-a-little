// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle
{
	/// <summary>
	/// Configuration for <see cref="UserIdleMonitor"/>.
	/// </summary>
	public interface IRestingMonitorConfiguration
	{
		/// <summary>
		/// Gets maximum amount of time using the computer without rest.
		/// </summary>
		public TimeSpan MaxBusyTime { get; }

		/// <summary>
		/// Gets how long the user must rest in a interval of maxBusyTime.
		/// </summary>
		public TimeSpan RestingTime { get; }

		/// <summary>
		/// Gets the initial status of the <see cref="RestingMonitor"/>.
		/// </summary>
		public InteractionStatus InitialStatus { get; }
	}
}
