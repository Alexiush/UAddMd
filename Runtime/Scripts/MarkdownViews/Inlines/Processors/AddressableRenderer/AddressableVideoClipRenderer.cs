using Markdig.Syntax.Inlines;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;
using UnityEngine.Video;

/// <summary>
/// Addressable renderer that handles videos
/// </summary>
[CreateAssetMenu(fileName = "AddressableVideoClipRenderer", menuName = "ScriptableObjects/MarkdownView/AddressableRenderers/AddressableVideoClipRenderer")]
public class AddressableVideoClipRenderer : AddressableRenderer
{
    public override bool Accepts(AsyncOperationHandle<object> handle) => handle.Status == AsyncOperationStatus.Succeeded && handle.Result is VideoClip;

    public override void ProcessAddressable(MarkdownContext context, VisualElement canvas, AsyncOperationHandle<object> handle, LinkInline inline)
    {
        if (canvas.panel == null)
        {
            // When multiple document changes occurred in quick succession creation of an image can leak
            return;
        }

        if (!(context.Renderer is MarkdownRendererComponent))
        {
            var label = new Label("Can't add video without MonoBehaviour-based renderer");
            canvas.Add(label);
            return;
        }

        var image = new Image();
        var video = handle.Result as VideoClip;
        var player = (context.Renderer as MarkdownRendererComponent).gameObject.AddComponent<VideoPlayer>();

        player.isLooping = true;
        var texture = new RenderTexture((int)video.width, (int)video.height, 16, RenderTextureFormat.ARGB32);

        player.clip = video;
        player.targetTexture = texture;

        image.image = texture;

        // Scaling
        image.RegisterCallback<GeometryChangedEvent>(evt => image.Rescale());
        canvas.Add(image);

        // Disposing video players
        if (Application.isEditor)
        {
            // In editor detaching with attaching immediately after happens very often
            bool detachedForGood = false;

            image.RegisterCallback<DetachFromPanelEvent>(evt =>
            {
                detachedForGood = true;
                canvas.schedule.Execute(() =>
                {
                    if (!detachedForGood)
                    {
                        return;
                    }

                    DestroyImmediate(player);
                }).StartingIn(100);
            });

            image.RegisterCallback<AttachToPanelEvent>(evt => detachedForGood = false);
        }
        else
        {
            image.RegisterCallback<DetachFromPanelEvent>(evt =>
            {
                Destroy(player);
            });
        }
    }
}
