using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class ProjectileManager : IComponent {
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    public List<Projectile> activeProjectiles = new List<Projectile>();

    public int projectileType = 1;

    public ProjectileManager(GameObject gameObject, bool enabled) {
        GameObject = gameObject;
        this.enabled = enabled;
    }

    public void FireProjectile(/*Rectangle collider, Vector2 velocity, int damageAmount*/) {
        //activeProjectiles.Add(new Projectile(collider.X, collider.Y, new SpriteRenderer(GameObject, true, collider, true), collider.X, collider.Y, damageAmount));
    }

    public void Update(GameTime gameTime) {
        foreach (var projectile in activeProjectiles) {
            projectile.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch) { 


    }
}
