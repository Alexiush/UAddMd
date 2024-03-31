using UnityEditor;
using UnityEngine;

/// <summary>
/// Component to pass data to markdown renderer from inspector 
/// </summary>
[CustomEditor(typeof(MarkdownRendererEditor))]
public class MarkdownRendererInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var rendererEditor = target as MarkdownRendererEditor;

        if (rendererEditor.EditorManualUpdate && GUILayout.Button("Update"))
        {
            rendererEditor.SubmitChanges();
        }
    }
}
