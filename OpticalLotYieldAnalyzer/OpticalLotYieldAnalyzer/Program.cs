using System.Text.Json;
using OpticalLotYieldAnalyzer;

string productFile = args.Length > 0 ? args[0] : "product-100G.json";
string json = File.ReadAllText(Path.Combine("Config", productFile));
var config = JsonSerializer.Deserialize<ProductConfig>(json)!;

Console.WriteLine($"LotYieldAnalyzer - Product: {config.ProductId} - Lot size: {config.LotSize}");
Console.WriteLine(new string('-', 60));

var generator = new MeasurementGenerator();
var steps = new List<ITestStep>
{
    new ParameterTest("Power"),
    new ParameterTest("Wavelength"),
    new ParameterTest("Osnr"),
    new ParameterTest("BiasCurrent")
};

var runner = new LotRunner(steps, generator, config);
var unitResults = runner.RunLot();

// print one line per unit
foreach (var unit in unitResults)
{
    var fields = unit.TestResults.Select(r => $"{r.ParameterKey} {r.MeasuredValue:F2}");
    Console.WriteLine($"Unit {unit.SerialNumber}  [{unit.TestsStatus}]  {string.Join("  ", fields)}");
}

// build yield report
var yieldReport = YieldReport.FromUnitResults(unitResults);

Console.WriteLine(new string('-', 60));
Console.WriteLine($"Total units: {yieldReport.TotalUnits}   Passed: {yieldReport.PassedUnits}   Yield: {yieldReport.YieldPercent:F1}%");
Console.WriteLine();
Console.WriteLine("Failure breakdown (Pareto):");
foreach (var kv in yieldReport.ParetoOrder())
    Console.WriteLine($"  {kv.Key,-15} {kv.Value}");

// gather all measured values per parameter, compute Cpk
var stats = new Dictionary<string, (double mean, double stdDev, double cpk)>();

foreach (var key in config.Parameters.Keys)
{
    var values = unitResults
        .SelectMany(u => u.TestResults)
        .Where(r => r.ParameterKey == key)
        .Select(r => r.MeasuredValue)
        .ToList();

    var spec = config.Parameters[key];
    double mean = StatisticsCalculator.Mean(values);
    double stdDev = StatisticsCalculator.StdDev(values);
    double cpk = StatisticsCalculator.Cpk(values, spec.Min, spec.Max);

    stats[key] = (mean, stdDev, cpk);
}

Console.WriteLine();
Console.WriteLine("Process capability (Cpk):");
foreach (var (key, s) in stats)
    Console.WriteLine($"  {key,-12} Cpk = {s.cpk:F2}   (mean {s.mean:F2}, stddev {s.stdDev:F3})");

// write reports
ReportWriter.WriteUnitResultsCsv("output/unit_results.csv", unitResults);
ReportWriter.WriteYieldSummary("output/yield_summary.txt", config, yieldReport, stats);

Console.WriteLine();
Console.WriteLine("Report saved: output/unit_results.csv");
Console.WriteLine("Summary saved: output/yield_summary.txt");