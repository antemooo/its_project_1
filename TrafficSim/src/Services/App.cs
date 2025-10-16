//==============================================================================
// FILE: App.cs
// ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 25-35 minutes
// 🧪 TEST: Run app, type "path 0 0 2 2", expect path output
//==============================================================================

using TrafficSim.Models;
using System;
using System.Linq;

namespace TrafficSim.Services;

/// <summary>
/// Minimal CLI driver for the traffic simulation.
/// Demonstrates pathfinding on the city grid.
/// </summary>
public static class App
{
    private static readonly City city = new City(3, 3);

    //==========================================================================
    // STUDENT TODO: Implement REPL command loop for traffic simulation
    //
    // COMMANDS TO IMPLEMENT:
    // - "path x1 y1 x2 y2" - Find shortest path between two crossroads
    // - "exit" - Quit application
    //
    // STEPS:
    // 1. Print welcome message with command help
    // 2. Infinite loop: for (;;)
    // 3. Read line from Console
    // 4. Check for exit command → return
    // 5. Parse: var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
    // 6. Switch on parts[0]:
    //
    //    case "path":
    //      - Check parts.Length == 5, else print usage
    //      - Parse: var s = (int.Parse(parts[1]), int.Parse(parts[2]))
    //      - Parse: var g = (int.Parse(parts[3]), int.Parse(parts[4]))
    //      - Call: var p = ShortestPath.Dijkstra(city, s, g)
    //      - Print: if (p.Count == 0) "No path"
    //               else string.Join(" -> ", p.Select(t => $"({t.Item1},{t.Item2})"))
    //
    //    default:
    //      - Print "Unknown command"
    //
    // EXAMPLE OUTPUT:
    // > path 0 0 2 2
    // (0,0) -> (1,0) -> (2,0) -> (2,1) -> (2,2)
    //
    // HINTS:
    // - Tuple parsing: (int.Parse(parts[1]), int.Parse(parts[2]))
    // - string.Join formats path nicely
    // - Select projects tuple to string format
    //==========================================================================
    
    public static void Run()
    {
        throw new NotImplementedException("TODO: Implement REPL with path command");
    }
}
