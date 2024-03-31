using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Pair of bullet strings, parsed and to be rendered
/// </summary>
[System.Serializable]
public struct BulletMapEntry
{
    public string MarkdownBullet;
    public string RenderBullet;
}

/// <summary>
/// Stores mapping from parsed bullets to rendered bullets
/// </summary>
[System.Serializable]
public class BulletMapping
{
    /// <summary>
    /// List of mapping pairs
    /// </summary>
    /// <seealso cref="BulletMapEntry"/>
    public List<BulletMapEntry> Entries = new List<BulletMapEntry>();

    /// <summary>
    /// Default bullet to use
    /// </summary>
    public string DefaultBullet = "\u2022";
}


/// <summary>
/// Markdown view that processes list item blocks
/// </summary>
[CreateAssetMenu(fileName = "ListItemBlock", menuName = "ScriptableObjects/MarkdownView/ContainerBlocks/ListItemBlock")]
public class ListItemBlockMarkdownView : ContainerBlockMarkdownView<ListItemBlock>
{
    /// <summary>
    /// Bullet mapping used by this view
    /// </summary>
    /// <seealso cref="BulletMapping"/>
    [SerializeField]
    private BulletMapping _bulletMapping;

    /// <summary>
    /// Maps markdown bullet to bullet used in UI
    /// </summary>
    /// <param name="bulletType">Bullet parsed from markdown</param>
    /// <returns>Bullet to render</returns>
    private string BulletMap(char bulletType) => _bulletMapping.Entries
        .Where(e => e.MarkdownBullet == bulletType.ToString())
        .Select(e => e.RenderBullet)
        .DefaultIfEmpty(_bulletMapping.DefaultBullet)
        .FirstOrDefault();

    protected override void RenderTyped(MarkdownContext context, ListItemBlock mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector)
    {
        var listBlock = mdObject.Parent as ListBlock;
        var identifier = listBlock.IsOrdered ? $"{mdObject.Order}." : BulletMap(listBlock.BulletType);

        var identifierLabel = new Label(identifier);
        identifierLabel.AddToClassList("ListIdentifier");

        layout.Add(identifierLabel);
    }
}
