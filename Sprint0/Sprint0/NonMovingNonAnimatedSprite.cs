using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class NonMovingNonAnimatedSprite : ISprite
{
    private Texture2D texture;
    private Vector2 position;

    public NonMovingNonAnimatedSprite(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
    }

    public void Update(GameTime gameTime)
    {
        // No movement or animation logic needed
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 scale = new Vector2(5f, 5f);

        Rectangle sourceRectangle = new Rectangle(10, 0, 18, 25);


        spriteBatch.Draw(
        texture: texture,           // (sprite sheet)
        position: position,  
        sourceRectangle: sourceRectangle, // Portion of the sprite sheet to draw
        color: Color.White,           
        rotation: 0f,                 
        origin: Vector2.Zero,         
        scale: scale,                 
        effects: SpriteEffects.None,  
        layerDepth: 0f                
        ); ;
    }
}
