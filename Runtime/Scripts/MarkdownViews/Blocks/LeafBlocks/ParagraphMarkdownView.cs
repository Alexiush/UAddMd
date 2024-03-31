using Markdig.Syntax;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes paragraphs
/// </summary>
[CreateAssetMenu(fileName = "Paragraph", menuName = "ScriptableObjects/MarkdownView/LeafBlocks/Paragraph")]
public class ParagraphMarkdownView : LeafBlockMarkdownView<ParagraphBlock>
{
    protected override void RenderTyped(MarkdownContext context, ParagraphBlock mdObject, VisualElement layout,
        Func<MarkdownObject, IMarkdownView> viewSelector)
    { }
}
