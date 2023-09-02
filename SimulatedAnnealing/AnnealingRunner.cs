using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedAnnealing
{
    internal class AnnealingRunner
    {
        private readonly ISolution _initialSolution;
        private readonly double _initialTemperature;
        private readonly double _coolingRate;
        private readonly int _iterationsPerTemperature;
        private readonly Random _random;

        public AnnealingRunner(ISolution initialSolution, double initialTemperature, double coolingRate, int iterationsPerTemperature, Random random)
        {
            _initialSolution = initialSolution;
            _initialTemperature = initialTemperature;
            _coolingRate = coolingRate;
            _iterationsPerTemperature = iterationsPerTemperature;
            _random = random;
        }

        public ISolution Run()
        {
            ISolution currentSolution = _initialSolution;
            double currentTemperature = _initialTemperature;

            while (currentTemperature > 1)
            {
                for (int i = 0; i < _iterationsPerTemperature; i++)
                {
                    ISolution newSolution = currentSolution.Mutate();
                    double currentEnergy = currentSolution.GetScore();
                    double newEnergy = newSolution.GetScore();
                    if(i==0)
                        Console.WriteLine("Current energy: " + currentEnergy + "\tCurrent Temperature" + currentTemperature);
                    if (AcceptanceProbability(currentEnergy, newEnergy, currentTemperature) > _random.NextDouble())
                    {
                        currentSolution = newSolution;
                    }
                }

                currentTemperature *= 1 - _coolingRate;
            }

            return currentSolution;
        }

        private double AcceptanceProbability(double currentEnergy, double newEnergy, double temperature)
        {
            if (newEnergy < currentEnergy)
            {
                return 1;
            }

            return Math.Exp((currentEnergy - newEnergy) / temperature);
        }
    }
}
