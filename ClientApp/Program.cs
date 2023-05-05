using ClientApp;
using ClientApp.Models;

//var testParameters = new TestParameters();
//testParameters.ScalingType = ScalingType.Horizontal;
//testParameters.TestUseCaseIdentifier = "uc-horizontal-native-cpu-only";
//testParameters.LowModeSleepInSeconds = 10;
//testParameters.TestDurationInSeconds = 240;
//testParameters.HighModeStartingPointInSeconds = 30;
//testParameters.HighModeDurationInSeconds = 120;
//testParameters.HighModeThreadCount = 10;

var testParameters = new TestParameters();
testParameters.ScalingType = ScalingType.Vertical;
testParameters.PodPrefix = "php-apache";
testParameters.TestUseCaseIdentifier = "vpa_native";
testParameters.LowModeSleepInSeconds = 10;
testParameters.TestDurationInSeconds = 600;
testParameters.HighModeStartingPointInSeconds = 30;
testParameters.HighModeDurationInSeconds = 180;
testParameters.HighModeThreadCount = 10;

// SetUp test specific actions
var setupScriptPath = $@"C:\D\msc_project\msc-project\experiments\{testParameters.TestUseCaseIdentifier}\setup.ps1";
new ScriptExecutor().Execute(setupScriptPath, "setup test specific actions", 40);

// Create autoscaler
var createAutoscalerScriptPath = $@"C:\D\msc_project\msc-project\experiments\{testParameters.TestUseCaseIdentifier}\create-autoscaler.ps1";
new ScriptExecutor().Execute(createAutoscalerScriptPath, "create autoscaler specific action", 0);

// Start collecting metrics
var metricsAccumulator = new MetricAccumulator();
Task task = Task.Run(() => metricsAccumulator.Accumulate(testParameters.ScalingType, testParameters.PodPrefix, 10));

// Execute load
var loadTestResult = await new LoadTester().ExecuteLoad(testParameters);

// Generate graph
var graphGenerator = new GraphGenerator();
graphGenerator.Generate(
	testParameters.ScalingType,
	testParameters.TestUseCaseIdentifier,
	metricsAccumulator.Statistics.CPUStatistics,
	metricsAccumulator.Statistics.MemoryStatistics);

// Generate usage results
var resultCreator = new ResultCreator();
resultCreator.Create(testParameters.ScalingType,
	testParameters.TestUseCaseIdentifier,
	metricsAccumulator.Statistics.CPUStatistics,
	metricsAccumulator.Statistics.MemoryStatistics);

// Teardown test specific actions
//var teardownScriptPath = $@"C:\D\msc_project\msc-project\ClientApp\{testParameters.TestUseCaseIdentifier}-teardown.ps1";
//new ScriptExecutor().Execute(teardownScriptPath, "teardown test specific actions");

Console.ReadKey();