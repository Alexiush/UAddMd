using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MarkdownTestEditor : EditorWindow
{
    [SerializeField]
    private ViewsCollection _defaultViewsCollection;

    [MenuItem("Window/UI Toolkit/MarkdownTestEditor")]
    public static void ShowExample()
    {
        MarkdownTestEditor wnd = GetWindow<MarkdownTestEditor>();
        wnd.titleContent = new GUIContent("MarkdownTestEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        var renderer = new MarkdownRenderer();
        renderer.Initialize(root, _defaultViewsCollection);

        renderer.Text =
@"
Also available in editor!

> Quotes there too

```
Code there too
```

[Links there too](example.com)

Actually there are **3 MarkdownRenderer variants**:
* Base that can be used directly from code or under the hood of another implementation
* VisualElement **(This one!!!)** that can be more comfortable to use with UI Toolkit
* Component aka MonoBehaviour that is useful to addressable rendering as it's GameObject can be used to instantiate other components 
";
    }
}
