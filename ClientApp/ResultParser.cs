using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientApp
{
	public class ResultParser
	{
		public Dictionary<string, (DateTime FromDate, DateTime ToDate)> Parse(string metricData)
		{
			var experimentResult = new Dictionary<string, (DateTime FromDate, DateTime ToDate)>();

			var rootData = JsonSerializer.Deserialize<Root>(metricData);

			foreach (var result in rootData.data.result)
			{
				var startDateUnixEpoch = Convert.ToInt64(decimal.Round(Convert.ToDecimal(result.values.First()[0].ToString())));
				var endDateUnixEpoch = Convert.ToInt64(decimal.Round(Convert.ToDecimal(result.values.Last()[0].ToString())));

				var fromDate = DateTimeOffset.FromUnixTimeSeconds(startDateUnixEpoch).DateTime;
				var toDate = DateTimeOffset.FromUnixTimeSeconds(endDateUnixEpoch).DateTime;

				if (experimentResult.ContainsKey(result.metric.pod))
				{
					var experimentResultEntry = experimentResult[result.metric.pod];
					if (fromDate < experimentResultEntry.FromDate) { experimentResultEntry.FromDate = fromDate; }
					if (toDate > experimentResultEntry.ToDate) { experimentResultEntry.ToDate = toDate; }
				}
				else
				{
					experimentResult[result.metric.pod] = new(fromDate, toDate);
				}
			}

			return experimentResult;
		}
	}
}
