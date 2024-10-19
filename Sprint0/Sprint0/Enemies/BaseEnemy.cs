using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public abstract class BaseEnemy : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;
    public int HitPoints { get; protected set; }

    protected Texture2D spriteTexture;
    protected Texture2D deathVFXTexture = GOManager.Instance.textureStorage.GetTexture("EnemyDeath");
    protected float spriteScale = 1f;     
    protected Rectangle sourceRectangle; 
    protected Vector2 origin;           
    protected Texture2DStorage textureStorage;
    protected GameObject player;
    protected SpriteRenderer sRend;

    public abstract void Move(GameTime gameTime);
    public abstract void Shoot(GameTime gameTime);

    public virtual void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        spriteTexture = texture;
        textureStorage = storage;
        player = GOManager.Instance.Player;
        sRend = GameObject.GetComponent<SpriteRenderer>();
    }

    public virtual void Update(GameTime gameTime)
    {
        if(GameObject.GetComponent<HealthComponent>().currentHealth == 0) {
            Rectangle destRectangle = new Rectangle(GameObject.X - 72, GameObject.Y - 72, 144, 144);
            VisualEffectFactory.createVisualEffect(destRectangle, deathVFXTexture, 3, 9, 2f, true);
        }

        Move(gameTime);
        Shoot(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
    }
}
