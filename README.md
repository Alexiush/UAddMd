# UAddMd

UAddMd is a highly customizable markdown renderer for Unity. 

Though the most popular UI text solutions support rich text tags, writing is usually done outside of the editor and very often in Markdown.  

There is already a bunch of similar projects, however, from what I have seen it is either *only text* support for runtime or full support, *but in editor*.
Meaning all those projects haven't solved my problem of rendering the tutorial with illustrations (which is included as one of the samples).

UAddMd solves this by treating paths in *image links* `![label](path)` as addressable name - **U**nity + **Add**ressables + **M**ark**d**own. By default
UAddMd supports image and video rendering, but it also allows to extend the range of supported data by prowiding custom renderers.

UAddMd is based on [Markdig](https://github.com/xoofx/markdig) - highly pluggable Markdown processor for .NET and tries to follow its philosophy of being easily extensible,
so that if some part of your notes to game pipeline is missing you are likely to find or create an extension for both.

UAddMd uses Unity UI Toolkit, but as mentioned above it's not that hard to make it work with something else.

## Usage

### Installation

The package can be installed through the UPM by the link `https://github.com/Alexiush/UAddMd.git`.

> P.S. When Addressables are present in project but there was no initialization for them addressables from the package samples can fail to load. 

### Markdown renderers

To render markdown document IMarkdownRenderer implementations are used:

```csharp
public interface IMarkdownRenderer
{
    public void Initialize(VisualElement renderTarget, ViewsCollection viewsCollection, MarkdownContext outerContext = null);

    public void UpdateRender();

    public void ScrollTo(string targetName);
}
```

So in order to work with renderer you need to initialize it with `VisualElement` which will be the canvas, `ViewsCollection` to use and, if needed, pass it a context (dependencies).
There is a base implementation present as well as its versions wrapped as `MonoBehaviour` and as a `VisualElement`.

### Markdown views

`ViewsCollection` is an asset that contains a list of `MarkdownView` objects that will be matched by the markdown element that must be processed. 
It is actually a recurring pattern that is also used for inlines and addressables processing:

```csharp
public interface IMatchable<T>
{
    public bool Accepts(T obj);
}

public interface IMatchCollection<O, T> where O : IMatchable<T>
{
    public IEnumerable<O> Matchables { get; }
    public O DefaultMatchable { get; }
}
```

Such elements also are scriptable objects, so static collections can be formed and reused.

Markdown views are used to process blocks and container inlines, because those elements always produce a new `VisualElement`. Their main function looks like this:
```csharp
public void Render(MarkdownContext context, MarkdownObject mdObject, VisualElement layout, Func<MarkdownObject, IMarkdownView> viewSelector);
```
Usually, they just prepare given layout for their children as well as styling it. 
To style their elements views use class lists and often it is possible to just enter your class names to the serialized list.  
You also can setup the USS dependencies, so your custom styling can be used. 

### Inline processors

Inline elements usually are parts of the text, so they're not given their own visual element, but the current processing state they can commit to:
```csharp
public void Process(InlineRenderingState renderingState, Func<Inline, InlineProcessor> processorSelector, Inline inline);
```

The state consists of two parts: text draft and rendered elements list. 
So with text inlines usual algorithm is to add embracing rich tags on the draft stack, append the contents to the draft and then remove tags. Draft also has a list
of postproccessing functions `Func<VisualElement, VisualElement>` that can be used for things like registering the callbacks.

However, some inlines (like image addressable) need different treatment. Usually, they *push* current draft so it is built as a visual element as is and reset it 
(which usually means removing the text, but not rich text tags and postprocessors). Their own element then gets added right into rendered elements list.

### Addressable renderers

And the third category is the renderers for addressables. Their main function looks like this:
```csharp
public abstract void ProcessAddressable(MarkdownContext context, VisualElement canvas, AsyncOperationHandle<object> handle, LinkInline inline);
```
They use not the loading result but the handle so they can handle erroneous or absent data too, they also keep source element just in case.

`InlineProcessor` that handles addressable links also works in non-blocking manner. At first the placeholder is instantiated and processor selection and invokation is bound to addressables loading callback.

## Supported features

* [Basic syntax](https://www.markdownguide.org/basic-syntax/), though html is not processed, so no custom heading ids.
* Rendering images and videos in a runtime.
* Custom styling.

## Not supported features

* Custom styling in the editor (Somehow I can't get Unity's style rich text tag to work with custom stylesheets)
* Rendering videos in editor (VideoPlayer component is used)
* [Extended syntax](https://www.markdownguide.org/extended-syntax/)

## Samples

* **Base document** - MarkdownRenderer with [TestMarkdown](https://github.com/mxstbr/markdown-test-file) as a text.
* **Tutorial example** - That exact tutorial for my game that made me think about creating the tool implemented.
* **EditorWindow sample** - Shows the usage of MarkdownRenderer in Editor UI.
* **Custom addressable example** - Example where custom renderer is created for config files so their addressables can be rendered.

## Credits

[Markdig](https://github.com/xoofx/markdig) - Amazing Markdown processor.
