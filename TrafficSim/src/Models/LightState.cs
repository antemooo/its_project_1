namespace TrafficSim.Models;

/// <summary>
/// Traffic light states in the simulation.
/// Lights cycle through: Green → Yellow → Red → Green
/// 
/// Real-world traffic light behavior:
/// - Green: Vehicles can pass
/// - Yellow: Transition state, some vehicles can still pass
/// - Red: Vehicles must stop
/// </summary>
public enum LightState
{
    /// <summary>
    /// Stop - no vehicles pass (except priority vehicles)
    /// </summary>
    Red,
    
    /// <summary>
    /// Caution - reduced flow (half of green flow)
    /// </summary>
    Yellow,
    
    /// <summary>
    /// Go - full flow of vehicles
    /// </summary>
    Green
}
