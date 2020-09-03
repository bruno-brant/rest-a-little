// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			using var context = new RestLittleApplicationContext();

			Application.Run(context);
		}
	}
}
