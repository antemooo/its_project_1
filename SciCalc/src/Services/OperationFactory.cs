using System.Collections.Generic;
using SciCalc.Models;
using SciCalc.Services.Operations;

namespace SciCalc.Services;

/// <summary>
/// Factory class that creates and registers all calculator operations.
/// This is the FACTORY PATTERN - centralizes object creation.
/// 
/// Benefits:
/// - Single place to add new operations
/// - Easy to see all available operations
/// - Separates operation creation from usage
/// </summary>
public static class OperationFactory
{
    /// <summary>
    /// Creates and returns a dictionary of all available operations.
    /// The key is the command name, the value is the operation object.
    /// </summary>
    /// <returns>Dictionary mapping command names to operation instances</returns>
    public static Dictionary<string, IOperation> CreateAll()
    {
        // Dictionary initializer syntax - creates and populates dictionary in one go
        return new Dictionary<string, IOperation>
        {
            // Basic arithmetic operations
            ["add"] = new Add(),
            ["sub"] = new Sub(),
            ["mul"] = new Mul(),
            ["div"] = new Div(),
            
            // Advanced mathematical operations
            ["sqrt"] = new Sqrt(),
            ["pow"] = new Pow(),
            ["log10"] = new Log10(),
            ["ln"] = new Ln(),
            ["mod"] = new Mod(),
            ["idiv"] = new IDiv(),
            ["fact"] = new Fact(),
            
            // Trigonometric operations
            ["sin"] = new Sin(),
            ["cos"] = new Cos(),
            ["tan"] = new Tan(),
        };
    }
}
