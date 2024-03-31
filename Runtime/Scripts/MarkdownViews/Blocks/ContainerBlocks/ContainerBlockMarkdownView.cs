using Markdig.Syntax;
using System;
using System.Linq;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes container blocks
/// </summary>
public abstract class ContainerBlockMarkdownView<T> : MarkdownView<ContainerBlock> where T : ContainerBlock
{
    public override bool Accepts(MarkdownObject mdObject) => typeof(T).IsInstanceOfType(mdObject);

    protected override void RenderTyped(MarkdownContext context, ContainerBlock mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector)
    {
        RenderTyped(context, mdObject as T, layout, viewSelector);

        var children = mdObject.ToList();
        var container = new VisualElement();

        children.ForEach(c =>
        {
            var subcontainer = new VisualElement();
            container.Add(subcontainer);

            var view = viewSelector(c);
            view.Render(context, c, subcontainer, viewSelector);
        });

        layout.Add(container);
    }

    protected abstract void RenderTyped(MarkdownContext context, T mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector);
}
