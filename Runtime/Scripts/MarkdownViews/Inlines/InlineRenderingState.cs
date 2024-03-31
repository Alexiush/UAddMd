using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

/// <summary>
/// Pair of string embracing the inline content
/// </summary>
public record OpeningClosing
{
    public string Opening;
    public string Closing;
}

/// <summary>
/// Represents the current state of text accumulated by InlineRenderingState
/// </summary>
public class InlineRenderingDraft
{
    private string _text;

    /// <summary>
    /// Accumulated text
    /// </summary>
    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            IsEmpty = false;
            _text = value;
        }
    }

    /// <summary>
    /// Functions to apply on final result (like registering callbacks)
    /// </summary>
    public List<Func<VisualElement, VisualElement>> PostProcessors = new List<Func<VisualElement, VisualElement>>();

    /// <summary>
    /// Accumulated pairs of prefixes and suffixes 
    /// </summary>
    /// <seealso cref="OpeningClosing"/>
    public Stack<OpeningClosing> OpeningsAndClosings = new Stack<OpeningClosing>();

    /// <summary>
    /// Was text just reset and considered empty (may contain openings without closings)
    /// </summary>
    public bool IsEmpty { get; private set; } = true;

    public void ResetText()
    {
        Text = string.Join("", OpeningsAndClosings
            .Select(p => p.Opening));
        IsEmpty = true;
    }

    public void ResetPostProcessors() => PostProcessors.Clear();
    public void ResetOpeningsAndClosings() => OpeningsAndClosings.Clear();

    /// <summary>
    /// Builds visual element from accumulated text
    /// </summary>
    /// <returns>Visual element that contains accumulated tags</returns>
    public VisualElement Build()
    {
        VisualElement element = new Label(Text);

        var processedElement = PostProcessors.Aggregate(
            element,
            (e, postProcessor) => postProcessor(e)
        );

        return processedElement;
    }
}

/// <summary>
/// Current state of processed inlines.
/// Inlines usually represent things like styles of the part of the text, thus they do not represent any particular UIElement by themselves.
/// So inlines modify not the dedicated visual elements, but their shared state. 
/// </summary>
public class InlineRenderingState
{
    public InlineRenderingState(MarkdownContext context)
    {
        MarkdownContext = context;
    }

    public MarkdownContext MarkdownContext { get; private set; }

    /// <summary>
    /// Current draft
    /// </summary>
    /// <seealso cref="InlineRenderingDraft"/>
    public InlineRenderingDraft Draft { get; set; } = new InlineRenderingDraft();

    /// <summary>
    /// Finalizes current draft and pushes it to the list of elements to render
    /// </summary>
    public void PushDraft()
    {
        if (Draft.IsEmpty)
        {
            return;
        }

        RenderedElements.Add(Draft.Build());
    }

    /// <summary>
    /// Visual elements to be rendered on layout dedicated to root container inline
    /// </summary>
    public List<VisualElement> RenderedElements { get; set; } = new List<VisualElement>();

    /// <summary>
    /// Combines processed data into final visual element
    /// </summary>
    /// <returns></returns>
    public VisualElement Collect()
    {
        var container = new VisualElement();
        RenderedElements.ForEach(e => container.Add(e));

        return container;
    }
}