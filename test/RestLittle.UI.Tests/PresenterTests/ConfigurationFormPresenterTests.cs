// Copyright (c) Bruno Brant. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using AutoFixture;
using AutoFixture.Xunit2;
using NSubstitute;
using RestLittle.UI.Presenters;
using Xunit;

namespace RestLittle.UI.Tests.PresenterTests
{
	public class ConfigurationFormPresenterTests
	{
		private readonly Fixture _fixture = new Fixture();

		[Theory, AutoData]
		public void Load_CarriesDataFromModel(Settings model)
		{
			if (model is null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			var view = Substitute.For<IConfigurationFormView>();

			using (new ConfigurationFormPresenter(view, model))
			{
				view.Load += Raise.Event();

				Assert.Equal(view.TimeToIdle, model.TimeToIdle.TotalSeconds.ToString(CultureInfo.CurrentCulture));
				Assert.Equal(view.MaxBusyTime, model.MaxBusyTime.TotalSeconds.ToString(CultureInfo.CurrentCulture));
				Assert.Equal(view.RestingTime, model.RestingTime.TotalSeconds.ToString(CultureInfo.CurrentCulture));
				Assert.Equal(view.WarningInterval, model.WarningInterval.TotalSeconds.ToString(CultureInfo.CurrentCulture));
			}
		}

		[Theory, AutoData]
		public void FormCancelled_ClosesForm(Settings model)
		{
			if (model is null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			var view = Substitute.For<IConfigurationFormView>();

			using (new ConfigurationFormPresenter(view, model))
			{
				view.Cancelled += Raise.Event();

				view.Received().Close();
			}
		}

		[Theory, AutoData]
		public void FormAccepted_DataIsValid_UpdatesModelAndClosesForm(Settings model, [Range(1, 20)] int time)
		{
			if (model is null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			var view = Substitute.For<IConfigurationFormView>();

			view.MaxBusyTime.Returns(time.ToString(CultureInfo.InvariantCulture));
			view.RestingTime.Returns(time.ToString(CultureInfo.InvariantCulture));
			view.TimeToIdle.Returns(time.ToString(CultureInfo.InvariantCulture));
			view.WarningInterval.Returns(time.ToString(CultureInfo.InvariantCulture));

			using (new ConfigurationFormPresenter(view, model))
			{
				view.Accepted += Raise.Event();

				Assert.Equal(view.MaxBusyTime, model.MaxBusyTime.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				Assert.Equal(view.RestingTime, model.RestingTime.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				Assert.Equal(view.TimeToIdle, model.TimeToIdle.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				Assert.Equal(view.WarningInterval, model.WarningInterval.TotalSeconds.ToString(CultureInfo.InvariantCulture));

				view.Received().Close();
			}
		}

		[Theory, AutoData]
		public void FormAccepted_DataIsInvalid_UpdatesModelAndClosesForm(Settings model)
		{
			if (model is null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			var view = Substitute.For<IConfigurationFormView>();

			view.TimeToIdle.Returns((-1).ToString(CultureInfo.InvariantCulture));
			view.MaxBusyTime.Returns(_fixture.Create<int>().ToString(CultureInfo.InvariantCulture));
			view.RestingTime.Returns(_fixture.Create<int>().ToString(CultureInfo.InvariantCulture));
			view.WarningInterval.Returns(_fixture.Create<int>().ToString(CultureInfo.InvariantCulture));

			using (new ConfigurationFormPresenter(view, model))
			{
				view.Accepted += Raise.Event();

				Assert.Equal(view.MaxBusyTime, model.MaxBusyTime.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				Assert.Equal(view.RestingTime, model.RestingTime.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				Assert.Equal(view.TimeToIdle, model.TimeToIdle.TotalSeconds.ToString(CultureInfo.InvariantCulture));
				Assert.Equal(view.WarningInterval, model.WarningInterval.TotalSeconds.ToString(CultureInfo.InvariantCulture));

				view.DidNotReceive().Close();
			}
		}
	}
}
