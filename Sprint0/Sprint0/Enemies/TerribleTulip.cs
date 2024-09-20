using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System.Collections.Generic;       // For managing projectiles

public class TerribleTulip : BaseEnemy
{
    private double shootCooldown;
    private List<HomingProjectile> projectiles; // List of projectiles to track
    private Texture2D projectileTexture;        // Store the projectile texture

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(startPosition, hitPoints, texture, storage);
        shootCooldown = 3.0;
        projectiles = new List<HomingProjectile>();
        projectileTexture = storage.GetTexture("Seed"); // Get projectile texture from storage
    }

    public override void Move(GameTime gameTime)
    {
        // Terrible Tulip remains stationary, so no movement
    }

    public override void Shoot(GameTime gametime)
    {
        shootCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
        if (shootCooldown <= 0)
        {
            // Create and shoot a homing projectile towards the player
            projectiles.Add(new HomingProjectile(position, Player.Instance.Position, projectileTexture));
            shootCooldown = 3.0; // Reset the cooldown after shooting
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // Shoot logic handled in the Shoot method
        Shoot(gameTime);

        // Update each homing projectile
        for (int i = 0; i < projectiles.Count; i++)
        {
            projectiles[i].Update(gameTime);

            // Remove projectiles that are no longer active
            if (!projectiles[i].IsActive)
            {
                projectiles.RemoveAt(i);
                i--; // Adjust the index after removal
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (HitPoints <= 0)
        {
            // Handle the death of the tulip
            IsActive = false;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            // Draw the Terrible Tulip sprite
            spriteBatch.Draw(spriteTexture, position, Color.White);

            // Draw each active projectile
            foreach (var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
    }
}
