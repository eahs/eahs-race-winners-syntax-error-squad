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


            string winner = "";
            double totalScore = 0;
            double highest = int.MaxValue;
        //Mean Value Algorithm
        foreach(var classes in data)
        {
            double meanScore = 0;

            foreach (var rank in classes.Ranks)
            {
                totalScore += rank;
            }

            meanScore = totalScore / classes.Ranks.Count;

            if(meanScore < highest)
            {
                highest = meanScore;
                winner = classes.Name;
            }

            Console.WriteLine(classes.Name + " " + meanScore);
        }
        Console.WriteLine(winner);




    }
}