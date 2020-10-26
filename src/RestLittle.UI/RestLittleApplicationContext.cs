// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Windows.Forms;
using RestLittle.UI.Views;

namespace RestLittle.UI
{
	/// <summary>
	/// This class acts like a controller for the application.
	/// </summary>
	public class RestLittleApplicationContext : ApplicationContext
	{
		private readonly TrayIconView _trayIconView;

		/// <summary>
		/// Initializes a new instance of the <see cref="RestLittleApplicationContext"/> class.
		/// </summary>
		public RestLittleApplicationContext()
		{
			var restingMonitor = new RestingMonitor(
				Settings.Default,
				new UserIdleMonitor(Settings.Default, new InputManager()));

			_trayIconView = new TrayIconView(restingMonitor);
			_trayIconView.ExitClicked += TrayIconPresenter_ExitClicked;
		}

		/// <inheritdoc/>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_trayIconView.Dispose();
			}

			base.Dispose(disposing);
		}

		private void TrayIconPresenter_ExitClicked(object sender, EventArgs e)
		{
			ExitThread();
		}
	}
}
