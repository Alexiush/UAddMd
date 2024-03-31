using Markdig.Syntax;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes quote blocks
/// </summary>
[CreateAssetMenu(fileName = "QuoteBlock", menuName = "ScriptableObjects/MarkdownView/ContainerBlocks/QuoteBlock")]
public class QuoteBlockMarkdownView : ContainerBlockMarkdownView<QuoteBlock>
{
    protected override void RenderTyped(MarkdownContext context, QuoteBlock mdObject, VisualElement layout,
        Func<MarkdownObject, IMarkdownView> viewSelector)
    { }
}
