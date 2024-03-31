using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Handy functions to work with Markdig
/// </summary>
public static class MarkdigUtilities
{
    /// <summary>
    /// Direct descendants of markdown object (objects for which this object is a parent) 
    /// </summary>
    /// <typeparam name="T">Type of markdown objects to collect</typeparam>
    /// <param name="mdObject">Parent markdown object</param>
    /// <returns>IEnumerable of direct descendants of type T</returns>
    public static IEnumerable<T> DirectDescendants<T>(this MarkdownObject mdObject) where T : MarkdownObject => (mdObject switch
    {
        ContainerBlock containerBlock => containerBlock.DirectDescendants(),
        ContainerInline containerInline => containerInline.DirectDescendants(),
        LeafBlock leaf => new List<MarkdownObject> { leaf.Inline },
        _ => Enumerable.Empty<MarkdownObject>()
    })
        .Where(x => x is T)
        .Cast<T>();

    /// <summary>
    /// Direct descendants of container inline (objects for which this object is a parent) 
    /// </summary>
    /// <param name="mdObject">Parent container inline</param>
    /// <returns>IEnumerable of direct descendants</returns>
    private static IEnumerable<Inline> DirectDescendants(this ContainerInline mdObject)
    {
        var directDescendants = Enumerable.Empty<Inline>();
        if (mdObject == null || mdObject.FirstChild == null)
        {
            return directDescendants;
        }

        Inline child = mdObject.FirstChild;

        while (true)
        {
            directDescendants = directDescendants.Append(child);
            if (child == mdObject.LastChild)
            {
                break;
            }

            child = child.NextSibling;
        }

        return directDescendants;
    }

    /// <summary>
    /// Direct descendants of container block (objects for which this object is a parent) 
    /// </summary>
    /// <param name="mdObject">Parent container block</param>
    /// <returns>IEnumerable of direct descendants</returns>
    private static IEnumerable<Block> DirectDescendants(this ContainerBlock mdObject) => mdObject;

    /// <summary>
    /// Renders passed markdown object to html string
    /// </summary>
    /// <param name="mdObject">Markdown object to render</param>
    /// <returns>Html string for passed markdown object</returns>
    public static string ToHtml(this MarkdownObject mdObject)
    {
        var text = "";

        using (var writer = new StringWriter())
        {
            var htmlRenderer = new HtmlRenderer(writer);
            htmlRenderer.Write(mdObject);

            text = writer.ToString();
        }

        return text;
    }
}
