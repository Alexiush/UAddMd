using Markdig.Syntax.Inlines;
using System;

/// <summary>
/// Generic implementation for container inline processors
/// </summary>
/// <typeparam name="T">Type of container inline</typeparam>
public abstract class GenericContainerInlineProcessor<T> : InlineProcessor<ContainerInline> where T : ContainerInline
{
    public override bool Accepts(Inline inline) => typeof(T).IsInstanceOfType(inline);

    /// <summary>
    /// Typed version of Process
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="processorSelector">Function to select processor for children inlines</param>
    /// <param name="inline">Inline to process</param>
    public virtual void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, T inline) =>
        ProcessTyped(renderingState, processorSelector, inline as ContainerInline);

    public override void ProcessOpeningTyped(InlineRenderingState renderingState, ContainerInline inline) => ProcessOpeningTyped(renderingState, inline as T);

    /// <summary>
    /// Typed version of ProcessOpening
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="inline">Inline to process</param>
    public abstract void ProcessOpeningTyped(InlineRenderingState renderingState, T inline);

    public override void ProcessClosingTyped(InlineRenderingState renderingState, ContainerInline inline) => ProcessClosingTyped(renderingState, inline as T);

    /// <summary>
    /// Typed version of ProcessClosing
    /// </summary>
    /// <param name="renderingState">Inline rendering state</param>
    /// <param name="inline">Inline to process</param>
    public virtual void ProcessClosingTyped(InlineRenderingState renderingState, T inline)
    {
        var context = renderingState.Draft;

        if (context.OpeningsAndClosings.Count == 0)
        {
            return;
        }

        context.Text += context.OpeningsAndClosings.Pop().Closing;
    }
}
