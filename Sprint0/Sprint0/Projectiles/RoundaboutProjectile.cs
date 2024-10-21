using Cuphead.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class RoundaboutProjectile : Projectile
{
    private float speed = 5f;
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

    public RoundaboutProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
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

        // Set initial horizontal velocity
        velocity = new Vector2(isFacingRight ? speed : -speed, 0);

        playerLaunchPosition = GOManager.Instance.Player.position;

        returning = false;
        elapsedTime = 0f;
    }

    public override void Update(GameTime gameTime)
    {
        if (collided)
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
            if (IsActive)
            {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Switch to returning phase after the launch duration
                if (!returning && elapsedTime >= launchDuration)
                {
                    returning = true;

                    // In returning phase, move diagonally upwards while traveling back
                    velocity = new Vector2(isFacingRight ? -speed : speed, -speed / 2);
                }

                // Update the projectile's position based on its velocity
                GameObject.Move((int)velocity.X, (int)velocity.Y);


                collider = GameObject.GetComponent<Collider>();
                projectileCollision = new ProjectileCollision(GameObject, collider, collisionAnimationName);
                collided = projectileCollision.CollisionCheck();

                Camera camera = GOManager.Instance.Camera;
                if (returning && (GameObject.X > camera.Position.X + 1200 || GameObject.X < camera.Position.X ||  GameObject.Y < camera.Position.Y || GameObject.Y > camera.Position.Y + 720))
                {
                    GameObject.Destroy();
                }


            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Do Nothing, handled by Sprite Renderer
    }
}
