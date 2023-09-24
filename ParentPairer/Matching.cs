using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SimulatedAnnealing;
namespace ParentPairer
{
    internal class Matching : ISolution
    {
        //Probability two children swap rather than one just changing parents
        //Could increase as time goes on
        const double swapProbability = 0.4;


        private readonly Random r = new Random();
        private readonly Child[] _children;
        private readonly Marriage[] _marriages;
        
        public Matching(Child[] children, Marriage[] marriages, int[]? matching = null)
        {
            _children = children;
            _marriages = marriages;
            if(matching == null)
            {
                _matching = new int[children.Length];
                for (int i = 0; i < children.Length; i++)
                {
                    _matching[i] = r.Next(marriages.Length);
                }
            }
            else
            {
                _matching = matching;
            }
            MarriageScores = new double[_marriages.Length];
        }
        double[] MarriageScores;
        //Index i represents the index of marriage child i belongs to
        private int[] _matching;
        public ISolution Clone()
        {
            return new Matching(_children, _marriages, (int[])_matching.Clone());
        }
        double? Score = null;
        public double GetScore()
        {
            if (Score.HasValue)
                return (double)Score;
            
            NoCacheCalculateScore();
            return (double)Score;
        }
        
    
        private void NoCacheCalculateScore()
        {
            for(int i = 0; i< _marriages.Length; i++)
            {
                MarriageScores[i] = CompatibilityCalculator.Compatibility(
                    _marriages[i],
                    _children.Where((child, j) => _matching[j] == i).ToList());
            }
            double total = 0;
            foreach (var score in MarriageScores)
            {
                total += score;
            }
            Score = total;
        }

        private void UpdateIndividualScore(int index)
        {
           
            double newScore = CompatibilityCalculator.Compatibility(
                _marriages[index],
                _children.Where((child, i) => _matching[i] == index).ToList());

            Score -= MarriageScores[index];
            Score += newScore;
            MarriageScores[index] = newScore;



        }

        public ISolution Mutate()
        {
            return new Matching(this);
        }

        internal void WriteToStream(StreamWriter sw)
        {
            for(int i = 0; i < _matching.Length; i++)
            {
                Child child = _children[i];
                Marriage marriage = _marriages[_matching[i]];
                StringBuilder output = new StringBuilder(child.Name);
                foreach(string c in marriage.Crsids)
                {
                    output.Append("," + c);
                }
                sw.WriteLine(output);
            }
        }

        Matching(Matching old)
        {
            Score = old.GetScore();
            _children = old._children;
            _marriages = old._marriages;
            _matching = (int[])old._matching.Clone();
            MarriageScores = old.MarriageScores;
            if(r.NextDouble() < swapProbability)
            {
                int index1 = r.Next(_children.Length);
                int index2 = r.Next(_children.Length);

                int temp = _matching[index1];
                _matching[index1] = _matching[index2];
                _matching[index2] = temp;
                UpdateIndividualScore(_matching[index1]);
                UpdateIndividualScore(_matching[index2]);
            }
            else
            {
                int Child  = r.Next(_children.Length);
                int Marriage = r.Next(_marriages.Length);
                _matching[Child] = Marriage;
                UpdateIndividualScore(Marriage);
            }
           
            
        }
    }
}
