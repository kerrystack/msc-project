using ClientApp.Models;
using System.Text.Json;

namespace ClientApp
{
	public class ResultParser
	{
		public Dictionary<string, List<(DateTime Date, decimal Value)>> Parse(string metricData, string keySuffix = "")
		{
			var sampleResult = new Dictionary<string, List<(DateTime Date, decimal Value)>>();

			var rootData = JsonSerializer.Deserialize<Root>(metricData);

			foreach (var result in rootData.data.result)
			{
				var metricDate = DateTimeOffset.FromUnixTimeSeconds(
					Convert.ToInt64(decimal.Round(Convert.ToDecimal(result.value[0].ToString())))).DateTime;
				var metricValue = Convert.ToDecimal(result.value[1].ToString());

				sampleResult[$"{result.metric.pod}{keySuffix}"] = new List<(DateTime Date, decimal Value)> { (metricDate, metricValue) };
			}

			return sampleResult;
		}
	}
}