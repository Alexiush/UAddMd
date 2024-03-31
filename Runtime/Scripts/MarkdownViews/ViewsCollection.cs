using Markdig.Syntax;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object that stores a custom set of MarkdownViews to use
/// </summary>
[CreateAssetMenu(fileName = "ViewsCollection", menuName = "ScriptableObjects/MarkdownView/General/ViewsCollection")]
public class ViewsCollection : ScriptableObject, IMatchCollection<MarkdownView, MarkdownObject>
{
    /// <summary>
    /// List of used views
    /// </summary>
    [field: SerializeField]
    public List<MarkdownView> Views { get; private set; }

    /// <summary>
    /// Default view that is used as a fallback when there is no MarkdownView that accepts the passed element
    /// </summary>
    [field: SerializeField]
    public MarkdownView DefaultView { private set; get; }

    public IEnumerable<MarkdownView> Matchables => Views;

    public MarkdownView DefaultMatchable => DefaultView;
}
