using Markdig.Syntax.Inlines;
using System;
using UnityEngine;

/// <summary>
/// InlineProcessor for AutolinkInline
/// </summary>
[CreateAssetMenu(fileName = "AutolinkInlineProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/AutolinkInlineProcessor")]
public class AutolinkInlineProcessor : GenericLeafInlineProcessor<AutolinkInline>
{
    public override void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, AutolinkInline inline)
    {
        var context = renderingState.Draft;

        var linkStart = inline.IsEmail ? "<a href=\"mailto:" : "<a href=\"";
        var linkBody = inline.Url;
        var text = $"<style=\"Link\">{linkStart + linkBody}\">{linkBody}</a></style>";

        context.Text += text;
    }
}
