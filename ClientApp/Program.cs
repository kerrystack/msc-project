using ClientApp;


// Execute load
//new LoadTester().ExecuteLoad(1000);

// Get metrics
var metrics = await MetricGetter.Get().ConfigureAwait(false);

//// Parse result by parsing metric data and extracting data required to generate result file
var resultParser = new ResultParser();
var parseResult = resultParser.Parse(metrics);

//// Create experiment result, in a file and saves to file system
var resultCreator = new ResultCreator();
resultCreator.Create(parseResult);

Console.ReadKey();
