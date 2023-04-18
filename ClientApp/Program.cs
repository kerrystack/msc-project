using ClientApp;
using ClientApp.Models;

var testParameters = new TestParameters();
testParameters.TestUseCaseIdentifier = "uc-horizontal-native-cpu-only";
testParameters.LowModeSleepInSeconds = 10;
testParameters.TestDurationInSeconds = 600;
testParameters.HighModeStartingPointInSeconds = 120;
testParameters.HighModeDurationInSeconds = 120;
testParameters.HighModeThreadCount = 1;

//var testParameters = new TestParameters();
//testParameters.TestUseCaseIdentifier = "uc-vertical-native-cpu-only";
//testParameters.LowModeSleepInSeconds = 10;
//testParameters.TestDurationInSeconds = 600;
//testParameters.HighModeStartingPointInSeconds = 120;
//testParameters.HighModeDurationInSeconds = 120;
//testParameters.HighModeThreadCount = 1;

// SetUp test specific actions
var setupScriptPath = $@"C:\D\msc_project\msc-project\ClientApp\{testParameters.TestUseCaseIdentifier}-setup.ps1";
new ScriptExecutor().Execute(setupScriptPath, "setup test specific actions");

// Execute load
var loadTestResult = await new LoadTester().ExecuteLoad(testParameters);

// Get metrics
var metrics = await MetricGetter.Get(loadTestResult).ConfigureAwait(false);

// Parse result by parsing metric data and extracting data required to generate result file
var resultParser = new ResultParser();
var parseResult = resultParser.Parse(metrics);

// Create experiment result, in a file and saves to file system
var resultCreator = new ResultCreator();
resultCreator.Create(parseResult);

// Teardown test specific actions
var teardownScriptPath = $@"C:\D\msc_project\msc-project\ClientApp\{testParameters.TestUseCaseIdentifier}-teardown.ps1";
new ScriptExecutor().Execute(teardownScriptPath, "teardown test specific actions");

Console.ReadKey();
