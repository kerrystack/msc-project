namespace ClientApp
{
	public class ResultCreator2
	{
		public void Create(Dictionary<string, List<(DateTime Date, decimal Value)>> parsedResultData)
		{
			Console.WriteLine("Starting result creation");

			var resultfilePath = $@"C:\D\msc_project\msc-project\experiments\results\native_horizontal_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
			double resourceSeconds = 0;
			using var streamWriter = new StreamWriter(resultfilePath);
			foreach (var parsedResult in parsedResultData)
			{
				var fromDate = parsedResult.Value.First().Date;
				var toDate = parsedResult.Value.Last().Date;

				streamWriter.WriteLine($"Pod:{parsedResult.Key} active from ({parsedResult.Value.First().Date}) to ({parsedResult.Value.Last().Date})");
				var totalPodSeconds = Math.Abs((fromDate - toDate).TotalSeconds);
				streamWriter.WriteLine($"Pod:{parsedResult.Key} active for ({totalPodSeconds}) seconds\n");
				resourceSeconds += totalPodSeconds;


				streamWriter.WriteLine($"Pod stats\n");
				foreach (var item in parsedResult.Value)
				{
					streamWriter.WriteLine($"Date:{item.Date}, Value: {item.Value}");
				}
			}

			streamWriter.WriteLine($"Total Pod Active Seconds:{resourceSeconds}");

			Console.WriteLine("Finished result creation");
		}
	}
}
