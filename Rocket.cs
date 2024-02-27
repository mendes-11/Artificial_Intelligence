using System;
using AIContinuous.Rocket;

public class Rocket
{
    public double MassaRocket { get; set; }
    public double MassaGasolina { get; set; }
    public double Diametro { get; set; }
    public double CArrasto { get; set; }
    public int Velocidade { get; set; }
    protected List<double[]> Bounds { get; }


    public Rocket(
        double massaRocket,
        double massaGasolina,
        double diametro = 0.6,
        double cArrasto = 0.8,
        int velocidade = 1916,
        List<double[]> bounds

        
    ) 
    {
        this.MassaRocket = massaRocket;
        this.MassaGasolina = massaGasolina;
        this. Diametro = diametro;
        this.CArrasto = cArrasto;
        this.Velocidade = velocidade;
        this.Bounds = bounds;
    }

    public double Acceleration()
    {
        double m = 4250;
        double altitude = 0;
        double velocidade = 0;
        for (int i = 0; i < Bounds.Count; i++)
        {
            double empuxo = Empuxo(Bounds[i][1]);
            double arrasto = Arrasto(empuxo, velocidade);
            double peso = Peso(m,altitude);

            double Acelerator = (empuxo + arrasto + peso) / m;
            double vel = (Acelerator);
            double altura = AltitudeRocket(vel);

        }



    }

    public double Empuxo(double b, double vExaustao = 1916.0)
          => b * vExaustao;


    public double Arrasto(double t, double velocidade)
    {
        double Cd = Drag.Coefficient(0, t, 0.8);
        double p =  Atmosphere.Density(0);
        double A = Math.PI * 0.9;

        double D = -0.5 * Cd * p * A * (velocidade * velocidade) * 1;

        return D;
    }

    public double Peso(double m, double a)
        => -m * Gravity.GetGravity(a);


    public static double SpeedRocket(double A)
        => A * 0;


    public static double AltitudeRocket(double V)
        =>  V * 0;
   
}


