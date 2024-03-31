using Markdig.Syntax;
using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown view that handles unknown markdown objects
/// </summary>
[CreateAssetMenu(fileName = "Fallback", menuName = "ScriptableObjects/MarkdownView/General/Fallback")]
public class FallbackMarkdownView : MarkdownView<MarkdownObject>
{
    protected override void RenderTyped(MarkdownContext context, MarkdownObject mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector)
    {
        Debug.Log($"Not implemented: {mdObject.GetType()}");

        var label = new Label($"Not implemented: {mdObject.GetType()}");
        label.AddToClassList("Error");

        layout.Add(label);
    }
}
