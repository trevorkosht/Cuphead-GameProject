using Cuphead.Projectiles;
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
        // Create bullets once per shot
        for (int i = 0; i < directions.Length; i++)
        {
            GameObject spreadShot = new GameObject(GameObject.X, GameObject.Y);
            spreadShot.type = "PlayerProjectile";

            SpriteRenderer spriteRenderer = new SpriteRenderer(new Rectangle(spreadShot.X, spreadShot.Y, 144, 144), false);
            spriteRenderer.spriteScale = 0.5f;

            Collider collider = new BoxCollider(new Vector2(60, 45), new Vector2(15, 15), GOManager.Instance.GraphicsDevice);
            spreadShot.AddComponent(collider);

            var spreadLogic = new SpreadShotInstance(directions[i] * speed, spriteRenderer);
            spriteRenderer.addAnimation("SpreadAnimation", new Animation(textureStorage.GetTexture("Spread"), 5, 4, 144, 144));
            spriteRenderer.addAnimation("SpreadExplosionAnimation", new Animation(textureStorage.GetTexture("SpreadExplosion"), 5, 5, 144, 144));
            spriteRenderer.setAnimation("SpreadAnimation");
            spriteRenderer.isFacingRight = isFacingRight;

            spreadShot.AddComponent(spreadLogic);
            spreadLogic.Initialize(spriteTexture, textureStorage);
            spreadShot.AddComponent(spriteRenderer);

            GOManager.Instance.allGOs.Add(spreadShot);
        }

        GameObject.Destroy(); // Destroy the parent spread shot after creating bullets
   
    }
}

public class SpreadShotInstance : Projectile
{
    private Vector2 direction;
    private float lifetime = .5f;
    private float explosionTimer;
    private bool collided, lifetimeExpired;
    private SpriteRenderer spriteRenderer;
    private ProjectileCollision projectileCollision;
    private const float explosionDuration = 0.425f;
    private const string collisionAnimationName = "RoundaboutExplosionAnimation";

    public SpreadShotInstance(Vector2 direction, SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
        explosionTimer = 0.0f;
        collided = false;
        lifetimeExpired = false;
        this.direction = direction;
        if(direction.X < 0)
        {
            spriteRenderer.isFacingRight = false;
        }
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
    }

    public override void Update(GameTime gameTime)
    {
        if (GameObject == null)
            return;

        if (collided || lifetimeExpired)
        {
            explosionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (explosionTimer >= explosionDuration)
            {
                GameObject.Destroy();
                return;
            }
        }
        else
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            GameObject.Move((int)(direction.X * deltaTime), (int)(direction.Y * deltaTime));

            lifetime -= deltaTime;
            Camera camera = GOManager.Instance.Camera;
            if (lifetime <= 0)
            {
                spriteRenderer.setAnimation("SpreadExplosionAnimation");
                lifetimeExpired = true;
                return;
            }

            Collider collider = GameObject.GetComponent<Collider>();
            projectileCollision = new ProjectileCollision(GameObject, collider, collisionAnimationName);
            collided = projectileCollision.CollisionCheck();
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // No changes here, handled by the SpriteRenderer
    }
}
