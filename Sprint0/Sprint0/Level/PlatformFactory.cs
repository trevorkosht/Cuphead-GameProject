using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public static class PlatformFactory
{
    private static Dictionary<string, (int width, int height, Vector2 bounds, Vector2 offset, float orderInLayer)> platformSizes = new Dictionary<string, (int width, int height, Vector2 bounds, Vector2 offset, float orderInLayer)>()
    {
        { "Platform1", (427, 102, new Vector2(427, 25), new Vector2(0, 10), 0.5f) },
        { "Platform2", (208, 110, new Vector2(188, 25), new Vector2(10, 10), 0.501f) }
    };

    public static GameObject CreatePlatform(string subtype, Vector2 position)
    {
        Texture2D texture = GOManager.Instance.textureStorage.GetTexture(subtype);

        if (platformSizes.TryGetValue(subtype, out (int width, int height, Vector2 bounds, Vector2 offset, float orderInLayer) platformData))
        {
            Vector2 adjustedPosition = position + platformData.offset;

            Rectangle destRectangle = new Rectangle((int)adjustedPosition.X, (int)adjustedPosition.Y, platformData.width, platformData.height);

            bool collider = true;

            GameObject platform = CreatePlatform(destRectangle, texture, platformData.width, platformData.height, collider, platformData.bounds, platformData.offset, platformData.orderInLayer);
            platform.type = "Platform" + subtype;

            return platform;
        }
        else
        {
            Console.WriteLine($"Warning: Platform subtype '{subtype}' not found. Using default size.");
            Rectangle defaultRect = new Rectangle((int)position.X, (int)position.Y, 200, 50);
            return CreatePlatform(defaultRect, texture, 200, 50, true, new Vector2(200, 50), Vector2.Zero, 0.5f); 
        }
    }

    private static GameObject CreatePlatform(Rectangle destRectangle, Texture2D texture, int frameWidth, int frameHeight, bool collider, Vector2 bounds, Vector2 offset, float orderInLayer)
    {
        GameObject platform = new GameObject(destRectangle.X, destRectangle.Y);

        SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
        platform.AddComponent(spriteRenderer);

        Animation platformTexture = new Animation(texture, 1, 1, frameHeight, frameWidth);
        spriteRenderer.addAnimation("texture", platformTexture);
        spriteRenderer.setAnimation("texture");

        spriteRenderer.orderInLayer = orderInLayer;

        if (collider)
        {
            BoxCollider boxCollider = new BoxCollider(bounds, offset, GOManager.Instance.GraphicsDevice);
            platform.AddComponent(boxCollider);
        }

        GOManager.Instance.allGOs.Add(platform);

        return platform;
    }
}
