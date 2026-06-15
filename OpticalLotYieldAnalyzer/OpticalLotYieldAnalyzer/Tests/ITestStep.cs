using System;
using System.Collections.Generic;
using System.Text;

namespace OpticalLotYieldAnalyzer;

public interface ITestStep
{
    string Name { get; }
    TestResult Test(MeasurementGenerator generator, ProductConfig config);
}

