namespace SciCalc.Models;

/// <summary>
/// Represents the angle measurement mode for trigonometric operations.
/// Degrees: Common unit (360° in a circle)
/// Radians: Mathematical unit (2π radians in a circle)
/// </summary>
public enum AngleMode
{
    /// <summary>
    /// Angle measurement in degrees (0-360°)
    /// </summary>
    Degrees,
    
    /// <summary>
    /// Angle measurement in radians (0-2π)
    /// Default mode for mathematical calculations
    /// </summary>
    Radians
}
