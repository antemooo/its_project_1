
---

# Mini-Project Pack (Week 5, Solo) — Choose **ONE**

**Common rules (for all options)**

* **Language/runtime:** C# (.NET 8), console app only.
* **OOP:** clear modelling (classes/records), encapsulation, composition-first, sensible use of at least **one** small pattern (Strategy/Factory/Repository/Adapter).
* **Data structures & LINQ:** use the right collections and LINQ where it helps readability.
* **Persistence:** JSON or CSV where specified.
* **Errors:** validate inputs; catch and handle exceptions with friendly messages.
* **Git/GitHub workflow:** issues → small feature branches → PRs → peer review (or self-review notes if working solo) → squash merge.
* **Deliverables (by end of Week 5):**

  * Working console app + source code.
  * README with run steps, features, and sample commands/output.
  * At least **5 PRs** merged (setup, core feature, persistence, algorithm, polish).
  * Brief demo (≤5 min) OR screencast (≤3 min).

## 📊 Marking Rubric (100 points)

| Category | Points | What We're Looking For |
|----------|--------|------------------------|
| **Functionality** | 40 | Does it work? Meets all requirements? Produces correct outputs? |
| **Code Quality & OOP** | 25 | Clean code, proper OOP usage, design patterns applied correctly |
| **Git/GitHub Workflow** | 15 | Good issues, branches, PRs, meaningful commits |
| **Robustness** | 10 | Input validation, error handling, handles edge cases |
| **Documentation & Demo** | 10 | Clear README, good examples, effective demo video |

## 📁 Suggested Repository Layout (all projects)

**Important**: Separate your code into logical folders. This shows professional organization and makes code easier to find and maintain.

```
your-project/
├── src/                          # All source code goes here
│   ├── Models/                   # Data classes (Vehicle, Movie, Room, etc.)
│   │   ├── YourClass.cs
│   │   └── YourInterface.cs
│   ├── Services/                 # Business logic, strategies, repositories
│   │   ├── Operations/           # (Calculator only) Individual operations
│   │   ├── YourFactory.cs
│   │   ├── YourRepository.cs
│   │   └── YourStrategy.cs
│   └── Program.cs                # Entry point (Main method)
├── data/                         # Sample data files
│   ├── examples.txt              # Example commands
│   └── sample-data.json          # Test data (if using JSON)
├── docs/                         # Documentation (optional)
│   ├── concepts.md               # Explain your algorithms
│   └── screenshots/              # Demo images
├── README.md                     # Project documentation
└── .gitignore                    # Ignore bin/, obj/, etc.
```

### Why This Structure?
- **Models/** = "What data do we have?" (nouns: Movie, Vehicle, Room)
- **Services/** = "What can we do?" (verbs: Calculate, Plan, Store)
- **Program.cs** = Entry point that starts everything
- **data/** = Sample inputs for testing
- **docs/** = Explanations and documentation

## 📅 Working Week Guide (Day-by-Day)

### **Day 1: Setup & Foundation** (Monday)
- ✅ Create GitHub repository
- ✅ Add .gitignore for C# (ignore `bin/`, `obj/`)
- ✅ Create basic folder structure (`src/Models/`, `src/Services/`)
- ✅ Write skeleton Program.cs with basic menu
- ✅ Create first model class
- 🔄 **PR #1**: "Initial project setup"

### **Day 2: Core Features** (Tuesday)
- ✅ Implement main user commands (add, list, calculate)
- ✅ Add input parsing and validation
- ✅ Create basic services (Calculator, Store, etc.)
- 🔄 **PR #2**: "Implement core features"

### **Day 3: Persistence & Algorithms** (Wednesday)
- ✅ Add data persistence (JSON/CSV or in-memory repository)
- ✅ Implement main algorithm (operations, shortest path, marathon planning)
- ✅ Test with sample data
- 🔄 **PR #3**: "Add persistence layer"
- 🔄 **PR #4**: "Implement main algorithm"

### **Day 4: Polish & Patterns** (Thursday)
- ✅ Add error handling for all inputs
- ✅ Improve user experience (better messages, help text)
- ✅ Ensure design pattern is clearly implemented
- ✅ Add code comments explaining complex parts
- 🔄 **PR #5**: "Error handling and UX improvements"

### **Day 5: Documentation & Demo** (Friday)
- ✅ Write comprehensive README with examples
- ✅ Create sample data file (`data/examples.txt`)
- ✅ Record demo video or prepare live demo
- ✅ Final testing and bug fixes
- ✅ Tag release as `v1.0`

## 🎯 Success Tips

1. **Start Simple**: Get basic version working first, then add features
2. **Commit Often**: Make small commits with clear messages
3. **Test As You Go**: Run your app after each feature to catch bugs early
4. **Ask for Help**: Use course forum, office hours, or study groups
5. **Read the Examples**: Check `data/examples.txt` in each project folder

---

## Option 1 — Scientific Calculator (Console)

### Goal

Design a **scientific calculator** with basic arithmetic and **≥5 scientific operations** (e.g., `sin`, `cos`, `tan`, `sqrt`, `pow x^y`, `log10`, `ln`, `mod`, **integer division**, **factorial**). Operations accept **any number of numeric parameters** (e.g., `add 2.5 1 -2 24.5125 0.33`).

### Requirements

* **Input style (recommended):** command + space-separated arguments, e.g.:

  * `add 2.5 1 -2 24.5125 0.33`
  * `sin deg 30` or `sin rad 0.5235987756`
  * `pow 2 8` , `idiv 7 3`, `mod 7 3`, `fact 5`, `log10 1000`, `ln 2.71828`
* **Typing:** accept `int`/`double` seamlessly (parse to `double` internally).
* **Arity:** most functions support **variable arity** via `params double[]`.
* **Edge cases:** divide by zero; `sqrt` of negative; `fact` for non-integers or negatives (reject politely); domain for `log`.
* **Extensibility:** register operations via a **Strategy** + **Factory** combo.

### OOP & Patterns

* `IOperation { string Name {get;} string Help {get;} double Evaluate(params double[] args); }`
* `OperationFactory` maps command string → `IOperation` (Creational).
* `Calculator` owns a registry (`Dictionary<string, IOperation>`); can print help and dispatch.

### CLI commands (minimum)

* `add`, `sub`, `mul`, `div`, plus **≥5** scientific ops (`sin`, `cos`, `tan`, `sqrt`, `pow`, `log10`, `ln`, `mod`, `idiv`, `fact`).
* `mode` for angle unit (default **rad**, allow `deg`).
* `help`, `exit`.

### Minimal skeleton

```csharp
// Program.cs
using System.Globalization;

Calculator.Run();

public static class Calculator
{
    private static readonly Dictionary<string, IOperation> Ops = OperationFactory.CreateAll();
    private static AngleMode _angleMode = AngleMode.Radians;

    public static void Run()
    {
        Console.WriteLine("SciCalc — type 'help' or 'exit'");
        for(;;)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.Equals("exit", StringComparison.OrdinalIgnoreCase)) return;

            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var cmd = parts[0].ToLowerInvariant();

            if (cmd == "help") { PrintHelp(); continue; }
            if (cmd == "mode" && parts.Length == 2)
            { _angleMode = parts[1].StartsWith("deg", true, CultureInfo.InvariantCulture) ? AngleMode.Degrees : AngleMode.Radians; Console.WriteLine($"Angle mode: {_angleMode}"); continue; }

            if (!Ops.TryGetValue(cmd, out var op))
            { Console.WriteLine("Unknown command. Try 'help'"); continue; }

            try
            {
                var args = parts.Skip(1).Select(ParseNumber).ToArray();
                var result = op switch
                {
                    ITrigonometric trig => trig.Evaluate(args, _angleMode),
                    _ => op.Evaluate(args)
                };
                Console.WriteLine(result.ToString("G17", CultureInfo.InvariantCulture));
            }
            catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
        }
    }

    private static double ParseNumber(string s)
    {
        if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var d)) return d;
        throw new ArgumentException($"Not a number: '{s}'");
    }

    private static void PrintHelp()
    {
        Console.WriteLine("Commands: " + string.Join(", ", Ops.Keys.OrderBy(x => x)) +
                          "\nAlso: help, mode (deg|rad), exit");
    }
}

public enum AngleMode { Degrees, Radians }

public interface IOperation
{
    string Name { get; }
    string Help { get; }
    double Evaluate(params double[] args);
}
public interface ITrigonometric : IOperation
{
    double Evaluate(double[] args, AngleMode mode);
}

// src/Services/OperationFactory.cs
public static class OperationFactory
{
    public static Dictionary<string, IOperation> CreateAll() => new()
    {
        ["add"] = new Add(), ["sub"] = new Sub(), ["mul"] = new Mul(), ["div"] = new Div(),
        ["sqrt"] = new Sqrt(), ["pow"] = new Pow(), ["log10"] = new Log10(), ["ln"] = new Ln(),
        ["mod"] = new Mod(), ["idiv"] = new IDiv(), ["fact"] = new Fact(),
        ["sin"] = new Sin(), ["cos"] = new Cos(), ["tan"] = new Tan(),
    };
}

// Example ops (put each in its own file under /Services/Operations)
public sealed class Add : IOperation { public string Name=>"add"; public string Help=>"add x y [z ...]";
    public double Evaluate(params double[] a) => a.Length==0 ? 0 : a.Sum(); }

public sealed class Div : IOperation { public string Name=>"div"; public string Help=>"div a b";
    public double Evaluate(params double[] a)
    { if (a.Length<2) throw new ArgumentException("Need at least 2 args");
      return a.Skip(1).Aggregate(a[0], (acc, x) => x==0 ? throw new DivideByZeroException() : acc/x); } }

public sealed class Sin : ITrigonometric { public string Name=>"sin"; public string Help=>"sin [deg|rad] value";
    public double Evaluate(params double[] args) => throw new NotSupportedException("Use trig overload");
    public double Evaluate(double[] args, AngleMode m)
    { if (args.Length!=1) throw new ArgumentException("Need 1 arg");
      var x = args[0]; if (m==AngleMode.Degrees) x = x*Math.PI/180.0; return Math.Sin(x); } }

public sealed class Fact : IOperation { public string Name=>"fact"; public string Help=>"fact n";
    public double Evaluate(params double[] a)
    { if (a.Length!=1) throw new ArgumentException("Need 1 arg");
      var n = a[0]; if (n<0 || n!=Math.Truncate(n)) throw new ArgumentException("n must be non-negative integer");
      long r=1; for (int i=1; i<= (int)n; i++) r*=i; return r; } }
```

### Acceptance tests (samples)

* `add 2.5 1 -2 24.5125 0.33` → `26.3425`
* `div 7 2` → `3.5` ; `idiv 7 2` → `3`; `mod 7 2` → `1`
* `mode deg` → `Angle mode: Degrees`; `sin 30` → `0.5`
* `pow 2 8` → `256` ; `sqrt 9` → `3` ; `log10 1000` → `3` ; `ln 1` → `0`
* `fact 5` → `120` ; `fact -1` → friendly error.

---

## Option 2 — Manhattan Traffic Simulation (Console)

### Goal

Simulate traffic on a **Manhattan-like grid**: vehicles move between crossings; traffic lights alternate per axis; streets have capacities/flows; priority vehicles can pass on red; roadworks can close a road; end state prints each vehicle’s route/time and totals. The **final part** adds **shortest-path** routing (street travel times only) and optional reading from a config file. *(This mirrors the brief’s staged parts and concepts: vehicles, lights with flow/capacity/states, streets, crossroads, city grid, register, roadworks, final stats, and precomputed route planning.)*

### Core model (suggested)

* `Vehicle` (name/id, destination, priority, route history, travelTime).
* `TrafficLight` (state enum: Red/Yellow/Green; flow per minute; time per state; capacity; internal queue prioritising priority vehicles).
* `Street` (travelTime, capacity, vehicles with remaining time).
* `CrossRoad` (Name; four `TrafficLight`s + four `Street`s; `AddVehicle(side, vehicle)`, `Iterate()`: advance lights; move vehicles to opposite street respecting flow/yellow-half-flow; always serve priority vehicles first).
* `City` (grid of `CrossRoad`s; `AddVehicle(crossroadName, side, vehicle)`, `Iterate()`; allow random turns; roadworks to disable a street).
* `VehicleRegister` (lookup vehicle by name; track route/time).
* `TrafficFlowSimulation` (compose city + register; `Run(cycles)` or until all arrived; print totals; add/remove roadworks; precompute routes with **Dijkstra**).

### Behavioural notes

* Light timing (green durations per axis; yellow=1 min; red implied).
* Flow: X cars/min on green; **yellow is half flow (round down)**.
* Capacity: if full, cars queue upstream.
* Priority vehicles ignore red; still respect capacity constraints.

### OOP & Patterns

* Composition of components; `IClockTick`/`Iterate()` method across.
* **Strategy** (optional) for routing decisions (random vs shortest-path).
* **Repository** (optional) for reading config (if you load from file).

### Minimal skeleton (selected)

```csharp
public enum LightState { Red, Yellow, Green }
public enum Side { North, East, South, West }

public sealed class Vehicle
{
    public string Name { get; }
    public string Destination { get; private set; }
    public bool Priority { get; }
    public List<string> Route { get; } = new();
    public int TravelTime { get; private set; }
    public Vehicle(string name, string destination, bool priority=false)
    { Name = name; Destination = destination; Priority = priority; }
    public void AddHop(string cross) => Route.Add(cross);
    public void Tick(int minutes=1) => TravelTime += minutes;
    public override string ToString() => Priority
      ? $"Priority vehicle '{Name}' to {Destination}"
      : $"Vehicle '{Name}' to {Destination}";
}

public sealed class TrafficLight
{
    private readonly Queue<Vehicle> _q = new(); // extend to prioritise Priority vehicles
    public LightState State { get; private set; }
    private readonly Dictionary<LightState,int> _dur = new() {{LightState.Green,3},{LightState.Yellow,1},{LightState.Red,5}};
    private int _left = 3;
    public int FlowPerMinute { get; set; } = 30;
    public int Capacity { get; set; } = 50;

    public TrafficLight(LightState init = LightState.Red) { State = init; }
    public void SetTimeForState(LightState s, int minutes) => _dur[s]=minutes;

    public bool TryEnqueue(Vehicle v) { if (_q.Count >= Capacity) return false; _q.Enqueue(v); return true; }
    public IEnumerable<Vehicle> ReleaseThisMinute()
    {
        int allowed = State switch { LightState.Green => FlowPerMinute,
                                     LightState.Yellow => FlowPerMinute/2,
                                     _ => 0 };
        while (allowed-- > 0 && _q.Count>0) yield return _q.Dequeue();
    }

    public void Tick()
    {
        if (--_left > 0) return;
        State = State switch
        {
            LightState.Green => LightState.Yellow,
            LightState.Yellow => LightState.Red,
            LightState.Red => LightState.Green,
            _ => LightState.Red
        };
        _left = _dur[State];
    }
}

// CrossRoad, Street, City, VehicleRegister, and Dijkstra-based router follow similarly
```

### Shortest path (final part)

* Build a graph where nodes are crossroads; edges are streets with **weight = street travel time** (ignore lights waiting time).
* Pre-plan each vehicle’s route using **Dijkstra**; if roadworks close an edge, recompute affected routes (simple re-plan is fine for this scope).

### CLI (example)

* `add-vehicle <name> <startX,Y> <destX,Y> [priority]`
* `tick <minutes>` advances simulation; `status` prints crossings/queues; `roadworks <A>-<B> on|off`; `stats` prints per-vehicle routes & times + totals; `exit`.

### Acceptance tests (samples)

* **Crossing flow:** with `Green` flow=2/min and `Yellow`=1/min, after 3 minutes green + 1 minute yellow, expect 7 cars released (2+2+2+1).
* **Capacity:** when a light/street reaches capacity, further cars remain upstream.
* **Priority:** a priority vehicle at red still passes first minute.
* **Routing:** on a 2×2 grid with unit street times and no roadworks, shortest path from (0,0) to (1,1) has length 2 edges.
* **Roadworks:** closing one edge forces alternate equal-length path; if no path, vehicle remains queued.

---

## Option 3 — Cinema Application (Console)

### Goal

Model a cinema with **rooms, screens, shows** (title, start/end times, room, price), seat selection/booking, and daily schedules. The final part adds a **Marathon Ticket**: a full-day pass that automatically composes a **sequence of non-overlapping shows** (movie after movie without gaps/conflicts).

### Core model

* `Cinema` (name; rooms; shows; booking service).
* `Room` (Id/Name, capacity, seat map strategy: simple numbered seats).
* `Movie` (Title, Duration, Rating?).
* `Show` (Id, Movie, Room, Start, End, Price; remaining seats).
* `Booking` (Id, ShowId, Seats[], Buyer).
* `IMarathonPlanner` → returns an ordered list of shows that fit a user’s day.

### OOP & Patterns

* **Repository** for show storage (in-memory JSON store).
* **Strategy** for marathon planning (start with greedy by earliest finish; stretch: maximise total watch time).
* **Adapter (optional)** if you later import from a CSV schedule.

### Algorithms

* **Marathon ticket:** classic interval scheduling. Greedy algorithm:

  1. Sort all shows for the day by **end time**.
  2. Iteratively pick the next show whose `Start >= lastEnd`.
  3. Result is a maximal set of non-overlapping shows (often near-optimal for count).
     *(Stretch: Weighted by duration if maximising watch time — DP on intervals.)*

### Minimal skeleton

```csharp
public sealed record Movie(string Title, TimeSpan Duration);
public sealed record Room(string Name, int Capacity);

public sealed class Show
{
    public Guid Id { get; } = Guid.NewGuid();
    public Movie Movie { get; }
    public Room Room { get; }
    public DateTime Start { get; }
    public DateTime End => Start + Movie.Duration;
    public decimal Price { get; }
    private readonly HashSet<int> _taken = new();
    public Show(Movie m, Room r, DateTime start, decimal price)
    { Movie=m; Room=r; Start=start; Price=price; }
    public bool TryBook(params int[] seats)
    {
        if (seats.Any(s => s<1 || s>Room.Capacity)) return false;
        if (seats.Any(_taken.Contains)) return false;
        foreach (var s in seats) _taken.Add(s);
        return true;
    }
}

public interface IShowStore { IEnumerable<Show> All(); void Add(Show s); }
public sealed class JsonShowStore : IShowStore
{
    private readonly List<Show> _shows = new(); // persist later if you like
    public IEnumerable<Show> All() => _shows;
    public void Add(Show s) => _shows.Add(s);
}

public interface IMarathonPlanner { List<Show> Plan(DateOnly day, IEnumerable<Show> shows); }

public sealed class GreedyMarathonPlanner : IMarathonPlanner
{
    public List<Show> Plan(DateOnly day, IEnumerable<Show> shows)
    {
        var dayShows = shows.Where(s => DateOnly.FromDateTime(s.Start)==day)
                            .OrderBy(s => s.End).ToList();
        var plan = new List<Show>();
        DateTime current = DateTime.MinValue;
        foreach (var s in dayShows)
        {
            if (s.Start >= current) { plan.Add(s); current = s.End; }
        }
        return plan;
    }
}
```

### CLI (example)

* `add-room "Room A" 100`
* `add-movie "Inception" 2:28`
* `add-show "Inception" "Room A" 2025-11-01T10:00 9.99`
* `list-shows 2025-11-01`
* `book <showId> 12 13 14`
* `marathon 2025-11-01` → prints selected show IDs in order.

### Acceptance tests (samples)

* Two overlapping shows in the same time range → only one included in marathon plan.
* Sequential shows across **different rooms** are allowed (no time conflict).
* Booking the same seat twice fails; booking invalid seat index fails.
* Marathon on empty day returns empty plan.

---

## Git/GitHub issue set (starter list for any project)

1. Initialise repo, `.gitignore`, README; scaffold console app.
2. Design model classes; print basic menu.
3. Implement core commands (MVP).
4. Persistence layer (JSON/CSV).
5. Algorithm feature (calc ops / shortest path / marathon planner).
6. Error handling & validation pass.
7. CLI help & UX polish.
8. README usage examples + sample data.
9. Final tidy-up (dead code, comments), tag `v1.0`.

---

## Submission checklist (students)

* [ ] Compiles on .NET 8; no runtime crashes on normal usage.
* [ ] README with run steps and **3–5 example commands** (with expected output).
* [ ] ≥5 PRs merged with clear commit messages.
* [ ] At least one small pattern used sensibly (and noted in README).
* [ ] Short demo or screencast (≤5 min).

---
