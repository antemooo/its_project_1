using System;
using System.Collections.Generic;
using System.Linq;
using CinemaApp.Models;

namespace CinemaApp.Services;

/// <summary>
/// Greedy algorithm for marathon planning.
/// This is a classic INTERVAL SCHEDULING algorithm from computer science.
/// 
/// Algorithm:
/// 1. Sort all shows by END time (earliest ending first)
/// 2. Pick the first show
/// 3. For each remaining show:
///    - If it starts after the previous show ends, pick it
///    - Otherwise, skip it
/// 
/// This maximizes the NUMBER of movies you can watch.
/// It's "greedy" because it always picks the next available show without looking ahead.
/// 
/// Time Complexity: O(n log n) due to sorting
/// Space Complexity: O(n) for storing the plan
/// </summary>
public sealed class GreedyMarathonPlanner : IMarathonPlanner
{
    /// <summary>
    /// Plans a marathon ticket using the greedy interval scheduling algorithm.
    /// </summary>
    public List<Show> Plan(DateOnly day, IEnumerable<Show> shows)
    {
        // Step 1: Filter shows for the specific day and sort by end time
        var dayShows = shows
            .Where(s => DateOnly.FromDateTime(s.Start) == day)
            .OrderBy(s => s.End)           // Sort by end time (earliest first)
            .ThenBy(s => s.Start)          // If end times equal, prefer earlier start
            .ToList();
        
        // Step 2: Build the plan using greedy selection
        var plan = new List<Show>();
        DateTime currentEndTime = DateTime.MinValue;  // Track when last show ended
        
        foreach (var show in dayShows)
        {
            // Can we fit this show? (starts after previous one ends)
            if (show.Start >= currentEndTime)
            {
                plan.Add(show);
                currentEndTime = show.End;  // Update end time for next iteration
            }
            // Otherwise skip this show - it overlaps with our current schedule
        }
        
        return plan;
    }
}
