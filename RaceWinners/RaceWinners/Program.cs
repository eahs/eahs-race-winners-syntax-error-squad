using System;
using System.Data.Common;
using System.Dynamic;
using System.Threading.Tasks;

namespace RaceWinners;

public class Program
{
    static async Task Main(string[] args)
    {
        DataService ds = new DataService();

        // Asynchronously retrieve the group (class) data
        var data = await ds.GetGroupRanksAsync();

        for (int i = 0; i < data.Count; i++)
        {
            // Combine the ranks to print as a list
            var ranks = String.Join(", ", data[i].Ranks);
            
            //Console.WriteLine($"{data[i].Name} - [{ranks}]");
        }


            string winner1 = String.Empty;
            double highest = int.MaxValue;
        //Mean Value Algorithm
        foreach(var classes in data)
        {
            double meanScore = 0;
            double totalScore = 0;
             

            foreach (var rank in classes.Ranks)
            {
                totalScore += rank;
            }

            meanScore = totalScore / classes.Ranks.Count;

            if(meanScore < highest)
            {
                highest = meanScore;
                winner1 = classes.Name;
            }

            //Console.WriteLine(classes.Name + " " + meanScore);
        }
        Console.WriteLine(winner1 + " is the winner based on mean value of all runners!!");


        //Compare each rank Algorithm
        string winner2 = String.Empty;
        int leader = 0;
        int highestPoints = 0;
       
        
        for (int i = 0; i < 20; i++) //All class ranks
        {
            int lowestRank = int.MaxValue;

            for (int k = 0; k<data.Count;k++) //checks all classes
            {
                if (i >= data[k].Ranks.Count)
                {
                    continue;
                } else if (data[k].Ranks[i] < lowestRank) //if the rank of class[k] at rank[i] < lowestRank so far
                {
                    lowestRank = data[k].Ranks[i];
                    leader = k;
                }
            }

            data[leader].Points++;
        }

        for (int i = 0; i < data.Count; i++)
        {
            if (highestPoints < data[i].Points)
            {
                highestPoints = data[i].Points;
                winner2 = data[i].Name;
            }
        }
        //Console.WriteLine(data[0].Points);
        //Console.WriteLine(data[1].Points);
        //Console.WriteLine(data[2].Points);
        //Console.WriteLine(data[3].Points);


        Console.WriteLine(winner2 + " is the winner based on comparing each class rank");


        //Quadratic Mean Algorithm
            double lowAverage = int.MaxValue;
            string winner3 = string.Empty;

        foreach (var classes in data)
        {
            double average = 0;

            foreach (var ranks in classes.Ranks)
            {
                average += ranks * ranks;
            }

            average = Math.Sqrt((1.0 / classes.Ranks.Count) * average);


            if (average < lowAverage)
            {
                lowAverage = average;
                winner3 = classes.Name;
            }
            //Console.WriteLine(classes.Name + " " + average);
        }

        Console.WriteLine(winner3 + " is the winner based on the Quadratic Mean Algorithm");


        //Geometric Mean Algorithm
        double lowAverageGeometric = int.MaxValue;
        string winner4 = String.Empty;

        foreach (var classes in data)
        {
            double geometricAverage = 1;

            foreach (var ranks in classes.Ranks)
            {
                geometricAverage *= ranks;
            }

            geometricAverage = Math.Pow(geometricAverage, 1.0 / classes.Ranks.Count);

            if (geometricAverage < lowAverageGeometric)
            {
                lowAverageGeometric = geometricAverage;
                winner4 = classes.Name;
            }
            //Console.WriteLine(classes.Name + " " + geometricAverage);
        }


        Console.WriteLine(winner4 + " is the winner based on the Geometric Average");
    }
}