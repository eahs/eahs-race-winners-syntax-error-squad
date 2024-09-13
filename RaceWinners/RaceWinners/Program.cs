using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RaceWinners;

public class Program
{
    static async Task Main(string[] args)
    {
        // Asynchronously retrieve the group (class) data
        var ds = new DataService();
        var data = await ds.GetGroupRanksAsync();




        //for (int i = 0; i < data.Count; i++)
        //{
        //    // Combine the ranks to print as a list
        //    var ranks = string.Join(", ", data[i].Ranks);
            
        //    //Console.WriteLine($"{data[i].Name} - [{ranks}]");
        //}

        var w1 = MeanValueAlg(data);
        var w2 = RankComparisonAlg(data);
        var w3 = QuadraticMeanAlg(data);
        var w4 = GeometricMeanAlg(data);
        var w5 = HarmonicMeanAlg(data);

        GetScore(w1, w2, w3, w4, w5, data);


    }


    public static string MeanValueAlg(List<Models.Group> data)
    {
        var winner1 = string.Empty;
        double lowest = int.MaxValue;
        //Mean Value Algorithm
        foreach (var classes in data)
        {
            double meanScore = 0;
            double totalScore = 0;


            foreach (var rank in classes.Ranks)
            {
                totalScore += rank;
            }

            meanScore = totalScore / classes.Ranks.Count;

            if (meanScore < lowest)
            {
                lowest = meanScore;
                winner1 = classes.Name;
            }

            //Console.WriteLine(classes.Name + " " + meanScore);
        }
        Console.WriteLine(winner1 + " is the winner based on mean value of all runners with an average of " + Math.Round(lowest, 3));

        return winner1;
    }

    public static string RankComparisonAlg(List<Models.Group> data)
    {
        //Compare each rank Algorithm
        var winner2 = string.Empty;
        var leader = 0;
        var highestPoints = 0;


        for (var i = 0; i < 20; i++) //All class ranks
        {
            var lowestRank = int.MaxValue;

            for (var k = 0; k < data.Count; k++) //checks all classes
            {
                if (i >= data[k].Ranks.Count)
                {
                    continue;
                }
                else if (data[k].Ranks[i] < lowestRank) //if the rank of class[k] at rank[i] < lowestRank so far
                {
                    lowestRank = data[k].Ranks[i];
                    leader = k;
                }
            }

            data[leader].Points++;
        }

        for(var i = 0; i < data.Count; i++)
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


        Console.WriteLine(winner2 + " is the winner based on comparing each class rank with " + highestPoints + " points");
        return winner2;
    }

    public static string QuadraticMeanAlg(List<Models.Group> data)
    {
        //Quadratic Mean Algorithm
        var lowAverage = double.MaxValue;
        var winner = string.Empty;

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
                winner = classes.Name;
            }
            //Console.WriteLine(classes.Name + " " + average);
        }

        Console.WriteLine(winner + " is the winner based on the Quadratic Mean Algorithm with an average of " + Math.Round(lowAverage, 3));
        return winner;
    }

    public static string GeometricMeanAlg(List<Models.Group> data)
    {
        //Geometric Mean Algorithm
        double lowAverage = double.MaxValue;
        var winner = string.Empty;

        foreach (var classes in data)
        {
            double average = 1;

            foreach (var ranks in classes.Ranks)
            {
                average *= ranks;
            }

            average = Math.Pow(average, 1.0 / classes.Ranks.Count);

            if (average < lowAverage)
            {
                lowAverage = average;
                winner = classes.Name;
            }
            //Console.WriteLine(classes.Name + " " + geometricAverage);
        }


        Console.WriteLine(winner + " is the winner based on the Geometric Average with an average of " + Math.Round(lowAverage, 3));
        return winner;
    }

    public static string HarmonicMeanAlg(List<Models.Group> data)
    {
        var lowAverage = double.MaxValue;
        var winner = string.Empty;

        foreach (var classes in data)
        {
            var average = 0.0;

            foreach (var ranks in classes.Ranks)
            {
                average += (1.0 / ranks);
            }

            average = classes.Ranks.Count / average;

            if (average < lowAverage)
            {
                lowAverage = average;
                winner = classes.Name;
            }
        }

        Console.WriteLine(winner + " is the winner based on the Harmonic Mean with an average of " + Math.Round(lowAverage, 3));
        return winner;
    }

    public static void GetScore(string w1, string w2, string w3, string w4, string w5, List<Models.Group> data)
    {
        var algList = new List<string> { w1, w2, w3, w4, w5 };

        //Chad u a bitch
        //Ben I carried
        foreach (var algs in algList)
        {

            foreach (var classes in data)
            {
                if (algs.Equals(classes.Name))
                {
                    classes.Score++;
                }
            }
        }
        foreach (var classes in data)
        {
            Console.WriteLine(classes.Name + " got a score of " + classes.Score + "!");
        }

        var highest = 0;
        var winner = "";

        foreach (var classes in data)
        {
            if (highest < classes.Score)
            {
                highest = classes.Score;
                winner = classes.Name;
            }
        }
        Console.WriteLine(winner + " Wins!");
    }
}