using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;
using System;

public class ChaserProjectile : Projectile
{
    private float speed = 2f; // Adjust speed based on desired behavior
    private GameObject targetEnemy; // The enemy that this projectile will chase
    private Vector2 lastDirection; // Stores the last direction of movement

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        lastDirection = Vector2.UnitX; // Set an initial direction (default is to the right)
    }

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            // Find the nearest enemy if no target is set or if the current target is destroyed
            if (targetEnemy == null || targetEnemy.destroyed)
            {
                targetEnemy = GOManager.Instance.currentEnemy;
            }

            Vector2 direction;

            if (targetEnemy != null)
            {
                // Calculate direction towards the target enemy
                direction = Vector2.Normalize(targetEnemy.position - GameObject.position);

                // Store the direction as the last known direction
                lastDirection = direction;

                // Destroy the projectile if it reaches the target enemy
                if (Vector2.Distance(GameObject.position, targetEnemy.position) < 20f) // Adjust this threshold for collision
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

            // Move the projectile in the calculated direction
            GameObject.Move((int)(direction.X * speed), (int)(direction.Y * speed));

            // Destroy the projectile if it goes off-screen
            if (GameObject.X > 1200 || GameObject.X < 0 || GameObject.Y > 800 || GameObject.Y < 0)
            {
                GameObject.Destroy();
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Do Nothing, handled by Sprite Renderer
    }
}
