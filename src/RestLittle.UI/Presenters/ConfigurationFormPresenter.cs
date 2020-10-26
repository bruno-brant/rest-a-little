// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using RestLittle.UI.Plumbing;

namespace RestLittle.UI.Presenters
{
	/// <summary>
	/// A Presenter from MVP for the ConfigurationFormView.
	/// </summary>
	public class ConfigurationFormPresenter : Presenter<IConfigurationFormView, Settings>, IComponent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationFormPresenter"/> class.
		/// </summary>
		/// <param name="view">Displays the form to the user.</param>
		/// <param name="model">Holds the data of the form.</param>
		public ConfigurationFormPresenter(IConfigurationFormView view, Settings model)
			: base(view, model)
		{
			View.Load += View_Load;
			View.Cancelled += View_Cancelled;
			View.Accepted += View_Accepted;
		}

		/// <inheritdoc/>
		public override event EventHandler Unload;

		/// <inheritdoc/>
		public event EventHandler Disposed;

		/// <inheritdoc/>
		public ISite Site { get; set; }

		/// <summary>
		/// Raises the Unload event.
		/// </summary>
		protected virtual void OnUnload()
		{
			var @event = Unload;

			@event?.Invoke(this, new EventArgs());
		}

		/// <summary>
		/// Converts an string of seconds to TimeSpan.
		/// </summary>
		/// <param name="secondsString">
		/// The string to be converted.
		/// </param>
		/// <returns>
		/// A timespan representation of the string.
		/// </returns>
		private static TimeSpan FromSeconds(string secondsString)
		{
			return TimeSpan.FromSeconds(int.Parse(secondsString, CultureInfo.CurrentCulture));
		}

		/// <summary>
		///     Handles the Accepted event from the view.
		/// </summary>
		/// <param name="sender">The object that raised the event.</param>
		/// <param name="e">The arguments of the event.</param>
		private void View_Accepted(object sender, EventArgs e)
		{
			Model.TimeToIdle = FromSeconds(View.TimeToIdle);
			Model.MaxBusyTime = FromSeconds(View.MaxBusyTime);
			Model.RestingTime = FromSeconds(View.RestingTime);
			Model.WarningInterval = FromSeconds(View.WarningInterval);

			if (Validator2.TryValidateObject(Model, out var validationResults))
			{
				View.Close();
				Model.Save();
				OnUnload();
			}
			else
			{
				View.SetError(validationResults[0].MemberNames.FirstOrDefault(), validationResults[0].ErrorMessage);
			}
		}

		/// <summary>
		///     Handles the Cancelled event from the view.
		/// </summary>
		/// <param name="sender">The object that raised the event.</param>
		/// <param name="e">The arguments of the event.</param>
		private void View_Cancelled(object sender, EventArgs e)
		{
			View.Close();
		}

		/// <summary>
		///     Sync's the data between the view and the model.
		/// </summary>
		/// <param name="sender">The object that raised the event.</param>
		/// <param name="e">The arguments of the event.</param>
		private void View_Load(object sender, EventArgs e)
		{
			// TODO: rethink where we should add this logic
			// TODO: Use settings as the model?
			Model.Reload();

			View.TimeToIdle = Model.TimeToIdle.TotalSeconds.ToString(CultureInfo.CurrentCulture);
			View.MaxBusyTime = Model.MaxBusyTime.TotalSeconds.ToString(CultureInfo.CurrentCulture);
			View.RestingTime = Model.RestingTime.TotalSeconds.ToString(CultureInfo.CurrentCulture);
			View.WarningInterval = Model.WarningInterval.TotalSeconds.ToString(CultureInfo.CurrentCulture);
		}
	}
}
