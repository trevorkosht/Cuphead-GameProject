using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Linq;

public class ChaserProjectile : Projectile
{
    private float speed = 2f; // Adjust speed based on desired behavior
    private GameObject targetEnemy; // The enemy that this projectile will chase


    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            // Find the nearest enemy if no target is set or if the current target is destroyed
                targetEnemy = GOManager.Instance.currentEnemy;


            if (targetEnemy != null)
            {
                // Calculate direction towards the target enemy
                Vector2 direction = Vector2.Normalize(targetEnemy.position - GameObject.position);
                GameObject.Move((int)(direction.X * speed), (int)(direction.Y * speed));
            }

            // Deactivate if the projectile goes off-screen (assuming screen boundaries)
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
