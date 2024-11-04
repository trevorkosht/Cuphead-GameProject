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
            { "WinScreenBackground", (1000, 1500, 0.001f) },
            { "WinScreenBoard", (980, 760, 0.002f) },
            { "WinScreenResultsText", (902, 710, 0.002f) },
            { "WinScreenCuphead", (273, 476, 0.002f) },
            { "WinScreenUnearnedStar", (650, 472, 0.002f) },
            { "WinScreenStarAppearAnimation", (1564, 458, 0.003f) },
            { "WinScreenLine", (642, 270, 0.003f) },
            { "WinScreenStar", (970, 682, 0.004f) },
            { "WinScreenCircle", (668, 522, 0.009f) },
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
