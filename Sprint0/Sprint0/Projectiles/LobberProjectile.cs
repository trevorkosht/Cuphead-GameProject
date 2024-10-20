using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class LobberProjectile : Projectile
{
    private float speed = 5f;
    private float gravity = 0.2f;
    private Vector2 velocity;
    private bool isFacingRight;
    private int bounceCount = 0;
    private int maxBounces = 3;
    private Collider collider;
    private SpriteRenderer spriteRenderer;
    private bool collided;
    private float explosionDuration;
    private float explosionTimer;

    public LobberProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
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

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        velocity = new Vector2(speed, -speed);
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
            foreach (GameObject GO in GOManager.Instance.allGOs)
            {
                if (GO.type != "PlayerProjectile" && GO.type != "Player" && !GO.type.Contains("Item"))
                {
                    if (collider.Intersects(GO.GetComponent<Collider>()))
                    {
                        HealthComponent enemyHealth = GO.GetComponent<HealthComponent>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.RemoveHealth(10); // Reduce enemy health by 10
                        }
                        spriteRenderer.setAnimation("LobberExplosionAnimation");
                        collided = true;
                        return;
                    }
                }

            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Drawing is handled by the Sprite Renderer
    }
}
