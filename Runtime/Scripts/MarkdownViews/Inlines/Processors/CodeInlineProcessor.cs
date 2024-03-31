using Markdig.Syntax.Inlines;
using System;
using UnityEngine;

/// <summary>
/// InlineProcessor for CodeInline
/// </summary>
[CreateAssetMenu(fileName = "CodeInlineProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/CodeInlineProcessor")]
public class CodeInlineProcessor : GenericLeafInlineProcessor<CodeInline>
{
    public override void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, CodeInline inline)
    {
        var context = renderingState.Draft;

        var body = inline.Content;
        var text = $"<style=\"Code\"><noparse>{body}</noparse></style>";

        context.Text += text;
    }
}
