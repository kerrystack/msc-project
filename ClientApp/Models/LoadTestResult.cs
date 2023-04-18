namespace ClientApp.Models
{
	public class LoadTestResult
	{
		public DateTime TestStartCheckpoint { get; set; }

		public DateTime HighModeStartCheckpoint { get; set; }

		public DateTime HighModeEndCheckpoint { get; set; }

		public DateTime TestEndCheckpoint { get; set; }
	}
}