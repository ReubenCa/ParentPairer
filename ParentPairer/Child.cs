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
       
        public bool PreferGoingOut;
        public LikesDrinking LikesToDrink;

        public Child(string name, Subject subject, bool preferGoingOut, LikesDrinking likesToDrink)
        {
            Name = name;
            Subject = subject;
           
            PreferGoingOut = preferGoingOut;
            LikesToDrink = likesToDrink;
        }
    }
}
