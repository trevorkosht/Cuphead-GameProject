using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class BaseEnemy : IEnemy
{
    public int HitPoints { get; protected set; }
    public bool IsActive { get; set; }
    protected Vector2 position;

    protected Texture2D spriteTexture;    // Holds the texture for the enemy sprite
    protected float spriteScale = 1f;     // Scaling factor for the sprite
    protected Rectangle sourceRectangle;  // Rectangle for sprite sheet animation
    protected Vector2 origin;             // Origin point for the sprite

    public abstract void Move(GameTime gameTime);
    public abstract void Shoot();

    // Initialize with position, hitpoints, and the texture
    public virtual void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture)
    {
        position = startPosition;
        HitPoints = hitPoints;
        IsActive = true;
        spriteTexture = texture;

        // Set the sourceRectangle to the entire texture if no animation (single frame)
        if (spriteTexture != null)
        {
            sourceRectangle = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
            origin = new Vector2(spriteTexture.Width / 2, spriteTexture.Height / 2);
        }
    }

    public virtual void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            Move(gameTime);
            Shoot();
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive && spriteTexture != null)
        {
            // Draw the sprite
            spriteBatch.Draw(
                spriteTexture,
                position,                   // Position of the sprite
                sourceRectangle,            // Part of the texture to draw (whole image in this case)
                Color.White,                // Tint color
                0f,                         // Rotation (0 = no rotation)
                origin,                     // Origin point (center)
                spriteScale,                // Scale of the sprite
                SpriteEffects.None,         // Effects (flipping, etc.)
                0f                          // Layer depth
            );
        }
    }

    public virtual void TakeDamage(int damage)
    {
        HitPoints -= damage;
        if (HitPoints <= 0)
        {
            IsActive = false;
        }
    }
}
