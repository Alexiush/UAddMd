using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Interface for inline processors - converting inline objects into Unity UIElements
/// </summary>
public interface IInlineProcessor
{
    /// <summary>
    /// Whether processor accepts inline or not
    /// </summary>
    /// <param name="inline">inline to process</param>
    /// <returns>True or false</returns>
    public bool Accepts(Inline inline);

    /// <summary>
    /// Process inline
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="processorSelector">Function to select processor for children inlines</param>
    /// <param name="inline">Inline to process</param>
    public void Process(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, Inline inline);

    /// <summary>
    /// Method to use before processing the children
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="inline">Inline to process</param>
    public void ProcessOpening(InlineRenderingState renderingState, Inline inline);

    /// <summary>
    /// Method to use after processing the children
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="inline">Inline to process</param>
    public void ProcessClosing(InlineRenderingState renderingState, Inline inline);
}

/// <summary>
/// Analog to MarkdownView for inline processing
/// </summary>
/// <seealso cref="MarkdownView"/>
public abstract class InlineProcessor : ScriptableObject, IStyleDependent, IInlineProcessor, IMatchable<Inline>
{
    public abstract bool Accepts(Inline inline);
    public virtual void Process(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, Inline inline)
    {
        ProcessOpening(renderingState, inline);

        var children = inline.DirectDescendants<Inline>()
            .ToList();

        children.ForEach(c =>
        {
            var processor = processorSelector(c);
            processor.Process(renderingState, processorSelector, c);
        });

        ProcessClosing(renderingState, inline);
    }

    public abstract void ProcessOpening(InlineRenderingState renderingState, Inline inline);
    public abstract void ProcessClosing(InlineRenderingState renderingState, Inline inline);

    /// <summary>
    /// List of styles processor depends on
    /// </summary>
    [field: SerializeField]
    public List<StyleSheet> Styles { get; private set; }

    public virtual IEnumerable<StyleSheet> StylesUsed() => Styles;
}

/// <summary>
/// Typed version of inline processor
/// </summary>
/// <typeparam name="T">Inline to process</typeparam>
/// <seealso cref="InlineProcessor"/>
public abstract class InlineProcessor<T> : InlineProcessor where T : Inline
{
    public override bool Accepts(Inline inline) => typeof(T).IsInstanceOfType(inline);

    /// <summary>
    /// Typed version of Process
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="processorSelector">Function to select processor for children inlines</param>
    /// <param name="inline">Inline to process</param>
    public virtual void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, T inline)
    {
        ProcessOpeningTyped(renderingState, inline);

        var children = inline.DirectDescendants<Inline>()
            .ToList();

        children.ForEach(c =>
        {
            var processor = processorSelector(c);
            processor.Process(renderingState, processorSelector, c);
        });

        ProcessClosingTyped(renderingState, inline);
    }

    public override void Process(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, Inline inline) =>
        ProcessTyped(renderingState, processorSelector, inline as T);

    /// <summary>
    /// Typed version of ProcessOpening
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="inline">Inline to process</param>
    public abstract void ProcessOpeningTyped(InlineRenderingState renderingState, T inline);
    public override void ProcessOpening(InlineRenderingState renderingState, Inline inline) =>
        ProcessOpening(renderingState, inline as T);

    /// <summary>
    /// Typed version of ProcessClosing
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="inline">Inline to process</param>
    public abstract void ProcessClosingTyped(InlineRenderingState renderingState, T inline);
    public override void ProcessClosing(InlineRenderingState renderingState, Inline inline) =>
        ProcessClosing(renderingState, inline as T);
}
