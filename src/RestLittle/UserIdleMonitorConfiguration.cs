// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle
{
	/// <summary>
	/// Configuration for service <see cref="UserIdleMonitor"/>.
	/// </summary>
	public class UserIdleMonitorConfiguration
	{
		/// <summary>
		/// Gets or sets the minimum amount of time without using the computer that is considered to be idle.
		/// </summary>
		public TimeSpan MinTimeToIdle { get; set; }
	}
}
