namespace AulasAI.Utils;

public static class Math
{
    public static double Rescale(double x, double min, double max)
        => (max - min) * x + min;
}