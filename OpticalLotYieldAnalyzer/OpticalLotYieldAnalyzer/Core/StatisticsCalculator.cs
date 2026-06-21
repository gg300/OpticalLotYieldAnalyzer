using System;
using System.Collections.Generic;
using System.Text;

namespace OpticalLotYieldAnalyzer;

public class StatisticsCalculator
{
    public static double Mean(List<double> values) => values.Average();
    public static double StdDev(List<double> values)
    {
        double mean = Mean(values);
        double sumSq = values.Sum(v=> Math.Pow(v-mean, 2));
        return Math.Sqrt(sumSq/(values.Count-1));
    }
    public static double Cpk(List<double> values, double? lsl, double? usl) //Process Capability Index
    {
        double mean = Mean(values);
        double std = StdDev(values);
        double cpu = usl.HasValue ? (usl.Value - mean) / (3 * std) : double.PositiveInfinity;
        double cpl = lsl.HasValue ? (mean - lsl.Value) / (3 * std) : double.PositiveInfinity;
        return Math.Min(cpu,cpl);
    }
}
