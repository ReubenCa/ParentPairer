using ParentPairer;
using System;

internal static class Serializer
{
	
	internal static Marriage StringToMarriage(string str)
    {
        string[] split = str.Split(',');
        List<string> crsids = new List<string>();
        crsids.Add(split[0]);
        crsids.Add(split[1]);
        if (split[2] != "")
        {
            crsids.Add(split[2]);
        }
        //3 is plaintext subjects
        //456 
        List<Subject> subjects = new List<Subject>();
        subjects.Add(stringtosubject(split[4]));
        subjects.Add(stringtosubject(split[5]));
        if (split[6] != "none")
        {
            subjects.Add(stringtosubject(split[6]));
        }
        LikesDrinking drinking = stringtolikesdrinking(split[7]);
        bool prefergoingout = split[8] == "out";

        /*  HashSet<Activities> activities = new HashSet<Activities>();
          string rawactivities = split[9];
          string[] splitactivities = rawactivities.Split(';');
          foreach(string activity in splitactivities)
          {
              activities.Add(stringtoactivities(activity));
          }*/
        int numberofchildren;
        bool willtakemore =split[11] ==  "as many as you can give me";
        if(!willtakemore)
        {
            numberofchildren = int.Parse(split[11]);
        }
        else
        {
            numberofchildren = 3;
        }
        return new Marriage(crsids, subjects, numberofchildren, prefergoingout, drinking, willtakemore);
       
        
    }

    public static Child StringToChild(string str)
    {

    }

   /* private static Activities stringtoactivities(string activity)
    {
        switch (activity)
        { 
            case "board games":
                return Activities.BoardGames;
            case "movie night":
                return Activities.MovieNight;
            case "video games":
                return Activities.VideoGames;
            case "table tennis":
                return Activities.TableTennis;
            case "Cards and drinking games":
                return Activities.DrinkingGames;
            default:
                return null;
                
            }

    }*/

    public static LikesDrinking stringtolikesdrinking(string str)
    {
        switch(str.ToLower())
        {
            case "yes":
                return LikesDrinking.Yes;
            case "no":
                return LikesDrinking.No;
            case "sometimes":
                return LikesDrinking.Sometimes;
            default:
                throw new Exception("Likes drinking not found");
        }
    }
    public static Subject stringtosubject(string str)
    {
        switch(str)
        {
            case "med":
                return Subject.Medicine;
            case "his":
                return Subject.History;
            case "hp":
                return Subject.HistoryAndPolitics;
            case "math":
                return Subject.Mathematics;
            case "nat":
                return Subject.NaturalSciences;
            case "mml":
                return Subject.ModernAndMedievalLanguages;
            case "geo":
                return Subject.Geography;
            case "eng":
                return Subject.Engineering;
            case "english":
                return Subject.English;
            case "vet":
                return Subject.VeterinaryMedicine;
            case "vetmed":
                return Subject.VeterinaryMedicine;
            case "law":
                return Subject.Law;
            case "eco":
                return Subject.Economics;
            case "cs":
                return Subject.ComputerScience;
            case "hsps":
                  return Subject.HumanSocialAndPoliticalSciences;
            case "pbs":
                   return Subject.PsychologicalAndBehaviouralSciences;
            case "music":
                return Subject.Music;
            case "psychology":
                return Subject.Psychology;
            case "ames":
                return Subject.AsianAndMiddleEasternStudies;
            case "arch":
                return Subject.Architecture;
            case "lin":
                return Subject.Linguistics;
            case "cla":
                return Subject.Classics;
            default:
                throw new Exception("Subject not found");
                    }
    }


}
