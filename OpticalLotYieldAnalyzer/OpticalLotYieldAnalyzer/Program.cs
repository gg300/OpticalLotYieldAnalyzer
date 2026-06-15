using OpticalLotYieldAnalyzer;

MeasurementGenerator mes = new MeasurementGenerator();
for(int i = 0; i < 1000; i++)
    Console.WriteLine(mes.Generate(10,1,0.1));
