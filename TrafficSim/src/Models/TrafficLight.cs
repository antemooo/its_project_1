using System;
using System.Collections.Generic;
using System.Linq;

namespace TrafficSim.Models;

/// <summary>
/// Traffic light with queue management and state transitions.
/// Demonstrates:
/// - STATE PATTERN: Light transitions between states
/// - QUEUE DATA STRUCTURE: First-in-first-out with priority
/// - TIME-BASED SIMULATION: Tick-based state changes
/// </summary>
public sealed class TrafficLight
{
    // Queue uses LinkedList to allow priority vehicles to jump to front
    private readonly LinkedList<Vehicle> _queue = new();
    
    /// <summary>
    /// Current light state (Red, Yellow, or Green)
    /// </summary>
    public LightState State { get; private set; }
    
    // How long each state lasts (in minutes)
    private readonly Dictionary<LightState, int> _stateDurations = new()
    {
        { LightState.Green, 3 },
        { LightState.Yellow, 1 },
        { LightState.Red, 5 }
    };
    
    // Time remaining in current state
    private int _timeLeft = 3;
    
    /// <summary>
    /// How many vehicles can pass per minute on green
    /// </summary>
    public int FlowPerMinute { get; set; } = 2;
    
    /// <summary>
    /// Maximum vehicles that can wait at this light
    /// </summary>
    public int Capacity { get; set; } = 50;
    
    public TrafficLight(LightState initialState = LightState.Red)
    {
        State = initialState;
        _timeLeft = _stateDurations[initialState];
    }
    
    /// <summary>
    /// Configures how long a state lasts.
    /// </summary>
    public void SetStateDuration(LightState state, int minutes)
    {
        _stateDurations[state] = minutes;
    }
    
    //==========================================================================
    // STUDENT TODO: Implement TryEnqueue - add vehicle to queue with priority handling
    // ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 20 minutes
    // STEPS: Check capacity, priority vehicles → AddFirst(), regular → AddLast()
    //==========================================================================
    /// <summary>
    /// Attempts to add a vehicle to the queue.
    /// Priority vehicles go to front, regular vehicles to back.
    /// Returns false if at capacity.
    /// </summary>
    public bool TryEnqueue(Vehicle vehicle)
    {
        throw new NotImplementedException("TODO: Check capacity, add priority vehicles to front, regular to back");
    }
    
    //==========================================================================
    // STUDENT TODO: Implement ReleaseThisMinute - release vehicles based on light state
    // ⭐⭐⭐ DIFFICULTY: Medium-Hard | ⏱️ TIME: 30 minutes
    // Use yield return pattern, calculate allowed count from State (Green/Yellow/Red)
    //==========================================================================
    /// <summary>
    /// Releases vehicles based on current light state.
    /// - Green: Full flow
    /// - Yellow: Half flow (rounded up)
    /// - Red: No flow (except we allow 0 for simplicity)
    /// </summary>
    public IEnumerable<Vehicle> ReleaseThisMinute()
    {
        throw new NotImplementedException("TODO: Calculate allowed count, dequeue and yield vehicles");
    }
    
    //==========================================================================
    // STUDENT TODO: Implement Tick - advance state machine
    // ⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 25 minutes
    // Decrement _timeLeft, when 0 → transition state (Green→Yellow→Red→Green), reset timer
    //==========================================================================
    /// <summary>
    /// Advances light state based on time.
    /// Called once per simulation minute.
    /// </summary>
    public void Tick()
    {
        throw new NotImplementedException("TODO: Decrement timer, transition state when expired");
    }
    
    /// <summary>
    /// Current queue length
    /// </summary>
    public int QueueLength => _queue.Count;
}
