using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MovingNonAnimatedSprite : ISprite
{
    private Texture2D texture;
    private Vector2 position;
    private float speed = 2f; 
    float range = 50f;
    float time;

    public MovingNonAnimatedSprite(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
    }

    public void Update(GameTime gameTime)
    {
        time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        position.Y += (float)Math.Sin(time * speed) * range * (float)gameTime.ElapsedGameTime.TotalSeconds;

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        
        Vector2 scale = new Vector2(5f, 5f);
        Rectangle sourceRectangle = new Rectangle(10, 0, 18, 25);

        spriteBatch.Draw(
            texture: texture,
            position: position,
            sourceRectangle: sourceRectangle, 
            color: Color.White,
            rotation: 0f,
            origin: Vector2.Zero,
            scale: scale,
            effects: SpriteEffects.None,
            layerDepth: 0f
        );
    }
}
