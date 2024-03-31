using Markdig.Syntax.Inlines;
using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SkipInlineProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/SkipInlineProcessor")]
public class SkipInlineProcessor : InlineProcessor
{
    public override bool Accepts(Inline inline) => true;

    // Skipping processing
    public override void ProcessOpening(InlineRenderingState renderingState, Inline inline) { }

    public override void ProcessClosing(InlineRenderingState renderingState, Inline inline) { }

    public override void Process(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, Inline inline)
    {
        ProcessOpening(renderingState, inline);

        var children = inline.DirectDescendants<Inline>()
            .ToList();

        children.ForEach(c =>
        {
            var processor = processorSelector(c);
            processor.Process(renderingState, processorSelector, c);
        });

        ProcessClosing(renderingState, inline);
    }
}
