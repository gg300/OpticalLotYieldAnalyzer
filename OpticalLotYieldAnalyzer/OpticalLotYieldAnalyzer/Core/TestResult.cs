using System;
using System.Collections.Generic;
using System.Text;

namespace OpticalLotYieldAnalyzer;
public class TestResult
{
    public string TestName { get; set; } = "";
    public string ParameterKey {get;set;} = "";
    public double MeasuredValue {get;set;}
    public string Unit {get;set;} = "";
    public double? MinLimit {get;set;}
    public double? MaxLimit {get;set;}
    public string Status {get;set;} = "";
}

