using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public static class BackgroundFactory
{
    private static Dictionary<string, (int width, int height, float parallaxFactor, float orderInLayer)> backgroundSizes = new Dictionary<string, (int width, int height, float parallaxFactor, float orderInLayer)>()
    {
            { "LevelBackground", (1827, 546, 0.1f, 0.91f) },
            { "Rock1", (526, 306, 0.1f, 0.801f) },
            { "Rock2", (980, 760, 0.1f, 0.802f) },
            { "Rock3", (642, 270, 0.1f, 0.05f) },
            { "Rock4", (970, 682, 0.1f, 0.804f) },
            { "Rock5", (650, 472, 0.1f, 0.805f) },
            { "Rock6", (668, 522, 0.1f, 0.806f) },
            { "Leaves1", (902, 710, 0.1f, 0.71f) },
            { "Leaves2", (1564, 458, 0.1f, 0.72f) },
            { "Tree1", (273, 476, 0.1f, 0.807f) },
            { "Tree2", (476, 682, 0.1f, 0.808f) },
            { "Tree3", (259, 456, 0.1f, 0.809f) },
            { "Tree4", (500, 655, 0.1f, 0.81f) },
            { "Tree5", (922, 720, 0.1f, 0.811f) },
            { "Tree6", (852, 1064, 0.1f, 0.812f) },
            { "Tree7", (304, 844, 0.1f, 0.813f) },
            { "Bush1", (1338, 625, 0.1f, 0.9f) },
            { "Bush2", (1809, 720, 0.1f, 0.9f) },
            { "Bush3", (1052, 350, 0.1f, 0.853f) },
            {"Clouds", (2036, 530, 0.1f, 0.91f)},
            {"SkyBackground1", (1806, 760, 0.1f, 0.95f)},
            {"HillBackground1", (2048, 256, 0.1f, 0.87f)},
            {"TreeBackground1", (2048, 594, 0.1f, 0.89f)},
            {"TreeBackground2", (2046, 432, 0.1f, 0.83f)},
            {"TreeBackground3", (2037, 762, 0.1f, 0.86f)},
            {"TreeBackground4", (2009, 414, 0.1f, 0.85f)},
            {"TreeBackground5", (2048, 756, 0.1f, 0.84f)},
            {"TreeBackground6", (2046, 810, 0.1f, 0.83f)},
            {"TreeBackground7", (2040, 841, 0.1f, 0.82f)},
            {"TreeBackground8", (2016, 786, 0.1f, 0.81f)},
            {"BossLevelHills", (1379, 509, 0.1f, 0.80f) },
            {"BossLevelClouds", (1401, 194, 0.1f, 0.75f) },
            {"BossLevelSky", (1376, 457, 0.1f, 0.90f) },
            {"BossLevelBush", (417, 323, 0.1f, 0.60f) },
            {"BossLevelGround", (1444, 809, 0.1f, .65f) }
    };
    public static float layerBoost = 0;

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
        spriteRenderer.orderInLayer = orderInLayer + layerBoost;

        background.AddComponent(spriteRenderer);
        layerBoost += .0001f;
        return background;
    }
}
