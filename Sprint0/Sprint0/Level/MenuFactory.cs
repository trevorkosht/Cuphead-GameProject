using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

public static class MenuFactory
{
    private static Dictionary<string, (int width, int height, float orderInLayer, int frameAmount)> Sizes = new Dictionary<string, (int width, int height, float orderInLayer, int frameAmount)>()
    {
        //start screen
        //cuphead
        { "Title1", (1000, 555, 0.099f, 1) },

        //background
        { "Title2", (1450, 810, 0.1f, 1) },

        //pause screen

        //start game
        { "GameStartText", (1285,720,0.91f,17) },

        //death screen
        { "DeathMessage", (500, 546, 0.91f, 1) },
        { "LossScreen", (554, 632, 0.91f, 1) },
        { "LossScreenIcon", (92, 81, 0.91f, 1) },

        //ending screen
        { "WinScreenBackground", (1580, 1493, 0.1f, 1) },
        { "WinScreenBoard", (565, 460, 0.099f, 1) },
        { "WinScreenResultsText", (800, 150, 0.098f, 3) },
        { "WinScreenCuphead", (450, 450, 0.097f, 12) },
        { "WinScreenUnearnedStar", (27, 27, 0.096f, 1) },
        { "WinScreenStarAppearAnimation", (50, 50, 0.095f, 8) },
        { "WinScreenLine", (358, 3, 0.094f, 1) },
        { "WinScreenStar", (27, 27, 0.093f, 1) },
        { "WinScreenCircle", (90, 90, 0.092f, 12) },
        { "Victory",(1300,700,0.91f,25) },

        //transition 
        { "FadeIn", (515, 290, 0.05f, 1) },
        { "FadeOut", (515, 290, 0.05f, 1) },

        //need to adjust width and height
        

    };

    public static GameObject CreateElement(string subtype, Vector2 position)
    {
        Console.WriteLine(subtype);
        Texture2D texture = GOManager.Instance.textureStorage.GetTexture(subtype);

        if (Sizes.TryGetValue(subtype, out (int width, int height, float orderInLayer, int frameAmount) endData))
        {
            Rectangle destRectangle = new Rectangle((int)position.X, (int)position.Y, endData.width, endData.height);
            GameObject Menu = new GameObject(destRectangle.X, destRectangle.Y);
            Menu.type = "Menu";
            SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
            Animation backgroundTexture = new Animation(texture, 5, endData.frameAmount, destRectangle.Height, destRectangle.Width);

            spriteRenderer.addAnimation("End", backgroundTexture);
            spriteRenderer.setAnimation("End");
            spriteRenderer.orderInLayer = endData.orderInLayer;
            Menu.AddComponent(spriteRenderer);
            spriteRenderer.Update();
            return Menu;
        }
        else
        {
            Console.WriteLine($"Unknown background subtype: {subtype}");
            return null;
        }
    }

}
