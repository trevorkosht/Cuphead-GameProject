using Cuphead.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PeashooterProjectile : Projectile
{
    private float speed = 9f;
    private bool isFacingRight;
    private Collider collider;
    private SpriteRenderer spriteRenderer;
    private ProjectileCollision projectileCollision;
    private bool collided;
    private float explosionTimer;
    private const float explosionDuration = 1.0f;
    private const string collisionAnimationName = "PeashooterExplosionAnimation";

    public PeashooterProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
    {
        collided = false;
        explosionTimer = 0f;
        this.spriteRenderer = spriteRenderer;
        this.isFacingRight = isFacingRight;
        if (!isFacingRight)
        {
           spriteRenderer.isFacingRight = false;
        }
    }
    public override void Update(GameTime gameTime)
    {
        
        if(collided)
        {
            explosionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (explosionTimer >= explosionDuration)
            {
                GameObject.Destroy();
                return;
            }
        } else { 
            if(GameObject != null)
            {
                if (isFacingRight)
                {
                    GameObject.Move((int)(speed), 0); // Move right

                }
                else
                {
                    GameObject.Move((int)(-speed), 0); // Move left
                }
            }

            collider = GameObject.GetComponent<Collider>();
            projectileCollision = new ProjectileCollision(GameObject, collider, collisionAnimationName);
            collided = projectileCollision.CollisionCheck();

            Camera camera = GOManager.Instance.Camera;
            if (GameObject.X > camera.Position.X + 1200 || GameObject.X < camera.Position.X || GameObject.Y < camera.Position.Y || GameObject.Y > camera.Position.Y + 720)
            {
                GameObject.Destroy();
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // No changes here, handled by the SpriteRenderer
    }
}
