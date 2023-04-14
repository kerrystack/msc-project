using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
	public class LoadTester
	{
		public async void ExecuteLoad(int requestCount)
		{

			var client = new HttpClient();
			for (int i = 0; i < requestCount; i++)
			{
				var response = await client.GetAsync("http://localhost:8001");
				var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
				Console.WriteLine(responseBody);
				Console.WriteLine(i);
			}
		}
	}
}
