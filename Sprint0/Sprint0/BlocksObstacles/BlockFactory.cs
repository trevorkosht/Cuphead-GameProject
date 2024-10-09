using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BlockFactory {
    public static GameObject createBlock(Rectangle destRectangle, Texture2D texture) {
        GameObject block = new GameObject(destRectangle.X, destRectangle.Y);
        SpriteRenderer spriteRenderer = new SpriteRenderer(destRectangle, true);
        block.AddComponent(spriteRenderer);
        Animation blockTexture = new Animation(texture, 1, 1, 144, 144);
        spriteRenderer.addAnimation("texture",blockTexture);
        spriteRenderer.loadAllAnimations();
        spriteRenderer.setAnimation("texture");

        BoxCollider boxCollider = new BoxCollider(new Vector2(destRectangle.Width, destRectangle.Height), new Vector2(0, 0), GOManager.Instance.GraphicsDevice);
        block.AddComponent(boxCollider);

        GOManager.Instance.allGOs.Add(block);
        return block;
    }
}