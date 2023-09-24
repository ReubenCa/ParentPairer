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

            double DrinkingScore = 0;
            if (marriage.LikesToDrink == LikesDrinking.Yes && children.All(c => c.LikesToDrink == LikesDrinking.Yes))
            {
                DrinkingScore = 10;
            }
            else if (marriage.LikesToDrink == LikesDrinking.No && children.All(c => c.LikesToDrink == LikesDrinking.No))
            {
                DrinkingScore = 10;
            }
            else if (marriage.LikesToDrink == LikesDrinking.Sometimes && children.All(c => c.LikesToDrink == LikesDrinking.Sometimes))
            {
                DrinkingScore = 10;
            }
            else if (marriage.LikesToDrink == LikesDrinking.Yes && children.Any(c => c.LikesToDrink == LikesDrinking.No))
            {
                DrinkingScore = 1;
            }
            else if (marriage.LikesToDrink == LikesDrinking.No && children.Any(c => c.LikesToDrink == LikesDrinking.Yes))
            {
                DrinkingScore = 1;
            }

            else
            {
                DrinkingScore = 4;
            }

            double SubjectScore = 0;

            foreach (Subject subject in marriage.Subjects)
            {
                double max = 0;
                foreach (Child child in children)
                {
                    double similarity = SubjectSimilarity(subject, child.Subject);
                    if (similarity > max)
                    {
                        max = similarity;
                    }
                }
                SubjectScore += max;
            }
            SubjectScore /= children.Count;

            double PreferGoingOutScore = 0;

            foreach (Child c in children)
            {
                PreferGoingOutScore += marriage.PreferGoingOut == c.PreferGoingOut ? 6 : 10;
            }

            return ChildAmountScore * DrinkingScore * SubjectScore * PreferGoingOutScore;
           
        }


        private static readonly HashSet<Subject> STEM = new HashSet<Subject>
             {
            Subject.ChemicalEngineeringAndBiotechnology,
            Subject.ComputerScience,
            Subject.Engineering,
            Subject.Mathematics,
            Subject.Medicine,
            Subject.MedicineGraduateCourse,
            Subject.NaturalSciences,
           
        };


        private static double SubjectSimilarity(Subject A, Subject B)
        {
            if(A== B)
            {
                return 10;
            }
            
            if(STEM.Contains(A) && STEM.Contains(B))
            {
                return 5;
            }
            if(!STEM.Contains(A) && !STEM.Contains(B))
            {
                return 5;
            }
            //TODO Refine
            throw new NotImplementedException();
        }
    }
}
