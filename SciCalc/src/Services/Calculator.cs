using System;
using System.Globalization;
using System.Linq;
using SciCalc.Models;

namespace SciCalc.Services;

/// <summary>
/// Main calculator engine that processes user input and executes operations.
/// This class demonstrates:
/// - ENCAPSULATION: Internal state (_angleMode) is private
/// - COMPOSITION: Uses OperationFactory to manage operations
/// - SINGLE RESPONSIBILITY: Only handles user interaction and operation dispatch
/// </summary>
public static class Calculator
{
    // Dictionary holds all available operations - created once using the Factory pattern
    private static readonly Dictionary<string, IOperation> Ops = OperationFactory.CreateAll();
    
    // Current angle mode setting (can be changed by user with 'mode' command)
    private static AngleMode _angleMode = AngleMode.Radians;

    /// <summary>
    /// Main calculator loop - reads commands and executes them until user types 'exit'
    /// </summary>
    public static void Run()
    {
        Console.WriteLine("SciCalc — type 'help' for commands or 'exit' to quit");
        Console.WriteLine();
        
        // Infinite loop - only exits when user types 'exit'
        for (;;)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line)) 
                continue;
            
            // Exit command
            if (line.Equals("exit", StringComparison.OrdinalIgnoreCase)) 
                return;

            // Split input into parts: command and arguments
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var cmd = parts[0].ToLowerInvariant();

            // Handle special commands
            if (cmd == "help") 
            { 
                PrintHelp(); 
                continue; 
            }
            
            if (cmd == "mode" && parts.Length == 2)
            {
                // Change angle mode between degrees and radians
                _angleMode = parts[1].StartsWith("deg", true, CultureInfo.InvariantCulture) 
                    ? AngleMode.Degrees 
                    : AngleMode.Radians;
                Console.WriteLine($"Angle mode: {_angleMode}");
                continue;
            }

            // Check if command exists
            if (!Ops.TryGetValue(cmd, out var op))
            {
                Console.WriteLine("Unknown command. Try 'help'");
                continue;
            }

            // Execute the operation
            try
            {
                // Parse all arguments after the command
                var args = parts.Skip(1).Select(ParseNumber).ToArray();
                
                // Use pattern matching to check if operation needs angle mode
                double result = op switch
                {
                    ITrigonometric trig => trig.Evaluate(args, _angleMode),
                    _ => op.Evaluate(args)
                };
                
                // Print result with high precision (G17 format shows up to 17 digits)
                Console.WriteLine(result.ToString("G17", CultureInfo.InvariantCulture));
            }
            catch (Exception ex) 
            { 
                // Catch and display any errors (division by zero, invalid input, etc.)
                Console.WriteLine($"Error: {ex.Message}"); 
            }
        }
    }

    /// <summary>
    /// Parses a string to a double number.
    /// Uses InvariantCulture to ensure '.' is always the decimal separator.
    /// </summary>
    private static double ParseNumber(string s)
    {
        if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var d))
            return d;
        throw new ArgumentException($"Not a number: '{s}'");
    }

    /// <summary>
    /// Displays all available commands and help information.
    /// </summary>
    private static void PrintHelp()
    {
        Console.WriteLine("\n=== Available Operations ===");
        
        // Group operations by category for better readability
        var basic = new[] { "add", "sub", "mul", "div" };
        var trig = new[] { "sin", "cos", "tan" };
        var advanced = new[] { "sqrt", "pow", "log10", "ln", "mod", "idiv", "fact" };
        
        Console.WriteLine("\nBasic Arithmetic:");
        foreach (var name in basic)
            if (Ops.TryGetValue(name, out var op))
                Console.WriteLine($"  {op.Name,-10} : {op.Help}");
        
        Console.WriteLine("\nTrigonometric (respects angle mode):");
        foreach (var name in trig)
            if (Ops.TryGetValue(name, out var op))
                Console.WriteLine($"  {op.Name,-10} : {op.Help}");
        
        Console.WriteLine("\nAdvanced Operations:");
        foreach (var name in advanced)
            if (Ops.TryGetValue(name, out var op))
                Console.WriteLine($"  {op.Name,-10} : {op.Help}");
        
        Console.WriteLine("\n=== System Commands ===");
        Console.WriteLine("  mode       : mode deg|rad (change angle unit)");
        Console.WriteLine("  help       : Show this message");
        Console.WriteLine("  exit       : Quit calculator");
        
        Console.WriteLine("\n=== Examples ===");
        Console.WriteLine("  add 2.5 1 -2 24.5125 0.33");
        Console.WriteLine("  mode deg");
        Console.WriteLine("  sin 30");
        Console.WriteLine("  pow 2 8");
        Console.WriteLine();
    }
}
