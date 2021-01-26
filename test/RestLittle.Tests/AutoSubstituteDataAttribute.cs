// Copyright (c) Bruno Brant. All rights reserved.

using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace RestLittle.Tests
{
	/// <summary>
	/// Specialization of <see cref="AutoDataAttribute"/> to work
	/// with <see cref="NSubstitute"/>.
	/// </summary>
	public class AutoSubstituteDataAttribute : AutoDataAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AutoSubstituteDataAttribute"/> class.
		/// </summary>
		public AutoSubstituteDataAttribute()
			: base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
		{
		}
	}
}
