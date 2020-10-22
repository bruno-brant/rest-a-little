using System;
using System.Threading;

namespace RestLittle.CLI
{
	class Program
	{
		static void Main()
		{
			var rest =
				new RestingMonitor(
					new RestingMonitorConfiguration
					{
						InitialStatus = UserStatus.Idle,
						MaxBusyTime = TimeSpan.FromMinutes(10),
						RestTimePerBusyTime = TimeSpan.FromMinutes(5),
					},
					new UserIdleMonitor(
						new UserIdleMonitorConfiguration
						{
							MinTimeToIdle = TimeSpan.FromSeconds(10),
						},
						new InputManager()));

			while (true)
			{
				Thread.Sleep(2000);
				rest.Update(TimeSpan.FromMilliseconds(2000));

				var msg = $@"
LastStatus: {rest.LastStatus}
TimeSinceLastStatus: {rest.TimeSinceLastStatus:hh\:mm\:ss}
TotalBusyTimeSinceRested: {rest.TotalBusyTimeSinceRested:hh\:mm\:ss}
TotalIdleTimeSinceRested: {rest.TotalIdleTimeSinceRested:hh\:mm\:ss}
MustRest: {rest.MustRest}
";
				Console.Clear();
				Console.WriteLine(msg.Trim());
			}
		}
	}
}
