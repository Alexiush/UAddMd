using Markdig.Syntax;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes list blocks
/// </summary>
[CreateAssetMenu(fileName = "ListBlock", menuName = "ScriptableObjects/MarkdownView/ContainerBlocks/ListBlock")]
public class ListBlockMarkdownView : ContainerBlockMarkdownView<ListBlock>
{
    protected override void RenderTyped(MarkdownContext context, ListBlock mdObject, VisualElement layout,
        Func<MarkdownObject, IMarkdownView> viewSelector)
    { }
}
