// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle.UI.Presenters
{
	/// <summary>
	/// Represents a view of the configuration of the application.
	/// </summary>
	public interface IConfigurationFormView : IDisposable
	{
		/// <summary>
		///    Occurs before a form is displayed for the first time.
		/// </summary>
		event EventHandler Load;

		/// <summary>
		///    Occurs when the user cancels the form.
		/// </summary>
		event EventHandler Cancelled;

		/// <summary>
		///    Occurs when the user accepts the form.
		/// </summary>
		event EventHandler Accepted;

		/// <summary>
		/// Gets or sets how long the user must be inactive to be considered idle.
		/// </summary>
		TimeSpan TimeToIdle { get; set; }

		/// <summary>
		/// Gets or sets how long the user can be working without resting.
		/// </summary>
		TimeSpan MaxBusyTime { get; set; }

		/// <summary>
		/// Gets or sets how long should the user rest for each busy time session.
		/// </summary>
		TimeSpan RestingTime { get; set; }

		/// <summary>
		/// Gets or sets interval between warning calls.
		/// </summary>
		TimeSpan WarningInterval { get; set; }

		/// <summary>
		/// Closes the view.
		/// </summary>
		void Close();

		/// <summary>
		/// Sets an error of a property.
		/// </summary>
		/// <param name="name">The property name.</param>
		/// <param name="errorMessage">The error message of the property.</param>
		void SetError(string name, string errorMessage);
	}
}
