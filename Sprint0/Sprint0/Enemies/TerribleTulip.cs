using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D


public class TerribleTulip : BaseEnemy
{
    private double shootCooldown;

    public override void Move(GameTime gameTime)
    {
        // No movement for Terrible Tulip
    }

    public override void Shoot()
    {
        shootCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
        if (shootCooldown <= 0)
        {
            // Shoot homing projectile logic
            shootCooldown = 3.0; // Reset cooldown
        }
    }

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture)
    {
        base.Initialize(startPosition, hitPoints, texture);
        shootCooldown = 3.0;
    }
}
