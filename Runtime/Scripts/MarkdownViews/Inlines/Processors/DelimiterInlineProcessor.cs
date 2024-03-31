using Markdig.Syntax.Inlines;
using UnityEngine;

/// <summary>
/// InlineProcessor for DelimiterInline
/// </summary>
[CreateAssetMenu(fileName = "DelimiterInlineProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/DelimiterInlineProcessor")]
public class DelimiterInlineProcessor : InlineProcessor<DelimiterInline>
{
    public override void ProcessOpeningTyped(InlineRenderingState renderingState, DelimiterInline inline)
    {
        var context = renderingState.Draft;
        var text = inline.ToLiteral();

        context.Text += text;
    }

    // No closing
    public override void ProcessClosingTyped(InlineRenderingState renderingState, DelimiterInline inline) { }
}
