using System.Text;

namespace OpticalLotYieldAnalyzer;

public static class ReportWriter
{
    public static void WriteUnitResultsCsv(string path, List<UnitResult> units)
    {
        var sb = new StringBuilder();
        sb.AppendLine("SerialNumber,TestName,MeasuredValue,Unit,MinLimit,MaxLimit,Status");

        foreach (var unit in units)
            foreach (var r in unit.TestResults)
                sb.AppendLine($"{unit.SerialNumber},{r.TestName},{r.MeasuredValue:F3},{r.Unit},{r.MinLimit},{r.MaxLimit},{r.Status}");

        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, sb.ToString());
    }

    public static void WriteYieldSummary(
        string path,
        ProductConfig config,
        YieldReport yieldReport,
        Dictionary<string, (double mean, double stdDev, double cpk)> stats)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"YIELD SUMMARY - {config.ProductId}");
        sb.AppendLine($"Total units: {yieldReport.TotalUnits}   Passed: {yieldReport.PassedUnits}   Yield: {yieldReport.YieldPercent:F1}%");
        sb.AppendLine();
        sb.AppendLine("Failure breakdown (Pareto):");

        foreach (var kv in yieldReport.ParetoOrder())
            sb.AppendLine($"  {kv.Key,-15} {kv.Value}");

        sb.AppendLine();
        sb.AppendLine("Process capability (Cpk):");

        foreach (var (key, s) in stats)
            sb.AppendLine($"  {key,-12} Cpk = {s.cpk:F2}   (mean {s.mean:F2}, stddev {s.stdDev:F3})");

        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, sb.ToString());
    }
}