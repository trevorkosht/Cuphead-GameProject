using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public abstract class BaseEnemy : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;
    public int HitPoints { get; protected set; }
    public bool IsActive { get; set; }

    protected Texture2D spriteTexture;    // Holds the texture for the enemy sprite
    protected float spriteScale = 1f;     // Scaling factor for the sprite
    protected Rectangle sourceRectangle;  // Rectangle for sprite sheet animation
    protected Vector2 origin;             // Origin point for the sprite
    protected Texture2DStorage textureStorage;
    protected GameObject player;
    protected SpriteRenderer sRend;

    public abstract void Move(GameTime gameTime);
    public abstract void Shoot(GameTime gameTime);

    // Initialize with and the texture
    public virtual void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        IsActive = true;
        spriteTexture = texture;
        textureStorage = storage;
        player = GOManager.Instance.Player;
        sRend = GameObject.GetComponent<SpriteRenderer>();
    }

    public virtual void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            Move(gameTime);
            Shoot(gameTime);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        //Do nothing
    }
}
