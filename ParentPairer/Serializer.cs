using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentPairer
{
    internal static class Serializer
    {
        internal static List<Marriage> StreamToManages(StreamReader sr)
        {
            List<Marriage> marriages = new List<Marriage>();
            while(!sr.EndOfStream)
            {
                marriages.Add(StringToMarriage(sr.ReadLine()!));
            }
            return marriages;
        }
        public static Marriage StringToMarriage(string str)
        {
            string[] strings = str.Replace("\"","").Split(',');
            List<string> crsids = new List<string>();
            crsids.Add(strings[2]) ;
            crsids.Add(strings[3]);
            if (!String.IsNullOrEmpty( strings[4]))
            { crsids.Add(strings[4]); }
            LikesToDrink likesToDrink = stringtoDrink(strings[6]);
            OutOrIn preferGoingOut = stringToPreferOut(strings[7]);
            string Activities = strings[8] + ";" + strings[9];
            HashSet<Activities> activities = new HashSet<Activities>();
            string[] rawActivities = Activities.Split(';');
            foreach(string activity in rawActivities)
            {
                if (!String.IsNullOrEmpty(activity))
                {
                    Activities? a = stringtoActivity(activity);
                    if (a.HasValue)
                        activities.Add(a.Value);
                }
            }
            int NumberOfChildren;
            bool WillTakeFourChildren;
            if (strings[10] == "as many as you can give me")
            {
                NumberOfChildren = 3;
                WillTakeFourChildren = true;
            }
            else
            {
                 NumberOfChildren = int.Parse(strings[10]);
                WillTakeFourChildren = false;
            }
            List < Subject > subjects= new();
            subjects.Add(stringtoSubject(strings[12]));
            subjects.Add(stringtoSubject(strings[13]));
            if (!String.IsNullOrEmpty(strings[14]) && strings[14]!= "none")
            { subjects.Add(stringtoSubject(strings[14])); }

            return new Marriage
                (crsids, subjects, activities, NumberOfChildren, preferGoingOut, likesToDrink, WillTakeFourChildren
                );
        }
        private static Activities? stringtoActivity(string str)
        {
            switch (str.ToLower()) // Convert input to lowercase to handle case-insensitive matching
            {
                
                case "board games":
                    return Activities.BoardGames;
                case "movie night":
                    return Activities.MovieNight;
                case "video games":
                    return Activities.VideoGames;
                case "table tennis/pool":
                    return Activities.TableTennis;
                case "cards and drinking games":
                    return Activities.DrinkingGames;
                case "pub":
                    return Activities.Pub;
                case "restraunt":
                    return Activities.Restaurant;
                case "bar":
                    return Activities.Bar;
                case "club":
                    return Activities.Club;
                default:
                    return null;
            }
        }
        private static LikesToDrink stringtoDrink(string str)
        {
            switch (str)
            {
                case "Yes":
                    return LikesToDrink.Yes;
                case "No":
                    return LikesToDrink.No;
                case "Sometimes/Depends":
                    return LikesToDrink.Sometimes;
                default:
                    throw new ArgumentException("Couldn't match " + str + "to enum");
            }
        }
        private static Subject stringtoSubject(string str)
        {
            switch (str.ToLower())
            {
                case "computer science":
                    return Subject.ComputerScience;
                case "architecture":
                    return Subject.Architecture;
                case "economics":
                    return Subject.Economics;
                case "education":
                    return Subject.Education;
                case "engineering":
                    return Subject.Engineering;
                case "english":
                    return Subject.English;
                case "psychology":
                    return Subject.PsychologicalAndBehaviouralSciences;
                case "geography":
                    return Subject.Geography;
                case "history":
                    return Subject.History;
                case "historyandmodernlanguages":
                    return Subject.HistoryAndModernLanguages;
                case "historyandpolitics":
                    return Subject.HistoryAndPolitics;
                case "ames":
                    return Subject.AngloSaxonNorseAndCeltic;
                case "hsps":
                    return Subject.HumanSocialAndPoliticalSciences;
                case "landeconomy":
                    return Subject.LandEconomy;
                case "law":
                    return Subject.Law;
                case "linguistics":
                    return Subject.Linguistics;
                case "maths":
                    return Subject.Mathematics;
                case "medicine":
                    return Subject.Medicine;
                case "music":
                    return Subject.Music;
                case "mml":
                    return Subject.ModernAndMedievalLanguages;
                case "NatSci":
                    return Subject.NaturalSciences;
                case "philosophy":
                    return Subject.Philosophy;
                case "pbs":
                    return Subject.PsychologicalAndBehaviouralSciences;
                case "classics":
                    return Subject.Classics;
                case "vetmed":
                    return Subject.VeterinaryMedicine;
                default:
                    throw new ArgumentException("Couldn't match " + str + " to enum");
            }
        }

        public static OutOrIn stringToPreferOut(string str)
        {
            switch(str)
            {
                case "out (pub/bar/club)":
                    return OutOrIn.Out;
                case "in (college buttery/ JCR games room)":
                    return OutOrIn.In;
                case "in (college buttery/ JCR games room);out (pub/bar/club)":
                    return OutOrIn.Both;
                default:
                    throw new ArgumentException("Couldn't match " + str + " to enum");
            }
        }

        
    }
}
