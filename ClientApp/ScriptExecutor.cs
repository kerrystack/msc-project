using System.Diagnostics;

namespace ClientApp
{
	public class ScriptExecutor
	{
		public void Execute(string path, string loggingSuffix)
		{
			Console.WriteLine($"Starting {loggingSuffix}");

			var startInfo = new ProcessStartInfo()
			{
				FileName = "powershell.exe",
				Arguments = $"-NoProfile -ExecutionPolicy ByPass -File \"{path}\"",
				UseShellExecute = false
			};
			Process.Start(startInfo);

			Thread.Sleep(10000);

			Console.WriteLine($"Finished {loggingSuffix}");
		}
	}
}
