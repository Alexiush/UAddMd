using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TabbedMenu : MonoBehaviour
{
    /* Define member variables*/
    private const string tabClassName = "tab";
    private const string currentlySelectedTabClassName = "currentlySelectedTab";
    private const string unselectedContentClassName = "unselectedContent";
    // Tab and tab content have the same prefix but different suffix
    // Define the suffix of the tab name
    private const string tabNameSuffix = "Tab";
    // Define the suffix of the tab content name
    private const string contentNameSuffix = "Content";

    [SerializeField]
    private List<Tab> _tabs = new List<Tab>();

    [SerializeField]
    private ViewsCollection _viewsCollection;

    private TabbedMenuController controller;
    private List<MarkdownRendererComponent> _renderers = new List<MarkdownRendererComponent>();

    private void ClearLayout(VisualElement root)
    {
        var tabsLayout = root.Q("Tabs");
        var contentLayout = root.Q("TabContent");

        tabsLayout.Clear();
        contentLayout.Clear();

        _renderers.ForEach(r => Destroy(r));
        _renderers.Clear();
    }

    private void InitializeTabs(VisualElement root)
    {
        var tabsLayout = root.Q("Tabs");
        var contentLayout = root.Q("TabContent");

        bool processedAny = false;

        _tabs.ForEach(t =>
        {
            // Add tab
            var tabElement = new Label(t.TabName);
            tabElement.name = t.TabName + tabNameSuffix;
            tabElement.AddToClassList(tabClassName);

            if (!processedAny)
            {
                tabElement.AddToClassList(currentlySelectedTabClassName);
            }

            tabsLayout.Add(tabElement);

            // Add content
            var contentElement = new VisualElement();
            contentElement.name = t.TabName + contentNameSuffix;

            if (processedAny)
            {
                contentElement.AddToClassList(unselectedContentClassName);
            }

            contentLayout.Add(contentElement);

            // Add renderer
            var renderer = gameObject.AddComponent<MarkdownRendererComponent>();
            renderer.Initialize(contentElement, _viewsCollection);
            renderer.Text = t.TabMarkdown;
            _renderers.Add(renderer);

            processedAny = true;
        });
    }

    private void OnEnable()
    {
        UIDocument menu = GetComponent<UIDocument>();
        VisualElement root = menu.rootVisualElement;

        ClearLayout(root);
        InitializeTabs(root);

        controller = new(root);
        controller.RegisterTabCallbacks();
    }
}

[System.Serializable]
public struct Tab
{
    public string TabName;
    [TextArea(15, 20)]
    public string TabMarkdown;
}