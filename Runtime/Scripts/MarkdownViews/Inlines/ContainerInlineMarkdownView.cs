using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that processes container inlines
/// </summary>
[CreateAssetMenu(fileName = "ContainerInline", menuName = "ScriptableObjects/MarkdownView/Inlines/ContainerInline")]
public class ContainerInlineMarkdownView : MarkdownView<ContainerInline>, IMatchCollection<InlineProcessor, Inline>
{
    /// <summary>
    /// List of inline processors to use
    /// </summary>
    /// <seealso cref="InlineProcessor"/>
    [SerializeField]
    private List<InlineProcessor> InlineProcessors;
    /// <summary>
    /// Default inline processor used as fallback
    /// </summary>
    /// <seealso cref="InlineProcessor"/>
    [SerializeField]
    private InlineProcessor DefaultProcessor;

    public IEnumerable<InlineProcessor> Matchables => InlineProcessors;

    public InlineProcessor DefaultMatchable => DefaultProcessor;

    protected override void RenderTyped(MarkdownContext context, ContainerInline mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector)
    {
        Func<Inline, InlineProcessor> processorSelector = inline => this.Match(inline);

        var rootProcessor = processorSelector(mdObject);

        var state = new InlineRenderingState(context);
        rootProcessor.Process(state, processorSelector, mdObject);

        state.PushDraft();
        var renderedElements = state.Collect();

        layout.Add(renderedElements);
    }

    public override IEnumerable<StyleSheet> StylesUsed() => InlineProcessors.Append(DefaultProcessor)
            .SelectMany(p => p.StylesUsed());
}

