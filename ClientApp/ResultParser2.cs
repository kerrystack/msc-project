using ClientApp.Models;
using System.Text.Json;

namespace ClientApp
{
	public class ResultParser2
	{
		public Dictionary<string, List<(DateTime Date, decimal Value)>> Parse(string metricData)
		{
			Console.WriteLine("Starting result parsing");

			var experimentResult = new Dictionary<string, List<(DateTime Date, decimal Value)>>();

			var rootData = JsonSerializer.Deserialize<Root>(metricData);

			foreach (var result in rootData.data.result)
			{
				foreach (var value in result.values)
				{
					var metricDate = DateTimeOffset.FromUnixTimeSeconds(
						Convert.ToInt64(decimal.Round(Convert.ToDecimal(value[0].ToString())))).DateTime;
					var metricValue = Convert.ToDecimal(value[1].ToString());

					if (experimentResult.ContainsKey(result.metric.pod))
					{
						var experimentResultEntry = experimentResult[result.metric.pod];

						var tupleData = (metricDate, metricValue);
						experimentResultEntry.Add(tupleData);
					}
					else
					{
						experimentResult[result.metric.pod] = new List<(DateTime Date, decimal Value)> {
							(metricDate, metricValue) };
					}
				}
			}

			var sortedResult = new Dictionary<string, List<(DateTime Date, decimal Value)>>();
			foreach (var item in experimentResult)
			{
				sortedResult[item.Key] = item.Value.OrderBy(x => x.Date).ToList();
			}

			Console.WriteLine("Completed result parsing");

			return sortedResult;
		}
		
	}
}