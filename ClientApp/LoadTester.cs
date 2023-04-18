using ClientApp.Models;

namespace ClientApp
{
	public class LoadTester
	{
		public async Task<LoadTestResult> ExecuteLoad(TestParameters testParameters)
		{
			Console.WriteLine("Starting execution of load");

			var testStartCheckpoint = DateTime.UtcNow;
			var highModeStartCheckpoint = testStartCheckpoint.AddSeconds(testParameters.HighModeStartingPointInSeconds);
			var highModeEndCheckpoint = highModeStartCheckpoint.AddSeconds(testParameters.HighModeDurationInSeconds);
			var testEndCheckpoint = testStartCheckpoint.AddSeconds(testParameters.TestDurationInSeconds);

			// Execute low load
			await ExecuteBatchOfRequests(highModeStartCheckpoint, testParameters.LowModeSleepInSeconds);

			// Execute high load
			await ExecuteBatchOfRequests(highModeEndCheckpoint);

			// Execute low load for remainder
			await ExecuteBatchOfRequests(testEndCheckpoint, testParameters.LowModeSleepInSeconds);

			var result = new LoadTestResult()
			{
				TestStartCheckpoint = testStartCheckpoint,
				HighModeStartCheckpoint = highModeStartCheckpoint,
				HighModeEndCheckpoint = highModeEndCheckpoint,
				TestEndCheckpoint = testEndCheckpoint,
			};

			Console.WriteLine("Finished execution of load");

			return result;
		}

		private async Task ExecuteBatchOfRequests(DateTime endBatchCheckpoint, int sleepInSeconds = 0)
		{
			var client = new HttpClient();

			do
			{
				var response = await client.GetAsync("http://localhost:8001");
				var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				var printPrefix = sleepInSeconds != 0
					? $"Low mode, {sleepInSeconds} sleepInSeconds, {DateTime.UtcNow} timestamp, Response body:"
					: $"High mode, {sleepInSeconds} sleepInSeconds, {DateTime.UtcNow} timestamp, Response body:";
				Console.WriteLine(printPrefix + responseBody);

				if (sleepInSeconds > 0) { Thread.Sleep(sleepInSeconds * 1000); }
			}
			while (DateTime.UtcNow < endBatchCheckpoint);
	
}
	}
}
