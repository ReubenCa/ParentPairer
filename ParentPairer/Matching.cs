using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
        const double swapProbability = 10;


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
            {
                Debug.Assert(CacheAccurate());
                return (double)Score;
            }
             
            NoCacheCalculateScore();
            return (double)Score;
        }
        
        private bool CacheAccurate()
        {
            if (r.NextDouble() < 0.5)
                return true;//Only check every now and again as otherwise performance is too far degraded
            if (!Score.HasValue)
                return true;
            double cachedScore = Score!.Value;
            Debug.WriteLine("cachedScore " + cachedScore!);
             NoCacheCalculateScore();
            Debug.WriteLine("True Score " + Score!.Value);
            bool Answer =  Math.Abs(cachedScore - Score!.Value) < 0.1;
            Score = cachedScore;
            Debug.WriteLine(Answer);
            return Answer;
        }
    
        public void NoCacheCalculateScore()
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

        public ISolution Mutate(double Temperature)
        {
            return new Matching(this, Temperature);
        }

        internal void WriteToStream(StreamWriter sw)
        {
            Dictionary<Marriage, List<Child>> matching = new();
            for(int i = 0; i < _matching.Length; i++)
            {
                Child child = _children[i];
                Marriage m = _marriages[_matching[i]];
                if (!matching.ContainsKey(m))
                    matching[m] = new List<Child>() { child };
                else
                    matching[m].Add(child);
            }

            foreach(KeyValuePair<Marriage, List<Child> > kv in matching )
                {
                    while(kv.Key.Crsids.Count < 3)
                    {
                    kv.Key.Crsids.Add("");
                    }
                    StringBuilder stringBuilder = new StringBuilder();
                foreach(string crsid in kv.Key.Crsids)
                {
                    stringBuilder.Append(crsid);
                    stringBuilder.Append(",");
                }
                foreach(Child child in kv.Value)
                {
                    stringBuilder.Append(child.Name);
                    stringBuilder.Append(",");
                }
                stringBuilder.Append(CompatibilityCalculator.Compatibility(kv.Key, kv.Value));
                sw.WriteLine(stringBuilder.ToString());
            }
        }


        static private double? StartingTemperature = null;
        Matching(Matching old, double Temperature)
        {
            StartingTemperature ??= Temperature*1.1;

            Score = old.GetScore();
            _children = old._children;
            _marriages = old._marriages;
            _matching = (int[])old._matching.Clone();
            MarriageScores = (double[])old.MarriageScores.Clone();
            if(r.NextDouble() > 0.1 + Math.Pow(Temperature/(double)(StartingTemperature),2))
            {
                Debug.WriteLine("Swapping");
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
                Debug.WriteLine("Moving");
                int Child  = r.Next(_children.Length);
                int Marriage = r.Next(_marriages.Length);
                int oldmarriage = _matching[Child];
                _matching[Child] = Marriage;
                UpdateIndividualScore(oldmarriage);
                UpdateIndividualScore(Marriage);
            }
           
            
        }
    }
}
