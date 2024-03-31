using Markdig.Syntax.Inlines;
using UnityEngine;

/// <summary>
/// InlineProcessor for EmphasisInline with stylesheet independent styling
/// </summary>
[CreateAssetMenu(fileName = "EmphasisInlineStaticallyStyledProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/EmphasisInlineStaticallyStyledProcessor")]
public class EmphasisInlineStaticallyStyledProcessor : GenericContainerInlineProcessor<EmphasisInline>
{
    /// <summary>
    /// Gets tag that corresponds to parsed emphasis
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string GetDefaultTag(EmphasisInline obj)
    {
        if (obj.DelimiterChar is '*' or '_')
        {
            Debug.Assert(obj.DelimiterCount <= 2);
            return obj.DelimiterCount == 2 ? "strong" : "em";
        }
        return null;
    }

    public override void ProcessOpeningTyped(InlineRenderingState renderingState, EmphasisInline inline)
    {
        var context = renderingState.Draft;

        var tag = GetDefaultTag(inline);

        var opening = tag switch
        {
            "strong" => "<b>",
            "em" => "<i>",
            _ => ""
        };
        var closing = tag switch
        {
            "strong" => "</b>",
            "em" => "</i>",
            _ => ""
        };

        context.OpeningsAndClosings.Push(new OpeningClosing
        {
            Opening = opening,
            Closing = closing
        });

        context.Text += opening;
    }
}
