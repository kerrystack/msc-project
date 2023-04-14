using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
	public class ResultCreator
	{
		public void Create(Dictionary<string, (DateTime FromDate, DateTime ToDate)> parsedResultData)
		{
			var resultfilePath = $@"C:\D\msc_project\msc-project\experiments\results\native_horizontal_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
			double resourceSeconds = 0;
			using var streamWriter = new StreamWriter(resultfilePath);
			foreach (var parsedResult in parsedResultData)
			{
				streamWriter.WriteLine($"Pod:{parsedResult.Key} active from ({parsedResult.Value.FromDate}) to ({parsedResult.Value.ToDate})");
				var totalPodSeconds = (parsedResult.Value.ToDate - parsedResult.Value.FromDate).TotalSeconds;
				streamWriter.WriteLine($"Pod:{parsedResult.Key} active for ({totalPodSeconds}) seconds\n");
				resourceSeconds += totalPodSeconds;
			}

			streamWriter.WriteLine($"Total Pod Active Seconds:{resourceSeconds}");
		}
	}
}
