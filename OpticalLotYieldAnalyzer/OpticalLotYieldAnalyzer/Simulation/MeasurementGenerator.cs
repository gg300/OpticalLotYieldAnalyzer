using System;
using System.Collections.Generic;
using System.Text;

namespace OpticalLotYieldAnalyzer;

public class MeasurementGenerator
{
    private readonly Random _rand = new();
    public double Generate(double nominal,double stdDev,double defectRate)
    {
        if(_rand.NextDouble() < defectRate)
        {
            return nominal + stdDev * 8;
        }
        double u1 = 1.0 - _rand.NextDouble();
        double u2 = _rand.NextDouble();
        double z = Math.Sqrt(-2.0* Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2); // Box–Muller transform
        return nominal + z * stdDev;
    }
}
