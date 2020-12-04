// Copyright (c) Bruno Brant. All rights reserved.

using System;

namespace RestLittle.UI.Plumbing
{
	/// <summary>
	/// A simple presenter.
	/// </summary>
	public interface IPresenter : IDisposable
	{
		/// <summary>
		///     Raised when the form is unloaded.
		/// </summary>
		event EventHandler Unload;
	}
}
