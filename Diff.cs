namespace AIContinuos;

public static class Diff
{
    public static double Differentiate3P(Func<double, double> func, double x, double h = 1e-2) =>
        (func(x + h) - func(x - h)) / (2.0 * h);

    public static double Differentiate5P(Func<double, double> func, double x, double h = 1e-2) =>
        (func(x - 2.0 * h) - 8.0 * func(x - h) + 8.0 * func(x + h) - func(x + 2.0 * h)) / (12.0 * h);

   public static double[] Gradient(Func<double[], double> func, double[] x, double h = 1e-2)
   {
      var dim = x.Length;
      var grad = new double[dim];


      for(int i = 0; i < dim; i++)
      {
         var xp1 = (double[])x.Clone();
         var xp2 = (double[])x.Clone();

         xp1[i] += h;
         xp2[i] -= h;


         grad[i] = (func(xp1) - func(xp2)) / (2.0 * h);;
      }
      return grad;
   }
}
