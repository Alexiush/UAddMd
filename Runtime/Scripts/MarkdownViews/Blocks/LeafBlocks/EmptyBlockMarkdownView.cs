using Markdig.Syntax;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes empty blocks
/// </summary>
[CreateAssetMenu(fileName = "EmptyBlock", menuName = "ScriptableObjects/MarkdownView/LeafBlocks/EmptyBlock")]
public class EmptyBlockMarkdownView : LeafBlockMarkdownView<EmptyBlock>
{
    protected override void RenderTyped(MarkdownContext context, EmptyBlock mdObject, VisualElement layout,
        Func<MarkdownObject, IMarkdownView> viewSelector)
    { }
}
