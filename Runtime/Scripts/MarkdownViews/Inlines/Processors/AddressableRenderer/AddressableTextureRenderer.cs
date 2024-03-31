using Markdig.Syntax.Inlines;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

/// <summary>
/// Addressable renderer that handles textures
/// </summary>
[CreateAssetMenu(fileName = "AddressableTextureRenderer", menuName = "ScriptableObjects/MarkdownView/AddressableRenderers/AddressableTextureRenderer")]
public class AddressableTextureRenderer : AddressableRenderer
{
    public override bool Accepts(AsyncOperationHandle<object> handle) => handle.Status == AsyncOperationStatus.Succeeded && handle.Result is Texture;

    public override void ProcessAddressable(MarkdownContext context, VisualElement canvas, AsyncOperationHandle<object> handle, LinkInline inline)
    {
        var image = new Image();
        var texture = handle.Result as Texture;
        image.image = texture;

        image.RegisterCallback<GeometryChangedEvent>(evt => image.Rescale());

        canvas.Add(image);
    }
}
