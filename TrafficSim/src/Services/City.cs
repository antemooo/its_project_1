//==============================================================================
// FILE: City.cs
// ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 30-40 minutes
// 🧪 TEST: Create City(3,3), check At(1,1), verify Neighbors returns 4 adjacent nodes
//==============================================================================

using TrafficSim.Models;
using System.Collections.Generic;

namespace TrafficSim.Services;

/// <summary>
/// Represents the city as a grid of crossroads.
/// Manhattan-style grid: vehicles can move North, South, East, West.
/// Used for pathfinding and simulation.
/// </summary>
public sealed class City
{
    private readonly CrossRoad[,] _grid;
    public int W { get; }
    public int H { get; }
    
    //==========================================================================
    // STUDENT TODO: Implement City grid initialization and navigation
    //
    // CONSTRUCTOR: public City(int w, int h)
    // - Set W = w, H = h
    // - Initialize: _grid = new CrossRoad[w, h]
    // - Nested loop: for x in [0, w), for y in [0, h)
    //   * Create crossroad: _grid[x, y] = new CrossRoad(x, y)
    //
    // AT METHOD:
    // - public CrossRoad At(int x, int y) => _grid[x, y];
    //
    // NEIGHBORS METHOD: (Returns adjacent crossroads for pathfinding)
    // - public IEnumerable<((int X, int Y) P, Side Dir)> Neighbors((int X, int Y) p)
    // - Check each direction and yield if in bounds:
    //   * North: if (p.Y > 0) yield return ((p.X, p.Y - 1), Side.North);
    //   * East:  if (p.X < W - 1) yield return ((p.X + 1, p.Y), Side.East);
    //   * South: if (p.Y < H - 1) yield return ((p.X, p.Y + 1), Side.South);
    //   * West:  if (p.X > 0) yield return ((p.X - 1, p.Y), Side.West);
    //
    // COORDINATE SYSTEM:
    // - (0, 0) is top-left corner
    // - X increases East (right)
    // - Y increases South (down)
    //
    // HINTS:
    // - 2D array [x, y] stores grid
    // - Neighbors is used by Dijkstra for graph traversal
    // - yield return creates iterator pattern
    //==========================================================================

    public City(int w, int h)
    {
        throw new NotImplementedException("TODO: Initialize W, H, create grid, populate with CrossRoads");
    }

    public CrossRoad At(int x, int y)
    {
        throw new NotImplementedException("TODO: Return crossroad at given coordinates");
    }

    /// <summary>
    /// Returns adjacent crossroads for pathfinding (graph neighbors).
    /// </summary>
    public IEnumerable<((int X, int Y) P, Side Dir)> Neighbors((int X, int Y) p)
    {
        throw new NotImplementedException("TODO: Yield adjacent positions in 4 directions (check bounds)");
    }
}
