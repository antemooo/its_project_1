namespace SciCalc.Models;

/// <summary>
/// Base interface for all calculator operations.
/// Each operation (add, subtract, sin, etc.) implements this interface.
/// This is an example of the STRATEGY PATTERN - each operation is a different strategy.
/// </summary>
public interface IOperation
{
    /// <summary>
    /// The command name used to invoke this operation (e.g., "add", "sin")
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Help text showing how to use this operation
    /// </summary>
    string Help { get; }
    
    /// <summary>
    /// Executes the operation with the given arguments.
    /// Uses 'params' keyword to accept variable number of arguments.
    /// </summary>
    /// <param name="args">The numeric arguments for the operation</param>
    /// <returns>The result of the calculation</returns>
    double Evaluate(params double[] args);
}
