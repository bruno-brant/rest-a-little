// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Windows.Forms;

namespace RestLittle.UI
{
	/// <summary>
	/// This class acts like a controller for the application.
	/// </summary>
	public class RestLittleApplicationContext : ApplicationContext
	{
		private readonly TrayIcon _trayIcon = new TrayIcon();
		private readonly Timer _timer = new Timer
		{
			Interval = 2000,
		};

		// bootstrap monitor
		private readonly RestingMonitor _restingMonitor =
				new RestingMonitor(
					new RestingMonitorConfiguration
					{
						InitialStatus = RestLittle.UserStatus.Idle,
						MaxBusyTime = TimeSpan.FromMinutes(30),
						RestTimePerBusyTime = TimeSpan.FromMinutes(5),
					},
					new UserIdleMonitor(
						new UserIdleMonitorConfiguration
						{
							MinTimeToIdle = TimeSpan.FromSeconds(45),
						},
						new InputManager()));
		
		/// <summary>
		/// The time when we last issued an warning.
		/// </summary>
		private DateTime _lastWarning = DateTime.MinValue;
		
		/// <summary>
		/// How long to wait between intervals.
		/// </summary>
		private readonly TimeSpan _warningInterval = TimeSpan.FromSeconds(30);

		public RestLittleApplicationContext()
		{
			_trayIcon.Close += TrayIcon_Close;

			_timer.Tick += Timer_Tick;

			_timer.Start();
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			_restingMonitor.Update(TimeSpan.FromMilliseconds(_timer.Interval));

			var busyElapsed = _restingMonitor.TotalBusyTimeSinceRested.ToString(@"mm\:ss");
			var idleElapsed = _restingMonitor.TotalIdleTimeSinceRested.ToString(@"mm\:ss"); 

			switch (_restingMonitor.LastStatus)
			{
				case RestLittle.UserStatus.Idle:
					_trayIcon.Status = UserStatus.Resting;
					_trayIcon.SetStatus($"User has been idle for {idleElapsed}.");
					break;

				case RestLittle.UserStatus.Busy:
					_trayIcon.Status = UserStatus.Active;
					_trayIcon.SetStatus($"User has been busy for {busyElapsed}.");
					break;

				default:
					throw new Exception($"unknown status '{_restingMonitor.LastStatus}'");
			}

			if (_restingMonitor.MustRest && _lastWarning.UntilNow() > _warningInterval)
			{
				var tipText = $"Please go rest! You haven't rested for {busyElapsed} minutes.";
				_trayIcon.ShowBalloonTip(5000, "Must rest!", tipText, ToolTipIcon.Warning);

				_lastWarning = DateTime.Now;
			}
		}

		private void TrayIcon_Close(object sender, EventArgs e)
		{
			_timer.Stop();
			ExitThread();
		}

		protected override void Dispose(bool disposing)
		{
			_trayIcon.Dispose();
			_timer.Dispose();

			base.Dispose(disposing);
		}
	}
}