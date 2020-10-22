// Copyright (c) Bruno Brant. All rights reserved.

using System;
using static RestLittle.NativeMethods;

namespace RestLittle
{
	/// <inheritdoc/>
	public class InputManager : IInputManager
	{
		/// <inheritdoc/>
		public DateTime GetLastInputTime()
		{
			var lastInputInfo = new LASTINPUTINFO
			{
				CbSize = LASTINPUTINFO.Size,
			};

			if (!GetLastInputInfo(ref lastInputInfo))
			{
				throw new Exception("Couldn't get last time.");
			}

			var lastInput = DateTime.Now.AddMilliseconds(-(Environment.TickCount - lastInputInfo.DwTime));

			return lastInput;
		}
	}
}
