﻿using System.Diagnostics;
using System.Drawing;

namespace ClientApp
{
	internal class GraphGenerator
	{
		public void GenerateTest()
		{
			var dictionary = new Dictionary<string, List<(DateTime Date, decimal Value)>>();
			dictionary.Add("pod1", new List<(DateTime Date, decimal Value)>()
			{
				(DateTime.UtcNow, 3),
				(DateTime.UtcNow.AddSeconds(2), 3),
				(DateTime.UtcNow.AddSeconds(4), 6),
				(DateTime.UtcNow.AddSeconds(5), 1),
				(DateTime.UtcNow.AddSeconds(6), 4),
				(DateTime.UtcNow.AddSeconds(7), 5),
				(DateTime.UtcNow.AddSeconds(8), 6),
				(DateTime.UtcNow.AddSeconds(9), 9),
				(DateTime.UtcNow.AddSeconds(13), 11),
				(DateTime.UtcNow.AddSeconds(17), 7)
			});

			dictionary.Add("pod2", new List<(DateTime Date, decimal Value)>()
			{
				(DateTime.UtcNow, 3),
				(DateTime.UtcNow.AddSeconds(2), 8),
				(DateTime.UtcNow.AddSeconds(4), 12),
				(DateTime.UtcNow.AddSeconds(5), 7),
				(DateTime.UtcNow.AddSeconds(6), 9),
				(DateTime.UtcNow.AddSeconds(7), 2),
				(DateTime.UtcNow.AddSeconds(8), 12),
				(DateTime.UtcNow.AddSeconds(9), 13),
				(DateTime.UtcNow.AddSeconds(13), 17),
				(DateTime.UtcNow.AddSeconds(17), 20)
			});

			Generate("some_use_case", dictionary, dictionary);
		}


		public void Generate(
			string testUseCaseIdentifier,
			Dictionary<string, List<(DateTime Date, decimal Value)>> cpuInput,
			Dictionary<string, List<(DateTime Date, decimal Value)>> memoryInput)
		{
			Console.WriteLine("Starting graph generation");

			var plt = new ScottPlot.Plot(800, 400);
			plt.XLabel("Time (UTC)");
			plt.YLabel("CPU Usage");
			plt.XAxis.DateTimeFormat(true);
			plt.YAxis2.SetSizeLimit(min: 40);
			plt.Title("Pod CPU Usage over time");

			foreach (var podIdentifier in cpuInput.Keys)
			{
				var values = new List<double>();
				var dates = new List<DateTime>();

				var podStatistics = cpuInput[podIdentifier];

				foreach (var item in podStatistics)
				{
					dates.Add(item.Date);
					values.Add(Convert.ToDouble(item.Value));
				}

				var convertedDates = dates.Select(_ => _.ToOADate()).ToArray();

				plt.AddScatter(convertedDates, values.ToArray(), label: podIdentifier);
			}

			var plt2 = new ScottPlot.Plot(800, 400);
			plt2.XLabel("Time (UTC)");
			plt2.YLabel("Memory Usage");
			plt2.XAxis.DateTimeFormat(true);
			plt2.YAxis2.SetSizeLimit(min: 40);
			plt2.Title("Pod Memory Usage over time");

			foreach (var podIdentifier in memoryInput.Keys)
			{
				var values = new List<double>();
				var dates = new List<DateTime>();

				var podStatistics = memoryInput[podIdentifier];

				foreach (var item in podStatistics)
				{
					dates.Add(item.Date);
					values.Add(Convert.ToDouble(item.Value));
				}

				var convertedDates = dates.Select(_ => _.ToOADate()).ToArray();

				plt2.AddScatter(convertedDates, values.ToArray(), label: podIdentifier);
			}

			var bmpPlot = plt.GetBitmap();
			var bmpLegend = plt.RenderLegend();
			var bmpPlot2 = plt2.GetBitmap();
			var bmpLegend2 = plt2.RenderLegend();

			var bmp = new Bitmap(1200, bmpPlot.Height * 2 + 50);
			using Graphics gfx = Graphics.FromImage(bmp);
			gfx.Clear(Color.White);
			gfx.DrawImage(bmpPlot, 0, 0);
			gfx.DrawImage(bmpLegend, bmpPlot.Width, 40);
			gfx.DrawImage(bmpPlot2, 0, 450);
			gfx.DrawImage(bmpLegend2, bmpPlot.Width, 490);
			bmp.Save("MultiPlot.png");

			var filePath = $"{testUseCaseIdentifier}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.png";

			Console.WriteLine("Completed graph generation");
			Thread.Sleep(1000);

			ProcessStartInfo Info = new ProcessStartInfo()
			{
				FileName = "mspaint.exe",
				WindowStyle = ProcessWindowStyle.Maximized,
				Arguments = filePath
			};
			Process.Start(Info);
		}
	}
}
