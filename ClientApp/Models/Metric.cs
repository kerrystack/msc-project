namespace ClientApp.Models
{
	public class Metric
	{
		public string __name__ { get; set; }

		public string container { get; set; }

		public string cpu { get; set; }

		public string endpoint { get; set; }

		public string id { get; set; }

		public string image { get; set; }

		public string instance { get; set; }

		public string job { get; set; }

		public string metrics_path { get; set; }

		public string name { get; set; }

		public string @namespace { get; set; }

		public string node { get; set; }

		public string pod { get; set; }

		public string service { get; set; }
	}
}
