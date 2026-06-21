namespace OpticalLotYieldAnalyzer;

public class YieldReport
{
    public int TotalUnits { get; set; }
    public int PassedUnits { get; set; }
    public double YieldPercent => TotalUnits == 0 ? 0 : 100.0 * PassedUnits / TotalUnits;
    public Dictionary<string, int> FailuresByTest { get; set; } = new();

    public static YieldReport FromUnitResults(List<UnitResult> units)
    {
        var report = new YieldReport { TotalUnits = units.Count };

        foreach (var unit in units)
        {
            if (unit.TestsStatus == "PASS")
            {
                report.PassedUnits++;
                continue;
            }

            foreach (var result in unit.TestResults.Where(r => r.Status == "FAIL"))
            {
                report.FailuresByTest[result.TestName] =
                    report.FailuresByTest.GetValueOrDefault(result.TestName, 0) + 1;
            }
        }

        return report;
    }

    public IEnumerable<KeyValuePair<string, int>> ParetoOrder() =>
        FailuresByTest.OrderByDescending(kv => kv.Value);
}