using Markdig.Syntax.Inlines;
using System;
using UnityEngine;

/// <summary>
/// InlineProcessor for AutolinkInline with stylesheet independent styling
/// </summary>
[CreateAssetMenu(fileName = "AutolinkInlineStaticallyStyledProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/AutolinkInlineStaticallyStyledProcessor")]
public class AutolinkInlineStaticallyStyledProcessor : GenericLeafInlineProcessor<AutolinkInline>
{
    public override void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, AutolinkInline inline)
    {
        var context = renderingState.Draft;

        var linkStart = inline.IsEmail ? "<a href=\"mailto:" : "<a href=\"";
        var linkBody = inline.Url;
        var text = $"<color=\"blue\"><u>{linkStart + linkBody}\">{linkBody}</u></color>";

        context.Text += text;
    }
}
