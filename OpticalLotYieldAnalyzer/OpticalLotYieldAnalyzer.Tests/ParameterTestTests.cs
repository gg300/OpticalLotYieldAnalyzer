using OpticalLotYieldAnalyzer;
namespace OpticalLotYieldAnalyzer.Tests;

public class ParameterTestTests
{
    [Test]
    public void PassingTest()
    {
        var config = new ProductConfig
        {
            Parameters = new Dictionary<string, ParameterSpec>
            {
                ["Power"] = new ParameterSpec { Nominal = -5, StdDev = 0, DefectRate = 0, Min = -8, Max = -3, Unit = "dBm" }
            },
        };
        var test = new ParameterTest("Power");
        var result = test.Test(new MeasurementGenerator(), config);

        Assert.That(result.Status, Is.EqualTo("PASS"));
    }
    
    [Test]
    public void FailTest()
    {
        var config = new ProductConfig { 
            Parameters = new Dictionary<string, ParameterSpec> {
                ["Power"] = new ParameterSpec {Nominal=-29, StdDev=0,DefectRate=0,Min=-8,Max=-3,Unit="dBm"}
            }
        };
        var test = new ParameterTest("Power");
        var result = test.Test(new MeasurementGenerator(),config);
        Assert.That(result.Status, Is.EqualTo("FAIL"));
    }
}