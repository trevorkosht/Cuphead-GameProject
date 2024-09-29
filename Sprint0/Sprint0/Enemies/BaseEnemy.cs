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

    //TEMPORARY ANIMATION VARIABLES - REMOVE WHILE REFACTORING
    public Rectangle destRectangle { get; set; }
    public bool isFacingRight { get; set; }

    protected KeyValuePair<string, Animation> currentAnimation = new KeyValuePair<string, Animation>();
    protected Dictionary<string, Animation> spriteAnimations = new Dictionary<string, Animation>();


    public abstract void Move(GameTime gameTime);
    public abstract void Shoot(GameTime gameTime);

    // Initialize with position, hitpoints, and the texture
    public virtual void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        IsActive = true;
        spriteTexture = texture;
        textureStorage = storage;
        player = GOManager.Instance.Player;

        destRectangle = new Rectangle((int)GameObject.X, (int)GameObject.Y, 144, 144);
    }

    public virtual void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            Move(gameTime);
            Shoot(gameTime);


            //TEMPORARY
            destRectangle = new Rectangle((int)GameObject.X, (int)GameObject.Y, destRectangle.Width, destRectangle.Height);
            if(currentAnimation.Key != null) {
                spriteAnimations[currentAnimation.Key].updateAnimation();
            }

        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive && spriteTexture != null)
        {
            if(currentAnimation.Key != null) {
                spriteAnimations[currentAnimation.Key].draw(spriteBatch, destRectangle, isFacingRight);
            }
        }
    }


    //TEMPORARY ANIMATION HANDLING CODE - REMOVE WHILE REFACTORING
    public void addAnimation(string animationName, Animation animation) {
        spriteAnimations.Add(animationName, animation);
    }

    public bool removeAnimation(string animationName) {
        return spriteAnimations.Remove(animationName);
    }

    public void loadAllAnimations() {
        foreach (var animationKVPair in spriteAnimations) {
            if (animationKVPair.Value.Frames.Count == 0) {
                animationKVPair.Value.loadFrames();
            }
        }
    }

    public void changeDirection() {
        isFacingRight = !isFacingRight;
    }

    public void moveAnimation(int deltaX, int deltaY) {
        destRectangle = new Rectangle(destRectangle.Location.X + deltaX, destRectangle.Location.Y + deltaY, destRectangle.Width, destRectangle.Height);
    }

    public void moveAnimation(Rectangle newDestRectangle) {
        destRectangle = newDestRectangle;
    }

    public void setAnimation(string animationName) {
        if (spriteAnimations.ContainsKey(animationName)) {
            spriteAnimations[animationName].resetAnimation();
            currentAnimation = new KeyValuePair<string, Animation>(animationName, spriteAnimations[animationName]);
        }
    }
}
