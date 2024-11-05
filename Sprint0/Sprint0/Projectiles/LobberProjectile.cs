using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Cuphead.Projectiles;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Diagnostics;

public class LobberProjectile : Projectile
{
    private float speedX = 6f, speedY = 10f, flipTime = .2f, timeToFlip = 0;
    private float gravity = 0.2f;
    private Vector2 velocity;
    private bool isFacingRight, flipped = false;
    private float timeAlive = 0;
    private float duration = 4f;
    private Collider collider;
    private SpriteRenderer spriteRenderer;
    private int collided;
    private float explosionTimer;
    private ProjectileCollision projectileCollision;
    private const float explosionDuration = 1.0f;
    private const string collisionAnimationName = "LobberExplosionAnimation";
    private SoundEffectInstance impactSoundInstance;
    private Vector2 direction;

    public LobberProjectile(bool isFacingRight, SpriteRenderer spriteRenderer, float angleInDegrees)
    {
        collided = -1;
        explosionTimer = 0.0f;
        this.spriteRenderer = spriteRenderer;
        this.isFacingRight = isFacingRight;
        if (!isFacingRight)
        {
            spriteRenderer.isFacingRight = false;
        }
        float angleInRadians = MathHelper.ToRadians(angleInDegrees);
        direction = new Vector2((float)Math.Cos(angleInRadians), (float)Math.Sin(angleInRadians));
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        velocity = new Vector2(speedX * direction.X, speedY * direction.Y);
        if (!isFacingRight)
            velocity.X *= -1;
        speedY -= 2;
        impactSoundInstance = GOManager.Instance.audioManager.getNewInstance("LobberShotImpact");
    }

    public override void Update(GameTime gameTime)
    {
        timeAlive += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (collided == 1)
        {
            impactSoundInstance.Play();
            explosionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (explosionTimer >= explosionDuration)
            {
                GameObject.Destroy();
                return;
            }
        }
        else
        {
            impactSoundInstance.Stop();
            velocity.Y += gravity;
            velocity.X += -velocity.X * 2 / 1000;
            speedY -= 10f / 1000f;

            GameObject.Move((int)velocity.X, (int)velocity.Y);

            if (collided == 0)
            {
                velocity.Y = -speedY;
                timeToFlip += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(!flipped && timeToFlip >= flipTime)
                {
                    velocity.X *= -1;
                    flipped = true;
                }

                GameObject.Move(0, -2);
                //GameObject.MoveToPosition(GameObject.X, 600);

                if (timeAlive >= duration)
                {
                    collided = 1;
                    spriteRenderer.setAnimation(collisionAnimationName);
                    return;
                }
            }
            else
            {
                timeToFlip = 0;
                flipped = false;
            }

            collider = GameObject.GetComponent<Collider>();
            projectileCollision = new ProjectileCollision(GameObject, collider, collisionAnimationName);
            collided = projectileCollision.LobberCollisionCheck();
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Drawing is handled by the Sprite Renderer
    }
}
