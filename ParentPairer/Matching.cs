using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        //Index i represents the index of marriage child i belongs to
        private int[] _matching;
        public ISolution Clone()
        {
            return new Matching(_children, _marriages, (int[])_matching.Clone());
        }

        public double GetScore()
        {
            throw new NotImplementedException();
        }

        public ISolution Mutate()
        {
            if(r.NextDouble() < swapProbability)
            {
                int index1 = r.Next(_children.Length);
                int index2 = r.Next(_children.Length);

                int temp = _matching[index1];
                _matching[index1] = _matching[index2];
                _matching[index2] = temp;
            }
            else
            {
                int Child  = r.Next(r.Next(_children.Length));
                int Marriage = r.Next(_marriages.Length);
                _matching[Child] = Marriage;
            }
  
        }
    }
}
