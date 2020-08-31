using System;
using System.Configuration;
using System.Windows.Forms;

namespace RestLittle.UI
{
	public class RestLittleApplicationContext : ApplicationContext
	{
		private readonly TrayIcon _trayIcon = new TrayIcon();
		private readonly Timer _timer = new Timer
		{
			Interval = 1000,
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

		public RestLittleApplicationContext()
		{
			_trayIcon.Close += TrayIcon_Close;

			_timer.Tick += Timer_Tick;

			_timer.Start();
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			_restingMonitor.Update(TimeSpan.FromMilliseconds(_timer.Interval));

			var busyMinutes = _restingMonitor.TotalBusyTimeSinceRested.TotalMinutes;
			var idleMinutes = _restingMonitor.TotalIdleTimeSinceRested.TotalMinutes;

			switch (_restingMonitor.LastStatus)
			{
				case RestLittle.UserStatus.Idle:
					_trayIcon.SetStatus($"User has been idle for {idleMinutes:N0} minutes.");
					break;

				case RestLittle.UserStatus.Busy:
					_trayIcon.SetStatus($"User has been busy for {idleMinutes:N0} minutes.");
					break;

				default:
					throw new Exception($"unknown status '{_restingMonitor.LastStatus}'");
			}

			if (_restingMonitor.MustRest)
			{
				var tipText = $"Please go rest! You haven't rested for {busyMinutes:N0} minutes.";
				_trayIcon.ShowBalloonTip(5000, "Must rest!", tipText, ToolTipIcon.Warning);
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