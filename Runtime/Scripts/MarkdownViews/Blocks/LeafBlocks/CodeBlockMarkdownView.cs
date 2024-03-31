using Markdig.Helpers;
using Markdig.Syntax;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes code blocks
/// </summary>
[CreateAssetMenu(fileName = "CodeBlock", menuName = "ScriptableObjects/MarkdownView/LeafBlocks/CodeBlock")]
public class CodeBlockMarkdownView : LeafBlockMarkdownView<CodeBlock>
{
    /// <summary>
    /// Special parser for code block content
    /// </summary>
    /// <param name="block">Code block</param>
    /// <returns>Text of code block content</returns>
    private string ProcessCodeBlock(CodeBlock block)
    {
        var text = new StringBuilder();

        var slices = block.Lines.Lines;

        if (slices is not null)
        {
            for (int i = 0; i < slices.Length; i++)
            {
                ref StringSlice slice = ref slices[i].Slice;
                if (slice.Text is null)
                {
                    break;
                }

                ReadOnlySpan<char> span = slice.AsSpan();
                text.Append(span.ToString());
                text.Append("\n");
            }
        }

        return text.ToString();
    }

    protected override void RenderTyped(MarkdownContext context, CodeBlock mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector)
    {
        var text = ProcessCodeBlock(mdObject);
        layout.Add(new Label(text));
    }
}
