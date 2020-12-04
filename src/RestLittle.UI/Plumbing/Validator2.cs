// Copyright (c) Bruno Brant. All rights reserved.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestLittle.UI.Plumbing
{
	/// <summary>
	/// Simplify validating an object.
	/// </summary>
	public static class Validator2
	{
		/// <summary>
		///     Determines whether the specified object is valid using the validation context
		///     and validation results collection.
		/// </summary>
		/// <param name="instance">The object to validate.</param>
		/// <param name="validationResults">A collection to hold each failed validation.</param>
		/// <returns>true if the object validates; otherwise, false.</returns>
		/// <exception cref="System.ArgumentNullException">
		///     <paramref name="instance"/> is null.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		///     <paramref name="instance"/> doesn't match the <see cref="ValidationContext.ObjectInstance"/>.
		///     validationContext.
		/// </exception>
		public static bool TryValidateObject(object instance, out List<ValidationResult> validationResults)
		{
			validationResults = new List<ValidationResult>();

			return Validator.TryValidateObject(instance, new ValidationContext(instance), validationResults, true);
		}
	}
}
