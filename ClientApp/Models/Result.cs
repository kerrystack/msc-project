using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
	public class Result
	{
		public Metric metric { get; set; }

		public List<object[]> values { get; set; }
	}
}
