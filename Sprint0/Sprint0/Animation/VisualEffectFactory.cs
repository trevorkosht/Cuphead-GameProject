using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class VisualEffectFactory {
    public static GameObject createVisualEffect(Rectangle destRectangle, Texture2D texture, int updatesPerFrame, int frameCount, float scale, bool isFacingRight) {
        GameObject effect = new GameObject(destRectangle.X, destRectangle.Y);
        Animation effectAnimation = new Animation(texture, updatesPerFrame, frameCount, destRectangle.Height, destRectangle.Width);
        VisualEffectRenderer effectRenderer = new VisualEffectRenderer(destRectangle, effectAnimation, isFacingRight);
        effectRenderer.effectScale = scale;
        effect.AddComponent(effectRenderer);
        effect.type = "VFX";
        GOManager.Instance.allGOs.Add(effect);
        return effect;
    }

    public static GameObject createVisualEffect(Rectangle destRectangle, Rectangle sourceRectangle, Texture2D texture, int updatesPerFrame, int frameCount, float scale, bool isFacingRight) {
        GameObject effect = new GameObject(destRectangle.X, destRectangle.Y);
        Animation effectAnimation = new Animation(texture, updatesPerFrame, frameCount, (int)sourceRectangle.Height, (int)sourceRectangle.Width);
        VisualEffectRenderer effectRenderer = new VisualEffectRenderer(destRectangle, effectAnimation, isFacingRight);
        effectRenderer.effectScale = scale;
        effect.AddComponent(effectRenderer);
        effect.type = "VFX";
        GOManager.Instance.allGOs.Add(effect);
        return effect;
    }
}