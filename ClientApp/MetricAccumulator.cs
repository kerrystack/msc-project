using ClientApp.Models;

namespace ClientApp
{
	/// <summary>
	/// https://stackoverflow.com/questions/63347233/how-to-get-cpu-and-memory-usage-of-pod-in-percentage-using-promethus
	/// </summary>
	public class MetricAccumulator
	{
		public AccumulatedStatistics Statistics { get; set; }

		public void Accumulate(int collectPeriodInSeconds)
		{
			Console.WriteLine("Starting metric data accumulation");

			Statistics = new AccumulatedStatistics();

			while (true)
			{
				var cpuMetrics = MetricGetter2.GetCPUMetrics();
				var memoryMetrics = MetricGetter2.GetCPUMetrics();

				var cpuMetricData = new ResultParser().Parse(cpuMetrics);
				var memoryMetricData = new ResultParser().Parse(memoryMetrics);

				Statistics.AddCPUStats(cpuMetricData);
				Statistics.AddMemoryStats(memoryMetricData);

				Thread.Sleep(collectPeriodInSeconds * 1000);

				Console.WriteLine($"Metrics accumulated @ {DateTime.UtcNow}");
			}
		}
	}
}