using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class SpreadShotProjectile : Projectile
{
    private float speed = 5f;

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            // Create and shoot three projectiles in a cone
            for (int i = -1; i <= 1; i++) // Loop for three shots
            {
                Vector2 shotDirection = new Vector2((float)Math.Cos(MathHelper.ToRadians(15 * i)), (float)Math.Sin(MathHelper.ToRadians(15 * i))); // 15 degrees spread
                GameObject shot = new GameObject(GameObject.X, GameObject.Y); // Create new shot
                shot.Move((int)(shotDirection.X * speed), (int)(shotDirection.Y * speed));

                // Logic to add the shot to the game (you might want to instantiate it properly)
                // Example: GOManager.Instance.AddGameObject(shot);
            }

            // Deactivate after shooting
            IsActive = false;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Optionally draw the original shot or handle differently if multiple shots created
        // DO Nothing, Handled by Sprite Renderer
    }
}
