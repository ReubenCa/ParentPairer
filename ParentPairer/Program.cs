using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParentPairer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Marriage> marriages = new List<Marriage>();
            string marriagedatapath = @"C:\Users\reube\source\repos\ParentPairer\ParentPairer\Data\Parents\data.csv";
            using (StreamReader sr = new StreamReader(marriagedatapath))
            {
                sr.ReadLine();//skip header
                Serializer.StreamToManages(sr);
            }
            Console.ReadLine();
        }
    }
}