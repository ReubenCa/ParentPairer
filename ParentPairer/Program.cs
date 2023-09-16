﻿using System.Runtime.Serialization;

namespace ParentPairer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Marriage> ms = new List<Marriage>();
            using(StreamReader sr = new StreamReader("C:\\Users\\reube\\source\\repos\\ReubenCa\\ParentPairer\\Data\\Parents\\cleaneddata.csv"))
            {
                while(!sr.EndOfStream)
                    ms.Add(Serializer.StringToMarriage(sr.ReadLine()!));
            }

        }
    }
}