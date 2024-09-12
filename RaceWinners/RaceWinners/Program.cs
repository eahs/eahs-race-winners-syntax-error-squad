using System;
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


            string winner1 = "";
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
        string winner2 = "";
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




    }
}