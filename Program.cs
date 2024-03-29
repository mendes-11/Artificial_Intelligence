﻿using AIContinuos;


double Rosenbrock(double[]x)
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

double Restriction(double[] x)
{
    return -1.0;
}

List<double[]> bounds = new() 
{
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},

};


date = DateTime.Now;
var diffEvolution = new DiffEvolution(Rosenbrock, bounds, 200, Restriction);
var res = diffEvolution.Optimize(10000);
Console.WriteLine($"Solution: {res[0]} and {res[1]} | Time: {(DateTime.Now - date).TotalMicroseconds}");


