using System.Collections.Generic;
using UnityEngine.UIElements;

/// <summary>
/// Interface for markdown object rendering pipeline elements that depend on styles
/// </summary>
public interface IStyleDependent
{
    /// <summary>
    /// Styles that element depends on
    /// </summary>
    /// <returns>IEnumerable of styles to be used</returns>
    public IEnumerable<StyleSheet> StylesUsed();
}
