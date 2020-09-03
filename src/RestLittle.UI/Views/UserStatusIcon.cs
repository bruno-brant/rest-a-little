// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.Drawing;
using System.Globalization;

namespace RestLittle.UI.Views
{
	/// <summary>
	/// List of possible user icons.
	/// </summary>
	/// <remarks>
	/// This class implements an enum pattern for a <see cref="Icon"/> types.
	/// </remarks>
	public sealed class UserStatusIcon
	{
		/// <summary>
		/// An icon for a tired user.
		/// </summary>
		public static readonly UserStatusIcon Tired = new UserStatusIcon("tired");

		/// <summary>
		/// An icon for a working user.
		/// </summary>
		public static readonly UserStatusIcon Active = new UserStatusIcon("busy");

		/// <summary>
		/// An icon for a resting user.
		/// </summary>
		public static readonly UserStatusIcon Resting = new UserStatusIcon("resting");

		/// <summary>
		/// The icon held by this Enum Pattern.
		/// </summary>
		private readonly Icon _icon;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserStatusIcon"/> class.
		/// </summary>
		/// <param name="iconName">Identifies the icon resource.</param>
		private UserStatusIcon(string iconName)
		{
			_icon = GetIcon(iconName);
		}

		/// <summary>
		/// Converts a <see cref="UserStatusIcon"/> to a <see cref="Icon"/> instance.
		/// </summary>
		/// <param name="userStatusIcon">The instance to convert from.</param>
		public static implicit operator Icon(UserStatusIcon userStatusIcon)
		{
			if (userStatusIcon is null)
			{
				return null;
			}

			return userStatusIcon._icon;
		}

		/// <summary>
		///     Gets the Icon for this <see cref="UserStatusIcon"/>.
		/// </summary>
		/// <returns>
		///     The <see cref="Icon"/> instance.
		/// </returns>
		public Icon ToIcon()
		{
			return _icon;
		}

		/// <summary>
		/// Gets an Icon from the resource file.
		/// </summary>
		/// <param name="name">The name of the icon.</param>
		/// <returns>The Icon instance.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// When there's no Icon for the given name.
		/// </exception>
		private static Icon GetIcon(string name)
		{
			var obj = Resource.ResourceManager.GetObject(name, CultureInfo.CurrentCulture);

			if (obj == null)
			{
				throw new ArgumentOutOfRangeException(nameof(name), name, "Couldn't find the icon .");
			}

			return (Icon)obj;
		}
	}
}
