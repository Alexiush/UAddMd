using Markdig.Syntax.Inlines;
using System;
using UnityEngine;

/// <summary>
/// Inline processor for literal inlines
/// </summary>
[CreateAssetMenu(fileName = "LiteralInlineProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/LiteralInlineProcessor")]
public class LiteralInlineProcessor : GenericLeafInlineProcessor<LiteralInline>
{
    public override void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, LiteralInline inline)
    {
        var context = renderingState.Draft;

        var body = inline.Content;
        var text = body;

        context.Text += text;
    }
}
