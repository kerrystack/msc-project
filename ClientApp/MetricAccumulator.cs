using ClientApp.Models;

namespace ClientApp
{
	/// <summary>
	/// https://stackoverflow.com/questions/63347233/how-to-get-cpu-and-memory-usage-of-pod-in-percentage-using-promethus
	/// </summary>
	public class MetricAccumulator
	{
		public AccumulatedStatistics Statistics { get; set; }

		public void Accumulate(ScalingType scalingType, string podPrefix, int collectPeriodInSeconds)
		{
			Console.WriteLine("Starting metric data accumulation");

			Statistics = new AccumulatedStatistics();

			while (true)
			{
				if (scalingType == ScalingType.Horizontal)
					AccumulateForHorizontalType(podPrefix);
				else
					AccumulateForVerticalType(podPrefix);

				Thread.Sleep(collectPeriodInSeconds * 1000);
				Console.WriteLine($"Metrics accumulated @ {DateTime.UtcNow}");
			}
		}


		public void AccumulateForHorizontalType(string podPrefix)
		{
			var cpuMetrics = MetricGetter2.GetCPUMetrics();
			var memoryMetrics = MetricGetter2.GetCPUMetrics();

			var cpuMetricData = new ResultParser().Parse(cpuMetrics);
			var memoryMetricData = new ResultParser().Parse(memoryMetrics);

			Statistics.AddCPUStats(podPrefix, cpuMetricData);
			Statistics.AddMemoryStats(podPrefix, memoryMetricData);
		}

		public void AccumulateForVerticalType(string podPrefix)
		{
			var cpuRequests = MetricGetter2.GetResourceCPURequests("nginx-deployment");
			var cpuLimits = MetricGetter2.GetResourceCPULimits("nginx-deployment");

			var memoryRequests = MetricGetter2.GetResourceMemoryRequests("nginx-deployment");
			var memoryLimits = MetricGetter2.GetResourceMemoryLimits("nginx-deployment");

			var cpuRequestMetricData = new ResultParser().Parse(cpuRequests, "-cpu-request");
			var cpuLimitMetricData = new ResultParser().Parse(cpuLimits, "-cpu-limit");
			var memoryRequestMetricData = new ResultParser().Parse(memoryRequests, "-memory-request");
			var memoryLimitMetricData = new ResultParser().Parse(memoryLimits, "-memory-limit");

			foreach (var key in cpuLimitMetricData.Keys)
			{
				cpuRequestMetricData.Add(key, cpuLimitMetricData[key]);
			}
			foreach (var key in memoryLimitMetricData.Keys)
			{
				memoryRequestMetricData.Add(key, memoryLimitMetricData[key]);
			}

			Statistics.AddCPUStats(podPrefix, cpuRequestMetricData);
			Statistics.AddMemoryStats(podPrefix, memoryRequestMetricData);
		}
	}
}