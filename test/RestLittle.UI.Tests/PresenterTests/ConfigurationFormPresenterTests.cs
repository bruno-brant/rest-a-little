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

				Assert.Equal(view.TimeToIdle, model.TimeToIdle);
				Assert.Equal(view.MaxBusyTime, model.MaxBusyTime);
				Assert.Equal(view.RestingTime, model.RestingTime);
				Assert.Equal(view.WarningInterval, model.WarningInterval);
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

			view.MaxBusyTime.Returns(TimeSpan.FromSeconds(time));
			view.RestingTime.Returns(TimeSpan.FromSeconds(time));
			view.TimeToIdle.Returns(TimeSpan.FromSeconds(time));
			view.WarningInterval.Returns(TimeSpan.FromSeconds(time));

			// capture the strings (for debugging purposes)
			string name, errorMsg;

			view.When(_ =>
				_.SetError(
					Arg.Do<string>(_ => name = _),
					Arg.Do<string>(_ => errorMsg = _)));

			using (new ConfigurationFormPresenter(view, model))
			{
				view.Accepted += Raise.Event();

				Assert.Equal(view.MaxBusyTime, model.MaxBusyTime);
				Assert.Equal(view.RestingTime, model.RestingTime);
				Assert.Equal(view.TimeToIdle, model.TimeToIdle);
				Assert.Equal(view.WarningInterval, model.WarningInterval);

				view.DidNotReceive().SetError(Arg.Any<string>(), Arg.Any<string>());
				view.Received().Close();
			}
		}

		[Theory, AutoData]
		public void FormAccepted_DataIsInvalid_SetErrorsOnView(Settings model)
		{
			if (model is null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			var view = Substitute.For<IConfigurationFormView>();

			view.TimeToIdle.Returns(TimeSpan.FromSeconds(-1)); // invalid data
			view.MaxBusyTime.Returns(_fixture.Create<TimeSpan>());
			view.RestingTime.Returns(_fixture.Create<TimeSpan>());
			view.WarningInterval.Returns(_fixture.Create<TimeSpan>());

			using (new ConfigurationFormPresenter(view, model))
			{
				view.Accepted += Raise.Event();

				Assert.Equal(view.MaxBusyTime, model.MaxBusyTime);
				Assert.Equal(view.RestingTime, model.RestingTime);
				Assert.Equal(view.TimeToIdle, model.TimeToIdle);
				Assert.Equal(view.WarningInterval, model.WarningInterval);

				view.DidNotReceive().Close();

				view.Received().SetError(Arg.Any<string>(), Arg.Any<string>());
			}
		}
	}
}
