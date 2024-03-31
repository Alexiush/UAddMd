using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class LinksHandler
{
    private string linkId = string.Empty;

    private void OnOverLinkTag(PointerOverLinkTagEvent evt)
    {
        linkId = evt.linkID;
    }

    private void OnOutLinkTag(PointerOutLinkTagEvent evt)
    {
        linkId = string.Empty;
    }

    private Dictionary<string, EventCallback<PointerUpEvent>> _linkCallbacks = new Dictionary<string, EventCallback<PointerUpEvent>>();

    private void Execute(PointerUpEvent evt)
    {
        if (string.IsNullOrEmpty(linkId) || !_linkCallbacks.ContainsKey(linkId))
        {
            return;
        }

        _linkCallbacks[linkId].Invoke(evt);
    }

    public LinksHandler(VisualElement visualElement)
    {
        visualElement.RegisterCallback<PointerOverLinkTagEvent>(OnOverLinkTag);
        visualElement.RegisterCallback<PointerOutLinkTagEvent>(OnOutLinkTag);
        visualElement.RegisterCallback<PointerUpEvent>(Execute);
    }

    public void Expand(string id, EventCallback<PointerUpEvent> callback)
    {
        var success = _linkCallbacks.TryAdd(id, callback);

        if (!success)
        {
            Debug.LogError($"Failed to register callback for link with id: {id}");
        }
    }
}

public static class LinksHandlersManager
{
    private static Dictionary<VisualElement, LinksHandler> _registeredHandlers = new Dictionary<VisualElement, LinksHandler>();

    public static LinksHandler GetHandler(VisualElement visualElement)
    {
        LinksHandler handler = null;
        _registeredHandlers.TryGetValue(visualElement, out handler);

        if (handler is null)
        {
            handler = new LinksHandler(visualElement);
            _registeredHandlers.Add(visualElement, handler);

            visualElement.RegisterCallback<DetachFromPanelEvent>(evt => _registeredHandlers.Remove(visualElement));
        }

        return handler;
    }
}
