namespace ClientApp.Models
{
	public class TestParameters
	{
		public string TestUseCaseIdentifier { get; set; }

		public int LowModeSleepInSeconds { get; set; }

		public int HighModeStartingPointInSeconds { get; set; }

		public int HighModeDurationInSeconds { get; set; }

		public int HighModeThreadCount { get; set; }

		public int TestDurationInSeconds { get; set; }
	}
}