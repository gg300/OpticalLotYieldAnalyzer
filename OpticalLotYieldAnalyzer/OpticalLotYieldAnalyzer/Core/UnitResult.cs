using System;
using System.Collections.Generic;
using System.Text;

namespace OpticalLotYieldAnalyzer;

public class UnitResult
{
    public string SerialNumber { get; set; } = "";
    public List<TestResult> TestResults { get; set; } = new();
    public string TestsStatus => TestResults.Any(result => result.Status == "FAIL") ? "FAIL" : "PASS";
}

