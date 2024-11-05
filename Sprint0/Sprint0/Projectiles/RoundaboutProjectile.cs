using Cuphead.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

public class RoundaboutProjectile : Projectile
{
    private float speed = 8f;
    private Vector2 velocity;
    private Vector2 playerLaunchPosition;
    private bool returning;
    private bool isFacingRight;
    private Collider collider;
    private SpriteRenderer spriteRenderer;
    private bool collided;
    private float explosionTimer;
    private ProjectileCollision projectileCollision;
    private const float explosionDuration = 1.0f;
    private const string collisionAnimationName = "RoundaboutExplosionAnimation";
    private float launchDuration = 1f;
    private float elapsedTime;
    private SoundEffectInstance impactSoundInstance;
    private Vector2 direction;

    public RoundaboutProjectile(bool isFacingRight, SpriteRenderer spriteRenderer, float angleInDegrees)
    {
        collided = false;
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

        // Set initial horizontal velocity
        velocity = new Vector2(isFacingRight ? speed/2 : -speed/2, speed/2);

        playerLaunchPosition = GOManager.Instance.Player.position;

        returning = false;
        elapsedTime = 0f;
        impactSoundInstance = GOManager.Instance.audioManager.getNewInstance("RoundaboutShotImpact");
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
            if (IsActive)
            {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Switch to returning phase after the launch duration
                if (!returning && elapsedTime >= launchDuration)
                {
                    returning = true;

                    // In returning phase, move diagonally upwards while traveling back
                    velocity = new Vector2(isFacingRight ? -speed : speed, direction.Y * speed);
                }

                // Update the projectile's position based on its velocity
                GameObject.Move((int)(velocity.X * direction.X), (int)(velocity.Y * direction.Y));


                collider = GameObject.GetComponent<Collider>();
                projectileCollision = new ProjectileCollision(GameObject, collider, collisionAnimationName);
                collided = projectileCollision.CollisionCheck();

                Camera camera = GOManager.Instance.Camera;
                //if (returning && (GameObject.X > camera.Position.X + 1200 || GameObject.X < camera.Position.X ||  GameObject.Y < camera.Position.Y || GameObject.Y > camera.Position.Y + 720))
                //{
                //    GameObject.Destroy();
                //}


            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Do Nothing, handled by Sprite Renderer
    }
}
