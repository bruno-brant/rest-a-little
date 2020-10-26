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
		public string TimeToIdle { get => txtTimeToIdle.Text; set => txtTimeToIdle.Text = value; }

		/// <inheritdoc/>
		public string MaxBusyTime { get => txtMaxBusyTime.Text; set => txtMaxBusyTime.Text = value; }

		/// <inheritdoc/>
		public string RestingTime { get => txtRestingTime.Text; set => txtRestingTime.Text = value; }

		/// <inheritdoc/>
		public string WarningInterval { get => txtWarningInterval.Text; set => txtWarningInterval.Text = value; }

		/// <inheritdoc/>
		// TODO: Improve this
		public void SetError(string name, string errorMessage)
		{
			var control = name switch
			{
				nameof(TimeToIdle) => txtTimeToIdle,
				nameof(MaxBusyTime) => txtMaxBusyTime,
				nameof(RestingTime) => txtRestingTime,
				nameof(WarningInterval) => txtWarningInterval,
				_ => throw new ArgumentOutOfRangeException(nameof(name), name, "Component doesn't exists."),
			};

			errorProvider1.SetError(control, errorMessage);
		}
	}
}
