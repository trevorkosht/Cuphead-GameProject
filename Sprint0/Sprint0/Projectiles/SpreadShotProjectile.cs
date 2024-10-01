using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;

public class SpreadShotProjectile : Projectile
{
    private float speed = 500f;
    private float angleSpread = 45f;
    private Vector2[] directions;
    bool isFacingRight;

    public SpreadShotProjectile(bool isFacingRight)
    {
        this.isFacingRight = isFacingRight;

        directions = new Vector2[]
        {
            new Vector2((float)Math.Cos(MathHelper.ToRadians(-angleSpread)), (float)Math.Sin(MathHelper.ToRadians(-angleSpread))),
            new Vector2(1, 0), 
            new Vector2((float)Math.Cos(MathHelper.ToRadians(angleSpread)), (float)Math.Sin(MathHelper.ToRadians(angleSpread)))
        };

        // Flip the direction vectors horizontally if facing left
        if (!isFacingRight)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                directions[i].X = -directions[i].X;
            }
        }
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
    }

    public override void Update(GameTime gameTime)
    {
        for (int i = 0; i < directions.Length; i++)
        {
            GameObject spreadShot = new GameObject(GameObject.X, GameObject.Y);
            
            SpriteRenderer spriteRenderer = new SpriteRenderer(new Rectangle(spreadShot.X, spreadShot.Y, 144, 144), false);
            spriteRenderer.spriteScale = 0.5f;

            var spreadLogic = new SpreadShotInstance(directions[i] * speed, spriteRenderer);
            spriteRenderer.addAnimation("SpreadAnimation", new Animation(textureStorage.GetTexture("Spread"), 5, 4, 144, 144));
            spriteRenderer.setAnimation("SpreadAnimation");
            spriteRenderer.isFacingRight = isFacingRight;

            spreadShot.AddComponent(spreadLogic);
            spreadLogic.Initialize(spriteTexture, textureStorage);
            spreadShot.AddComponent(spriteRenderer);


            GOManager.Instance.allGOs.Add(spreadShot);
            spriteRenderer.loadAllAnimations();
        }

        GameObject.Destroy();
    }
}

public class SpreadShotInstance : Projectile
{
    private Vector2 direction;
    private float lifetime = 2.0f;

    public SpreadShotInstance(Vector2 direction, SpriteRenderer spriteRendeer)
    {
        this.direction = direction;
        if(direction.X < 0)
        {
            spriteRendeer.isFacingRight = false;
        }
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        GameObject.Move((int)(direction.X * deltaTime), (int)(direction.Y * deltaTime));

        lifetime -= deltaTime;
        if (GameObject.X > player.X + 200 || GameObject.X < 0 || GameObject.Y > 800 || GameObject.Y < 0 || GameObject.Y < player.Y - 200 || lifetime <= 0)
        {
            GameObject.Destroy();
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // No changes here, handled by the SpriteRenderer
    }
}
