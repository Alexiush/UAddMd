using Markdig.Syntax.Inlines;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

/// <summary>
/// Inline processor that renders addressables via the image link inline
/// </summary>
[CreateAssetMenu(fileName = "AddressableLinkProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/AddressableLinkProcessor")]
public class AddressableLinkProcessor : GenericContainerInlineProcessor<LinkInline>, IMatchCollection<AddressableRenderer, AsyncOperationHandle<object>>
{
    public override bool Accepts(Inline inline)
    {
        var castToLink = inline as LinkInline;
        return castToLink != null && castToLink.IsImage;
    }

    /// <summary>
    /// List of renderers that process loaded addressable
    /// </summary>
    /// <seealso cref="AddressableRenderer"/>
    [SerializeField]
    private List<AddressableRenderer> _addressableRenderers = new List<AddressableRenderer>();
    /// <summary>
    /// Default addressables renderer
    /// </summary>
    /// <seealso cref="AddressableRenderer"/>
    [SerializeField]
    private AddressableRenderer _defaultRenderer;

    public IEnumerable<AddressableRenderer> Matchables => _addressableRenderers;

    public AddressableRenderer DefaultMatchable => _defaultRenderer;

    /// <summary>
    /// Callback to process addressable loading result 
    /// </summary>
    /// <param name="context">Used markdown context</param>
    /// <param name="canvas">Visual element dedicated to draw on with loading style applied</param>
    /// <param name="handle">Loading handle</param>
    /// <param name="inline">Source inline</param>
    /// <param name="loadingAnimationCallback">Callback of loading animation</param>
    private void ProcessHandle(MarkdownContext context, VisualElement canvas, AsyncOperationHandle<object> handle, LinkInline inline,
        EventCallback<TransitionEndEvent> loadingAnimationCallback)
    {
        this.Match(handle)
            .ProcessAddressable(context, canvas, handle, inline);

        StopLoadingAnimation(canvas, loadingAnimationCallback);
        Addressables.Release(handle);
    }

    /// <summary>
    /// Starts loading animation on a placeholder
    /// </summary>
    /// <param name="canvas">Placeholder visual element</param>
    /// <returns>Callback to unregister to stop animation</returns>
    private EventCallback<TransitionEndEvent> StartLoadingAnimation(VisualElement canvas)
    {
        EventCallback<TransitionEndEvent> loadingAnimationCallback = evt =>
        {
            canvas.ToggleInClassList("change-background-yoyo");
        };

        canvas.RegisterCallback<TransitionEndEvent>(loadingAnimationCallback);
        canvas.schedule.Execute(() => canvas.ToggleInClassList("change-background-yoyo")).StartingIn(100);

        return loadingAnimationCallback;
    }

    /// <summary>
    /// Stops loading animation on a placeholder
    /// </summary>
    /// <param name="canvas">Placeholder</param>
    /// <param name="loadingCallback">Callback to unregister</param>
    private void StopLoadingAnimation(VisualElement canvas, EventCallback<TransitionEndEvent> loadingCallback)
    {
        canvas.RemoveFromClassList("loading-default");
        canvas.UnregisterCallback<TransitionEndEvent>(loadingCallback);

        canvas.schedule.Execute(() =>
        {
            canvas.RemoveFromClassList("change-background-yoyo");
        }).StartingIn(150);
    }

    public override void ProcessOpeningTyped(InlineRenderingState renderingState, LinkInline inline) { }

    public override void ProcessClosingTyped(InlineRenderingState renderingState, LinkInline inline) { }

    public override void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, ContainerInline inline) =>
        ProcessTyped(renderingState, processorSelector, inline as LinkInline);

    public override void ProcessTyped(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, LinkInline inline)
    {
        var context = renderingState.Draft;

        var linkUrl = inline.GetDynamicUrl != null ? inline.GetDynamicUrl() ?? inline.Url : inline.Url;

        // Make state push the draft and add content manually
        renderingState.PushDraft();
        renderingState.Draft.ResetText();

        // As addressables are made for dynamic load placeholder is instantiated
        var canvas = new VisualElement();
        canvas.AddToClassList("loading-default");
        var callback = StartLoadingAnimation(canvas);

        renderingState.RenderedElements.Add(canvas);

        var loadHandle = Addressables.LoadAssetAsync<object>(linkUrl);
        loadHandle.Completed += h => ProcessHandle(renderingState.MarkdownContext, canvas, h, inline, callback);
    }

    public override IEnumerable<StyleSheet> StylesUsed() => _addressableRenderers.Append(_defaultRenderer)
        .SelectMany(renderer => renderer.StylesUsed());
}
