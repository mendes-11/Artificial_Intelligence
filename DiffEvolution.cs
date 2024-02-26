using System.Runtime.CompilerServices;

namespace AIContinuos;

public class DiffEvolution
{
    protected Func<double[], double> Fitness { get; }
    protected int Dimension { get; }
    protected double mutationMin { get; set; }
    protected double MutationMax { get; set; }
    protected double Recombination { get; set; }
    protected int NPop { get; }
    protected Func<double[], double> Restriction { get; }
    protected List<double[]> Individuals { get; set; }
    protected int BestIndividualIndex { get; set; }
    protected List<double[]> Bounds { get; }
    private double[] IndividualsRestriction { get; set; }
    private double[] IndividualsFitness { get; set; }


    public DiffEvolution(
        Func<double[], double> fitness,
        List<double[]> bounds,
        int npop,
        Func<double[], double> restricion,
        double mutationMin = 0.5,
        double mutationMax = 0.9,
        double recombination = 0.8
    )
    {
        this.Fitness = fitness;
        this.Dimension = bounds.Count;
        this.NPop = npop;
        this.Restriction = restricion;
        this.Individuals = new List<double[]>(NPop);
        this.Bounds = bounds;
        this.mutationMin = mutationMin;
        this.MutationMax = mutationMax;
        this.Recombination = recombination;
        this.IndividualsRestriction = new double[NPop];
        this.IndividualsFitness = new double[NPop];
    }

    private void GeneratePopulation()
    {
        var dimension = Dimension;
        for (int i = 0; i < NPop; i++)
        {
            Individuals.Add(new double[dimension]);
            for (int j = 0; j < dimension; j++)
            {
                Individuals[i][j] = Utils.Rescale(
                    Random.Shared.NextDouble(),
                    Bounds[j][0],
                    Bounds[j][1]
                );
            }

            IndividualsRestriction[i] = Restriction(Individuals[i]);

            IndividualsFitness[i] = IndividualsRestriction[i] <= 0.0 ? Fitness(Individuals[i]) : double.MaxValue;
        }
        FindBestIndividual();
    }

    private void FindBestIndividual()
    {
        var fitnessBest = IndividualsFitness[BestIndividualIndex];

        for (int i = 0; i < NPop; i++)
        {
            var fitnessCurrent = Fitness(Individuals[i]);

            if (fitnessCurrent < fitnessBest)
            {
                BestIndividualIndex = i;
                fitnessBest = fitnessCurrent;
            }
        }
        IndividualsFitness[BestIndividualIndex] = fitnessBest;
    }

    private double[] Mutate(int index)
    {
        int individualRand1;
        int individualRand2;

        do individualRand1 = Random.Shared.Next(NPop);
        while (individualRand1 == index);

        do individualRand2 = Random.Shared.Next(NPop);
        while (individualRand2 == individualRand1);

        var newIndividual = (double[])Individuals[BestIndividualIndex].Clone();
        for (int i = 0; i < Dimension; i++)
        {
            newIndividual[i] +=
                Utils.Rescale(Random.Shared.NextDouble(), mutationMin, MutationMax)
                * (Individuals[individualRand1][i] - Individuals[individualRand2][i]);
        }

        return newIndividual;
    }

    protected double[] Crossover(int index)
    {
        var trial = Mutate(index);
        var trial2 = trial;

        for (int i = 0; i < Dimension; i++)
        {
            if (
                !(
                    (Random.Shared.NextDouble() < Recombination)
                    || (i == Random.Shared.Next(Dimension))
                )
            )
                trial2[i] = Individuals[index][i];
        }
        return trial2;
    }

    protected void Iterate()
    {
        for (int i = 0; i < NPop; i++)
        {
            var trial = Crossover(i);
            var resTrial = Restriction(trial);

            double fitnessTrial = resTrial <= 0.0 ? Fitness(trial) : double.MaxValue;


            var resIndividual = IndividualsRestriction[i];

            if (
                (resIndividual > 0.0 && (resTrial < resIndividual))
                || (resTrial <= 0.0 && resIndividual > 0.0)
                || (resTrial <= 0.0 && fitnessTrial < IndividualsFitness[i])
            )
            {
                Individuals[i] = trial;
                IndividualsRestriction[i] = resTrial;
                IndividualsFitness[i] = Fitness(trial);
            }
        }

        FindBestIndividual();
    }

    public double[] Optimize(int n)
    {
        GeneratePopulation();
        FindBestIndividual();

        for (int i = 0; i < n; i++)
        {
            Iterate();
        }

        return Individuals[BestIndividualIndex];
    }
}
