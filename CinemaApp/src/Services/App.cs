using System;
using System.Collections.Generic;
using System.Linq;
using CinemaApp.Models;
using CinemaApp.Services;

namespace CinemaApp;

/// <summary>
/// Main application class that handles user interaction.
/// This class demonstrates:
/// - COMPOSITION: Uses IShowStore and IMarathonPlanner
/// - DEPENDENCY INJECTION: Could easily swap implementations
/// - SINGLE RESPONSIBILITY: Only handles CLI and user commands
/// </summary>
public static class App
{
    // Application dependencies - could be injected for better testability
    private static readonly Dictionary<string, Room> rooms = new();
    private static readonly Dictionary<string, Movie> movies = new();
    private static readonly IShowStore store = new InMemoryShowStore();
    private static readonly IMarathonPlanner planner = new GreedyMarathonPlanner();

    //==========================================================================
    // STUDENT TODO: Implement main REPL command loop
    // ⭐⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 30-45 minutes
    //
    // STEPS:
    // 1. Print help/welcome message
    // 2. Infinite loop: for (;;)
    // 3. Read line from Console
    // 4. Check for 'exit' command → return
    // 5. Parse with Split(line) helper (handles quotes)
    // 6. Extract command: parts[0].ToLowerInvariant()
    // 7. Switch/dispatch to handler methods
    // 8. Wrap in try/catch to show errors
    //
    // HINT: Use switch expression or switch statement for command dispatch
    //==========================================================================
    /// <summary>
    /// Main application loop - reads and processes commands.
    /// </summary>
    public static void Run()
    {
        throw new NotImplementedException("TODO: Implement REPL command loop with switch dispatch");
    }

    //==========================================================================
    // STUDENT TODO: Implement add-room command handler
    // ⭐ DIFFICULTY: Easy | ⏱️ TIME: 15 minutes
    // USAGE: add-room "Room A" 100
    // STEPS:
    // 1. Check parts.Length == 3, else print usage and return
    // 2. Parse: name = parts[1], capacity = int.Parse(parts[2])
    // 3. Validate capacity > 0
    // 4. Create and store: rooms[name] = new Room(name, capacity)
    // 5. Print success message
    //==========================================================================
    /// <summary>
    /// Handles: add-room "Room A" 100
    /// Creates a new cinema room with specified capacity.
    /// </summary>
    private static void HandleAddRoom(string[] parts)
    {
        throw new NotImplementedException("TODO: Parse room name and capacity, create Room, store in dictionary");
    }

    //==========================================================================
    // STUDENT TODO: Implement add-movie command handler
    // ⭐ DIFFICULTY: Easy | ⏱️ TIME: 15 minutes
    // USAGE: add-movie "Inception" 02:28
    // STEPS:
    // 1. Check parts.Length == 3
    // 2. Parse: title = parts[1], duration = TimeSpan.Parse(parts[2])
    // 3. Store: movies[title] = new Movie(title, duration)
    // 4. Print success
    //==========================================================================
    /// <summary>
    /// Handles: add-movie "Inception" 02:28
    /// Creates a new movie with title and duration.
    /// </summary>
    private static void HandleAddMovie(string[] parts)
    {
        throw new NotImplementedException("TODO: Parse title and duration, create Movie, store in dictionary");
    }

    //==========================================================================
    // STUDENT TODO: Implement add-show command handler
    // ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 25 minutes
    // USAGE: add-show "Inception" "Room A" 2025-11-01T10:00 9.99
    // STEPS:
    // 1. Check parts.Length == 5
    // 2. Look up movie: movies.TryGetValue(parts[1], out var movie)
    // 3. Look up room: rooms.TryGetValue(parts[2], out var room)
    // 4. Parse: start = DateTime.Parse(parts[3]), price = decimal.Parse(parts[4])
    // 5. Create: var show = new Show(movie, room, start, price)
    // 6. Store: store.Add(show)
    // 7. Print show ID
    //==========================================================================
    /// <summary>
    /// Handles: add-show "Inception" "Room A" 2025-11-01T10:00 9.99
    /// Creates a new show with all details.
    /// </summary>
    private static void HandleAddShow(string[] parts)
    {
        throw new NotImplementedException("TODO: Look up movie & room, parse datetime & price, create Show, add to store");
    }

    //==========================================================================
    // STUDENT TODO: Implement list-shows command handler
    // ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 20 minutes
    // USAGE: list-shows 2025-11-01
    // STEPS:
    // 1. Check parts.Length == 2
    // 2. Parse: day = DateOnly.Parse(parts[1])
    // 3. Query: store.All().Where(s => DateOnly.FromDateTime(s.Start) == day).OrderBy(s => s.Start)
    // 4. If empty: print "No shows"
    // 5. Else: print header, loop through shows, print each
    //==========================================================================
    /// <summary>
    /// Handles: list-shows 2025-11-01
    /// Lists all shows for a specific date, ordered by start time.
    /// </summary>
    private static void HandleListShows(string[] parts)
    {
        throw new NotImplementedException("TODO: Parse date, filter & sort shows using LINQ, print list");
    }

    //==========================================================================
    // STUDENT TODO: Implement book command handler
    // ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 25 minutes
    // USAGE: book showId 12 13 14
    // STEPS:
    // 1. Check parts.Length >= 3
    // 2. Parse: Guid.TryParse(parts[1], out var showId)
    // 3. Find: var show = store.Find(showId)
    // 4. Parse seats: parts.Skip(2).Select(int.Parse).ToArray()
    // 5. Try booking: if (show.TryBook(seats)) { success } else { failure }
    // 6. Print result and remaining seats
    //==========================================================================
    /// <summary>
    /// Handles: book showId 12 13 14
    /// Books specified seats for a show.
    /// </summary>
    private static void HandleBook(string[] parts)
    {
        throw new NotImplementedException("TODO: Parse showId, find show, parse seats, call TryBook, print result");
    }

    //==========================================================================
    // STUDENT TODO: Implement marathon command handler
    // ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 25 minutes
    // USAGE: marathon 2025-11-01
    // STEPS:
    // 1. Check parts.Length == 2
    // 2. Parse: day = DateOnly.Parse(parts[1])
    // 3. Plan: var plan = planner.Plan(day, store.All())
    // 4. If empty: print "No plan"
    // 5. Else: print header, loop through plan, calculate total duration, print summary
    // HINT: var totalDuration = TimeSpan.Zero; foreach: totalDuration += show.Movie.Duration
    //==========================================================================
    /// <summary>
    /// Handles: marathon 2025-11-01
    /// Plans a marathon ticket with maximum non-overlapping shows.
    /// </summary>
    private static void HandleMarathon(string[] parts)
    {
        throw new NotImplementedException("TODO: Parse date, call planner.Plan, print results with total duration");
    }

    /// <summary>
    /// Custom string splitter that handles quoted strings.
    /// Example: 'add-room "Room A" 100' → ["add-room", "Room A", "100"]
    /// </summary>
    private static string[] Split(string input)
    {
        var list = new List<string>();
        bool inQuotes = false;
        var current = "";
        
        foreach (var ch in input)
        {
            if (ch == '"')
            {
                inQuotes = !inQuotes;  // Toggle quote mode
                continue;
            }
            
            if (char.IsWhiteSpace(ch) && !inQuotes)
            {
                // Space outside quotes - word boundary
                if (current.Length > 0)
                {
                    list.Add(current);
                    current = "";
                }
            }
            else
            {
                // Regular character or space inside quotes
                current += ch;
            }
        }
        
        if (current.Length > 0)
            list.Add(current);
        
        return list.ToArray();
    }
}
