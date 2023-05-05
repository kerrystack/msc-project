namespace ClientApp.Models
{
	public class TestParameters
	{
		public ScalingType ScalingType { get; set; }

		public string PodPrefix { get; set; }

		public string TestUseCaseIdentifier { get; set; }

		public int LowModeSleepInSeconds { get; set; }

		public int HighModeStartingPointInSeconds { get; set; }

		public int HighModeDurationInSeconds { get; set; }

		public int HighModeThreadCount { get; set; }

		public int TestDurationInSeconds { get; set; }
	}
}