namespace OpticalLotYieldAnalyzer;

public class LotRunner
{
    private readonly List<ITestStep> _steps;
    private readonly MeasurementGenerator _generator;
    private readonly ProductConfig _config;

    public LotRunner(List<ITestStep> steps, MeasurementGenerator generator, ProductConfig config)
    {
        _steps = steps;
        _generator = generator;
        _config = config;
    }

    public List<UnitResult> RunLot()
    {
        var results = new List<UnitResult>();

        for (int i = 1; i <= _config.LotSize; i++)
        {
            var unit = new UnitResult { SerialNumber = $"SN-{i:D3}" };

            foreach (var step in _steps)
            {
                unit.TestResults.Add(step.Test(_generator, _config));
            }

            results.Add(unit);
        }

        return results;
    }
}