using System;
using System.Collections.Generic;
using System.Linq;

//==============================================================================
// FILE: Show.cs
// ⭐⭐⭐ DIFFICULTY: Medium | ⏱️ TIME: 45-60 minutes
// 🧪 TEST: See skeleton-examples/Show.cs for complete reference
//==============================================================================

namespace CinemaApp.Models;

/// <summary>
/// Represents a specific showing of a movie in a room at a particular time.
/// This class demonstrates:
/// - ENCAPSULATION: Private _taken set, public read-only properties
/// - DATA VALIDATION: TryBook checks seat validity and availability
/// - BUSINESS LOGIC: Manages seat booking without external dependencies
/// </summary>
public sealed class Show
{
    //==========================================================================
    // STUDENT TODO: Implement Show class with seat booking logic
    //
    // PROPERTIES TO IMPLEMENT:
    // - public Guid Id { get; } = Guid.NewGuid();
    // - public Movie Movie { get; }
    // - public Room Room { get; }
    // - public DateTime Start { get; }
    // - public DateTime End => Start + Movie.Duration;  // Calculated property!
    // - public decimal Price { get; }
    // - private readonly HashSet<int> _taken = new();  // For fast lookup
    // - public int AvailableSeats => Room.Capacity - _taken.Count;
    //
    // CONSTRUCTOR:
    // - public Show(Movie movie, Room room, DateTime start, decimal price)
    // - Assign all properties
    //
    // TRYBBOK METHOD: (Returns true if booking succeeds)
    // - public bool TryBook(params int[] seats)
    // - Validation steps:
    //   1. Check seats.Length > 0 (return false if empty)
    //   2. Check all seats are in range [1, Room.Capacity] (use Any + lambda)
    //   3. Check no seat is already in _taken set (use Any + Contains)
    //   4. If all valid: Add each seat to _taken, return true
    //
    // TOSTRING:
    // - Format: "[{Id:N}] {Movie.Title} | {Room.Name} | {Start:yyyy-MM-dd HH:mm} - {End:HH:mm} | ${Price:F2} | Seats: {AvailableSeats}/{Room.Capacity} available"
    //
    // HINTS:
    // - HashSet<int> provides O(1) Contains operation
    // - Use LINQ: seats.Any(s => s < 1 || s > Room.Capacity)
    // - See skeleton-examples/Show.cs for complete implementation example
    //==========================================================================
    
    public Show(Movie movie, Room room, DateTime start, decimal price)
    {
        throw new NotImplementedException("TODO: Initialize Show properties");
    }
    
    public bool TryBook(params int[] seats)
    {
        throw new NotImplementedException("TODO: Implement seat booking with validation");
    }
}
