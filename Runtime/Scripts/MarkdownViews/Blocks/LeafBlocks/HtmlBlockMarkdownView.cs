using Markdig.Syntax;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes html blocks
/// </summary>
[CreateAssetMenu(fileName = "HtmlBlock", menuName = "ScriptableObjects/MarkdownView/LeafBlocks/HtmlBlock")]
public class HtmlBlockMarkdownView : LeafBlockMarkdownView<HtmlBlock>
{
    protected override void RenderTyped(MarkdownContext context, LeafBlock mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector)
    {
        var text = mdObject.ToHtml();
        layout.Add(new Label(text));
    }

    protected override void RenderTyped(MarkdownContext context, HtmlBlock mdObject, VisualElement layout,
        Func<MarkdownObject, IMarkdownView> viewSelector)
    { }
}
