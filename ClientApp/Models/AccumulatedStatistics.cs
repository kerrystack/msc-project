namespace ClientApp.Models
{
	public class AccumulatedStatistics
	{
		private Dictionary<string, List<(DateTime Date, decimal Value)>> _cpuStatistics;
		private Dictionary<string, List<(DateTime Date, decimal Value)>> _memoryStatistics;

		
		public Dictionary<string, List<(DateTime Date, decimal Value)>> CPUStatistics { get { return _cpuStatistics; }}

		public Dictionary<string, List<(DateTime Date, decimal Value)>> MemoryStatistics {  get { return _memoryStatistics; } }

		public AccumulatedStatistics()
		{
			_cpuStatistics = new Dictionary<string, List<(DateTime Date, decimal Value)>>();
			_memoryStatistics = new Dictionary<string, List<(DateTime Date, decimal Value)>>();
		}

		public void AddCPUStats(string podPrefix, Dictionary<string, List<(DateTime Date, decimal Value)>> subject)
		{
			foreach (var key in subject.Keys)
			{
				if(!key.StartsWith(podPrefix)) {  continue; }

				if (_cpuStatistics.ContainsKey(key))
				{
					_cpuStatistics[key].Add(subject[key].Single());
				}
				else
				{
					_cpuStatistics[key] = new List<(DateTime Date, decimal Value)>() { subject[key].Single() };
				}
			}
		}

		public void AddMemoryStats(string podPrefix, Dictionary<string, List<(DateTime Date, decimal Value)>> subject)
		{
			foreach (var key in subject.Keys)
			{
				if (!key.StartsWith(podPrefix)) { continue; }

				if (_memoryStatistics.ContainsKey(key))
				{
					_memoryStatistics[key].Add(subject[key].Single());
				}
				else
				{
					_memoryStatistics[key] = new List<(DateTime Date, decimal Value)>() { subject[key].Single() };
				}
			}
		}
	}
}
