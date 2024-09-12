using System.Collections.Generic;

namespace RaceWinners.Models;

/// <summary>
/// Stores all the data associated with a group of runners
/// </summary>
public class Group
{
    // Name of group
    public string Name { get; set; }
    
    /// <summary>
    /// Each spot in the list represents the overall rank of finish place in this group
    /// (not the overall race).  So if index 0 contains a 4, then the first runner in this
    /// group placed 4th overall.  
    /// </summary>
    public List<int> Ranks { get; set; }


    public int Points { get; set; }
}
