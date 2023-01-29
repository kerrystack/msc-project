using Microsoft.AspNetCore.Mvc;

namespace SampleWebApplication1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProcessorController : ControllerBase
	{
		private readonly ILogger<ProcessorController> _logger;

		public ProcessorController(ILogger<ProcessorController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetProcessedResult")]
		public ProcessResult Get()
		{
			return new ProcessResult() { Value = "TestResult" };

		}
	}
}