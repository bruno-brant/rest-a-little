// Copyright (c) Bruno Brant. All rights reserved.

namespace RestLittle
{
	/// <summary>
	/// Monitors if the user has rested a little in a certain interval.
	/// </summary>
	public interface IUserIdleMonitor
	{
		/// <summary>
		///     Gets the current usage status of the computer.
		/// </summary>
		/// <returns>
		///    <see cref="UserStatus.Busy"/> if the user is using the computer,
		///    <see cref="UserStatus.Idle"/> otherwise.
		/// </returns>
		UserStatus GetStatus();
	}
}
