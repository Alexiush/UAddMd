using Markdig.Syntax;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes link reference definition groups
/// </summary>
[CreateAssetMenu(fileName = "LinkReferenceDefinitionGroup", menuName = "ScriptableObjects/MarkdownView/ContainerBlocks/LinkReferenceDefinitionGroup")]
public class LinkReferenceDefinitionGroupMarkdownView : ContainerBlockMarkdownView<LinkReferenceDefinitionGroup>
{
    // It is intended to be always hidden

    protected override void RenderTyped(MarkdownContext context, ContainerBlock mdObject, VisualElement layout,
        Func<MarkdownObject, IMarkdownView> viewSelector)
    { }
    protected override void RenderTyped(MarkdownContext context, LinkReferenceDefinitionGroup mdObject, VisualElement layout,
        Func<MarkdownObject, IMarkdownView> viewSelector)
    { }
}
