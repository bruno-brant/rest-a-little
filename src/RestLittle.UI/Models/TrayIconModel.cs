// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace RestLittle.UI.Models
{
	/// <summary>
	/// A model that encapsulates a <see cref="RestingMonitor"/>.
	/// </summary>
	public class TrayIconModel : IDisposable
	{
		/// <summary>
		/// Instance of _restingMonitor used by this class.
		/// </summary>
		private readonly RestingMonitor _restingMonitor =
				new RestingMonitor(
					new RestingMonitorConfiguration
					{
						InitialStatus = UserStatus.Idle,
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
		/// How long to wait between updates.
		/// </summary>
		private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(2);

		/// <summary>
		/// Run an action every interval.
		/// </summary>
		private readonly RepeatingEvent _updater;

		/// <summary>
		/// Used to control elapsed time that is fed into <see cref="RestingMonitor"/>.
		/// </summary>
		private readonly Stopwatch _stopwatch = new Stopwatch();

		/// <summary>
		/// Used to log information for diagnostics.
		/// </summary>
		private readonly ILogger<TrayIconModel> _logger;

		/// <summary>
		/// Whether the object is disposed.
		/// </summary>
		private bool _disposedValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="TrayIconModel"/> class.
		/// </summary>
		/// <param name="logger">Logs informational messages.</param>
		public TrayIconModel(ILogger<TrayIconModel> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));

			_stopwatch.Start();
			_updater = new RepeatingEvent(UpdateMonitor, _updateInterval);
			_updater.Start();

			logger.LogInformation($"Initializing loop with interval of {_updateInterval}");
		}

		/// <summary>
		/// Called whenever the resting monitor is updated.
		/// </summary>
		public event EventHandler RestingMonitorUpdated;

		/// <summary>
		/// Gets the accumulated WORKING time since the user was last considered rested.
		/// </summary>
		/// <remarks>
		/// User is considered rested once he's idle for at least
		/// <see cref="RestingMonitorConfiguration.RestTimePerBusyTime"/>.
		/// </remarks>
		public TimeSpan BusyTimeSinceRested => _restingMonitor.TotalBusyTimeSinceRested;

		/// <summary>
		/// Gets the accumulated RESTING time since the user was last considered rested.
		/// </summary>
		/// <remarks>
		/// User is considered rested once he's idle for at least
		/// <see cref="RestingMonitorConfiguration.RestTimePerBusyTime"/>.
		/// </remarks>
		public TimeSpan IdleTimeSinceRested => _restingMonitor.TotalIdleTimeSinceRested;

		/// <summary>
		/// Gets the last status of the user.
		/// </summary>
		public UserStatus LastStatus => _restingMonitor.LastStatus;

		/// <summary>
		/// Gets a value indicating whether the user must rest.
		/// </summary>
		public bool MustRest => _restingMonitor.MustRest;

		/// <inheritdoc/>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Dispose of managed and unmanaged resources.
		/// </summary>
		/// <param name="disposing">If managed state should be disposed.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
					_updater.Stop(TimeSpan.FromSeconds(5));
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~RestingMonitorModel()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		/// <summary>
		/// Ran when the timer ticks.
		/// </summary>
		private void UpdateMonitor()
		{
			_restingMonitor.Update(_stopwatch.Elapsed);
			_stopwatch.Restart();
			OnRestingMonitorUpdated();
		}

		/// <summary>
		/// Raises the RestingMonitorUpdated event.
		/// </summary>
		private void OnRestingMonitorUpdated()
		{
			var @event = RestingMonitorUpdated;

			if (@event != null)
			{
				RestingMonitorUpdated(this, new EventArgs());
			}
			else
			{
				_logger.LogWarning($"No subscribers for {nameof(RestingMonitorUpdated)}");
			}
		}
	}
}
