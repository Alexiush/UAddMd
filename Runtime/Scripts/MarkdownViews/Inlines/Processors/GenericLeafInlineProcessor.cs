using Markdig.Syntax.Inlines;
using System;

/// <summary>
/// Generic implementation for leaf inline processors
/// </summary>
/// <typeparam name="T">Type of leaf inline</typeparam>
public abstract class GenericLeafInlineProcessor<T> : InlineProcessor<LeafInline> where T : LeafInline
{
    public override bool Accepts(Inline inline) => typeof(T).IsInstanceOfType(inline);

    public override void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, LeafInline inline) =>
        ProcessTyped(renderingState, processorSelector, inline as T);

    /// <summary>
    /// Typed version of Process
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="processorSelector">Function to select processor for children inlines</param>
    /// <param name="inline">Inline to process</param>
    public abstract void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, T inline);

    public override void ProcessOpeningTyped(InlineRenderingState renderingState, LeafInline inline) => ProcessOpeningTyped(renderingState, inline as T);

    /// <summary>
    /// Typed version of ProcessOpening
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="inline">Inline to process</param>
    public virtual void ProcessOpeningTyped(InlineRenderingState renderingState, T inline) { }

    public override void ProcessClosingTyped(InlineRenderingState renderingState, LeafInline inline) => ProcessClosingTyped(renderingState, inline as T);

    /// <summary>
    /// Typed version of ProcessClosing
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="inline">Inline to process</param>
    public virtual void ProcessClosingTyped(InlineRenderingState renderingState, T inline) { }
}
