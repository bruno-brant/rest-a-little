// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestLittle.UI.DependencyInjection;
using RestLittle.UI.Models;
using RestLittle.UI.Presenters;
using RestLittle.UI.Views;

namespace RestLittle.UI
{
	/// <summary>
	/// Main program entry point.
	/// </summary>
	public static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			var services = new ServiceCollection();

			ConfigureServices(services);

			using var serviceProvider = services.BuildServiceProvider();

			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var context = serviceProvider.GetService<RestLittleApplicationContext>();

			Application.Run(context);
		}

		private static void ConfigureServices(ServiceCollection services)
		{
			services
				.AddLogging(configure => configure.AddFile("restalittle.log"))
				.AddTrayIcon()
				.AddScoped<RestLittleApplicationContext>();
		}
	}
}
