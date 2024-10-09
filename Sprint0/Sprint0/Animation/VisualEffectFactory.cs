﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class VisualEffectFactory {
    public static GameObject createVisualEffect(Rectangle destRectangle, Texture2D texture, int updatesPerFrame, int frameCount) {
        GameObject effect = new GameObject(destRectangle.X, destRectangle.Y);
        Animation effectAnimation = new Animation(texture, updatesPerFrame, frameCount, destRectangle.Height, destRectangle.Width);
        VisualEffectRenderer effectRenderer = new VisualEffectRenderer(destRectangle, effectAnimation);
        return effect;
    }
}