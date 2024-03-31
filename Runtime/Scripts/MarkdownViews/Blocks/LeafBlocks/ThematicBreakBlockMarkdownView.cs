using Markdig.Syntax;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes thematic break blocks
/// </summary>
[CreateAssetMenu(fileName = "ThematicBreakBlock", menuName = "ScriptableObjects/MarkdownView/LeafBlocks/ThematicBreakBlock")]
public class ThematicBreakBlockMarkdownView : LeafBlockMarkdownView<ThematicBreakBlock>
{
    protected override void RenderTyped(MarkdownContext context, ThematicBreakBlock mdObject, VisualElement layout,
        Func<MarkdownObject, IMarkdownView> viewSelector)
    { }
}
