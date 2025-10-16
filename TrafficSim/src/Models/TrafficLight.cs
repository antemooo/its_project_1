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
    
    /// <summary>
    /// Attempts to add a vehicle to the queue.
    /// Priority vehicles go to front, regular vehicles to back.
    /// Returns false if at capacity.
    /// </summary>
    public bool TryEnqueue(Vehicle vehicle)
    {
        if (_queue.Count >= Capacity)
            return false;
        
        // Priority vehicles jump to front of queue
        if (vehicle.Priority)
            _queue.AddFirst(vehicle);
        else
            _queue.AddLast(vehicle);
        
        return true;
    }
    
    /// <summary>
    /// Releases vehicles based on current light state.
    /// - Green: Full flow
    /// - Yellow: Half flow (rounded up)
    /// - Red: No flow (except we allow 0 for simplicity)
    /// </summary>
    public IEnumerable<Vehicle> ReleaseThisMinute()
    {
        int allowed = State switch
        {
            LightState.Green => FlowPerMinute,
            LightState.Yellow => Math.Max(1, FlowPerMinute / 2),
            _ => 0
        };
        
        while (allowed-- > 0 && _queue.Count > 0)
        {
            var vehicle = _queue.First!.Value;
            _queue.RemoveFirst();
            yield return vehicle;
        }
    }
    
    /// <summary>
    /// Advances light state based on time.
    /// Called once per simulation minute.
    /// </summary>
    public void Tick()
    {
        _timeLeft--;
        
        if (_timeLeft > 0)
            return;
        
        // Time expired - transition to next state
        State = State switch
        {
            LightState.Green => LightState.Yellow,
            LightState.Yellow => LightState.Red,
            LightState.Red => LightState.Green,
            _ => LightState.Red
        };
        
        _timeLeft = _stateDurations[State];
    }
    
    /// <summary>
    /// Current queue length
    /// </summary>
    public int QueueLength => _queue.Count;
}
