using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public abstract class Projectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;
    public bool IsActive { get; set; }

    protected Texture2D spriteTexture;
    protected float spriteScale = 1f;
    protected Rectangle sourceRectangle;
    protected Vector2 origin;
    protected Texture2DStorage textureStorage;
    protected GameObject player;
    protected SpriteRenderer sRend;


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
            Update(gameTime);

        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(spriteTexture, sourceRectangle, Color.White);
    }
}