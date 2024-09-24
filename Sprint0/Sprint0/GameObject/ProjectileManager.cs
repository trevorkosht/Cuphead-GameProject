using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class ProjectileManager : IComponent {
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    public List<Projectile> activeProjectiles = new List<Projectile>();

    public int projectileType = 0;

    public ProjectileManager() {
    }

    public void FireProjectile(float playerX, float playerY, bool isFacingRight)
    {
        // Define the starting position for the projectile based on player's position
        float projectileX = isFacingRight ? playerX + 20 : playerX - 20;  // Offset slightly from the player
        float projectileY = playerY;  // Shoot from the player's height

        // Define the velocity (positive for right, negative for left)
        Vector2 velocity = new Vector2(isFacingRight ? 600f : -600f, 0f);

        // Create a new projectile and add it to the list
        Projectile newProjectile = new Projectile(projectileX, projectileY, velocity, projectileType);
        activeProjectiles.Add(newProjectile);
    }

    public void Update(GameTime gameTime) {
        foreach (var projectile in activeProjectiles) {
            projectile.Update(gameTime);
        }

        activeProjectiles.RemoveAll(p => p.IsOffScreen()); //Remove projectile if off-screen
    }

    public void Draw(SpriteBatch spriteBatch) {

        foreach (var projectile in activeProjectiles)
        {
            projectile.Draw(spriteBatch);
        }
    }
}
