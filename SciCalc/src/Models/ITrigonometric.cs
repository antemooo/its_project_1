namespace SciCalc.Models;

/// <summary>
/// Extended interface for trigonometric operations that need angle mode.
/// Inherits from IOperation and adds angle-aware evaluation.
/// This demonstrates INTERFACE INHERITANCE in C#.
/// </summary>
public interface ITrigonometric : IOperation
{
    /// <summary>
    /// Evaluates the trigonometric function with angle mode consideration.
    /// </summary>
    /// <param name="args">The angle value(s)</param>
    /// <param name="mode">Whether to interpret angles as degrees or radians</param>
    /// <returns>The result of the trigonometric calculation</returns>
    double Evaluate(double[] args, AngleMode mode);
}
