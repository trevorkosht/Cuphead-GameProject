﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public static class BackgroundFactory
{
    private static Dictionary<string, (int width, int height, float parallaxFactor, float orderInLayer)> backgroundSizes = new Dictionary<string, (int width, int height, float parallaxFactor, float orderInLayer)>()
    {
            { "LevelBackground", (1827, 546, 0.1f, 0.95f) },
            { "Rock1", (526, 306, 0.1f, 0.801f) },
            { "Rock2", (980, 760, 0.1f, 0.802f) },
            { "Rock3", (642, 270, 0.1f, 0.803f) },
            { "Rock4", (970, 682, 0.1f, 0.804f) },
            { "Rock5", (650, 472, 0.1f, 0.805f) },
            { "Rock6", (668, 522, 0.1f, 0.806f) },
            { "Leaves1", (902, 710, 0.1f, 0.851f) },
            { "Leaves2", (1564, 458, 0.1f, 0.852f) },
            { "Leaves3", (1052, 350, 0.1f, 0.853f) },
            { "Tree1", (273, 476, 0.1f, 0.807f) },
            { "Tree2", (476, 682, 0.1f, 0.808f) },
            { "Tree3", (259, 456, 0.1f, 0.809f) },
            { "Tree4", (500, 655, 0.1f, 0.81f) },
            { "Tree5", (922, 720, 0.1f, 0.811f) },
            { "Tree6", (852, 1064, 0.1f, 0.812f) },
            { "Tree7", (304, 844, 0.1f, 0.813f) },
            { "Bush1", (1338, 625, 0.1f, 0.9f) }
    };

    public static GameObject CreateBackground(string subtype, Vector2 position)
    {
        Texture2D texture = GOManager.Instance.textureStorage.GetTexture(subtype);

        if (backgroundSizes.TryGetValue(subtype, out (int width, int height, float parallaxFactor, float orderInLayer) backgroundData))
        {
            Rectangle destRectangle = new Rectangle((int)position.X, (int)position.Y, backgroundData.width, backgroundData.height);
            GameObject background = CreateParallaxBackground(destRectangle, texture, backgroundData.parallaxFactor, backgroundData.orderInLayer);
            return background;
        }
        else
        {
            Console.WriteLine($"Unknown background subtype: {subtype}");
            return null;
        }
    }

    private static GameObject CreateParallaxBackground(Rectangle destRectangle, Texture2D texture, float parallaxFactor, float orderInLayer)
    {
        GameObject background = new GameObject(destRectangle.X, destRectangle.Y);
        SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
        Animation backgroundTexture = new Animation(texture, 1, 1, destRectangle.Height, destRectangle.Width);

        spriteRenderer.addAnimation("texture", backgroundTexture);
        spriteRenderer.setAnimation("texture");
        spriteRenderer.orderInLayer = orderInLayer;

        background.AddComponent(spriteRenderer);
        return background;
    }
}
