using Markdig.Syntax.Inlines;
using System;
using UnityEngine;

/// <summary>
/// InlineProcessor for CodeInline with stylesheet independent styling
/// </summary>
[CreateAssetMenu(fileName = "CodeInlineStaticallyStyledProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/CodeInlineStaticallyStyledProcessor")]
public class CodeInlineStaticallyStyledProcessor : GenericLeafInlineProcessor<CodeInline>
{
    public override void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, CodeInline inline)
    {
        var context = renderingState.Draft;

        var body = inline.Content;
        var text = $"<cspace=0.05em><noparse>{body}</noparse></cspace>";

        context.Text += text;
    }
}
