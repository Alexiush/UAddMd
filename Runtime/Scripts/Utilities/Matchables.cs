using System.Collections.Generic;
using System.Linq;

public interface IMatchable<T>
{
    public bool Accepts(T obj);
}

public interface IMatchCollection<O, T> where O : IMatchable<T>
{
    public IEnumerable<O> Matchables { get; }
    public O DefaultMatchable { get; }
}

public static class MatchableUtilities
{
    public static O Match<O, T>(this IMatchCollection<O, T> collection, T obj) where O : IMatchable<T> => collection.Matchables
        .Where(m => m.Accepts(obj))
        .DefaultIfEmpty(collection.DefaultMatchable)
        .FirstOrDefault();
}
