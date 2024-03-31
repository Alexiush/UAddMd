using Markdig.Syntax.Inlines;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

/// <summary>
/// Addressable renderer intended to handle failed loads
/// </summary>
[CreateAssetMenu(fileName = "AddressableErrorRenderer", menuName = "ScriptableObjects/MarkdownView/AddressableRenderers/AddressableErrorRenderer")]
public class AddressableErrorRenderer : AddressableRenderer
{
    public override bool Accepts(AsyncOperationHandle<object> handle) => handle.Status == AsyncOperationStatus.Failed;

    public override void ProcessAddressable(MarkdownContext context, VisualElement canvas, AsyncOperationHandle<object> handle, LinkInline inline)
    {
        var reason = handle.OperationException;
        var errorLabel = new Label($"Loading failed: {reason}");

        errorLabel.AddToClassList("Error");

        canvas.Add(errorLabel);
    }
}
