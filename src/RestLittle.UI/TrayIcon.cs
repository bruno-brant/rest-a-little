using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestLittle.UI
{
	public partial class TrayIcon : UserControl
	{
		/// <summary>
		/// The current status of the user.
		/// </summary>
		private UserStatus _status;

		/// <summary>
		/// The user asked the application to close.
		/// </summary>
		public event EventHandler Close;

		public TrayIcon()
		{
			InitializeComponent();

			_exitMenuItem.Click += new EventHandler(ExitMenuItem_Click);
		}

		private void ExitMenuItem_Click(object sender, EventArgs e)
		{
			Close(this, e);
		}

		//
		// Summary:
		//     Displays a balloon tip with the specified title, text, and icon in the taskbar
		//     for the specified time period.
		//
		// Parameters:
		//   timeout:
		//     The time period, in milliseconds, the balloon tip should display. This parameter
		//     is deprecated as of Windows Vista. Notification display times are now based on
		//     system accessibility settings.
		//
		//   tipTitle:
		//     The title to display on the balloon tip.
		//
		//   tipText:
		//     The text to display on the balloon tip.
		//
		//   tipIcon:
		//     One of the System.Windows.Forms.ToolTipIcon values.
		//
		// Exceptions:
		//   T:System.ArgumentOutOfRangeException:
		//     timeout is less than 0.
		//
		//   T:System.ArgumentException:
		//     tipText is null or an empty string.
		//
		//   T:System.ComponentModel.InvalidEnumArgumentException:
		//     tipIcon is not a member of System.Windows.Forms.ToolTipIcon.
		public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
		{
			notifyIcon1.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
		}

		public void SetStatus(string text)
		{
			notifyIcon1.Text = text;
		}

		public UserStatus Status
		{
			get => _status;

			set
			{
				_status = value;

				OnStatusChanged(_status);
			}
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
			var obj = Resource.ResourceManager.GetObject(name);

			if (obj == null)
			{
				throw new Exception("Couldn't find the icon.");
			}

			return (Icon)obj;
		}
	}
}