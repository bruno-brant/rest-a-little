// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using RestLittle.UI.Models;
using RestLittle.UI.Views;

namespace RestLittle.UI.Presenters
{
	/// <summary>
	/// A Presenter from MVP for the TrayIconView.
	/// </summary>
	public class TrayIconPresenter
	{
		/// <summary>
		/// The model that this presenter uses.
		/// </summary>
		private readonly TrayIconModel _restingMonitorModel;

		/// <summary>
		/// The view controlled by this presenter.
		/// </summary>
		private readonly ITrayIconView _trayIconView;

		/// <summary>
		/// How long to wait between intervals.
		/// </summary>
		private readonly TimeSpan _warningInterval = TimeSpan.FromSeconds(30);

		/// <summary>
		/// The last time the warning was displayed.
		/// </summary>
		private DateTime _lastWarning = DateTime.Now;

		/// <summary>
		/// Initializes a new instance of the <see cref="TrayIconPresenter"/> class.
		/// </summary>
		/// <param name="trayIconView">
		/// View that controls the tray icon.
		/// </param>
		/// <param name="restingMonitorModel">The model controlled by this presenter.</param>
		public TrayIconPresenter(ITrayIconView trayIconView, TrayIconModel restingMonitorModel)
		{
			_trayIconView = trayIconView ?? throw new ArgumentNullException(nameof(trayIconView));
			_restingMonitorModel = restingMonitorModel ?? throw new ArgumentNullException(nameof(restingMonitorModel));

			_restingMonitorModel.RestingMonitorUpdated += RestingMonitorModel_RestingMonitorUpdated;

			_trayIconView.ShowConfigurationClicked += TrayIconView_ShowConfigurationClicked;
			_trayIconView.PauseUnpauseClicked += TrayIconView_PauseUnpauseClicked;
			_trayIconView.ShowAboutClicked += TrayIconView_ShowAboutClicked;
		}

		/// <summary>
		/// Raised when the user has expressed the desire to exit the application.
		/// </summary>
		public event EventHandler ExitClicked
		{
			add { _trayIconView.ExitClicked += value; }
			remove { _trayIconView.ExitClicked -= value; }
		}

		/// <summary>
		/// Get the corresponding icon.
		/// </summary>
		/// <param name="status">Current status.</param>
		/// <returns>The correponding icon.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// When the status has no corresponding icon.
		/// </exception>
		private static UserStatusIcon GetIcon(UserStatus status)
		{
			// TODO: this should be moved to UserStatusIcon
			return status switch
			{
				UserStatus.Idle => UserStatusIcon.Resting,
				UserStatus.Busy => UserStatusIcon.Active,
				_ => throw new ArgumentOutOfRangeException(nameof(status), status, $"No corresponding icon."),
			};
		}

		private void TrayIconView_ShowConfigurationClicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void TrayIconView_ShowAboutClicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void TrayIconView_PauseUnpauseClicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void RestingMonitorModel_RestingMonitorUpdated(object sender, EventArgs e)
		{
			var busyElapsed = _restingMonitorModel.BusyTimeSinceRested.ToFriendlyString();
			var idleElapsed = _restingMonitorModel.IdleTimeSinceRested.ToFriendlyString();
			var icon = GetIcon(_restingMonitorModel.LastStatus);

			if (_trayIconView.Icon != icon.ToIcon())
			{
				_trayIconView.Icon = icon;
			}

			_trayIconView.Status = $"You've been busy for {busyElapsed} and idle for {idleElapsed}".Truncate(63, true);

			if (_restingMonitorModel.MustRest
				&& _lastWarning.UntilNow() > _warningInterval)
			{
				var tipText = $"Please go rest! You haven't rested for {busyElapsed}.";
				_trayIconView.ShowBalloonTip(5000, "Must rest!", tipText, ToolTipIcon.Warning);

				_lastWarning = DateTime.Now;
			}
		}

		/// <summary>
		///     Checks whether the user is idle.
		/// </summary>
		/// <returns>
		///     True if the user is idle, false otherwise.
		/// </returns>
		private bool IsIdle()
		{
			return _restingMonitorModel.LastStatus == UserStatus.Idle;
		}
	}
}
