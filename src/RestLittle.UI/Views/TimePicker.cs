// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Windows.Forms;

namespace RestLittle.UI.Views
{
	/// <summary>
	/// Specialized version of <see cref="DateTimePicker"/> that displays a timespan.
	/// </summary>
	public class TimePicker : DateTimePicker
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TimePicker"/> class.
		/// </summary>
		public TimePicker()
		{
			Format = DateTimePickerFormat.Custom;
			CustomFormat = "mmm minutes ss seconds";
			ShowUpDown = true;
			MinDate = DateTime.MinValue;
			MaxDate = MinDate.AddDays(1).AddSeconds(-1);
		}

		/// <summary>
		/// Gets or sets the time represented by this control.
		/// </summary>
		public TimeSpan TimeSpan
		{
			get
			{
				return Value - DateTime.MinValue;
			}

			set
			{
				Value = MinDate + value;
			}
		}

		/// <inheritdoc/>
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
		}
	}
}
