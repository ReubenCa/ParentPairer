using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentPairer
{
    internal static class CompatibilityCalculator
    {
        
        public static double Compatibility(Marriage marriage, List<Child> children)
        {
            
            double ChildAmountScore;
            int ParentCount = marriage.Subjects.Count;
            if (children.Count > 4 || children.Count <= 1)
            {
                return -1000;
            }
            if(children.Count ==4 )
            {
                if(marriage.WillTakeFourChildren)
                {
                  ChildAmountScore = ParentCount >= 3 ?  7 : 4;
                }
                else
                {
                    return -1000;
                }
            }
            
            if(marriage.NumberOfChildren == children.Count)
            {
                ChildAmountScore = 10;
            }
            else
            {
                ChildAmountScore = 5;
            }
            bool PerfectDrinking = false;
            double DrinkingScore = 0;
            if (children.All(c => c.LikesToDrink == marriage.LikesToDrink))
            {
                DrinkingScore = 25;
                PerfectDrinking = true;
            }
         
            else if (marriage.LikesToDrink == LikesDrinking.Yes && children.Any(c => c.LikesToDrink == LikesDrinking.No))
            {
                return -1000;
            }
            else if (marriage.LikesToDrink == LikesDrinking.No && children.Any(c => c.LikesToDrink == LikesDrinking.Yes))
            {
                return -1000;
            }



            else
            {
                DrinkingScore = 0;
                foreach(Child c in children)
                {
                    DrinkingScore += c.LikesToDrink == marriage.LikesToDrink ? 15 : 0;
                }
                DrinkingScore /= children.Count;
            }

            double SubjectScore = 0;
            bool PerfectSubject = true;
            foreach (Child child in children)
            {
                double max = 0;
                foreach (Subject subject in marriage.Subjects)
                {
                    double similarity = SubjectSimilarity(subject, child.Subject);
                    if (similarity > max)
                    {
                        max = similarity;
                    }
                }
                PerfectSubject = PerfectSubject && max == 25;
                SubjectScore += max;
            }
            SubjectScore /= children.Count;

            double PreferGoingOutScore = 0;

            //foreach (Child c in children)
            //{
            //    PreferGoingOutScore += marriage.PreferGoingOut == c.PreferGoingOut ? 7 : 0;
            //}
            //PreferGoingOutScore /= children.Count;
            
            return ChildAmountScore + DrinkingScore + SubjectScore
                 + (PerfectSubject && PerfectDrinking ? 15 : 0);// + PreferGoingOutScore;
           
        }


        private static readonly HashSet<Subject> STEM = new HashSet<Subject>
             {
            Subject.ChemicalEngineeringAndBiotechnology,
            Subject.ComputerScience,
            Subject.Engineering,
            Subject.Mathematics,
            Subject.Medicine,
            Subject.NaturalSciences,
           
        };


        private static readonly HashSet<Subject> Historyish = new HashSet<Subject>
        {

            Subject.History,
            Subject.HistoryAndPolitics,
            Subject.HistoryAndModernLanguages,
            Subject.HistoryOfArt,
            Subject.HumanSocialAndPoliticalSciences
        };

        private static readonly HashSet<Subject> Languages = new HashSet<Subject>
        {

        Subject.ModernAndMedievalLanguages,
                   Subject.Linguistics,
                   Subject.HistoryAndModernLanguages

        };

        private static readonly HashSet<Subject> Medical = new HashSet<Subject>
        { 
            Subject.Medicine,
                  
                   Subject.VeterinaryMedicine
               };

        private static double SubjectSimilarity(Subject A, Subject B)
        {
            if(A== B)
            {
                return 25;
            }

            if (Medical.Contains(A) && Medical.Contains(B))
                return 12;

            if (STEM.Contains(A) && STEM.Contains(B))
            {
                return 5;
            }
            if (Languages.Contains(A) && Languages.Contains(B))
                return 10;

            return 1;
        }
    }
}
