using OpticalLotYieldAnalyzer;

public class YieldReportTests
{
    [Test]
    public void FromUnitResults_AllPass_Yields100Percent()
    {
        var units = new List<UnitResult>
        {
            new() { TestResults = new() { new TestResult { TestName = "PowerTest", Status = "PASS" } } },
            new() { TestResults = new() { new TestResult { TestName = "PowerTest", Status = "PASS" } } }
        };

        var report = YieldReport.FromUnitResults(units);

        Assert.That(report.YieldPercent, Is.EqualTo(100));
        Assert.That(report.FailuresByTest, Is.Empty);
    }

    [Test]
    public void FromUnitResults_OneFailure_TracksFailureMode()
    {
        var units = new List<UnitResult>
        {
            new() { TestResults = new() { new TestResult { TestName = "OsnrTest", Status = "PASS" } } },
            new() { TestResults = new() { new TestResult { TestName = "OsnrTest", Status = "FAIL" } } }
        };

        var report = YieldReport.FromUnitResults(units);

        Assert.That(report.YieldPercent, Is.EqualTo(50));
        Assert.That(report.FailuresByTest["OsnrTest"], Is.EqualTo(1));
    }
}