// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle
{
	/// <summary>
	/// Object that monitors user interaction and knows for how long he's been
	/// working and how long he's rested.
	/// </summary>
	public interface IRestingMonitor
	{
		/// <summary>
		/// Gets the last status of the user.
		/// </summary>
		InteractionStatus LastStatus { get; }

		/// <summary>
		/// Gets a value indicating whether the user must rest.
		/// </summary>
		bool MustRest { get; }

		/// <summary>
		/// Gets time passed since the last status is the current.
		/// </summary>
		TimeSpan TimeSinceLastStatus { get; }

		/// <summary>
		///     Gets the accumulated WORKING time since the user was last considered rested.
		/// </summary>
		TimeSpan TotalBusyTimeSinceRested { get; }

		/// <summary>
		/// Gets the accumulated RESTING time since the user was last considered rested.
		/// </summary>
		TimeSpan TotalIdleTimeSinceRested { get; }

		/// <summary>
		///     Updates the current status.
		/// </summary>
		/// <param name="elapsed">
		///     How much time elapsed since the last call to <see cref="Update(TimeSpan)"/>.
		/// </param>
		void Update(TimeSpan elapsed);
	}
}
