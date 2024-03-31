using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Markdown renderer that is also a MonoBehaviour
/// </summary>
[ExecuteInEditMode]
public class MarkdownRendererComponent : MonoBehaviour, IMarkdownRenderer
{
    /// <summary>
    /// Visual element to render on
    /// </summary>
    private VisualElement _renderTarget;

    /// <summary>
    /// Collection of converters from markdown elements to unity ui elements
    /// </summary>
    /// <seealso cref="ViewsCollection"/>
    private ViewsCollection _viewsCollection;

    /// <summary>
    /// Markdown text to be rendered
    /// </summary>
    public string Text
    {
        get
        {
            return _renderer.Text;
        }
        set
        {
            _renderer.Text = value;
        }
    }

    /// <summary>
    /// Setting to track trivia in the text
    /// </summary>
    [SerializeField]
    private bool _trackTrivia;

    /// <summary>
    /// Underlying base MarkdownRenderer
    /// </summary>
    /// <seealso cref="MarkdownRendererBase"/>
    private MarkdownRendererBase _renderer = new MarkdownRendererBase();

    private void Update()
    {
        if (!_renderer.Initialized || !_renderer.Dirty)
        {
            return;
        }

        _renderer.UpdateRender();
    }

    /// <summary>
    /// Already formed context passed by initialize caller
    /// </summary>
    private MarkdownContext _outerContext;

    public void Initialize(VisualElement renderTarget, ViewsCollection viewsCollection, MarkdownContext outerContext = null)
    {
        _outerContext = outerContext;

        var context = _outerContext is null ? new MarkdownContext { Renderer = this } : _outerContext;
        _renderer.Initialize(renderTarget, viewsCollection, context);
    }

    public void UpdateRender() => _renderer.UpdateRender();

    public void ScrollTo(string targetName) => _renderer.ScrollTo(targetName);
}
