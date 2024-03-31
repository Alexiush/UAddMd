using Markdig.Syntax;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Interface for MarkdownView - converter from markdown text to Unity.UIElement
/// </summary>
public interface IMarkdownView
{
    /// <summary>
    /// Add UIElement based on the markdown object
    /// </summary>
    /// <param name="context">Context that contains dependencies used by renderer</param>
    /// <param name="mdObject">Markdown object to process</param>
    /// <param name="layout">Visual element to use for processing</param>
    /// <param name="viewSelector">Method to select processor for markdown object's children</param>
    public void Render(MarkdownContext context, MarkdownObject mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector);
}

/// <summary>
/// Abstract class for converter from markdown text to Unity.UIElement
/// </summary>
/// <seealso cref="IMarkdownView"/>
/// <seealso cref="IStyleDependent"/>
public abstract class MarkdownView : ScriptableObject, IMarkdownView, IStyleDependent, IMatchable<MarkdownObject>
{
    /// <summary>
    /// Whether this view can process the passed markdown object
    /// </summary>
    /// <param name="mdObject">Markdown object that needs to be processed</param>
    /// <returns>True or false</returns>
    public abstract bool Accepts(MarkdownObject mdObject);

    /// <summary>
    /// List of classes that resulting visual element should be member of.
    /// Intended to use with Styles property.
    /// </summary>
    [field: SerializeField]
    public List<string> Classes { get; private set; }

    /// <summary>
    /// List of styles that MarkdownView uses/depends on
    /// </summary>
    [field: SerializeField]
    public List<StyleSheet> Styles { get; private set; }

    public abstract void Render(MarkdownContext context, MarkdownObject mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector);
    public virtual IEnumerable<StyleSheet> StylesUsed() => Styles;
}

/// <summary>
/// Typed version of MarkdownView.
/// Works very well when there is one to one view to markdown object mapping.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="MarkdownView"/>
public abstract class MarkdownView<T> : MarkdownView where T : MarkdownObject
{
    public override bool Accepts(MarkdownObject mdObject) => typeof(T).IsInstanceOfType(mdObject);

    /// <summary>
    /// Typed version of Render method. 
    /// </summary>
    /// <param name="context">Context that contains dependencies used by renderer</param>
    /// <param name="mdObject">Markdown object to process, with explicit type</param>
    /// <param name="layout">Visual element to use for processing</param>
    /// <param name="viewSelector">Method to select processor for markdown object's children</param>
    protected abstract void RenderTyped(MarkdownContext context, T mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector);
    public override void Render(MarkdownContext context, MarkdownObject mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector)
    {
        Classes.ForEach(c => layout.AddToClassList(c));
        RenderTyped(context, mdObject as T, layout, viewSelector);
    }
}
