using Markdig.Syntax;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes heading blocks
/// </summary>
[CreateAssetMenu(fileName = "HeadingBlock", menuName = "ScriptableObjects/MarkdownView/LeafBlocks/HeadingBlock")]
public class HeadingBlockMarkdownView : LeafBlockMarkdownView<HeadingBlock>
{
    protected override void RenderTyped(MarkdownContext context, HeadingBlock mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector)
    {
        layout.AddToClassList($"heading_{mdObject.Level}");

        var nameWithTags = mdObject.Inline.ToHtml();
        var name = Regex.Replace(nameWithTags, @"<[^>]*>", "");
        var nameFormatted = name.ToLower().Replace(' ', '-');

        layout.name = nameFormatted;
    }
}
