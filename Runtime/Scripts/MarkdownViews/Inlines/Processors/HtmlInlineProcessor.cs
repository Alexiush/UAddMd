using Markdig.Syntax.Inlines;
using System;
using UnityEngine;

/// <summary>
/// InlineProcessor for HtmlInline
/// </summary>
[CreateAssetMenu(fileName = "HtmlInlineProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/HtmlInlineProcessor")]
public class HtmlInlineProcessor : GenericLeafInlineProcessor<HtmlInline>
{
    public override void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, HtmlInline inline)
    {
        var context = renderingState.Draft;

        var text = inline.Tag;

        context.Text += text;
    }
}
