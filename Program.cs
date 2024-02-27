using AIContinuos;


double Rosenbrock(double[]x) //altitude negativa
{
    double value = 0.0;
    var n = x.Length - 1;
    for(int i = 0; i < n; i++)
    {
        value += 100 * Math.Pow(x[i+1] - Math.Pow(x[i], 2), 2) + Math.Pow(1 - x[i], 2);
        
    }
    return value;
}

var date = DateTime.Now;

double Restriction(double[] x)//quantidade de combustivel
{
    return 1000;
}

List<double[]> bounds = new() 
{
    new double[] {0.0, 120},
    new double[] {0.0, 110},
    new double[] {0.0, 90},
    new double[] {0.0, 80},
    new double[] {0.0, 70},
    new double[] {0.0, 60},
    new double[] {0.0, 50},
    new double[] {0.0, 40},
    new double[] {0.0, 30},
    new double[] {0.0, 20},
};


date = DateTime.Now;
var diffEvolution = new DiffEvolution(Rosenbrock, bounds, 200, Restriction);
var res = diffEvolution.Optimize(10000);
Console.WriteLine($"Solution: {res[0]} and {res[1]} | Time: {(DateTime.Now - date).TotalMicroseconds}");


