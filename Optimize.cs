using AIContinuos;

public class Optimize
{
    public static double Newton(
        Func<double, double> func,
        double x0,
        double h = 1e-2,
        double atol = 1e-4,
        int maxInter = 10000
    )
    {
        Func<double, double> diffiFunction = x => Diff.Differentiate3P(func, x, 2.0 * h);
        Func<double, double> diffSecondFunction = x => Diff.Differentiate3P(diffiFunction, x, h);

        return Root.Newton(diffiFunction, diffSecondFunction, x0, atol, maxInter);
    }

    public static double GradientDescendent(
        Func<double, double> func,
        double x0,
        double learningRate = 1e-2,
        double atol = 1e-4
    )
    {
        double xp = x0;
        double diff = Diff.Differentiate3P(func, xp);

        while (Math.Abs(diff) > atol)
        {
            diff = Diff.Differentiate3P(func, xp);
            xp -= learningRate * diff;
        }
        return xp;
    }

    public static double[] GradientDescendent(
        Func<double[], double> func,
        double[] x0,
        double learningRate = 1e-2,
        double atol = 1e-4
    )
    {
        var dim = x0.Length;
        double[] xp = (double[])x0.Clone();
        var diff = Diff.Gradient(func, xp);
        double diffNorm;

        do
        {
            diffNorm = 0;
            diff = Diff.Gradient(func, xp);

            for(int i = 0; i < dim; i++)
            {
                xp[i] -= learningRate * diff[i];
                diffNorm += Math.Abs(diff[i]);
            }
            
        }while (diffNorm > dim * atol);
        return xp;
    }
}
