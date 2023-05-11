using ClientApp;

var testParameters = Experiments.hpa_native_low_mode_only();

// SetUp test specific actions
var setupScriptPath = $@"C:\D\msc_project\msc-project\experiments\{testParameters.TestUseCaseIdentifier}\setup.ps1";
new ScriptExecutor().Execute(setupScriptPath, "setup test specific actions", 25);

// Create autoscaler
var createAutoscalerScriptPath = $@"C:\D\msc_project\msc-project\experiments\{testParameters.TestUseCaseIdentifier}\create-autoscaler.ps1";
new ScriptExecutor().Execute(createAutoscalerScriptPath, "create autoscaler specific action", 0);

// Start collecting metrics
var metricsAccumulator = new MetricAccumulator();
Task task = Task.Run(() => metricsAccumulator.Accumulate(testParameters.ScalingType, testParameters.PodPrefix, 10));

// Executing load
var executeLoadScriptPath = $@"C:\D\msc_project\msc-project\experiments\{testParameters.TestUseCaseIdentifier}\execute-load.ps1";
new ScriptExecutor().Execute(executeLoadScriptPath, "executing load", testParameters.TestDurationInSeconds);

var testStartCheckpoint = DateTime.UtcNow.AddSeconds(-testParameters.TestDurationInSeconds);
var highModeStartCheckpoint = testStartCheckpoint.AddSeconds(testParameters.HighModeStartingPointInSeconds);
var highModeEndCheckpoint = highModeStartCheckpoint.AddSeconds(testParameters.HighModeDurationInSeconds);

// Generate graph
var graphGenerator = new GraphGenerator();
graphGenerator.Generate(
	testParameters.ScalingType,
	testParameters.TestUseCaseIdentifier,
	highModeStartCheckpoint,
	highModeEndCheckpoint,
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