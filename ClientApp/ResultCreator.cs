namespace ClientApp
{
	public class ResultCreator
	{
		public void Create(string testUseCaseIdentifier, Dictionary<string, List<(DateTime Date, decimal Value)>> parsedResultData)
		{
			Console.WriteLine("Starting result creation");

			var resultfilePath = $@"C:\D\msc_project\msc-project\experiments\results\{testUseCaseIdentifier}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
			double resourceSeconds = 0;
			using var streamWriter = new StreamWriter(resultfilePath);
			foreach (var parsedResult in parsedResultData)
			{
				var fromDate = parsedResult.Value.First().Date;
				var toDate = parsedResult.Value.Last().Date;
				var totalPodSeconds = Math.Abs((fromDate - toDate).TotalSeconds);

				streamWriter.WriteLine($"Pod:{parsedResult.Key} active from ({parsedResult.Value.First().Date}) to ({parsedResult.Value.Last().Date}) for ({totalPodSeconds}) seconds");
	
				resourceSeconds += totalPodSeconds;
			}

			streamWriter.WriteLine($"Total Pod Active Seconds:{resourceSeconds}");

			Console.WriteLine("Finished result creation");
		}
	}
}
