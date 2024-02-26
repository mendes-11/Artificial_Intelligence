namespace AIContinuos;

public class DiffEvolution
{
    protected Func<double[], double> Fitness { get;}
    protected int Dimension { get; }
    protected double Mutation { get; set; }
    protected double Recombination { get; set; }
    protected int NPop { get;}
    protected List<double[]> Individuals { get; set; }
    protected int BestIndividualIndex { get; set; }
    protected double BestIndividualFitness { get; set; } = double.MaxValue;
    protected List<double[]> Bounds { get; }

    public DiffEvolution(Func<double[], double> fitness, List<double[]> bounds, int npop, double mutation = 0.7, double recombination = 0.8)
    {
        this.Fitness = fitness;
        this.Dimension = bounds.Count;
        this.NPop = npop;
        this.Individuals = new List<double[]>(NPop);
        this.Bounds = bounds;
        this.Mutation = mutation;
        this.Recombination = recombination;
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
        }
    }

    private void FindBestIndividual()
    {
        var fitnessBest = BestIndividualFitness;

        for (int i = 0; i < NPop; i++)
        {
            var fitnessCurrent = Fitness(Individuals[i]);

            if(fitnessCurrent < fitnessBest)
            {
                BestIndividualIndex = i;
                fitnessBest = fitnessCurrent;
            }
        }
        BestIndividualFitness = fitnessBest;
    }

    private double[] Mutate(double[] individual)
    {
        var newIndividual = new double[Dimension];
        var individualRand1 = Random.Shared.Next(NPop);
        var individualRand2 = Random.Shared.Next(NPop);

        do
        {
            individualRand2 = Random.Shared.Next(NPop);
        } while(individualRand1 == individualRand2);


        newIndividual = Individuals[BestIndividualIndex];

        for (int i = 0; i < Dimension; i++)
        {
            newIndividual[i] += Mutation * (Individuals[individualRand1][i] - Individuals[individualRand2][i]);
        }
        return newIndividual;
    }

    protected double[] Crossover(int index)
    {
        var trial = Mutate(Individuals[index]);
        var trial2 = (double[])Individuals[index].Clone();

        for (int i = 0; i < Dimension; i++)
        {
            if(!((Random.Shared.NextDouble() < Recombination) || (i == Random.Shared.Next(Dimension))))
                trial2[i] = trial[i];
        }
        return trial2;
    }

    protected void Iterate()
    {
        for (int i = 0; i < NPop; i++)
        {
            var trial = Crossover(i);
            

            if(Fitness(trial) < Fitness(Individuals[i]))
                Individuals[i] = trial;
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
