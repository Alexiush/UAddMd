using Markdig.Syntax.Inlines;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Inline processor for link inlines that are not images (addressables)
/// </summary>
[CreateAssetMenu(fileName = "LinkInlineProcessor", menuName = "ScriptableObjects/MarkdownView/InlineProcessors/LinkInlineProcessor")]
public class LinkInlineProcessor : GenericContainerInlineProcessor<LinkInline>
{
    public override bool Accepts(Inline inline)
    {
        var castToLink = inline as LinkInline;
        return castToLink != null && !castToLink.IsImage;
    }

    public override void ProcessOpeningTyped(InlineRenderingState renderingState, LinkInline inline)
    {
        var context = renderingState.Draft;

        var linkId = Guid.NewGuid();

        var linkUrl = inline.GetDynamicUrl != null ? inline.GetDynamicUrl() ?? inline.Url : inline.Url;
        bool isDocumentLink = linkUrl.StartsWith("#");
        var linkBody = isDocumentLink ? $"https://validation.by.pass/{linkUrl}" : linkUrl;

        var linkTitle = string.IsNullOrEmpty(inline.Title) ? "" : $" title=\"{inline.Title}\"";

        var opening = $"<style=\"Link\"><link=\"{linkId}\">";
        var closing = "</link></style>";

        context.OpeningsAndClosings.Push(new OpeningClosing
        {
            Opening = opening,
            Closing = closing
        });

        context.Text += opening;

        void ScrollToSection(PointerUpEvent evt) => renderingState.MarkdownContext.Renderer.ScrollTo(linkUrl.Substring(1));

        void OpenUrl(PointerUpEvent evt) => Application.OpenURL(linkUrl);

        Func<VisualElement, VisualElement> markdownLinkCallback = e =>
        {
            EventCallback<PointerUpEvent> linkCallback = isDocumentLink ? ScrollToSection : OpenUrl;

            var handler = LinksHandlersManager.GetHandler(e);
            handler.Expand(linkId.ToString(), linkCallback);

            return e;
        };
        context.PostProcessors.Add(markdownLinkCallback);
    }
}
