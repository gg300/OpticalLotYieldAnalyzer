using System;
using System.Collections.Generic;
using System.Text;

namespace OpticalLotYieldAnalyzer;

public class ProductConfig
{
    public string ProductId { get; set; } = "";
    public int LotSize { get; set; }
    public Dictionary<string, ParameterSpec> Parameters { get; set; } = new();
}

public class ParameterSpec
{
    public double Nominal { get; set; }
    public double StdDev { get; set; }
    public double DefectRate { get; set; }
    public double? Min { get; set; }
    public double? Max { get; set; }
    public string Unit { get; set; } = "";
}