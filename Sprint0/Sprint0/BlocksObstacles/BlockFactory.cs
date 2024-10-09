using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BlockFactory {
    public static GameObject createBlock(Rectangle blockRectangle, Texture2D texture) {
        GameObject block = new GameObject(blockRectangle.X, blockRectangle.Y);
        SpriteRenderer spriteRenderer = new SpriteRenderer(blockRectangle, true);
        block.AddComponent(spriteRenderer);
        Animation blockTexture = new Animation(texture, 1, 1, 144, 144);
        spriteRenderer.addAnimation("texture",blockTexture);
        spriteRenderer.loadAllAnimations();
        spriteRenderer.setAnimation("texture");

        BoxCollider boxCollider = new BoxCollider(new Vector2(blockRectangle.Height, blockRectangle.Width), new Vector2(0, 0), GOManager.Instance.GraphicsDevice);
        block.AddComponent(boxCollider);

        GOManager.Instance.allGOs.Add(block);
        return block;
    }
}