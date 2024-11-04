using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

public static class EndingFactory
{
    private static Dictionary<string, (int width, int height, float orderInLayer)> EndSizes = new Dictionary<string, (int width, int height, float orderInLayer)>()
    {
            { "DeathMessage", (500, 546, 0.91f) },
            { "WinScreenBackground", (1580, 1493, 0.09f) },
            { "WinScreenBoard", (565, 460, 0.08f) },
            { "WinScreenResultsText", (800, 150, 0.07f) },
            { "WinScreenCuphead", (450, 450, 0.06f) },
            { "WinScreenUnearnedStar", (27, 27, 0.005f) },
            { "WinScreenStarAppearAnimation", (45, 50, 0.04f) },
            { "WinScreenLine", (358, 3, 0.03f) },
            { "WinScreenStar", (27, 27, 0.02f) },
            { "WinScreenCircle", (100, 89, 0.01f) },
    };

    public static GameObject CreateElement(string subtype, Vector2 position)
    {
        Console.WriteLine(subtype);
        Texture2D texture = GOManager.Instance.textureStorage.GetTexture(subtype);

        if (EndSizes.TryGetValue(subtype, out (int width, int height, float orderInLayer) endData))
        {
            Rectangle destRectangle = new Rectangle((int)position.X, (int)position.Y, endData.width, endData.height);
            GameObject EndScreen = new GameObject(destRectangle.X, destRectangle.Y);
            SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
            Animation backgroundTexture = new Animation(texture, 1, 1, destRectangle.Height, destRectangle.Width);

            spriteRenderer.addAnimation("End", backgroundTexture);
            spriteRenderer.setAnimation("End");
            EndScreen.AddComponent(spriteRenderer);
            return EndScreen;
        }
        else
        {
            Console.WriteLine($"Unknown background subtype: {subtype}");
            return null;
        }
    }

}
