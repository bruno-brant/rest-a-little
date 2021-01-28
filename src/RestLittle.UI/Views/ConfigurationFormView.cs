// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Windows.Forms;
using RestLittle.UI.Presenters;

namespace RestLittle.UI.Views
{
	/// <summary>
	/// Displays a configuration form for the user.
	/// </summary>
	public partial class ConfigurationFormView : Form, IConfigurationFormView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationFormView"/> class.
		/// </summary>
		public ConfigurationFormView()
		{
			InitializeComponent();

			// TODO: event leaks here :(
			var presenter = new ConfigurationFormPresenter(this, Settings.Default);

			components.Add(presenter);
		}

		/// <inheritdoc/>
		public event EventHandler Cancelled
		{
			add { btnCancel.Click += value; }
			remove { btnCancel.Click -= value; }
		}

		/// <inheritdoc/>
		public event EventHandler Accepted
		{
			add { btnOK.Click += value; }
			remove { btnOK.Click -= value; }
		}

		/// <inheritdoc/>
		public TimeSpan TimeToIdle { get => tpTimeToIdle.TimeSpan; set => tpTimeToIdle.TimeSpan = value; }

		/// <inheritdoc/>
		public TimeSpan MaxBusyTime { get => tpMaxBusyTime.TimeSpan; set => tpMaxBusyTime.TimeSpan = value; }

		/// <inheritdoc/>
		public TimeSpan RestingTime { get => tpRestingTime.TimeSpan; set => tpRestingTime.TimeSpan = value; }

		/// <inheritdoc/>
		public TimeSpan WarningInterval { get => tpWarningInterval.TimeSpan; set => tpWarningInterval.TimeSpan = value; }

		/// <inheritdoc/>
		public void SetError(string name, string errorMessage)
		{
			var control = GetControlFromName(name);

			errorProvider.SetError(control, errorMessage);
		}

		/// <summary>
		/// Returns a control reference accordingly to the name of the control.
		/// </summary>
		/// <param name="name">The name of the control.</param>
		/// <returns>A reference to the user control relating to that name.</returns>
		private Control GetControlFromName(string name)
		{
			// TODO: This is brittle - we are using strings to bind behavior between the presenter and the view.
			return name switch
			{
				nameof(TimeToIdle) => tpTimeToIdle,
				nameof(MaxBusyTime) => tpMaxBusyTime,
				nameof(RestingTime) => tpRestingTime,
				nameof(WarningInterval) => tpWarningInterval,
				_ => throw new ArgumentOutOfRangeException(nameof(name), name, "Component doesn't exists."),
			};
		}
	}
}
