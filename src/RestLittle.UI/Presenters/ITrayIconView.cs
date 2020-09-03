// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestLittle.UI.Presenters
{
	/// <summary>
	/// The view's contract.
	/// </summary>
	public interface ITrayIconView
	{
		/// <summary>
		/// Raised whenever the user express the desire to show the configuration view.
		/// </summary>
		event EventHandler ShowConfigurationClicked;

		/// <summary>
		/// Raised whenever the user express the desire to show the About view.
		/// </summary>
		event EventHandler ShowAboutClicked;

		/// <summary>
		/// Raised whenever the user express the desire to pause or unpause the timer.
		/// </summary>
		event EventHandler PauseUnpauseClicked;

		/// <summary>
		/// Raised whenever the user express the desire to close the application.
		/// </summary>
		event EventHandler ExitClicked;

		/// <summary>
		/// Gets or sets the current icon that appears on the system tray.
		/// </summary>
		Icon Icon { get; set; }

		/// <summary>
		/// Gets or sets the text that's displayed when the user hovers the
		/// mouse over the icon.
		/// </summary>
		string Status { get; set; }

		/// <summary>
		///     Displays a balloon tip with the specified title, text, and icon in the taskbar
		///     for the specified time period.
		/// </summary>
		/// <param name="timeout">
		///     The time period, in milliseconds, the balloon tip should display. This parameter
		///     is deprecated as of Windows Vista. Notification display times are now based on
		///     system accessibility settings.
		/// </param>
		/// <param name="tipTitle">
		///     The title to display on the balloon tip.
		/// </param>
		/// <param name="tipText">
		///     The text to display on the balloon tip.
		/// </param>
		/// <param name="tipIcon">
		///     One of the System.Windows.Forms.ToolTipIcon values.
		/// </param>
		/// <exception cref="System.ArgumentOutOfRangeException">
		///     Timeout is less than 0.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		///     tipText is null or an empty string.
		/// </exception>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">
		///     tipIcon is not a member of System.Windows.Forms.ToolTipIcon.
		/// </exception>
		void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon);
	}
}
