// Copyright (c) Bruno Brant. All rights reserved.

using System.Runtime.InteropServices;

namespace RestLittle
{
	/// <summary>
	/// External functions.
	/// </summary>
	internal static class NativeMethods
	{
		/// <summary>
		/// Retrieves the time of the last input event.
		/// </summary>
		/// <param name="plii">
		/// A pointer to a LASTINPUTINFO structure that receives the time of the last input event.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is true.
		/// If the function fails, the return value is false.
		/// </returns>
		[DllImport("user32.dll")]
		public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

#pragma warning disable IDE1006 // Naming Styles (interop)

		/// <summary>
		/// Contains the time of the last input.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct LASTINPUTINFO
		{
			/// <summary>
			/// Gets the value of sizeof(LASTINPUTINFO).
			/// </summary>
			public static readonly uint Size = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO));

			/// <summary>
			/// The size of the structure, in bytes. This member must be set to sizeof(LASTINPUTINFO).
			/// </summary>
			[MarshalAs(UnmanagedType.U4)]
			private uint cbSize;

			/// <summary>
			/// The tick count when the last input event was received.
			/// </summary>
			[MarshalAs(UnmanagedType.U4)]
			private uint dwTime;

			/// <summary>
			/// Gets or sets the tick count when the last input event was received.
			/// </summary>
			public uint DwTime { get => dwTime; set => dwTime = value; }

			/// <summary>
			/// Gets or sets the size of the structure, in bytes. This member must be set to sizeof(LASTINPUTINFO).
			/// </summary>
			public uint CbSize { get => cbSize; set => cbSize = value; }
		}

#pragma warning restore IDE1006 // Naming Styles (interop)
	}
}
