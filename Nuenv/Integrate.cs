namespace AulasAI.Nuenv;

public static class Integrate
{
    public static double Romberg(double[] x, double[] y)
    {
        if (x.Length != y.Length)
            throw new ArgumentException("x and y must have the same length");
            
        var size = x.Length - 1;
        double sum = 0.0;

        for (int i = 0; i < size; i++)
            sum += 0.5 * (x[i + 1] - x[i]) * (y[i] + y[i + 1]);

        return sum;
    }
}