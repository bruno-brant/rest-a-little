// Copyright (c) Bruno Brant. All rights reserved.

using NSubstitute;
using RestLittle.Tests;
using RestLittle.UI.Models;
using Xunit;

namespace RestLittle.UI.Tests.ModelTests
{
	public class TrayIconModelTests
	{
		[Theory, AutoSubstituteData]
		public void UserStatus_WhenUserMustRest_ReturnsTired(IRestingMonitor restingMonitor)
		{
			if (restingMonitor is null)
			{
				throw new System.ArgumentNullException(nameof(restingMonitor));
			}

			restingMonitor.MustRest.Returns(true);

			using var sut = new TrayIconModel(restingMonitor);

			Assert.Equal(UserStatus.Tired, sut.LastStatus);
		}
	}
}
