using Markdig.Syntax;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes markdown document
/// </summary>
[CreateAssetMenu(fileName = "MarkdownDocument", menuName = "ScriptableObjects/MarkdownView/General/MarkdownDocument")]
public class MarkdownDocumentMarkdownView : ContainerBlockMarkdownView<MarkdownDocument>
{
    protected override void RenderTyped(MarkdownContext context, MarkdownDocument mdObject, VisualElement layout,
        Func<MarkdownObject, IMarkdownView> viewSelector)
    { }
}
