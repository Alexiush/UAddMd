using Markdig.Syntax;
using System;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes leaf blocks
/// </summary>
public abstract class LeafBlockMarkdownView<T> : MarkdownView<LeafBlock> where T : LeafBlock
{
    public override bool Accepts(MarkdownObject mdObject) => typeof(T).IsInstanceOfType(mdObject);

    protected override void RenderTyped(MarkdownContext context, LeafBlock mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector)
    {
        RenderTyped(context, mdObject as T, layout, viewSelector);

        var inline = mdObject.Inline;

        if (inline != null)
        {
            var view = viewSelector(inline);
            view.Render(context, inline, layout, viewSelector);
        }
    }

    protected abstract void RenderTyped(MarkdownContext context, T mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector);
}
