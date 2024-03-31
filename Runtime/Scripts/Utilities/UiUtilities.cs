using UnityEngine.UIElements;

/// <summary>
/// Handy functions to work with unity UI
/// </summary>
public static class UiUtilities
{
    /// <summary>
    /// Rescales the image visual element to fill the available width while maintaining aspect ratio
    /// </summary>
    /// <param name="image">Image visual element</param>
    public static void Rescale(this Image image)
    {
        var texture = image.image;

        int width = texture.width;
        float scalingFactor = image.contentRect.width / texture.width;
        int targetHeight = (int)(texture.height * scalingFactor);

        image.style.height = targetHeight;
    }
}
