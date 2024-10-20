using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class PeashooterProjectile : Projectile
{
    private float speed = 5f;
    private bool isFacingRight;
    private Collider collider;
    private SpriteRenderer spriteRenderer;
    private bool collided;
    private float explosionDuration;
    private float explosionTimer;

    public PeashooterProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
    {
        collided = false;
        explosionTimer = 0.0f;
        explosionDuration = 1.0f;
        this.spriteRenderer = spriteRenderer;
        this.isFacingRight = isFacingRight;
        if (!isFacingRight)
        {
           spriteRenderer.isFacingRight = false;
        }
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
            if (isFacingRight)
            {
                GameObject.Move((int)(speed), 0); // Move right

            }
            else
            {
                GameObject.Move((int)(-speed), 0); // Move left
            }

            collider = GameObject.GetComponent<Collider>();
            foreach (GameObject GO in GOManager.Instance.allGOs)
            {
                if(GO.type == null) continue;
                if (GO.type != "PlayerProjectile" && GO.type != "Player" && !GO.type.Contains("Item"))
                {
                    if (collider.Intersects(GO.GetComponent<Collider>()))
                    {
                        HealthComponent enemyHealth = GO.GetComponent<HealthComponent>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.RemoveHealth(10); // Reduce enemy health by 10
                        }
                        spriteRenderer.setAnimation("PeashooterExplosionAnimation");
                        collided = true;
                        return;
                    }
                }
            }

            Camera camera = GOManager.Instance.Camera;
            if (GameObject.X > camera.Position.X + 1200 || GameObject.X < camera.Position.X)
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
