# Optical Lot Yield Analyzer

A C# console application that simulates a production test run for a lot of optical transceiver modules, extending the architecture from [Optical Module Test Station Simulator](https://github.com/gg300/OpticalTestStation) to a multi-unit, multi-product manufacturing context.

The application runs a configurable batch of units through the same functional test sequence (power, wavelength, OSNR, bias current), with each unit's measurements generated from a realistic statistical distribution and an injected defect rate, simulating natural unit-to-unit process variation seen on a production line.

After the run, it produces a yield report including overall pass rate, a Pareto breakdown of failure modes by test, and process capability (Cpk) for each measured parameter, surfacing which tests and parameters are driving yield loss, similar to data review workflows used in production test engineering.

## Architecture highlights

- Reuses the `ITestStep` abstractions from the original test station project
- Product-specific test limits and nominal values are externalized to JSON, supporting multiple products with different specs (NPI through end-of-life)
- Statistical analysis (mean, standard deviation, Cpk) implemented from first principles
- Pareto-style failure mode reporting to highlight where process variation concentrates

## Tech stack

C# / .NET, JSON configuration, CSV reporting

---

*Built as a self-directed learning project extending [Optical Module Test Station Simulator](https://github.com/gg300/OpticalTestStation) to explore yield analysis and process capability concepts used in manufacturing test engineering.*
