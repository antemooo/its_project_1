using System;
using System.Collections.Generic;
using System.Linq;
using CinemaApp.Models;

namespace CinemaApp.Services;

/// <summary>
/// In-memory implementation of IShowStore.
/// Stores shows in a List - data is lost when application closes.
/// 
/// For persistence, you could create:
/// - JsonShowStore (saves to JSON file)
/// - CsvShowStore (saves to CSV file)
/// - DatabaseShowStore (saves to database)
/// 
/// This demonstrates the REPOSITORY PATTERN's flexibility.
/// </summary>
public sealed class InMemoryShowStore : IShowStore
{
    // Private list holds all shows - only accessible through interface methods
    // This is ENCAPSULATION - internal data is hidden
    private readonly List<Show> _shows = new();
    
    /// <summary>
    /// Returns all shows. Returns IEnumerable to prevent external modification.
    /// </summary>
    public IEnumerable<Show> All() => _shows;
    
    /// <summary>
    /// Adds a show to the collection.
    /// In a real application, this might validate for conflicts.
    /// </summary>
    public void Add(Show show)
    {
        _shows.Add(show);
    }
    
    /// <summary>
    /// Finds a show by ID using LINQ.
    /// FirstOrDefault returns null if no match found.
    /// </summary>
    public Show? Find(Guid id)
    {
        return _shows.FirstOrDefault(show => show.Id == id);
    }
}
