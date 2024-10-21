using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class ChaserProjectile : Projectile
{
    private float speed = 9f;
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
        lastDirection = isFacingRight ? Vector2.UnitX : -Vector2.UnitX; // Start based on facing direction
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
                // Find the closest enemy in the direction the player is facing
                targetEnemy = FindClosestEnemy();

                Vector2 direction;

                if (targetEnemy != null)
                {
                    direction = Vector2.Normalize(targetEnemy.position - GameObject.position);
                    lastDirection = direction;

                    // Destroy the projectile if it's very close to the enemy
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

                // Move in the direction of the target or last direction
                GameObject.Move((int)(direction.X * speed), (int)(direction.Y * speed));

                // Check for collisions with other game objects
                collider = GameObject.GetComponent<Collider>();
                foreach (GameObject GO in GOManager.Instance.allGOs)
                {
                    if (GO.type == null) continue;
                    if (GO.type != "PlayerProjectile" && GO.type != "Player" && !GO.type.Contains("Item"))
                    {
                        if (collider.Intersects(GO.GetComponent<Collider>()))
                        {

                            HealthComponent enemyHealth = GO.GetComponent<HealthComponent>();
                            if (enemyHealth != null)
                            {
                                enemyHealth.RemoveHealth(10); // Reduce enemy health by 10
                            }

                            spriteRenderer.setAnimation("ChaserExplosionAnimation");
                            collided = true;
                            return;
                        }
                    }

                }

                // Check if projectile is out of bounds
                Camera camera = GOManager.Instance.Camera;
                if (GameObject.X > camera.Position.X + 1200 || GameObject.X < camera.Position.X || GameObject.Y < camera.Position.Y || GameObject.Y > camera.Position.Y + 720)
                {
                    GameObject.Destroy();
                }
            }
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var gameObject in GOManager.Instance.allGOs)
        {
            // Check if the game object is an enemy
            if(gameObject.type == null) continue;
            if (gameObject.type.Contains("Enemy"))
            {
                Vector2 directionToEnemy = gameObject.position - GameObject.position;
                float distance = directionToEnemy.Length();

                // Filter enemies based on the direction the player is facing
                if ((isFacingRight && directionToEnemy.X > 0) || (!isFacingRight && directionToEnemy.X < 0))
                {
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = gameObject;
                    }
                }
            }
        }

        return closestEnemy;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Do Nothing, handled by Sprite Renderer
    }
}
