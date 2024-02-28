using AIContinuos.Nuenv;
using AIContinuous.Nuenv;
using AIContinuous.Rocket;

namespace Foguete;

public class Rocket
{
    public double DryMass { get; set; }
    public double CrossSectionArea { get; set; }
    public double Ve { get; set; }
    public double Cd0 { get; set; }
    public double Time { get; set; }
    public double Height { get; set; }
    public double Velocity { get; set; }
    public double Mass { get; set; }
    public double[] TimeData { get; set; }
    public double[] MassFlowData { get; set; }




    public Rocket(
        double dryMass,
        double crossSectionArea,
        double ve,
        double cd0,
        double[] timeData,
        double[] massFlowData        
    ) 
    {
        this.DryMass = dryMass;
        this.CrossSectionArea = crossSectionArea;
        this.Ve = ve;
        this.Cd0 = cd0;
        this.TimeData = (double[])timeData.Clone();
        this.MassFlowData = (double[])massFlowData.Clone();

        this.Mass = DryMass + Integrate.Romberg(TimeData, massFlowData);
    }

    public double CalculateMassFlow(double t)
        => t > TimeData[^1] ? 0.0 : Interp1D.Linear(TimeData, MassFlowData, t);

    public double MomentunEq(double t)
    {
        var thust = CalculateThrust(t);
        var drag = CalculateDrag(Velocity, Height);
        var weight = CalculateWeight(Height, Mass);

        return (thust + drag + weight) / Mass;
    }

    public void UpdateVelocity(double t, double dt)
    {
        var accel = MomentunEq(t);
        Velocity += accel * dt;
    }
    public void UpdateHeight(double dt)
    {
        Height += Velocity * dt;
    }

    public void FlyAlittleBit(double dt)
    {
        UpdateVelocity(Time, dt);
        UpdateHeight(dt);
        UpdateMass(Time, dt);

        Time += dt;
    }

    public void UpdateMass(double t, double dt)
    {
        Mass -= 0.5 * dt * (CalculateMassFlow(t) + CalculateMassFlow(t + dt));
    }

    public double CalculateThrust(double t)
        => t > TimeData[^1] ? 0.0 : CalculateMassFlow(t) * Ve;
    


    public double CalculateDrag(double vel, double h )
    {
        var temperature = Atmosphere.Temperature(h);
        var cd = Drag.Coefficient(vel, temperature, Cd0);
        var rho =  Atmosphere.Density(h);

        return -0.5 * cd * rho * CrossSectionArea * (vel * vel) * Math.Sign(vel);
    }

    public static double CalculateWeight(double h, double m)
        => -m * Gravity.GetGravity(h);


    public double Launch(double time, double dt = 1e-1)
    {
        for (double i = 0.0; i < time; i += dt)
            FlyAlittleBit(dt);

        return Height;
    }

    public double LaunchUntilMax(double dt = 1e-1)
    {

        do FlyAlittleBit(dt);
        while( Velocity > 0.0);
        
        return Height;
    }

     public double LaunchUntilGround(double dt = 1e-1)
    {

        do FlyAlittleBit(dt);
        while( Height > 0.0);
        
        return Height;
    }
 
}


