// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Threading;

namespace RestLittle.UI.Plumbing
{
	/// <summary>
	/// Calls the provided action then waits for the inverval before calling again.
	/// </summary>
	public class RepeatingEvent
	{
		private readonly Action _callback;
		private readonly TimeSpan _interval;
		private readonly Thread _loopingThread;
		private bool _running;

		/// <summary>
		///     Initializes a new instance of the <see cref="RepeatingEvent"/> class.
		/// </summary>
		/// <param name="callback">
		///     The action to run after the inverval.
		/// </param>
		/// <param name="interval">
		///     How long to wait after action is done until calling it again.
		/// </param>
		public RepeatingEvent(Action callback, TimeSpan interval)
		{
			_callback = callback;
			_interval = interval;

			_loopingThread = new Thread(Loop);
		}

		/// <summary>
		/// Starts the event loop.
		/// </summary>
		public void Start()
		{
			_running = true;
			_loopingThread.Start();
		}

		/// <summary>
		/// Stops the event loop.
		/// </summary>
		/// <param name="timeout">
		/// How long to wait for the loop to stop.
		/// </param>
		public void Stop(TimeSpan timeout)
		{
			_running = false;
			_loopingThread.Join(timeout);
		}

		/// <summary>
		/// Method that controls the loop.
		/// </summary>
		private void Loop()
		{
			while (_running)
			{
				_callback();
				Thread.Sleep(_interval);
			}
		}
	}
}
