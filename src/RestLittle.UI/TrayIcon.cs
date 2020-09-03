// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace RestLittle.UI
{
	/// <summary>
	/// Implements an icon that is available at the system tray to control the
	/// application.
	/// </summary>
	public partial class TrayIcon : UserControl
	{
		/// <summary>
		/// The current status of the user.
		/// </summary>
		private UserStatus _status;

		/// <summary>
		/// Initializes a new instance of the <see cref="TrayIcon"/> class.
		/// </summary>
		public TrayIcon()
		{
			InitializeComponent();

			_exitMenuItem.Click += new EventHandler(ExitMenuItem_Click);
		}

		/// <summary>
		/// The user asked the application to close.
		/// </summary>
		public event EventHandler Close;

		/// <summary>
		/// Gets or sets the current user status.
		/// </summary>
		public UserStatus Status
		{
			get => _status;

			set
			{
				_status = value;

				OnStatusChanged(_status);
			}
		}

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
		public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
		{
			notifyIcon1.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
		}

		/// <summary>
		/// Change the status text of the TrayIcon.
		/// </summary>
		/// <param name="text">
		/// The text to display when the mouse is over the icon.
		/// </param>
		public void SetStatus(string text)
		{
			notifyIcon1.Text = text;
		}

		private void OnStatusChanged(UserStatus newStatus)
		{
			notifyIcon1.Icon = newStatus switch
			{
				UserStatus.Tired => GetIcon("tired"),
				UserStatus.Active => GetIcon("busy"),
				UserStatus.Resting => GetIcon("resting"),
				_ => throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, "Unknown status."),
			};
		}

		private Icon GetIcon(string name)
		{
			var obj = Resource.ResourceManager.GetObject(name, CultureInfo.CurrentCulture);

			if (obj == null)
			{
				throw new Exception("Couldn't find the icon.");
			}

			return (Icon)obj;
		}

		private void ExitMenuItem_Click(object sender, EventArgs e)
		{
			Close(this, e);
		}
	}
}
