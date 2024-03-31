using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public interface IMarkdownRenderer
{
    /// <summary>
    /// Binds renderer to its dependencies
    /// </summary>
    public void Initialize(VisualElement renderTarget, ViewsCollection viewsCollection, MarkdownContext outerContext = null);

    /// <summary>
    /// Renders current text to UI
    /// </summary>
    public void UpdateRender();

    /// <summary>
    /// Scrolls list view to specific element 
    /// </summary>
    /// <param name="targetName">Name of the element to scroll to</param>
    public void ScrollTo(string targetName);
}

/// <summary>
/// Object that renders passed text to specified UIDocument
/// </summary>
public class MarkdownRendererBase : IMarkdownRenderer
{
    private VisualElement _renderTarget;

    /// <summary>
    /// Visual element to render on
    /// </summary>
    public VisualElement RenderTarget
    {
        get
        {
            return _renderTarget;
        }
        set
        {
            _renderTarget = value;
            Dirty = true;
        }
    }

    /// <summary>
    /// List view which stores rendered markdown
    /// </summary>
    private ListView List { get; set; }

    /// <summary>
    /// Content part of the list view
    /// </summary>
    private VisualElement Content { get; set; }

    private ViewsCollection _viewsCollection;

    /// <summary>
    /// Collection of converters from markdown elements to unity ui elements
    /// </summary>
    /// <seealso cref="ViewsCollection"/>
    public ViewsCollection ViewsCollection
    {
        get
        {
            return _viewsCollection;
        }
        set
        {
            _viewsCollection = value;
            Dirty = true;
        }
    }

    private MarkdownContext _outerContext;

    /// <summary>
    /// Backing field for Text property
    /// </summary>
    private string _text = string.Empty;

    /// <summary>
    /// Markdown text to be rendered
    /// </summary>
    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;
            Dirty = true;
        }
    }

    /// <summary>
    /// Setting to track trivia in the text
    /// </summary>
    private bool _trackTrivia;

    public void Initialize(VisualElement renderTarget, ViewsCollection viewsCollection, MarkdownContext outerContext = null)
    {
        RenderTarget = renderTarget;
        ViewsCollection = viewsCollection;
        _outerContext = outerContext;

        _renderTarget.Clear();

        List = new ListView();
        List.name = "Scroll";
        List.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
        _renderTarget.Add(List);

        Content = new VisualElement();
        var children = new List<VisualElement> { Content };

        List.itemsSource = children;
        List.makeItem = () => new VisualElement();
        List.bindItem = (VisualElement element, int index) =>
        {
            element.Add(children[index]);
        };

        List.Rebuild();
        Initialized = true;
    }

    public void UpdateRender()
    {
        if (!Initialized)
        {
            return;
        }

        _viewsCollection.Views
            .SelectMany(v => v.StylesUsed())
            .Distinct()
            .ToList()
            .ForEach(v => _renderTarget.styleSheets.Add(v));

        Content?.Clear();

        Func<MarkdownObject, IMarkdownView> viewSelector = mdObject => _viewsCollection.Match(mdObject);

        var syntaxTree = Markdig.Markdown.Parse(_text, _trackTrivia);

        IMarkdownView initialView = viewSelector(syntaxTree);
        var context = _outerContext is null ? new MarkdownContext { Renderer = this } : _outerContext;
        initialView.Render(context, syntaxTree, Content, viewSelector);

        Dirty = false;
    }

    /// <summary>
    /// Is renderer initialized
    /// </summary>
    public bool Initialized { get; private set; } = false;

    /// <summary>
    /// Has renderer state changed and needs to be rendered again
    /// </summary>
    public bool Dirty { get; private set; } = false;

    public void ScrollTo(string targetName)
    {
        var target = _renderTarget.Q(targetName);

        if (target == null)
        {
            Debug.LogWarning("Target of the link is null, no scrolling");
            return;
        }

        List.ScrollTo(target);
    }
}
