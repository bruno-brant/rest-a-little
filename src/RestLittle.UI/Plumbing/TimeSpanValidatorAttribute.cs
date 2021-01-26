// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace RestLittle.UI.Plumbing
{
	/// <summary>
	/// Declarative validations for a <see cref="TimeSpan"/> field.
	/// </summary>
	public class TimeSpanValidatorAttribute : ValidationAttribute
	{
		private TimeSpan _minValue;
		private TimeSpan _maxValue;

		/// <summary>
		/// Gets or sets minimum inclusive allowed value for this TimeSpan.
		/// </summary>
		public string MinValue { get => _minValue.ToString(); set => _minValue = TimeSpan.Parse(value, CultureInfo.CurrentCulture); }

		/// <summary>
		/// Gets or sets maximum inclusive allowed value for this TimeSpan.
		/// </summary>
		public string MaxValue { get => _maxValue.ToString(); set => _maxValue = TimeSpan.Parse(value, CultureInfo.CurrentCulture); }

		/// <inheritdoc/>
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return ValidationResult.Success;
			}

			if (value is not TimeSpan ts)
			{
				return new ValidationResult("Invalid TimeSpan.");
			}

			if (ts < _minValue)
			{
				return new ValidationResult($"TimeSpan value '{value}' must be greater or equal to '{MinValue}'");
			}

			if (ts > _maxValue)
			{
				return new ValidationResult($"TimeSpan value '{value}' must be less or equal to '{MaxValue}'");
			}

			return ValidationResult.Success;
		}
	}
}
