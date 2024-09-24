using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class ProjectileManager : IComponent {
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    public List<GameObject> bullets = new List<GameObject>();

    public int projectileType = 0;

    public ProjectileManager() {
    }

    public void FireProjectile(float playerX, float playerY, bool isFacingRight)
    {
        // Define the starting position for the projectile based on player's position
        float projectileX = isFacingRight ? playerX + 70 : playerX - 25;  // Offset slightly from the player
        float projectileY = playerY + 30;  // Shoot from the player's height

        // Define the velocity (positive for right, negative for left)
        Vector2 velocity = new Vector2(isFacingRight ? 800f : -800f, 0f);

        // Create a new bullet GO and add it to the list
        if(projectileType == 2)
        {
            Vector2 velocity2 = new Vector2(isFacingRight ? 800f : -800f, 100f);
            Vector2 velocity3 = new Vector2(isFacingRight ? 800f : -800f, -100f);
            GameObject bullet2 = new GameObject((int)projectileX, (int)projectileY, new Projectile(projectileX, projectileY, velocity2, projectileType));
            GameObject bullet3 = new GameObject((int)projectileX, (int)projectileY, new Projectile(projectileX, projectileY, velocity3, projectileType));
            //bullet2.AddComponent(new SpriteRenderer(bullet2, true, new Rectangle((int)projectileX, (int)projectileY, 144, 144), true));
            //bullet3.AddComponent(new SpriteRenderer(bullet3, true, new Rectangle((int)projectileX, (int)projectileY, 144, 144), true));
            bullets.Add(bullet2);
            bullets.Add(bullet3);
        }
        GameObject bullet = new GameObject((int)projectileX, (int)projectileY, new Projectile(projectileX, projectileY, velocity, projectileType));
        //bullet.AddComponent(new SpriteRenderer(bullet, true, new Rectangle((int)projectileX, (int)projectileY, 144, 144), true));
        bullets.Add(bullet);
    }

    public void Update(GameTime gameTime) {
        foreach (GameObject GO in bullets) {
            GO.Update(gameTime);
        }

        bullets.RemoveAll(p => p.GetComponent<Projectile>().IsOffScreen()); //Remove projectile if off-screen
    }

    public void Draw(SpriteBatch spriteBatch) {

        foreach (GameObject GO in bullets)
        {
            GO.Draw(spriteBatch);
        }
    }
}
