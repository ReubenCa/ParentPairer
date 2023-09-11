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

            double SubjectScore = 10;
            double LikesDrinkingScore = 10; 
            LikesToDrink parentslike = marriage.LikesToDrink;
            double LikesGoingOutScore = 10;
            bool parentsGoingOut = marriage.PreferGoingOut;
            double ActivitiesScore = 10;
            foreach (var child in children)
            {
                foreach (var subject in marriage.Subjects)
                {
                    SubjectScore *= SubjectSimilarity(child.Subject, subject);
                }
                if (child.LikesToDrink == parentslike)
                {
                    LikesDrinkingScore *= 1;
                }
                else
                {
                    LikesDrinkingScore *= 0.25;
                }
                if (child.PreferGoingOut == parentsGoingOut)
                {
                    LikesGoingOutScore *= 1;
                }
                else
                {
                    LikesGoingOutScore *= 0.1;
                }
                
                int hits = 0;
                foreach(var activity in child.Activities)
                {

                    if(marriage.Activities.Contains(activity))
                    {
                        hits++;
                    }
                }
               ActivitiesScore = (hits / marriage.Activities.Count)*0.4 + 0.6;
            }

            return ActivitiesScore * LikesDrinkingScore * LikesGoingOutScore * SubjectScore * ChildAmountScore;

        }


        private static readonly HashSet<Subject> STEM = new HashSet<Subject>
             {
           
            Subject.ComputerScience,
            Subject.Engineering,
            Subject.Mathematics,
            Subject.Medicine,
           
            Subject.NaturalSciences,
           
        };


        private static double SubjectSimilarity(Subject A, Subject B)
        {
            if(A== B)
            {
                return 1;
            }
            
            if(STEM.Contains(A) && STEM.Contains(B))
            {
                return 0.5;
            }
            if(!STEM.Contains(A) && !STEM.Contains(B))
            {
                return 0.5;
            }
            //TODO Refine
            throw new NotImplementedException();
        }
    }
}
