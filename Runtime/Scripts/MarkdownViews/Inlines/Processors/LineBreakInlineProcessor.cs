using Markdig.Syntax.Inlines;
using System;
using UnityEngine;

/// <summary>
/// Inline processor for line break inlines
/// </summary>
[CreateAssetMenu(fileName = "LineBreakInlineProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/LineBreakInlineProcessor")]
public class LineBreakInlineProcessor : GenericLeafInlineProcessor<LineBreakInline>
{
    public override void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, LineBreakInline inline)
    {
        var context = renderingState.Draft;

        var text = inline.IsHard ? "<br>" : " ";
        context.Text += text;
    }
}
