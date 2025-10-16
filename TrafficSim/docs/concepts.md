=== Manhattan Traffic Simulation - Concepts ===

PURPOSE:
Simulate a grid city (like Manhattan) where vehicles move through
intersections controlled by traffic lights, with optional roadworks
and shortest-path routing.

KEY CONCEPTS FOR STUDENTS:

1. DISCRETE EVENT SIMULATION
   - Time advances in fixed steps (ticks = 1 minute)
   - Each tick: update lights → move vehicles → check arrivals
   
2. STATE MACHINES
   - Traffic lights cycle through states: Green → Yellow → Red → Green
   - Each state has a duration and different behavior
   
3. QUEUE DATA STRUCTURES
   - Vehicles wait in queues at traffic lights
   - Priority vehicles (ambulances) go to front of queue
   - FIFO (First In, First Out) for regular vehicles
   
4. GRAPH ALGORITHMS
   - City is a GRAPH: crossroads are nodes, streets are edges
   - Dijkstra's algorithm finds shortest path between points
   - Weight = street travel time (ignore light waiting time)
   
5. CAPACITY CONSTRAINTS
   - Streets have maximum vehicles they can hold
   - Lights have maximum queue size
   - Vehicles wait upstream if capacity reached

SIMULATION RULES:

Traffic Light Flow:
  - Green: Release FlowPerMinute vehicles
  - Yellow: Release FlowPerMinute/2 vehicles (transition)
  - Red: Release 0 vehicles (except priority can pass)
  
Priority Vehicles:
  - Can pass on red light
  - Jump to front of queues
  - Still respect capacity limits
  
Roadworks:
  - Can close a street (edge in graph)
  - Forces vehicles to use alternate routes
  - Requires recomputing shortest paths

EXAMPLE SCENARIO:

Grid: 3x3 (coordinates 0,0 to 2,2)
Vehicle "Car1": Start (0,0), Destination (2,2)
Route: (0,0) → (1,0) → (2,0) → (2,1) → (2,2)

At each crossroad:
1. Vehicle enters traffic light queue
2. Waits based on light state
3. Light releases vehicles based on flow
4. Vehicle enters street
5. Travels for street's travel time
6. Arrives at next crossroad

DIJKSTRA ALGORITHM (Shortest Path):

Purpose: Find fastest route from A to B
Input: Start coordinates, end coordinates
Output: List of coordinates to visit

Algorithm:
1. Set all distances to infinity, except start = 0
2. While unvisited nodes exist:
   a. Pick node with smallest distance
   b. For each neighbor:
      - Calculate distance through current node
      - If shorter, update neighbor's distance
3. Backtrack from end to start using recorded paths

Why it works:
- Always picks shortest known path
- Guarantees optimal solution
- Time: O((V+E) log V) with proper data structures

IMPLEMENTATION TIPS:

Start Simple:
1. Create Vehicle, Light, Side enums
2. Implement CrossRoad with 4 lights
3. Build City as 2D array of CrossRoads
4. Add tick() method that updates everything
5. Test with 1-2 vehicles manually

Then Add:
- Priority vehicle logic
- Capacity constraints
- Roadworks toggling
- Dijkstra pathfinding
- Statistics output

Common Pitfalls:
- Forgetting to tick() all components
- Not handling priority vehicles in queue
- Edge cases: vehicles arrive at same time
- Dijkstra: not handling closed streets
