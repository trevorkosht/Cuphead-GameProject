using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;
using System;

public class ChaserProjectile : Projectile
{
    private float speed = 5f;
    private GameObject targetEnemy;
    private Vector2 lastDirection;
    private bool isFacingRight;
    private Collider collider;
    private SpriteRenderer spriteRenderer;
    private bool collided;
    private float explosionDuration;
    private float explosionTimer;

    public ChaserProjectile(bool isFacingRight, SpriteRenderer spriteRenderer)
    {
        collided = false;
        explosionTimer = 0.0f;
        explosionDuration = 0.5f;
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
        lastDirection = Vector2.UnitX;
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
                if (targetEnemy == null || targetEnemy.destroyed)
                {
                    targetEnemy = GOManager.Instance.currentEnemy;
                }

                Vector2 direction;

                if (targetEnemy != null)
                {
                    direction = Vector2.Normalize(targetEnemy.position - GameObject.position);

                    lastDirection = direction;

                    if (Vector2.Distance(GameObject.position, targetEnemy.position) < 20f)
                    {
                        GameObject.Destroy();
                        return;
                    }
                }
                else
                {
                    // No enemy detected, continue in the last known direction
                    direction = lastDirection;
                }

                GameObject.Move((int)(direction.X * speed), (int)(direction.Y * speed));

                collider = GameObject.GetComponent<Collider>();
                foreach (GameObject GO in GOManager.Instance.allGOs)
                {
                    if (GO.type != "PlayerProjectile" && GO.type != "Player")
                    {
                        if (collider.Intersects(GO.GetComponent<Collider>()))
                        {
                            spriteRenderer.setAnimation("ChaserExplosionAnimation");
                            collided = true;
                            return;
                        }
                    }
                }

                if (GameObject.X > 1200 || GameObject.X < 0 || GameObject.Y > 800 || GameObject.Y < 0)
                {
                    GameObject.Destroy();
                    return;
                }
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Do Nothing, handled by Sprite Renderer
    }
}
