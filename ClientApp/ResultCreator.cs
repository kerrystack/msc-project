using ClientApp.Models;

namespace ClientApp
{
	public class ResultCreator
	{
		public void Create(
			ScalingType scalingType,
			string testUseCaseIdentifier,
			Dictionary<string, List<(DateTime Date, decimal Value)>> cpuResultData,
			Dictionary<string, List<(DateTime Date, decimal Value)>> memoryResultData)
		{
			if(scalingType == ScalingType.Horizontal)
			{
				CreateForHorizontal(testUseCaseIdentifier, cpuResultData);
			}
			else
			{
				CreateForVertical(testUseCaseIdentifier, cpuResultData, memoryResultData);
			}
		}
		private void CreateForHorizontal(
			string testUseCaseIdentifier,
			Dictionary<string, List<(DateTime Date, decimal Value)>> cpuResultData)
		{
			Console.WriteLine("Starting result creation");

			var resultfilePath = $@"C:\D\msc_project\msc-project\experiments\results\{testUseCaseIdentifier}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
			double resourceSeconds = 0;
			using var streamWriter = new StreamWriter(resultfilePath);

			foreach (var parsedResult in cpuResultData)
			{
				var fromDate = parsedResult.Value.First().Date;
				var toDate = parsedResult.Value.Last().Date;
				var totalPodSeconds = Math.Abs((fromDate - toDate).TotalSeconds);

				streamWriter.WriteLine($"Pod:{parsedResult.Key}");
				streamWriter.WriteLine($" - FromDate:{fromDate}");
				streamWriter.WriteLine($" - ToDate:{toDate}");
				streamWriter.WriteLine($" - TotalPodSeconds:{totalPodSeconds}\n\n");

				resourceSeconds += totalPodSeconds;
			}

			streamWriter.WriteLine($"Total Pod Active Seconds:{resourceSeconds}");

			Console.WriteLine("Finished result creation");
		}

		private void CreateForVertical(
			string testUseCaseIdentifier,
			Dictionary<string, List<(DateTime Date, decimal Value)>> cpuResultData,
			Dictionary<string, List<(DateTime Date, decimal Value)>> memoryResultData)
		{
			Console.WriteLine("Starting result creation");

			var verticalStatsPerPod = new Dictionary<string, VerticalStats>();

			var resultfilePath = $@"C:\D\msc_project\msc-project\experiments\results\{testUseCaseIdentifier}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
			using var streamWriter = new StreamWriter(resultfilePath);

			foreach (var parsedResult in cpuResultData)
			{
				if (parsedResult.Key.EndsWith("-cpu-request"))
				{
					var podName = parsedResult.Key.Replace("-cpu-request", "");
					if (verticalStatsPerPod.ContainsKey(podName))
					{
						verticalStatsPerPod[podName].CPURequest = parsedResult.Value.First().Value;
					}
					else
					{
						var fromDate = parsedResult.Value.First().Date;
						var toDate = parsedResult.Value.Last().Date;

						verticalStatsPerPod.Add(podName, new VerticalStats()
						{
							FromDate = fromDate,
							ToDate = toDate,
							TotalPodSeconds = Math.Abs((fromDate - toDate).TotalSeconds),
							CPURequest = parsedResult.Value.First().Value
						});
					}
				}

				if (parsedResult.Key.EndsWith("-cpu-limit"))
				{
					var podName = parsedResult.Key.Replace("-cpu-limit", "");
					if (verticalStatsPerPod.ContainsKey(podName))
					{
						verticalStatsPerPod[podName].CPULimit = parsedResult.Value.First().Value;
					}
					else
					{
						var fromDate = parsedResult.Value.First().Date;
						var toDate = parsedResult.Value.Last().Date;

						verticalStatsPerPod.Add(podName, new VerticalStats()
						{
							FromDate = fromDate,
							ToDate = toDate,
							TotalPodSeconds = Math.Abs((fromDate - toDate).TotalSeconds),
							CPULimit = parsedResult.Value.First().Value
						});
					}
				}
			}

			foreach (var parsedResult in memoryResultData)
			{
				if (parsedResult.Key.EndsWith("-memory-request"))
				{
					var podName = parsedResult.Key.Replace("-memory-request", "");
					if (verticalStatsPerPod.ContainsKey(podName))
					{
						verticalStatsPerPod[podName].MemoryRequest = parsedResult.Value.First().Value;
					}
					else
					{
						var fromDate = parsedResult.Value.First().Date;
						var toDate = parsedResult.Value.Last().Date;

						verticalStatsPerPod.Add(podName, new VerticalStats()
						{
							FromDate = fromDate,
							ToDate = toDate,
							TotalPodSeconds = Math.Abs((fromDate - toDate).TotalSeconds),
							MemoryRequest = parsedResult.Value.First().Value
						});
					}
				}

				if (parsedResult.Key.EndsWith("-memory-limit"))
				{
					var podName = parsedResult.Key.Replace("-memory-limit", "");
					if (verticalStatsPerPod.ContainsKey(podName))
					{
						verticalStatsPerPod[podName].MemoryLimit = parsedResult.Value.First().Value;
					}
					else
					{
						var fromDate = parsedResult.Value.First().Date;
						var toDate = parsedResult.Value.Last().Date;

						verticalStatsPerPod.Add(podName, new VerticalStats()
						{
							FromDate = fromDate,
							ToDate = toDate,
							TotalPodSeconds = Math.Abs((fromDate - toDate).TotalSeconds),
							MemoryLimit = parsedResult.Value.First().Value
						});
					}
				}
			}

			foreach (var podName in verticalStatsPerPod.Keys)
			{
				var verticalPodStats = verticalStatsPerPod[podName];
				streamWriter.WriteLine($"Pod:{podName}");
				streamWriter.WriteLine($" - FromDate:{verticalPodStats.FromDate}");
				streamWriter.WriteLine($" - ToDate:{verticalPodStats.ToDate}");
				streamWriter.WriteLine($" - TotalPodSeconds:{verticalPodStats.TotalPodSeconds}");
				streamWriter.WriteLine($" - CPURequest:{verticalPodStats.CPURequest}");
				streamWriter.WriteLine($" - CPULimit:{verticalPodStats.CPULimit}");
				streamWriter.WriteLine($" - MemoryRequest:{verticalPodStats.MemoryRequest}");
				streamWriter.WriteLine($" - MemoryLimit:{verticalPodStats.MemoryLimit}\n\n");
			}

			Console.WriteLine("Finished result creation");
		}

		private class VerticalStats
		{
			public DateTime FromDate;
			public DateTime ToDate;
			public double TotalPodSeconds;
			public decimal CPURequest;
			public decimal CPULimit;
			public decimal MemoryRequest;
			public decimal MemoryLimit;
		}
	}
}
