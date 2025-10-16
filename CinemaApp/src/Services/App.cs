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

    /// <summary>
    /// Main application loop - reads and processes commands.
    /// </summary>
    public static void Run()
    {
        Console.WriteLine("Cinema App — Available commands:");
        Console.WriteLine("  add-room \"Name\" capacity");
        Console.WriteLine("  add-movie \"Title\" HH:mm");
        Console.WriteLine("  add-show \"Movie\" \"Room\" yyyy-MM-ddTHH:mm price");
        Console.WriteLine("  list-shows yyyy-MM-dd");
        Console.WriteLine("  book showId seat1 [seat2 ...]");
        Console.WriteLine("  marathon yyyy-MM-dd");
        Console.WriteLine("  exit");
        Console.WriteLine();
        
        // Main command loop
        for (;;)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.Equals("exit", StringComparison.OrdinalIgnoreCase)) return;

            var parts = Split(line);  // Custom split that handles quoted strings
            if (parts.Length == 0) continue;
            
            var cmd = parts[0].ToLowerInvariant();

            try
            {
                // Dispatch to appropriate handler
                switch (cmd)
                {
                    case "add-room":
                        HandleAddRoom(parts);
                        break;
                    case "add-movie":
                        HandleAddMovie(parts);
                        break;
                    case "add-show":
                        HandleAddShow(parts);
                        break;
                    case "list-shows":
                        HandleListShows(parts);
                        break;
                    case "book":
                        HandleBook(parts);
                        break;
                    case "marathon":
                        HandleMarathon(parts);
                        break;
                    default:
                        Console.WriteLine("Unknown command. Type a valid command or 'exit'.");
                        break;
                }
            }
            catch (Exception ex)
            {
                // Catch and display any errors gracefully
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Handles: add-room "Room A" 100
    /// Creates a new cinema room with specified capacity.
    /// </summary>
    private static void HandleAddRoom(string[] parts)
    {
        if (parts.Length != 3)
        {
            Console.WriteLine("Usage: add-room \"Room Name\" capacity");
            return;
        }

        var name = parts[1];
        var capacity = int.Parse(parts[2]);
        
        if (capacity <= 0)
        {
            Console.WriteLine("Capacity must be positive");
            return;
        }

        rooms[name] = new Room(name, capacity);
        Console.WriteLine($"✓ Room added: {rooms[name]}");
    }

    /// <summary>
    /// Handles: add-movie "Inception" 02:28
    /// Creates a new movie with title and duration.
    /// </summary>
    private static void HandleAddMovie(string[] parts)
    {
        if (parts.Length != 3)
        {
            Console.WriteLine("Usage: add-movie \"Movie Title\" HH:mm");
            return;
        }

        var title = parts[1];
        var duration = TimeSpan.Parse(parts[2]);

        movies[title] = new Movie(title, duration);
        Console.WriteLine($"✓ Movie added: {movies[title]}");
    }

    /// <summary>
    /// Handles: add-show "Inception" "Room A" 2025-11-01T10:00 9.99
    /// Creates a new show with all details.
    /// </summary>
    private static void HandleAddShow(string[] parts)
    {
        if (parts.Length != 5)
        {
            Console.WriteLine("Usage: add-show \"Movie\" \"Room\" yyyy-MM-ddTHH:mm price");
            return;
        }

        if (!movies.TryGetValue(parts[1], out var movie))
        {
            Console.WriteLine($"Movie '{parts[1]}' not found. Add it first with add-movie.");
            return;
        }

        if (!rooms.TryGetValue(parts[2], out var room))
        {
            Console.WriteLine($"Room '{parts[2]}' not found. Add it first with add-room.");
            return;
        }

        var start = DateTime.Parse(parts[3]);
        var price = decimal.Parse(parts[4]);

        var show = new Show(movie, room, start, price);
        store.Add(show);
        Console.WriteLine($"✓ Show added with ID: {show.Id:N}");
    }

    /// <summary>
    /// Handles: list-shows 2025-11-01
    /// Lists all shows for a specific date, ordered by start time.
    /// </summary>
    private static void HandleListShows(string[] parts)
    {
        if (parts.Length != 2)
        {
            Console.WriteLine("Usage: list-shows yyyy-MM-dd");
            return;
        }

        var day = DateOnly.Parse(parts[1]);
        var dayShows = store.All()
            .Where(s => DateOnly.FromDateTime(s.Start) == day)
            .OrderBy(s => s.Start)
            .ToList();

        if (dayShows.Count == 0)
        {
            Console.WriteLine($"No shows scheduled for {day}");
            return;
        }

        Console.WriteLine($"\nShows for {day}:");
        Console.WriteLine(new string('-', 80));
        foreach (var show in dayShows)
        {
            Console.WriteLine(show);
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Handles: book showId 12 13 14
    /// Books specified seats for a show.
    /// </summary>
    private static void HandleBook(string[] parts)
    {
        if (parts.Length < 3)
        {
            Console.WriteLine("Usage: book showId seat1 [seat2 ...]");
            return;
        }

        if (!Guid.TryParse(parts[1], out var showId))
        {
            Console.WriteLine("Invalid show ID format");
            return;
        }

        var show = store.Find(showId);
        if (show is null)
        {
            Console.WriteLine("Show not found");
            return;
        }

        var seats = parts.Skip(2).Select(int.Parse).ToArray();
        
        if (show.TryBook(seats))
        {
            Console.WriteLine($"✓ Booked {seats.Length} seat(s): {string.Join(", ", seats)}");
            Console.WriteLine($"  Remaining seats: {show.AvailableSeats}/{show.Room.Capacity}");
        }
        else
        {
            Console.WriteLine("✗ Booking failed. Seats may be invalid or already taken.");
        }
    }

    /// <summary>
    /// Handles: marathon 2025-11-01
    /// Plans a marathon ticket with maximum non-overlapping shows.
    /// </summary>
    private static void HandleMarathon(string[] parts)
    {
        if (parts.Length != 2)
        {
            Console.WriteLine("Usage: marathon yyyy-MM-dd");
            return;
        }

        var day = DateOnly.Parse(parts[1]);
        var plan = planner.Plan(day, store.All());

        if (plan.Count == 0)
        {
            Console.WriteLine($"No marathon plan available for {day}");
            return;
        }

        Console.WriteLine($"\n🎬 Marathon Plan for {day}:");
        Console.WriteLine($"   You can watch {plan.Count} movie(s) without overlap!\n");
        Console.WriteLine(new string('-', 80));
        
        var totalDuration = TimeSpan.Zero;
        for (int i = 0; i < plan.Count; i++)
        {
            var show = plan[i];
            Console.WriteLine($"{i + 1}. {show}");
            totalDuration += show.Movie.Duration;
        }
        
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"Total viewing time: {totalDuration.Hours}h {totalDuration.Minutes}m");
        Console.WriteLine();
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
