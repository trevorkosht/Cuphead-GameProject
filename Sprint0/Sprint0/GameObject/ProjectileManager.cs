using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class ProjectileManager : IComponent {
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    public List<Projectile> activeProjectiles = new List<Projectile>();

    public ProjectileManager(GameObject gameObject, bool enabled) {
        GameObject = gameObject;
        this.enabled = enabled;
    }

    public void Update(GameTime gameTime) {
        foreach (var projectile in activeProjectiles) {
            projectile.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch) { 


    }
}
