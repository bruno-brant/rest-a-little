// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Configuration;
using RestLittle.UI.Presenters;
using RestLittle.UI.Views;

namespace RestLittle.UI
{
	/// <summary>
	/// Model for the <see cref="ConfigurationFormView"/>.
	/// </summary>
	public class Settings : ApplicationSettingsBase,
		IRestingMonitorConfiguration, IUserIdleMonitorConfiguration, ITrayIconViewConfiguration
	{
		/// <summary>
		/// Gets default instance of Settings.
		/// </summary>
		public static Settings Default { get; } = (Settings)Synchronized(new Settings());

		/// <summary>
		/// Gets or sets how long the user must be inactive to be considered idle.
		/// </summary>
		[UserScopedSetting]
		[DefaultSettingValue("00:00:15")]
		[Plumbing.TimeSpanValidator(MinValue = "00:00:01")]
		public TimeSpan TimeToIdle
		{
			get { return (TimeSpan)this[nameof(TimeToIdle)]; }
			set { this[nameof(TimeToIdle)] = value; }
		}

		/// <summary>
		/// Gets or sets how long the user can be working without resting.
		/// </summary>
		[UserScopedSetting]
		[DefaultSettingValue("00:05:00")]
		public TimeSpan MaxBusyTime
		{
			get { return (TimeSpan)this[nameof(MaxBusyTime)]; }
			set { this[nameof(MaxBusyTime)] = value; }
		}

		/// <summary>
		/// Gets or sets how long should the user rest for each busy time session.
		/// </summary>
		[UserScopedSetting]
		[DefaultSettingValue("00:05:00")]
		public TimeSpan RestingTime
		{
			get { return (TimeSpan)this[nameof(RestingTime)]; }
			set { this[nameof(RestingTime)] = value; }
		}

		/// <summary>
		/// Gets or sets interval between warning calls.
		/// </summary>
		[UserScopedSetting]
		[DefaultSettingValue("00:01:00")]
		public TimeSpan WarningInterval
		{
			get { return (TimeSpan)this[nameof(WarningInterval)]; }
			set { this[nameof(WarningInterval)] = value; }
		}

		/// <inheritdoc/>
		[UserScopedSetting]
		[DefaultSettingValue("Idle")]
		public InteractionStatus InitialStatus
		{
			get { return (InteractionStatus)this[nameof(InitialStatus)]; }
			set { this[nameof(InitialStatus)] = value; }
		}
	}
}
