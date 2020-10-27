// Copyright (c) Bruno Brant. All rights reserved.

using Microsoft.Extensions.DependencyInjection;
using RestLittle.UI.Models;
using RestLittle.UI.Presenters;
using RestLittle.UI.Views;

namespace RestLittle.UI.DependencyInjection
{
	/// <summary>
	/// DI methods to initialize TrayIcon dependencies.
	/// </summary>
	public static class TrayIconExtensionMethods
	{
		/// <summary>
		/// Adds all dependencies to initialize TrayIcon screen.
		/// </summary>
		/// <param name="services">
		/// The service collection.
		/// </param>
		/// <returns>
		/// The service collection for chaining.
		/// </returns>
		public static IServiceCollection AddTrayIcon(this IServiceCollection services)
		{
			return services
				.AddScoped<TrayIconModel>()
				.AddScoped<ITrayIconView, TrayIconView>()
				.AddScoped<TrayIconPresenter>();
		}
	}
}
