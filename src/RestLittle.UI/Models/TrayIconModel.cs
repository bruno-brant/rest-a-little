// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.ComponentModel;
using System.Diagnostics;
using RestLittle.UI.Plumbing;

namespace RestLittle.UI.Models
{
	/// <summary>
	/// A model that encapsulates a <see cref="RestingMonitor"/>.
	/// </summary>
	public class TrayIconModel : IDisposable, IComponent
	{
		/// <summary>
		/// Instance of _restingMonitor used by this class.
		/// </summary>
		private readonly IRestingMonitor _restingMonitor;

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
		/// Whether the object is disposed.
		/// </summary>
		private bool _disposedValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="TrayIconModel"/> class.
		/// </summary>
		/// <param name="restingMonitor">Service that monitors user resting time.</param>
		public TrayIconModel(IRestingMonitor restingMonitor)
		{
			_stopwatch.Start();
			_updater = new RepeatingEvent(UpdateMonitor, _updateInterval);
			_updater.Start();

			_restingMonitor = restingMonitor ?? throw new ArgumentNullException(nameof(restingMonitor));
		}

		/// <summary>
		/// Called whenever the resting monitor is updated.
		/// </summary>
		public event EventHandler RestingMonitorUpdated;

		/// <inheritdoc/>
		public event EventHandler Disposed;

		/// <summary>
		///     Gets the accumulated WORKING time since the user was last considered rested.
		/// </summary>
		/// <remarks>
		///     User is considered rested once he's idle for at least
		///     <see cref="IRestingMonitorConfiguration.RestingTime"/>.
		/// </remarks>
		public TimeSpan BusyTimeSinceRested => _restingMonitor.TotalBusyTimeSinceRested;

		/// <summary>
		///     Gets the accumulated RESTING time since the user was last considered rested.
		/// </summary>
		/// <remarks>
		///     User is considered rested once he's idle for at least
		///     <see cref="IRestingMonitorConfiguration.RestingTime"/>.
		/// </remarks>
		public TimeSpan IdleTimeSinceRested => _restingMonitor.TotalIdleTimeSinceRested;

		/// <summary>
		/// Gets the last status of the user.
		/// </summary>
		public UserStatus LastStatus
		{
			get
			{
				if (_restingMonitor.MustRest)
				{
					return UserStatus.Tired;
				}

				return _restingMonitor.LastStatus switch
				{
					InteractionStatus.Busy => UserStatus.Working,
					InteractionStatus.Idle => UserStatus.Resting,
					_ => UserStatus.Working, // default to working
				};
			}
		}

		/// <summary>
		/// Gets a value indicating whether the user must rest.
		/// </summary>
		public bool MustRest => _restingMonitor.MustRest;

		/// <inheritdoc/>
		public ISite Site { get; set; }

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

			var @event = RestingMonitorUpdated;

			if (@event != null)
			{
				RestingMonitorUpdated(this, new EventArgs());
			}
		}
	}
}
