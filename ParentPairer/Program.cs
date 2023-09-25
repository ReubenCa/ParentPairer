using System.ComponentModel;
using System.Runtime.Serialization;

namespace ParentPairer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Marriage> ms = new List<Marriage>();
            using(StreamReader sr = new StreamReader("C:\\Users\\reube\\source\\repos\\ReubenCa\\ParentPairer\\Data\\Parents\\cleaneddata.csv"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    
                    ms.Add(Serializer.StringToMarriage(sr.ReadLine()!));
                }
            }

            List<Child> children = new List<Child>();
            using (StreamReader sr = new StreamReader("C:\\Users\\reube\\source\\repos\\ReubenCa\\ParentPairer\\Data\\Child\\testingdata (DONT USE FOR ACTUAL THING).csv"))
            {                 
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    children.Add(Serializer.StringToChild(sr.ReadLine()!));
                }
            }
            Matching matching = new Matching(children.ToArray(), ms.ToArray());
            SimulatedAnnealing.AnnealingRunner runner = new SimulatedAnnealing.AnnealingRunner(matching, 50, 0.0015, 1000, new Random(), 2000);
            Matching finalMatching = (Matching)runner.Run();
            finalMatching.NoCacheCalculateScore();
            Console.WriteLine("Found Matching of score " + finalMatching.GetScore());
            using(StreamWriter sw = new StreamWriter("C:\\Users\\reube\\source\\repos\\ReubenCa\\ParentPairer\\Data\\output.csv"))
            {
                finalMatching.WriteToStream( sw);
            }   
            
        }
    }
}