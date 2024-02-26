namespace AIContinuos;

public static class Root
{
    public static double Bisection(
        Func<double, double> func,
        double a,
        double b,
        double tol = 1e-4,
        int maxInter = 1000
    )
    {
        double c = 0.0;

        for (int i = 0; i < maxInter; i++)
        {
            c = (a + b) / 2;
            // Valor Absolut = Math.Abs(func(c)) < tol
            // Valor Relativo = b - a < 2 * tol
            if (Math.Abs(func(c)) < tol || b - a < 2 * tol)
                break;

            if (func(c) * func(a) < 0)
                b = c;
            else
                a = c;
        }
        return c;
    }



    public static double FalsePosition(
        Func<double, double> func,
        double a,
        double b,
        double rtol = 1e-4,
        double atol = 1e-4,
        int maxInter = 1000
    )
    {
         double c = 0.0;

        for (int i = 0; i < maxInter; i++)
        {
            var fa = func(a);
            var m  = (func(b) - fa) / (b - a);
            var k  = -m * a + fa;
            c = -k / m;

            var fc = func(c);

            if (fa * fc < 0.0)
                b = c;
            else
                a = c;

            // Tolerancia Absoluta
            if (Math.Abs(fc) < atol)
                break;

            // Tolerancia Relativa
            if (b - a < 2.0 * rtol)
                break;
        }

        return c;
    }



    public static double Newton(
        Func<double, double> func,
        Func<double, double> der,
        double x0,
        double atol = 1e-4,
        int MaxInter = 10000
    )
    {
        double n = x0;
        for (int i = 0; i < MaxInter; i++)
        {
            // Aplicando a  memorização função e derivada
            // var fx = func(x0);
            // var dx = der(x0);
            // Calulo x = x - f(x) / d(x)
            // n = x0 - fx / dx;
            // x0 = n;

            var fx = func(n);
            var dx = der(n);
            n -= fx / dx;


            if (Math.Abs(fx) < atol)
                break;


        }
        return n;
    }
}