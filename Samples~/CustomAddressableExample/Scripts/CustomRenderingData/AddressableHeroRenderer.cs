using Markdig.Syntax.Inlines;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "AddressableHeroRenderer", menuName = "ScriptableObjects/MarkdownViewExtensions/AddressableRenderers/AddressableHeroRenderer")]
public class AddressableHeroRenderer : AddressableRenderer
{
    public override bool Accepts(AsyncOperationHandle<object> handle) => handle.Status == AsyncOperationStatus.Succeeded && handle.Result is Hero;

    [SerializeField]
    private VisualTreeAsset _heroView;

    public override void ProcessAddressable(MarkdownContext context, VisualElement canvas, AsyncOperationHandle<object> handle, LinkInline inline)
    {
        var heroData = handle.Result as Hero;

        var heroView = _heroView.Instantiate();

        /// Add an icon
        var icon = heroView.Q("Icon");
        icon.style.backgroundImage = new StyleBackground(Background.FromSprite(heroData.Icon));

        // Add stats
        var attack = heroView.Q("Attack") as Label;
        attack.text = $"<sprite=\"rpgicons\" index=5> {heroData.Attack}";

        var health = heroView.Q("Health") as Label;
        health.text = $"<sprite=\"rpgicons\" index=0> {heroData.Health}";

        var speed = heroView.Q("Speed") as Label;
        speed.text = $"<sprite=\"rpgicons\" index=9> {heroData.Speed}";

        var defense = heroView.Q("Defense") as Label;
        defense.text = $"<sprite=\"rpgicons\" index=11> {heroData.Defense}";

        // Print the name
        var label = heroView.Q("Name") as Label;
        label.text = heroData.name;

        canvas.Add(heroView);
    }
}
