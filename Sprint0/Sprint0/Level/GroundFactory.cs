using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public static class GroundFactory
{
    private static Dictionary<string, (int width, int height, Vector2 bounds, Vector2 offset, float orderInLayer)> groundSizes = new Dictionary<string, (int width, int height, Vector2 bounds, Vector2 offset, float orderInLayer)>()
    {
        { "Log1", (832, 186, new Vector2(832, 186), Vector2.Zero, 0.502f) },
        { "BigHill1", (1042, 842, new Vector2(1042, 842), new Vector2(0, 35), 0.7f) }
    };

    public static GameObject CreateGround(string subtype, Vector2 position)
    {
        return BlockFactory.CreateBlock(subtype, position);
    }
}

