using AIContinuos;
using AIContinuous.Nuenv;
using Foguete;

var date = DateTime.Now;

var timeData = Space.Linear(0.0, 50.0, 11);
var massFlowData = Space.Uniform(70.0, 11);

date = DateTime.Now;
double[] sol;

var rocket = new Rocket(750.0, Math.PI * 0.6 * 0.6 / 4.0, 1916.0, 0.8, timeData, massFlowData);

double Rosenbrock(double[] x)
{
        return Math.Pow(x[0],2) + Math.Pow(x[1],2);

}

double Restriction(double[] x)
{
    return 1000;
}

List<double[]> bounds =
    new()
    {
        new double[] { 0.0, 120.0 },
        new double[] { 0.0, 110.0 },
        new double[] { 0.0, 90.0 },
        new double[] { 0.0, 80.0 },
        new double[] { 0.0, 70.0 },
        new double[] { 0.0, 60.0 },
        new double[] { 0.0, 50.0 },
        new double[] { 0.0, 40.0 },
        new double[] { 0.0, 30.0 },
        new double[] { 0.0, 20.0 }
    };

date = DateTime.Now;
var diffEvolution = new DiffEvolution(Rosenbrock, bounds, 200, Restriction);
var res = diffEvolution.Optimize(100);
Console.WriteLine($"Velocidade: {rocket.LaunchUntilMax()} | Time: {(DateTime.Now - date).TotalMicroseconds}");



// date = DateTime.Now;
// sol = Root.Bisection(Rosenbrock, -10, 10 );
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMicroseconds}");
// System.Console.WriteLine();


// date = DateTime.Now;
// var MyFunction = rocket.LaunchUntilMax();
// sol = Optimize.GradientDescendent(MyFunction, new double[]{20, 20}, 1e-5, 1e-9);
// Console.WriteLine($"Solution: {sol[0]} and {sol[1]} | Time: {(DateTime.Now - date).TotalMicroseconds}");

System.Console.WriteLine();
