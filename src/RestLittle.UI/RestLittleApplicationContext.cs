// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Windows.Forms;
using RestLittle.UI.Models;
using RestLittle.UI.Presenters;
using RestLittle.UI.Views;

namespace RestLittle.UI
{
	/// <summary>
	/// This class acts like a controller for the application.
	/// </summary>
	public class RestLittleApplicationContext : ApplicationContext
	{
		private readonly TrayIconView _trayIconView;
		private readonly TrayIconModel _trayIconModel;
		private readonly TrayIconPresenter _trayIconPresenter;

		/// <summary>
		/// Initializes a new instance of the <see cref="RestLittleApplicationContext"/> class.
		/// </summary>
		public RestLittleApplicationContext()
		{
			_trayIconView = new TrayIconView();
			_trayIconModel = new TrayIconModel();
			_trayIconPresenter = new TrayIconPresenter(_trayIconView, _trayIconModel);

			_trayIconPresenter.ExitClicked += TrayIconPresenter_ExitClicked;
		}

		/// <inheritdoc/>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_trayIconView.Dispose();
				_trayIconModel.Dispose();
			}

			base.Dispose(disposing);
		}

		private void TrayIconPresenter_ExitClicked(object sender, EventArgs e)
		{
			ExitThread();
		}
	}
}
