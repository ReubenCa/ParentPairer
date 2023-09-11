using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentPairer
{
    internal struct Marriage
    {
        public readonly List<String> Crsids;
        public readonly List<Subject> Subjects;
        public readonly HashSet<Activities> Activities;
        public readonly int NumberOfChildren;
        public readonly bool PreferGoingOut;
        public readonly LikesToDrink LikesToDrink;
        public readonly bool WillTakeFourChildren;

        public Marriage(List<string> crsids, List<Subject> subjects, HashSet<Activities> activities, int numberOfChildren, bool preferGoingOut, LikesToDrink likesToDrink, bool willTakeFourChildren)
        {
            Crsids = crsids;
            Subjects = subjects;
            Activities = activities;
            NumberOfChildren = numberOfChildren;
            PreferGoingOut = preferGoingOut;
            LikesToDrink = likesToDrink;
            WillTakeFourChildren = willTakeFourChildren;
        }
    }
}
