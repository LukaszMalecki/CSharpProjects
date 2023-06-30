using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReversiVS.ReversiClasses
{
    internal class Result
    {
        public List<int> NodesSearchedB = new List<int>();
        public List<int> NodesSearchedW = new List<int>();
        public List<double> FullTimeSearchedB = new List<double>();
        public List<double> FullTimeSearchedW = new List<double>();
        public List<double> AverageTimeSearchedB = new List<double>();
        public List<double> AverageTimeSearchedW = new List<double>();
        public List<double> DeviationTimeSearchedB = new List<double>();
        public List<double> DeviationTimeSearchedW = new List<double>();
        public List<Player> Winners = new List<Player>();
        public Result() 
        {
            
        }

        public double WinRatio(Player player)
        {
            double sum = 0;

            foreach( var item in Winners)
            {
                if (item == player)
                    sum++;
            }
            return sum / Winners.Count;
        }

        public static double StandardDeviationInt(List<int> numList)
        {
            double avg = Result.AverageInt(numList);

            double upperSum = 0;

            foreach (var num in numList)
            {
                upperSum += Math.Pow(num - avg, 2);
            }
            upperSum /= numList.Count;

            return Math.Sqrt(upperSum);
        }
        public static double StandardDeviation(List<double> numList)
        {
            double avg = Result.Average(numList);

            double upperSum = 0;

            foreach( var num in numList)
            {
                upperSum += Math.Pow(num - avg, 2);
            }
            upperSum /= numList.Count;

            return Math.Sqrt(upperSum);
        }

        public static double AverageInt(List<int> numList)
        {
            double sum = 0;

            foreach (var item in numList)
            {
                sum += item;
            }

            return sum / (numList.Count);
        }
        public static double Average(List<double> numList)
        {
            double sum = 0;

            foreach (var item in numList) 
            {
                sum += item;
            }

            return sum/(numList.Count);
        }
    }
}
