using System;
using System.Collections.Generic;
using System.Text;

namespace OpticalLotYieldAnalyzer;

public class ParameterTest : ITestStep
{
    private readonly string _parameterKey;
    public ParameterTest(string parameterKey)
    {
        _parameterKey  = parameterKey;
    }
    public string Name => $"{_parameterKey}Test";
    public TestResult Test(MeasurementGenerator generator, ProductConfig config)
    {
        var spec = config.Parameters[_parameterKey];
        double value = generator.Generate(spec.Nominal,spec.StdDev,spec.DefectRate);
        bool passed = ((!spec.Min.HasValue || value >= spec.Min) && (!spec.Max.HasValue || value <= spec.Max));
        return new TestResult
        {
            TestName = Name,
            ParameterKey = _parameterKey,
            MeasuredValue=value,
            Unit = spec.Unit,
            MinLimit=spec.Min,
            MaxLimit=spec.Max,
            Status = passed?"PASS":"FAIL"
        };
    }
}

