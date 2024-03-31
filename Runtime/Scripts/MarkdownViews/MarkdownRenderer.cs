using UnityEngine;
using UnityEngine.UIElements;

public class MarkdownRenderer : VisualElement, IMarkdownRenderer
{
    /// <summary>
    /// Underlying base MarkdownRenderer
    /// </summary>
    /// <seealso cref="MarkdownRendererBase"/>
    private MarkdownRendererBase _renderer = new MarkdownRendererBase();

    public MarkdownRenderer() { }

    /// <summary>
    /// Visual element to render on
    /// </summary>
    public VisualElement RenderTarget
    {
        get
        {
            return _renderer.RenderTarget;
        }
        set
        {
            _renderer.RenderTarget = value;
            UpdateRender();
        }
    }

    /// <summary>
    /// Collection of converters from markdown elements to unity ui elements
    /// </summary>
    /// <seealso cref="ViewsCollection"/>
    public ViewsCollection ViewsCollection
    {
        get
        {
            return _renderer.ViewsCollection;
        }
        set
        {
            _renderer.ViewsCollection = value;
            UpdateRender();
        }
    }

    public string Text
    {
        get
        {
            return _renderer.Text;
        }
        set
        {
            _renderer.Text = value;
            UpdateRender();
        }
    }

    /// <summary>
    /// Setting to track trivia in the text
    /// </summary>
    [SerializeField]
    private bool _trackTrivia;

    /// <summary>
    /// Already formed context passed by initialize caller
    /// </summary>
    private MarkdownContext _outerContext;

    public void Initialize(VisualElement renderTarget, ViewsCollection viewsCollection, MarkdownContext outerContext = null)
    {
        _outerContext = outerContext;

        var context = _outerContext is null ? new MarkdownContext { Renderer = this } : _outerContext;
        _renderer.Initialize(renderTarget, viewsCollection, context);

        RenderTarget = renderTarget;
        ViewsCollection = viewsCollection;
    }

    public void UpdateRender() => _renderer.UpdateRender();

    public void ScrollTo(string targetName) => _renderer.ScrollTo(targetName);
}
