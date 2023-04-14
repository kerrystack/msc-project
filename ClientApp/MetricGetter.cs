using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;

namespace ClientApp
{
	public class MetricGetter
	{
		//var queryUrl = "http://localhost:9090/api/v1/query";
		//// var query = "avg_over_time(container_cpu_usage_seconds_total{pod_name='<pod-name>'}[5m]) * 100"; // Query for pod CPU usage as a percentage
		//var query = "rate(container_cpu_usage_seconds_total{pod=~\"example.*\"}[5m])";
		//query = "sum(rate(container_cpu_usage_seconds_total{container!~\"POD|\"}[1m])) by (pod)";
		// This might be what Im looking for
		//query = "container_cpu_usage_seconds_total{ pod=~\"php.*\"}[1h]";

		public static async Task<string> Get()
		{
			var queryUrl = "http://localhost:9090/api/v1/query";
			var query = "container_cpu_usage_seconds_total{ pod=~\"php.*\"}[1h]";
			var client = new HttpClient();
			client.BaseAddress = new Uri("http://localhost:9090/api/v1/query");
			client.DefaultRequestHeaders.Accept.Add(
					new MediaTypeWithQualityHeaderValue("application/json"));
			var queryParameters = new Dictionary<string, string>
			{
				["query"] = query
			};

			var response = await client.GetAsync(QueryHelpers.AddQueryString(queryUrl, queryParameters));
			return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
		}
	}
}
