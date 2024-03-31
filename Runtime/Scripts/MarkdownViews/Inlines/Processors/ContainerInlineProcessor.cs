using Markdig.Syntax.Inlines;
using UnityEngine;

/// <summary>
/// InlineProcessor for ContainerInline
/// </summary>
[CreateAssetMenu(fileName = "ContainerInlineProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/ContainerInlineProcessor")]
public class ContainerInlineProcessor : InlineProcessor<ContainerInline>
{
    // Skipping processing
    public override void ProcessOpeningTyped(InlineRenderingState renderingState, ContainerInline inline) { }

    public override void ProcessClosingTyped(InlineRenderingState renderingState, ContainerInline inline) { }
}
