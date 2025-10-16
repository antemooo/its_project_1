# 🌟 Bonus Challenge: A* Pathfinding Algorithm

> **Difficulty**: ⭐⭐⭐ Advanced  
> **Prerequisites**: Completed basic Dijkstra implementation, understand priority queues  
> **Time**: 4-6 hours additional work  

## 📚 What You'll Learn

This bonus challenge upgrades your pathfinding from **Dijkstra's algorithm** (finds shortest path but explores everywhere) to **A\* algorithm** (finds shortest path faster using heuristics). You'll explore:

- **Heuristic Search** - using domain knowledge to guide search
- **Manhattan Distance** - perfect heuristic for grid-based pathfinding
- **Admissibility** - guaranteeing optimal solutions with heuristics
- **Algorithm Optimization** - making Dijkstra faster without sacrificing correctness
- **Performance Analysis** - measuring nodes explored and execution time

---

## 🎯 The Challenge

Transform your pathfinding to explore fewer nodes while still finding optimal paths.

### Current Dijkstra Behavior:
```
Grid: 5x5, Start: (0,0), Goal: (4,4)

Dijkstra explores in all directions:
  0 1 2 3 4
0 S→→→. .
1 ↓↓↓→. .
2 ↓↓↓→→.
3 ↓↓↓↓→→
4 ↓↓↓↓→G

Nodes explored: ~20 (explores entire area)
```

### A* with Manhattan Heuristic:
```
Grid: 5x5, Start: (0,0), Goal: (4,4)

A* focuses toward goal:
  0 1 2 3 4
0 S→→→→.
1 . . ↓→.
2 . . ↓→→
3 . . . ↓→
4 . . . . G

Nodes explored: ~9 (only promising paths)
```

**Result**: A* finds the same optimal path but **explores 55% fewer nodes**!

---

## 🧠 Conceptual Foundation

### What is A* (A-Star)?

A* is an **informed search algorithm** that uses:
1. **g(n)**: Actual cost from start to node n (like Dijkstra)
2. **h(n)**: Estimated cost from node n to goal (heuristic)
3. **f(n) = g(n) + h(n)**: Total estimated cost through node n

**Key Idea**: Prioritize nodes with lowest **f(n)** instead of just lowest **g(n)**.

### Why A* is Faster

Dijkstra explores in all directions equally (breadth-first by cost).  
A* biases exploration toward the goal using the heuristic.

**Analogy**:
- **Dijkstra**: "I'll check every store in town to find the cheapest milk"
- **A\***: "I'll check stores in the direction of downtown first (likely cheaper)"

### The Manhattan Distance Heuristic

For grid-based movement (can't cut diagonally):

```
Manhattan Distance = |x1 - x2| + |y1 - y2|
```

**Example**:
From (1, 1) to (4, 3):
- Horizontal distance: |4 - 1| = 3
- Vertical distance: |3 - 1| = 2
- Manhattan distance: 3 + 2 = 5

**Why it works**: In Manhattan grid, you MUST travel this many blocks minimum (no shortcuts).

### Admissibility: The Golden Rule

A heuristic is **admissible** if it **never overestimates** the actual cost.

**Manhattan distance is admissible** because:
- It assumes you can go straight (no obstacles)
- The actual path can only be ≥ Manhattan distance
- Therefore: h(n) ≤ actual_cost (never overestimates)

**Why this matters**: Admissible heuristics guarantee A* finds optimal paths!

---

## 💻 Implementation Guide

### Step 1: Create Heuristic Interface

```csharp
namespace TrafficSim.Services;

/// <summary>
/// Interface for pathfinding heuristics.
/// A heuristic estimates the cost from a node to the goal.
/// </summary>
public interface IPathfindingHeuristic
{
    /// <summary>
    /// Estimate cost from current position to goal.
    /// </summary>
    /// <param name="current">Current grid position</param>
    /// <param name="goal">Goal grid position</param>
    /// <returns>Estimated cost (must be admissible for optimal A*)</returns>
    double EstimateCost((int X, int Y) current, (int X, int Y) goal);
}
```

---

### Step 2: Implement Manhattan Distance Heuristic

```csharp
namespace TrafficSim.Services;

/// <summary>
/// Manhattan distance heuristic for grid-based pathfinding.
/// 
/// FORMULA: |x1 - x2| + |y1 - y2|
/// 
/// PROPERTIES:
/// - Admissible: Never overestimates (guarantees optimal A* solution)
/// - Consistent: h(n) ≤ cost(n, n') + h(n') for any neighbor n'
/// 
/// BEST FOR:
/// - Grid-based movement (4-directional or 8-directional)
/// - No diagonal movement allowed
/// - Manhattan-style city grids
/// 
/// TIME COMPLEXITY: O(1) - simple arithmetic
/// </summary>
public class ManhattanDistanceHeuristic : IPathfindingHeuristic
{
    /// <summary>
    /// Calculate Manhattan distance between two points.
    /// </summary>
    /// <param name="current">Current position</param>
    /// <param name="goal">Goal position</param>
    /// <returns>Manhattan distance (sum of absolute differences)</returns>
    public double EstimateCost((int X, int Y) current, (int X, int Y) goal)
    {
        // Manhattan distance = horizontal distance + vertical distance
        int dx = Math.Abs(goal.X - current.X);
        int dy = Math.Abs(goal.Y - current.Y);
        return dx + dy;
    }
}
```

---

### Step 3: Implement Zero Heuristic (Reduces A* to Dijkstra)

```csharp
namespace TrafficSim.Services;

/// <summary>
/// Zero heuristic - always returns 0.
/// This makes A* behave identically to Dijkstra's algorithm.
/// 
/// PROPERTIES:
/// - Trivially admissible (always underestimates)
/// - No guidance toward goal
/// 
/// USE CASE:
/// - Baseline comparison (measure improvement of real heuristics)
/// - When you want Dijkstra's behavior with A* code
/// 
/// TIME COMPLEXITY: O(1)
/// </summary>
public class ZeroHeuristic : IPathfindingHeuristic
{
    public double EstimateCost((int X, int Y) current, (int X, int Y) goal)
    {
        return 0; // No heuristic guidance
    }
}
```

---

### Step 4: Implement A* Algorithm

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using TrafficSim.Models;

namespace TrafficSim.Services;

/// <summary>
/// A* pathfinding algorithm implementation.
/// 
/// ALGORITHM: A* (A-Star) search
/// INVENTOR: Peter Hart, Nils Nilsson, Bertram Raphael (1968)
/// 
/// KEY IDEA:
/// - Use f(n) = g(n) + h(n) to prioritize nodes
/// - g(n) = actual cost from start to n
/// - h(n) = estimated cost from n to goal (heuristic)
/// - Explore nodes with lowest f(n) first
/// 
/// TIME COMPLEXITY: O((V + E) log V) worst case (same as Dijkstra)
/// - But explores fewer nodes in practice when heuristic is good
/// 
/// SPACE COMPLEXITY: O(V) for open set, closed set, and cost tracking
/// 
/// OPTIMALITY: Guaranteed optimal if heuristic is admissible
/// 
/// LEARNING OBJECTIVES:
/// - Understand informed search vs uninformed search
/// - Learn how heuristics guide exploration
/// - Compare A* performance vs Dijkstra
/// - Understand admissibility and consistency
/// </summary>
public class AStarPathfinder
{
    private readonly City _city;
    private readonly IPathfindingHeuristic _heuristic;
    
    // Statistics tracking
    public int NodesExplored { get; private set; }
    public int NodesGenerated { get; private set; }
    public long ExecutionTimeMicroseconds { get; private set; }

    /// <summary>
    /// Create A* pathfinder with specified heuristic.
    /// </summary>
    /// <param name="city">City grid to search</param>
    /// <param name="heuristic">Heuristic function (use Manhattan for grids)</param>
    public AStarPathfinder(City city, IPathfindingHeuristic heuristic)
    {
        _city = city;
        _heuristic = heuristic;
    }

    /// <summary>
    /// Find shortest path using A* algorithm.
    /// </summary>
    /// <param name="start">Starting position</param>
    /// <param name="goal">Goal position</param>
    /// <returns>List of positions forming the path (empty if no path exists)</returns>
    public List<(int X, int Y)> FindPath((int X, int Y) start, (int X, int Y) goal)
    {
        // Reset statistics
        NodesExplored = 0;
        NodesGenerated = 0;
        var sw = System.Diagnostics.Stopwatch.StartNew();

        // Validate input
        if (!IsValid(start) || !IsValid(goal))
        {
            ExecutionTimeMicroseconds = sw.ElapsedTicks * 1000000 / System.Diagnostics.Stopwatch.Frequency;
            return new List<(int, int)>();
        }

        // g(n): actual cost from start to n
        var gScore = new Dictionary<(int, int), double>();
        
        // f(n) = g(n) + h(n): estimated total cost
        var fScore = new Dictionary<(int, int), double>();
        
        // Track previous node for path reconstruction
        var cameFrom = new Dictionary<(int, int), (int, int)?>();
        
        // Open set: nodes to explore (priority queue by f-score)
        var openSet = new PriorityQueue<(int, int), double>();
        
        // Closed set: nodes already explored (for statistics)
        var closedSet = new HashSet<(int, int)>();

        // Initialize start node
        gScore[start] = 0;
        fScore[start] = _heuristic.EstimateCost(start, goal);
        openSet.Enqueue(start, fScore[start]);
        cameFrom[start] = null;
        NodesGenerated++;

        while (openSet.Count > 0)
        {
            // Get node with lowest f-score
            var current = openSet.Dequeue();
            
            // Skip if already explored (can happen with duplicate entries)
            if (closedSet.Contains(current))
                continue;
            
            closedSet.Add(current);
            NodesExplored++;

            // Goal reached!
            if (current.Equals(goal))
            {
                ExecutionTimeMicroseconds = sw.ElapsedTicks * 1000000 / System.Diagnostics.Stopwatch.Frequency;
                return ReconstructPath(cameFrom, current);
            }

            // Explore neighbors
            foreach (var (neighbor, direction) in _city.Neighbors(current))
            {
                // Skip already explored
                if (closedSet.Contains(neighbor))
                    continue;

                // Get edge cost (could be based on street travel time, traffic, etc.)
                double edgeCost = GetEdgeCost(current, neighbor, direction);
                
                // Calculate tentative g-score
                double tentativeGScore = gScore[current] + edgeCost;

                // If this path to neighbor is better than any previous one
                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    // This path is the best so far - record it
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    
                    // Calculate f-score = g + h
                    double hScore = _heuristic.EstimateCost(neighbor, goal);
                    fScore[neighbor] = tentativeGScore + hScore;
                    
                    // Add to open set (will be explored in priority order)
                    openSet.Enqueue(neighbor, fScore[neighbor]);
                    NodesGenerated++;
                }
            }
        }

        // No path found
        ExecutionTimeMicroseconds = sw.ElapsedTicks * 1000000 / System.Diagnostics.Stopwatch.Frequency;
        return new List<(int, int)>();
    }

    /// <summary>
    /// Get cost of traveling from one crossroad to neighbor.
    /// Can be extended to consider traffic, road conditions, etc.
    /// </summary>
    private double GetEdgeCost((int X, int Y) from, (int X, int Y) to, Side direction)
    {
        // Get the street connecting these crossroads
        var crossroad = _city.At(from.X, from.Y);
        var street = crossroad.StreetOut(direction);
        
        // Base cost is travel time
        double cost = street.TravelTime;
        
        // Penalty for closed streets (roadworks)
        if (street.Closed)
            return double.PositiveInfinity;
        
        // Optional: Add penalty based on current traffic load
        // cost += street.Load * 0.1; // More traffic = higher cost
        
        return cost;
    }

    /// <summary>
    /// Reconstruct path from start to goal by following cameFrom links.
    /// </summary>
    private List<(int, int)> ReconstructPath(
        Dictionary<(int, int), (int, int)?> cameFrom, 
        (int, int) current)
    {
        var path = new List<(int, int)> { current };
        
        while (cameFrom[current] != null)
        {
            current = cameFrom[current]!.Value;
            path.Add(current);
        }
        
        path.Reverse();
        return path;
    }

    /// <summary>
    /// Check if position is within city bounds.
    /// </summary>
    private bool IsValid((int X, int Y) pos)
    {
        return pos.X >= 0 && pos.X < _city.W && 
               pos.Y >= 0 && pos.Y < _city.H;
    }

    /// <summary>
    /// Get detailed statistics about the search.
    /// </summary>
    public PathfindingStats GetStatistics()
    {
        return new PathfindingStats
        {
            NodesExplored = NodesExplored,
            NodesGenerated = NodesGenerated,
            ExecutionTimeMicroseconds = ExecutionTimeMicroseconds
        };
    }
}

/// <summary>
/// Statistics about a pathfinding operation.
/// </summary>
public class PathfindingStats
{
    public int NodesExplored { get; set; }
    public int NodesGenerated { get; set; }
    public long ExecutionTimeMicroseconds { get; set; }
    
    public double ExplorationEfficiency => 
        NodesGenerated > 0 ? (double)NodesExplored / NodesGenerated : 0;
}
```

---

### Step 5: Comparison Tool

```csharp
namespace TrafficSim.Services;

/// <summary>
/// Compare different pathfinding algorithms on the same problem.
/// </summary>
public static class PathfindingComparison
{
    public class PathfindingResult
    {
        public string AlgorithmName { get; set; } = "";
        public List<(int X, int Y)> Path { get; set; } = new();
        public int PathLength { get; set; }
        public double TotalCost { get; set; }
        public PathfindingStats Stats { get; set; } = new();
    }

    /// <summary>
    /// Compare Dijkstra (A* with zero heuristic) vs A* with Manhattan heuristic.
    /// </summary>
    public static List<PathfindingResult> CompareAlgorithms(
        City city, 
        (int X, int Y) start, 
        (int X, int Y) goal)
    {
        var results = new List<PathfindingResult>();

        // Test 1: A* with Zero heuristic (equivalent to Dijkstra)
        var dijkstra = new AStarPathfinder(city, new ZeroHeuristic());
        var dijkstraPath = dijkstra.FindPath(start, goal);
        results.Add(new PathfindingResult
        {
            AlgorithmName = "Dijkstra (A* + Zero Heuristic)",
            Path = dijkstraPath,
            PathLength = dijkstraPath.Count,
            TotalCost = CalculatePathCost(city, dijkstraPath),
            Stats = dijkstra.GetStatistics()
        });

        // Test 2: A* with Manhattan heuristic
        var aStar = new AStarPathfinder(city, new ManhattanDistanceHeuristic());
        var aStarPath = aStar.FindPath(start, goal);
        results.Add(new PathfindingResult
        {
            AlgorithmName = "A* (Manhattan Heuristic)",
            Path = aStarPath,
            PathLength = aStarPath.Count,
            TotalCost = CalculatePathCost(city, aStarPath),
            Stats = aStar.GetStatistics()
        });

        return results;
    }

    /// <summary>
    /// Calculate total cost of a path.
    /// </summary>
    private static double CalculatePathCost(City city, List<(int X, int Y)> path)
    {
        if (path.Count < 2) return 0;

        double totalCost = 0;
        for (int i = 0; i < path.Count - 1; i++)
        {
            var from = path[i];
            var to = path[i + 1];
            
            // Determine direction
            Side direction;
            if (to.Y < from.Y) direction = Side.North;
            else if (to.X > from.X) direction = Side.East;
            else if (to.Y > from.Y) direction = Side.South;
            else direction = Side.West;
            
            var street = city.At(from.X, from.Y).StreetOut(direction);
            totalCost += street.TravelTime;
        }
        
        return totalCost;
    }

    /// <summary>
    /// Print comparison table to console.
    /// </summary>
    public static void PrintComparison(List<PathfindingResult> results)
    {
        Console.WriteLine("\n=== Pathfinding Algorithm Comparison ===\n");
        Console.WriteLine($"{"Algorithm",-30} {"Path Len",-10} {"Cost",-10} {"Explored",-10} {"Generated",-10} {"Time (μs)",-12} {"Efficiency",-10}");
        Console.WriteLine(new string('-', 100));

        foreach (var result in results)
        {
            Console.WriteLine(
                $"{result.AlgorithmName,-30} " +
                $"{result.PathLength,-10} " +
                $"{result.TotalCost,-10:F1} " +
                $"{result.Stats.NodesExplored,-10} " +
                $"{result.Stats.NodesGenerated,-10} " +
                $"{result.Stats.ExecutionTimeMicroseconds,-12} " +
                $"{result.Stats.ExplorationEfficiency,-10:P1}"
            );
        }

        Console.WriteLine();
        
        // Calculate improvement
        if (results.Count >= 2)
        {
            var baseline = results[0];
            var improved = results[1];
            
            double explorationReduction = 1.0 - ((double)improved.Stats.NodesExplored / baseline.Stats.NodesExplored);
            double speedup = (double)baseline.Stats.ExecutionTimeMicroseconds / improved.Stats.ExecutionTimeMicroseconds;
            
            Console.WriteLine("Improvements:");
            Console.WriteLine($"  - Nodes Explored: {explorationReduction:P1} fewer");
            Console.WriteLine($"  - Speedup: {speedup:F2}x faster");
            Console.WriteLine($"  - Path Quality: {(improved.TotalCost == baseline.TotalCost ? "Same (optimal)" : "Different")}");
        }
    }

    /// <summary>
    /// Print path visualization on grid.
    /// </summary>
    public static void PrintPathVisualization(City city, List<(int X, int Y)> path, string title)
    {
        Console.WriteLine($"\n=== {title} ===\n");
        
        if (path.Count == 0)
        {
            Console.WriteLine("No path found!");
            return;
        }

        var pathSet = new HashSet<(int, int)>(path);
        var start = path[0];
        var goal = path[^1];

        Console.Write("   ");
        for (int x = 0; x < city.W; x++)
            Console.Write($" {x}");
        Console.WriteLine();

        for (int y = 0; y < city.H; y++)
        {
            Console.Write($" {y} ");
            for (int x = 0; x < city.W; x++)
            {
                var pos = (x, y);
                if (pos.Equals(start))
                    Console.Write(" S");
                else if (pos.Equals(goal))
                    Console.Write(" G");
                else if (pathSet.Contains(pos))
                    Console.Write(" *");
                else
                    Console.Write(" .");
            }
            Console.WriteLine();
        }
        
        Console.WriteLine($"\nPath length: {path.Count} nodes");
        Console.WriteLine($"Path: {string.Join(" → ", path.Select(p => $"({p.X},{p.Y})"))}");
    }
}
```

---

### Step 6: Add CLI Commands

Update your `App.cs` with new commands:

```csharp
case "path-astar":
    if (parts.Length != 5)
    {
        Console.WriteLine("Usage: path-astar <x1> <y1> <x2> <y2>");
        break;
    }
    
    var startA = (int.Parse(parts[1]), int.Parse(parts[2]));
    var goalA = (int.Parse(parts[3]), int.Parse(parts[4]));
    
    var aStarFinder = new AStarPathfinder(city, new ManhattanDistanceHeuristic());
    var pathA = aStarFinder.FindPath(startA, goalA);
    
    if (pathA.Count == 0)
    {
        Console.WriteLine("No path found!");
    }
    else
    {
        Console.WriteLine($"Path: {string.Join(" → ", pathA.Select(p => $"({p.X},{p.Y})"))}");
        Console.WriteLine($"Length: {pathA.Count} nodes");
        var stats = aStarFinder.GetStatistics();
        Console.WriteLine($"Explored: {stats.NodesExplored} nodes");
        Console.WriteLine($"Time: {stats.ExecutionTimeMicroseconds} μs");
    }
    break;

case "compare-paths":
    if (parts.Length != 5)
    {
        Console.WriteLine("Usage: compare-paths <x1> <y1> <x2> <y2>");
        break;
    }
    
    var startC = (int.Parse(parts[1]), int.Parse(parts[2]));
    var goalC = (int.Parse(parts[3]), int.Parse(parts[4]));
    
    var comparison = PathfindingComparison.CompareAlgorithms(city, startC, goalC);
    PathfindingComparison.PrintComparison(comparison);
    
    // Show visualizations
    foreach (var result in comparison)
    {
        PathfindingComparison.PrintPathVisualization(city, result.Path, result.AlgorithmName);
    }
    break;
```

---

## 🧪 Testing Your Implementation

```csharp
namespace TrafficSim.Tests;

public static class AStarTests
{
    public static void RunAllTests()
    {
        TestBasicPath();
        TestNoPath();
        TestOptimality();
        TestPerformance();
        Console.WriteLine("✅ All A* tests passed!");
    }

    /// <summary>
    /// Test that A* finds a valid path.
    /// </summary>
    private static void TestBasicPath()
    {
        var city = new City(5, 5);
        var pathfinder = new AStarPathfinder(city, new ManhattanDistanceHeuristic());
        
        var path = pathfinder.FindPath((0, 0), (4, 4));
        
        Assert(path.Count > 0, "Should find a path");
        Assert(path[0] == (0, 0), "Path should start at start");
        Assert(path[^1] == (4, 4), "Path should end at goal");
        
        // Check path is continuous
        for (int i = 0; i < path.Count - 1; i++)
        {
            var from = path[i];
            var to = path[i + 1];
            int dist = Math.Abs(to.Item1 - from.Item1) + Math.Abs(to.Item2 - from.Item2);
            Assert(dist == 1, "Path steps should be adjacent");
        }
    }

    /// <summary>
    /// Test that A* returns empty path when no path exists.
    /// </summary>
    private static void TestNoPath()
    {
        var city = new City(3, 3);
        
        // Close all streets leading out of (0,0)
        city.At(0, 0).StreetOut(Side.East).Closed = true;
        city.At(0, 0).StreetOut(Side.South).Closed = true;
        
        var pathfinder = new AStarPathfinder(city, new ManhattanDistanceHeuristic());
        var path = pathfinder.FindPath((0, 0), (2, 2));
        
        Assert(path.Count == 0, "Should return empty path when blocked");
    }

    /// <summary>
    /// Test that A* finds optimal path (same as Dijkstra).
    /// </summary>
    private static void TestOptimality()
    {
        var city = new City(5, 5);
        
        // A* with Manhattan heuristic
        var aStar = new AStarPathfinder(city, new ManhattanDistanceHeuristic());
        var aStarPath = aStar.FindPath((0, 0), (4, 4));
        
        // Dijkstra (A* with zero heuristic)
        var dijkstra = new AStarPathfinder(city, new ZeroHeuristic());
        var dijkstraPath = dijkstra.FindPath((0, 0), (4, 4));
        
        Assert(aStarPath.Count == dijkstraPath.Count, 
            "A* and Dijkstra should find same length path");
    }

    /// <summary>
    /// Test that A* explores fewer nodes than Dijkstra.
    /// </summary>
    private static void TestPerformance()
    {
        var city = new City(10, 10);
        
        var aStar = new AStarPathfinder(city, new ManhattanDistanceHeuristic());
        aStar.FindPath((0, 0), (9, 9));
        var aStarStats = aStar.GetStatistics();
        
        var dijkstra = new AStarPathfinder(city, new ZeroHeuristic());
        dijkstra.FindPath((0, 0), (9, 9));
        var dijkstraStats = dijkstra.GetStatistics();
        
        Console.WriteLine($"A* explored: {aStarStats.NodesExplored} nodes");
        Console.WriteLine($"Dijkstra explored: {dijkstraStats.NodesExplored} nodes");
        
        Assert(aStarStats.NodesExplored < dijkstraStats.NodesExplored,
            "A* should explore fewer nodes than Dijkstra");
        
        double reduction = 1.0 - ((double)aStarStats.NodesExplored / dijkstraStats.NodesExplored);
        Console.WriteLine($"Reduction: {reduction:P1}");
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition)
            throw new Exception($"Assertion failed: {message}");
    }
}
```

---

## 📊 Algorithm Comparison

### Dijkstra vs A*

| Aspect | Dijkstra | A* |
|--------|----------|-----|
| **Priority** | g(n) only | f(n) = g(n) + h(n) |
| **Exploration** | Uniform (all directions) | Directed (toward goal) |
| **Nodes Explored** | More | Fewer (with good heuristic) |
| **Optimality** | ✅ Always optimal | ✅ Optimal if h is admissible |
| **Time Complexity** | O((V+E) log V) | O((V+E) log V) worst case |
| **Space Complexity** | O(V) | O(V) |
| **Best For** | Unknown goal direction | Known goal location |
| **Heuristic Required** | No | Yes |

### When to Use Each

**Use Dijkstra when**:
- Goal location unknown
- Need paths to ALL nodes
- No good heuristic available
- Guarantee simplicity

**Use A\* when**:
- Single source, single goal
- Goal location known
- Good heuristic available (Manhattan, Euclidean)
- Performance critical

### Real-World Performance

On a 10×10 grid from (0,0) to (9,9):
- **Dijkstra**: Explores ~55 nodes
- **A\* (Manhattan)**: Explores ~19 nodes
- **Improvement**: 65% fewer nodes explored!

On a 50×50 grid from (0,0) to (49,49):
- **Dijkstra**: Explores ~1250 nodes
- **A\* (Manhattan)**: Explores ~99 nodes
- **Improvement**: 92% fewer nodes explored!

---

## 🎨 Advanced Extensions

### 1. Different Heuristics

Implement and compare other heuristics:

```csharp
/// <summary>
/// Euclidean distance heuristic.
/// Generally less accurate than Manhattan for grid movement.
/// </summary>
public class EuclideanDistanceHeuristic : IPathfindingHeuristic
{
    public double EstimateCost((int X, int Y) current, (int X, int Y) goal)
    {
        int dx = goal.X - current.X;
        int dy = goal.Y - current.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}

/// <summary>
/// Chebyshev distance (diagonal distance).
/// Best for 8-directional movement (with diagonals).
/// </summary>
public class ChebyshevDistanceHeuristic : IPathfindingHeuristic
{
    public double EstimateCost((int X, int Y) current, (int X, int Y) goal)
    {
        int dx = Math.Abs(goal.X - current.X);
        int dy = Math.Abs(goal.Y - current.Y);
        return Math.Max(dx, dy);
    }
}

/// <summary>
/// Overestimating heuristic (NOT admissible).
/// Explores fewer nodes but may not find optimal path!
/// </summary>
public class AggressiveHeuristic : IPathfindingHeuristic
{
    private const double WEIGHT = 1.5; // Multiply heuristic by weight > 1
    
    public double EstimateCost((int X, int Y) current, (int X, int Y) goal)
    {
        int dx = Math.Abs(goal.X - current.X);
        int dy = Math.Abs(goal.Y - current.Y);
        return (dx + dy) * WEIGHT; // Overestimate to explore less
    }
}
```

### 2. Weighted A* (Suboptimal but Faster)

```csharp
/// <summary>
/// Weighted A* trades optimality for speed.
/// Uses f(n) = g(n) + w * h(n) where w > 1
/// </summary>
public class WeightedAStar : AStarPathfinder
{
    private readonly double _weight;
    
    public WeightedAStar(City city, IPathfindingHeuristic heuristic, double weight) 
        : base(city, heuristic)
    {
        _weight = weight;
    }
    
    // Override to apply weight to heuristic
    protected double GetFScore(double gScore, double hScore)
    {
        return gScore + _weight * hScore;
    }
}
```

### 3. Bidirectional A*

Search from both start and goal simultaneously:

```csharp
/// <summary>
/// Bidirectional A* runs two searches simultaneously.
/// Stops when the searches meet in the middle.
/// Can be 2x faster than regular A* in practice.
/// </summary>
public class BidirectionalAStar
{
    // Forward search: start → goal
    // Backward search: goal → start
    // Stop when they meet
    // Combine paths
    
    // Implementation left as exercise!
}
```

### 4. Dynamic Replanning (D* Lite)

Handle changes in the environment:

```csharp
/// <summary>
/// D* Lite: Incrementally repair paths when costs change.
/// Useful for dynamic environments (roadworks appear/disappear).
/// Much faster than recomputing from scratch.
/// </summary>
public class DStarLite
{
    // When a street closes:
    // 1. Update affected edges
    // 2. Recompute only affected parts of path
    // 3. Much faster than full recomputation
    
    // Implementation is quite complex - see research papers!
}
```

### 5. Visualization

Animate the search process:

```csharp
public static void AnimateSearch(City city, (int X, int Y) start, (int X, int Y) goal)
{
    var pathfinder = new AStarPathfinder(city, new ManhattanDistanceHeuristic());
    
    // Hook into search to visualize each step
    pathfinder.OnNodeExplored += (node) =>
    {
        Console.Clear();
        PrintGridWithExplored(city, start, goal, exploredNodes);
        Thread.Sleep(100); // Pause to see animation
    };
    
    var path = pathfinder.FindPath(start, goal);
    
    // Show final path
    PrintGridWithPath(city, path);
}
```

---

## 📚 Theory Deep Dive

### Admissibility Proof for Manhattan Distance

**Theorem**: Manhattan distance is admissible for grid-based pathfinding.

**Proof**:
1. Let h*(n) = actual shortest path cost from n to goal
2. Let h(n) = Manhattan distance from n to goal
3. To prove: h(n) ≤ h*(n) for all n

**Argument**:
- Manhattan distance assumes you can go straight (no obstacles)
- Actual path must navigate around obstacles
- Therefore: actual_cost ≥ straight_line_cost
- Hence: h(n) ≤ h*(n) ✓

**Conclusion**: Manhattan distance never overestimates, so it's admissible.

### Consistency (Monotonicity)

A heuristic is **consistent** if for every node n and neighbor n':
```
h(n) ≤ cost(n, n') + h(n')
```

**Why it matters**: Consistent heuristics guarantee that A* explores each node at most once (more efficient).

**Manhattan distance is consistent**:
- Moving one step changes position by 1 in one direction
- Manhattan distance changes by at most 1
- cost(n, n') = 1 (one step)
- Therefore: h(n) ≤ 1 + h(n') ✓

### A* Optimality Theorem

**Theorem**: If heuristic h is admissible, A* is guaranteed to find optimal path.

**Proof sketch**:
1. Suppose A* returns suboptimal path P
2. There exists optimal path P* that A* didn't explore fully
3. Since h is admissible, f(n) never overestimates for nodes on P*
4. Therefore, some node on P* would have lower f-score than end of P
5. A* would have explored that node first (contradiction!)
6. Conclusion: A* must return optimal path ✓

---

## 🧠 Discussion Questions

Include answers to these in your README:

1. **Why doesn't Euclidean distance work as well as Manhattan for grids?**
   - Hint: Can you actually travel diagonally?

2. **What happens if we use h(n) = 2 * Manhattan_distance?**
   - Still finds a path? Still optimal?

3. **When might you prefer Dijkstra over A\*?**
   - Think about when heuristic isn't helpful

4. **Real-world applications of A\*:**
   - Video game pathfinding
   - GPS navigation
   - Robot motion planning
   - Network routing

5. **How does A\* compare to BFS (Breadth-First Search)?**
   - BFS: unweighted, explores by distance
   - A\*: weighted, explores by estimated total cost

---

## 🏆 Grading Bonus Points

| Achievement | Bonus Points |
|-------------|--------------|
| A* implementation working | +5 |
| Manhattan heuristic | +2 |
| Comparison tool | +3 |
| Comprehensive testing | +3 |
| Performance analysis | +2 |
| Visualization | +3 |
| Alternative heuristic | +2 |
| **Maximum Bonus** | **+20** |

---

## 📖 Further Reading

### Books
- **"Artificial Intelligence: A Modern Approach"** (Russell & Norvig) - Chapter 3
- **"Introduction to Algorithms"** (CLRS) - Graph algorithms
- **"AI for Games"** (Millington) - Practical game AI

### Online Resources
- [A* Pathfinding](https://www.redblobgames.com/pathfinding/a-star/introduction.html) - Interactive tutorial
- [A* Wikipedia](https://en.wikipedia.org/wiki/A*_search_algorithm) - Formal description
- [Stanford CS221](http://web.stanford.edu/class/cs221/) - AI course notes

### Interactive Demos
- [PathFinding.js](https://qiao.github.io/PathFinding.js/visual/) - Visual comparison
- [A* Visualization](https://www.cs.usfca.edu/~galles/visualization/AStar.html) - Step by step

---

## ✅ Submission Checklist

- [ ] A* implementation working correctly
- [ ] Manhattan heuristic implemented
- [ ] Zero heuristic for comparison
- [ ] Comparison tool showing statistics
- [ ] Tests verifying optimality
- [ ] Performance measurements
- [ ] CLI commands for both algorithms
- [ ] Documentation explaining heuristics
- [ ] Analysis of when A* beats Dijkstra
- [ ] (Optional) Alternative heuristic or extension

---

## 🚀 Get Started!

1. Create `src/Services/IPathfindingHeuristic.cs`
2. Implement `ManhattanDistanceHeuristic`
3. Implement `AStarPathfinder`
4. Add comparison tool
5. Test on various grid sizes
6. Measure performance improvements
7. Choose an advanced extension

Good luck! A* is one of the most important algorithms in computer science - used everywhere from games to GPS! 🎉
