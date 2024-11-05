using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Cuphead.Projectiles;
using Microsoft.Xna.Framework.Audio;

public class LobberProjectile : Projectile
{
    private float speed = 6f;
    private float gravity = 0.2f;
    private Vector2 velocity;
    private bool isFacingRight;
    private int bounceCount = 0;
    private int maxBounces = 3;
    private Collider collider;
    private SpriteRenderer spriteRenderer;
    private bool collided;
    private float explosionTimer;
    private ProjectileCollision projectileCollision;
    private const float explosionDuration = 1.0f;
    private const string collisionAnimationName = "LobberExplosionAnimation";
    private SoundEffectInstance impactSoundInstance;

    public LobberProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
    {
        collided = false;
        explosionTimer = 0.0f;
        this.spriteRenderer = spriteRenderer;
        this.isFacingRight = isFacingRight;
        if (!isFacingRight)
        {
            spriteRenderer.isFacingRight = false;
        }
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        velocity = new Vector2(speed, -speed);
        impactSoundInstance = GOManager.Instance.audioManager.getNewInstance("LobberShotImpact");
    }

    public override void Update(GameTime gameTime)
    {
        if (collided)
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

            if (isFacingRight)
            {
                GameObject.Move((int)velocity.X, (int)velocity.Y);
            }
            else
            {
                GameObject.Move((int)(-velocity.X), (int)velocity.Y);
            }

            if (GameObject.Y > 600)
            {
                velocity.Y = -velocity.Y * 0.7f;
                bounceCount++;
                GameObject.MoveToPosition(GameObject.X, 600);

                if (bounceCount >= maxBounces)
                {
                    GameObject.Destroy();
                    return;
                }
            }

            collider = GameObject.GetComponent<Collider>();
            projectileCollision = new ProjectileCollision(GameObject, collider, collisionAnimationName);
            collided = projectileCollision.CollisionCheck();
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Drawing is handled by the Sprite Renderer
    }
}
