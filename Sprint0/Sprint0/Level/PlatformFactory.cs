using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public static class PlatformFactory
{
    // Dictionary to store platform sizes and properties based on subtype
    private static Dictionary<string, (int width, int height, Vector2 bounds, Vector2 offset, float orderInLayer)> platformSizes = new Dictionary<string, (int width, int height, Vector2 bounds, Vector2 offset, float orderInLayer)>()
    {
        { "Platform1", (427, 102, new Vector2(427, 25), new Vector2(0, 10), 0.5f) },
        { "Platform2", (208, 110, new Vector2(188, 25), new Vector2(10, 10), 0.501f) }
    };

    public static GameObject CreatePlatform(string subtype, Vector2 position)
    {
        // Get the correct texture from the texture storage
        Texture2D texture = GOManager.Instance.textureStorage.GetTexture(subtype);

        // Get platform size, bounds, offset, and orderInLayer from the dictionary
        if (platformSizes.TryGetValue(subtype, out (int width, int height, Vector2 bounds, Vector2 offset, float orderInLayer) platformData))
        {
            // Adjust position using the offset
            Vector2 adjustedPosition = position + platformData.offset;

            // Create a destination rectangle for the platform
            Rectangle destRectangle = new Rectangle((int)adjustedPosition.X, (int)adjustedPosition.Y, platformData.width, platformData.height);

            // Platforms generally need colliders
            bool collider = true;

            // Create the platform with the specified properties
            GameObject platform = CreatePlatform(destRectangle, texture, platformData.width, platformData.height, collider, platformData.bounds, platformData.offset, platformData.orderInLayer);
            platform.type = "Platform" + subtype;

            return platform;
        }
        else
        {
            Console.WriteLine($"Warning: Platform subtype '{subtype}' not found. Using default size.");
            Rectangle defaultRect = new Rectangle((int)position.X, (int)position.Y, 200, 50);
            return CreatePlatform(defaultRect, texture, 200, 50, true, new Vector2(200, 50), Vector2.Zero, 0.5f); // Default platform
        }
    }

    // Consolidated method to handle platform creation, frame size, and collider size
    private static GameObject CreatePlatform(Rectangle destRectangle, Texture2D texture, int frameWidth, int frameHeight, bool collider, Vector2 bounds, Vector2 offset, float orderInLayer)
    {
        GameObject platform = new GameObject(destRectangle.X, destRectangle.Y);

        // Add sprite renderer
        SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
        platform.AddComponent(spriteRenderer);

        // Set the frame size for the animation
        Animation platformTexture = new Animation(texture, 1, 1, frameHeight, frameWidth);
        spriteRenderer.addAnimation("texture", platformTexture);
        spriteRenderer.setAnimation("texture");

        // Set sprite order in the layer
        spriteRenderer.orderInLayer = orderInLayer;

        // Add a collider for the platform
        if (collider)
        {
            BoxCollider boxCollider = new BoxCollider(bounds, offset, GOManager.Instance.GraphicsDevice);
            platform.AddComponent(boxCollider);
        }

        // Add platform to the game objects list
        GOManager.Instance.allGOs.Add(platform);

        return platform;
    }
}
