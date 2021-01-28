// Copyright (c) Bruno Brant. All rights reserved.

namespace RestLittle.UI.Models
{
	/// <summary>
	/// The status of the user.
	/// </summary>
	public enum UserStatus
	{
		/// <summary>
		/// User has been working for more than the max. time.
		/// </summary>
		Tired,

		/// <summary>
		/// User is rested and is active.
		/// </summary>
		Working,

		/// <summary>
		/// User is resting (away from the computer).
		/// </summary>
		Resting,
	}
}
