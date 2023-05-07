using ClientApp.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;

namespace ClientApp
{
	/// <summary>
	/// https://stackoverflow.com/questions/63347233/how-to-get-cpu-and-memory-usage-of-pod-in-percentage-using-promethus
	/// </summary>
	public class MetricGetter2
	{
		// TODO: VPA
		// kube_pod_container_resource_requests{pod=~"hamster.*"}
		// kube_pod_container_resource_limits{pod=~"hamster.*"}
		public static string GetCPUMetrics()
		{
			var queryUrl = "http://localhost:9090/api/v1/query";

			var query = $"100 * max(rate(container_cpu_usage_seconds_total[5m]) / on(container, pod) kube_pod_container_resource_limits{{ resource = \"cpu\"}}) by(pod)";

			var client = new HttpClient();
			client.BaseAddress = new Uri("http://localhost:9090/api/v1/query");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var queryParameters = new Dictionary<string, string>
			{
				["query"] = query,
				["time"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
			};

			var response = client.GetAsync(QueryHelpers.AddQueryString(queryUrl, queryParameters)).Result;
			var result = response.Content.ReadAsStringAsync().Result;

			Console.WriteLine($"CPU metrics retrieved @ {DateTime.UtcNow}");

			return result;
		}

		public static string GetMemoryMetrics()
		{
			var queryUrl = "http://localhost:9090/api/v1/query";

			var query = $"100 * max(container_memory_working_set_bytes / on(container, pod) kube_pod_container_resource_limits{{ resource = \"memory\" }}) by(pod)";

			var client = new HttpClient();
			client.BaseAddress = new Uri("http://localhost:9090/api/v1/query");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var queryParameters = new Dictionary<string, string>
			{
				["query"] = query,
				["time"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
			};

			var response = client.GetAsync(QueryHelpers.AddQueryString(queryUrl, queryParameters)).Result;
			var result = response.Content.ReadAsStringAsync().Result;

			Console.WriteLine($"Memory metrics retrieved @ {DateTime.UtcNow}");

			return result;
		}

		public static string GetResourceCPURequests(string podPrefix)
		{
			var queryUrl = "http://localhost:9090/api/v1/query";

			var query = $"kube_pod_container_resource_requests{{pod=~\"{podPrefix}.*\", resource=\"cpu\"}}";

			var client = new HttpClient();
			client.BaseAddress = new Uri("http://localhost:9090/api/v1/query");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var queryParameters = new Dictionary<string, string>
			{
				["query"] = query,
				["time"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
			};

			var response = client.GetAsync(QueryHelpers.AddQueryString(queryUrl, queryParameters)).Result;
			var result = response.Content.ReadAsStringAsync().Result;

			Console.WriteLine($"Resource CPU metrics retrieved @ {DateTime.UtcNow}");

			return result;
		}

		public static string GetResourceMemoryRequests(string podPrefix)
		{
			var queryUrl = "http://localhost:9090/api/v1/query";

			var query = $"kube_pod_container_resource_requests{{pod =~\"{podPrefix}.*\", resource=\"memory\"}}";

			var client = new HttpClient();
			client.BaseAddress = new Uri("http://localhost:9090/api/v1/query");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var queryParameters = new Dictionary<string, string>
			{
				["query"] = query,
				["time"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
			};

			var response = client.GetAsync(QueryHelpers.AddQueryString(queryUrl, queryParameters)).Result;
			var result = response.Content.ReadAsStringAsync().Result;

			Console.WriteLine($"Resource memory metrics retrieved @ {DateTime.UtcNow}");

			return result;
		}

		public static string GetResourceCPULimits(string podPrefix)
		{
			var queryUrl = "http://localhost:9090/api/v1/query";

			var query = $"kube_pod_container_resource_limits{{pod =~\"{podPrefix}.*\", resource=\"cpu\"}}";

			var client = new HttpClient();
			client.BaseAddress = new Uri("http://localhost:9090/api/v1/query");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var queryParameters = new Dictionary<string, string>
			{
				["query"] = query,
				["time"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
			};

			var response = client.GetAsync(QueryHelpers.AddQueryString(queryUrl, queryParameters)).Result;
			var result = response.Content.ReadAsStringAsync().Result;

			Console.WriteLine($"CPU Limit metrics retrieved @ {DateTime.UtcNow}");

			return result;
		}

		public static string GetResourceMemoryLimits(string podPrefix)
		{
			var queryUrl = "http://localhost:9090/api/v1/query";

			var query = $"kube_pod_container_resource_limits{{pod =~\"{podPrefix}.*\", resource=\"memory\"}}";

			var client = new HttpClient();
			client.BaseAddress = new Uri("http://localhost:9090/api/v1/query");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var queryParameters = new Dictionary<string, string>
			{
				["query"] = query,
				["time"] = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
			};

			var response = client.GetAsync(QueryHelpers.AddQueryString(queryUrl, queryParameters)).Result;
			var result = response.Content.ReadAsStringAsync().Result;

			Console.WriteLine($"Memory metrics retrieved @ {DateTime.UtcNow}");

			return result;
		}
	}
}
