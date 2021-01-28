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
		///    <see cref="InteractionStatus.Busy"/> if the user is using the computer,
		///    <see cref="InteractionStatus.Idle"/> otherwise.
		/// </returns>
		InteractionStatus GetStatus();
	}
}
