using Markdig.Syntax.Inlines;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

/// <summary>
/// Renderer for addressable objects passed through link inlines
/// </summary>
public abstract class AddressableRenderer : ScriptableObject, IStyleDependent, IMatchable<AsyncOperationHandle<object>>
{
    /// <summary>
    /// Whether the renderer accepts the result of addressable loading 
    /// meaning not only addressables, but outcomes like fails.
    /// So it is expected for handle to be passed after loading process was completed 
    /// and it gained status and, possibly, result
    /// </summary>
    /// <param name="handle">Handle to check</param>
    /// <returns>True or false</returns>
    public abstract bool Accepts(AsyncOperationHandle<object> handle);

    /// <summary>
    /// Render an addressable on a placeholder
    /// </summary>
    /// <param name="context">Used markdown context</param>
    /// <param name="canvas">Placeholder visual element</param>
    /// <param name="handle">Handle with loading result</param>
    /// <param name="inline">Source inline</param>
    public abstract void ProcessAddressable(MarkdownContext context, VisualElement canvas, AsyncOperationHandle<object> handle, LinkInline inline);

    /// <summary>
    /// Styles renderer depends on
    /// </summary>
    [field: SerializeField]
    public List<StyleSheet> Styles { get; private set; }

    public virtual IEnumerable<StyleSheet> StylesUsed() => Styles;
}
