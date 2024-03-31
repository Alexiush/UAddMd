using Markdig.Syntax;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes link reference definitions
/// </summary>
[CreateAssetMenu(fileName = "LinkReferenceDefinition", menuName = "ScriptableObjects/MarkdownView/LeafBlocks/LinkReferenceDefinition")]
public class LinkReferenceDefinitionMarkdownView : LeafBlockMarkdownView<LinkReferenceDefinition>
{
    // It is an always hidden element

    protected override void RenderTyped(MarkdownContext context, LeafBlock mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector) { }
    protected override void RenderTyped(MarkdownContext context, LinkReferenceDefinition mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector) { }
}
