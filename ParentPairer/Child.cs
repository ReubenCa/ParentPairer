using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentPairer
{
    internal class Child
    {
        public string Name;
        public Subject Subject;
        public HashSet<Activities> Activities;
        public bool PreferGoingOut;
        public bool LikesToDrink;

        public Child(string name, Subject subject, HashSet<Activities> activities, bool preferGoingOut, bool likesToDrink)
        {
            Name = name;
            Subject = subject;
            Activities = activities;
            PreferGoingOut = preferGoingOut;
            LikesToDrink = likesToDrink;
        }
    }
}
