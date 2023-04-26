using ScottPlot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
	internal class GraphGenerator
	{
		public void Generate()
		{
			var plt = new ScottPlot.Plot(600, 400);

			double[] xs = DataGen.Consecutive(24);
			double[] ys = DataGen.Sin(24);

			double[] x2s = DataGen.Consecutive(51);
			double[] y2s = DataGen.Sin(51);

			plt.AddScatterLines(xs, ys, Color.Red, 3,label:"pod1");
			plt.AddScatterLines(x2s, y2s, Color.Green, 3, label: "pod2");
			plt.Legend();
			plt.XLabel("This is the X Axis");
			plt.YLabel("This is the Y Axis");

			//plt.YAxis.Label("Vertical Axis", Color.Magenta, size: 24, fontName: "Comic Sans MS");
			// This method will set the color of axis labels, lines, ticks, and tick labels
			//plt.XAxis.Color(Color.Green);

			plt.SaveFig("scatter_lineplot.png");

		}

		public void GenerateWithDates()
		{
			int pointCount = 100;
			Random rand = new Random(0);
			double[] values = ScottPlot.DataGen.RandomWalk(rand, pointCount);
			DateTime[] dates = Enumerable.Range(0, pointCount)
										  .Select(x => new DateTime(2016, 06, 27).AddSeconds(x))
										  .ToArray();

			double[] values2 = ScottPlot.DataGen.RandomWalk(rand, pointCount);
			DateTime[] dates2 = Enumerable.Range(0, pointCount)
										  .Select(x => new DateTime(2016, 06, 27).AddSeconds(x*2))
										  .ToArray();

			// use LINQ and DateTime.ToOADate() to convert DateTime[] to double[]
			double[] xs = dates.Select(x => x.ToOADate()).ToArray();
			double[] xs2 = dates2.Select(x => x.ToOADate()).ToArray();

			// plot the double arrays using a traditional scatter plot
			var plt = new ScottPlot.Plot(600, 400);
			plt.AddScatter(xs, values,label:"testing");
			plt.AddScatter(xs2, values2, color:Color.Green, label: "testing2");
			plt.Legend();
			plt.XLabel("This is the X Axis");
			plt.YLabel("This is the Y Axis");

			// indicate the horizontal axis tick labels should display DateTime units
			plt.XAxis.DateTimeFormat(true);

			// add padding to the right to prevent long dates from flowing off the figure
			plt.YAxis2.SetSizeLimit(min: 40);

			// save the output
			plt.Title("Scatter Plot with DateTime Axis");
			plt.SaveFig("datetime-scatter.png");


		}

		//public void GenerateTest()
		//{
		//	var dictionary = new Dictionary<string, List<(DateTime Date, decimal Value)>>();
		//	dictionary.Add("pod1", new List<(DateTime Date, decimal Value)>()
		//	{
		//		(DateTime.UtcNow, 3),
		//		(DateTime.UtcNow.AddSeconds(2), 3),
		//		(DateTime.UtcNow.AddSeconds(4), 6),
		//		(DateTime.UtcNow.AddSeconds(5), 1),
		//		(DateTime.UtcNow.AddSeconds(6), 4),
		//		(DateTime.UtcNow.AddSeconds(7), 5),
		//		(DateTime.UtcNow.AddSeconds(8), 6),
		//		(DateTime.UtcNow.AddSeconds(9), 9),
		//		(DateTime.UtcNow.AddSeconds(13), 11),
		//		(DateTime.UtcNow.AddSeconds(17), 7)
		//	});

		//	dictionary.Add("pod2", new List<(DateTime Date, decimal Value)>()
		//	{
		//		(DateTime.UtcNow, 3),
		//		(DateTime.UtcNow.AddSeconds(2), 8),
		//		(DateTime.UtcNow.AddSeconds(4), 12),
		//		(DateTime.UtcNow.AddSeconds(5), 7),
		//		(DateTime.UtcNow.AddSeconds(6), 9),
		//		(DateTime.UtcNow.AddSeconds(7), 2),
		//		(DateTime.UtcNow.AddSeconds(8), 12),
		//		(DateTime.UtcNow.AddSeconds(9), 13),
		//		(DateTime.UtcNow.AddSeconds(13), 17),
		//		(DateTime.UtcNow.AddSeconds(17), 20)
		//	});

		//	Generate(dictionary);
		//}


		public void Generate(
			Dictionary<string, List<(DateTime Date, decimal Value)>> cpuInput,
			Dictionary<string, List<(DateTime Date, decimal Value)>> memoryInput)
		{
			var plt = new ScottPlot.Plot(600, 400);
			plt.XLabel("Time (UTC)");
			plt.YLabel("CPU Usage");
			plt.XAxis.DateTimeFormat(true);
			plt.Legend();
			plt.YAxis2.SetSizeLimit(min: 40);
			plt.Title("Scatter Plot with DateTime Axis");

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

				var convertedDates = dates.Select(x => x.ToOADate()).ToArray();

				plt.AddScatter(convertedDates, values.ToArray(), label: podIdentifier);
			}

			var plt2 = new ScottPlot.Plot(600, 400);
			plt2.XLabel("Time (UTC)");
			plt2.YLabel("Memory Usage");
			plt2.XAxis.DateTimeFormat(true);
			plt2.Legend();
			plt2.YAxis2.SetSizeLimit(min: 40);
			plt2.Title("Scatter Plot with DateTime Axis");

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

				var convertedDates = dates.Select(x => x.ToOADate()).ToArray();

				plt2.AddScatter(convertedDates, values.ToArray(), label: podIdentifier);
			}

			var bmp1 = plt.Render();
			var bmp2 = plt2.Render();

			using (var bmp = new System.Drawing.Bitmap(1400, 900))
			using (var gfx = System.Drawing.Graphics.FromImage(bmp))
			{
				gfx.DrawImage(bmp1, 0, 0);
				gfx.DrawImage(bmp2, 600, 0);
				bmp.Save("MultiPlot.png");
			}

			var filePath = @"MultiPlot.png";
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
