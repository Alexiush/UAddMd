using Markdig.Syntax.Inlines;
using System;
using UnityEngine;

/// <summary>
/// InlineProcessor for HtmlEntityInline
/// </summary>
[CreateAssetMenu(fileName = "HtmlEntityInlineProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/HtmlEntityInlineProcessor")]
public class HtmlEntityInlineProcessor : GenericLeafInlineProcessor<HtmlEntityInline>
{
    public override void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, HtmlEntityInline inline)
    {
        var context = renderingState.Draft;

        var body = inline.Transcoded;
        var text = body;

        context.Text += text;
    }
}
