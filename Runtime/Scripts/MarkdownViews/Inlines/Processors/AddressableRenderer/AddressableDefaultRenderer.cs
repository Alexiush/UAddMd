using Markdig.Syntax.Inlines;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

/// <summary>
/// Addressable renderer intended to use as a fallback
/// </summary>
[CreateAssetMenu(fileName = "AddressableDefaultRenderer", menuName = "ScriptableObjects/MarkdownView/AddressableRenderers/AddressableDefaultRenderer")]
public class AddressableDefaultRenderer : AddressableRenderer
{
    public override bool Accepts(AsyncOperationHandle<object> handle) => true;

    public override void ProcessAddressable(MarkdownContext context, VisualElement canvas, AsyncOperationHandle<object> handle, LinkInline inline)
    {
        canvas.Add(new Label($"Loading failed: Unknown addressable format"));
    }
}
