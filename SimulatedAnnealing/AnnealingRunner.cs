using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedAnnealing
{
    public class AnnealingRunner
    {
        private readonly ISolution _initialSolution;
        private readonly double _initialTemperature;
        private readonly double _coolingRate;
        private readonly int _iterationsPerTemperature;
        private readonly Random _random;
        private readonly int _RevertAmount;

        public AnnealingRunner(ISolution initialSolution, double initialTemperature, double coolingRate, int iterationsPerTemperature, Random random, int RevertAmount = int.MaxValue)
        {
            _initialSolution = initialSolution;
            _initialTemperature = initialTemperature;
            _coolingRate = coolingRate;
            _iterationsPerTemperature = iterationsPerTemperature;
            _random = random;
            _RevertAmount = RevertAmount;
        }

        public ISolution Run()
        {
            int iterationssincebestfound = 0;
            ISolution bestSolution = _initialSolution;
            double bestScore = _initialSolution.GetScore();
            ISolution currentSolution = _initialSolution;
            double currentTemperature = _initialTemperature;
            
            while (currentTemperature > 1)
            {
                for (int i = 0; i < _iterationsPerTemperature; i++)
                {
                    ISolution newSolution = currentSolution.Mutate(currentTemperature);
                    double currentScore = currentSolution.GetScore();
                    double newScore = newSolution.GetScore();
                    if (i == 0)
                    {
                        Console.WriteLine("Current Score: " + bestScore + "\t\tCurrent Temperature " + currentTemperature);// + "\n"+
                       // "Average Acceptance: " + totalAcceptanceProb/amountAcceptanceProb);
                      //  totalAcceptanceProb = 0;
                       // amountAcceptanceProb = 0;
                    }
                    
                    if (AcceptanceProbability(currentScore, newScore, currentTemperature) > _random.NextDouble())
                    {
                        currentSolution = newSolution;
                    }
                    if(newScore > bestScore)
                    {
                        bestScore = newScore;
                        bestSolution = newSolution;
                        iterationssincebestfound = 0;
                    }
                    else
                    {
                        iterationssincebestfound++;
                    }
                    if(iterationssincebestfound > _RevertAmount)
                    {
                        currentSolution = bestSolution;
                        iterationssincebestfound = 0;
                    }
                }

                currentTemperature *= 1 - _coolingRate;
            }

            return bestSolution;
        }


        double totalAcceptanceProb;
        int amountAcceptanceProb;

        private double AcceptanceProbability(double currentScore, double newScore, double temperature)
        {
            if (newScore > currentScore)
            {
                return 1;
            }
            double Prob = Math.Exp((newScore - currentScore ) / temperature);
            Debug.WriteLine("Acceptance Prob: " + Prob);
            totalAcceptanceProb += Prob;
            amountAcceptanceProb++;
            return Prob;
        }
    }
}
