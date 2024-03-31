using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Default component to manage markdown rendering through the inspector
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(MarkdownRendererComponent))]
public class MarkdownRendererEditor : MonoBehaviour
{
    /// <summary>
    /// Text input field
    /// </summary>
    [field: SerializeField]
    [field: TextArea(15, 20)]
    private string _text;

    /// <summary>
    /// Simplest editor passes target's root to the renderer
    /// </summary>
    [SerializeField]
    private UIDocument _renderTarget;

    /// <summary>
    /// Markdown renderer instance to use
    /// </summary>
    /// <seealso cref="MarkdownRendererComponent"/>
    [SerializeField]
    private MarkdownRendererComponent _renderer;

    /// <summary>
    /// Collection of converters from markdown elements to unity ui elements
    /// </summary>
    /// <seealso cref="ViewsCollection"/>
    [SerializeField]
    private ViewsCollection _viewsCollection;

    /// <summary>
    /// Whether to update document render manually or each time the text has changed
    /// </summary>
    [field: SerializeField]
    public bool EditorManualUpdate { get; set; } = false;

    private void OnValidate()
    {
        if (EditorManualUpdate)
        {
            return;
        }

        SubmitChanges();
    }

    /// <summary>
    /// Pass the updated markdown to the renderer
    /// </summary>
    public void SubmitChanges()
    {
        if (Application.isEditor)
        {
            _renderer.Initialize(_renderTarget.rootVisualElement, _viewsCollection);
        }

        _renderer.Text = _text;
    }

    private void Start()
    {
        _renderer = GetComponent<MarkdownRendererComponent>();
        _renderer.Initialize(_renderTarget.rootVisualElement, _viewsCollection);
        SubmitChanges();
    }
}
