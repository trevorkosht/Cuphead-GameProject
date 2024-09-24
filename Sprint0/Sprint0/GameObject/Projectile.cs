using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Projectile
{
    public enum ProjectileType
    {
        Standard,    // Default
        Spread,      // Spread shot
        Lober,        // Slow-moving projectile
        MegaBlast,    // Very slow-moving projectile
        Chaser,       // Chases enemies (placeholder for now)
        Way8          // Circle attack
    }

    private Vector2 position;
    private Vector2 velocity;
    private Texture2D texture;
    int projectileType;

    public Projectile(float x, float y, Vector2 vel, int type)
    {
        this.position = new Vector2(x, y);
        this.velocity = vel;
        projectileType = type;

        switch (type)
        {
            case 0:
                //texture = GOManager.Instance.textureStorage.GetTexture("DefaultProjectile");
                break;
            case 1:
                //texture = GOManager.Instance.textureStorage.GetTexture("SpreadProjectile");
                break;
            case 2:
                velocity /= 1.5f;
                //texture = GOManager.Instance.textureStorage.GetTexture("LoberProjectile");
                break;
            case 3:
                velocity /= 2;
                //texture = GOManager.Instance.textureStorage.GetTexture("MegaBlastProjectile");
                break;
            case 4:
                //texture = GOManager.Instance.textureStorage.GetTexture("ChaserProjectile");
                break;
            case 5:
                //texture = GOManager.Instance.textureStorage.GetTexture("Way8Projectile");
                break;
            default:
                //texture = GOManager.Instance.textureStorage.GetTexture("StandardProjectile");
                break;
        }

        // Load the texture (this is dummy code for now until we get the actual textures)
        texture = GOManager.Instance.textureStorage.GetTexture("PurpleSpore");
    }

    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        position += velocity * deltaTime;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
    }

    public bool IsOffScreen()
    {
        // Check if the projectile has gone off of the screen (you can adjust this logic as needed)
        return position.X < 0 || position.X > 800;  // 0 and 800 are arbitrary for now
    }
}