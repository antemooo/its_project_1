# Manhattan Traffic Simulation — Console (C#/.NET 8)

## 1) Purpose
Simulate vehicles moving on a **Manhattan grid** of crossroads and streets with lights, capacities and optional roadworks. Final part adds **shortest‑path routing**.

## 2) Learning goals
- OOP modelling: `Vehicle`, `TrafficLight`, `Street`, `CrossRoad`, `City`.
- Queues, dictionaries and lists in practice.
- Discrete‑time simulation; separation of concerns.
- Intro to **Dijkstra** (shortest path on a grid graph).

## 3) Simulation rules
- **Tick:** 1 minute per step. Every tick: update lights → move vehicles along streets → enqueue at next light.
- **Lights:** `Green` releases `flow` vehicles/min; `Yellow` releases `floor(flow/2)`; `Red` releases 0.
- **Capacity:** if a queue or a street is full, vehicles wait upstream.
- **Priority vehicles:** may pass on red; still limited by capacity.
- **Roadworks:** a street (edge) can be **closed**/**opened**.
- **Shortest path (final):** precompute path using **street travel time** as weight; ignore waiting times.

## 4) CLI (suggested)
- `add-vehicle <name> <sx> <sy> <dx> <dy> [priority]`
- `tick <n>` — advance n minutes
- `status` — print per‑crossroad queues/loads
- `roadworks <x1> <y1> <x2> <y2> on|off`
- `stats` — per‑vehicle route & total travel time
- `exit`

## 5) Design notes
- `TrafficLight` owns a **queue** (priority vehicles to front).
- `Street` carries vehicles for `TravelTime` ticks.
- `CrossRoad` coordinates lights and streets (N/E/S/W).
- `City` builds a W×H grid and provides neighbours.
- **Routing:** Dijkstra on nodes `(x,y)`; edge weight = street travel time.

## 6) Run
```bash
	dotnet new console -n TrafficSim && cd TrafficSim
	# Add TrafficSimulation.cs from this template (or replace Program.cs)
	dotnet run --project TrafficSim.csproj
```

## 7) One‑week plan
- **D1:** grid + tick loop + status.
- **D2:** flows/yellow/queues/capacity; priority handling.
- **D3:** roadworks on/off with validation.
- **D4:** Dijkstra routing; print path.
- **D5:** stats, README examples, final tidy‑up.

## 8) Acceptance checks
- With `flow=2`, 3 min **Green** + 1 min **Yellow** releases **7** vehicles.
- Closing one street reroutes if an alternative exists; otherwise vehicles wait.
- On 2×2 grid (unit weights), shortest path from (0,0) to (1,1) uses **2 edges**.
