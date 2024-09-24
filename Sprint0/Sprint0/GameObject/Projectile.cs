using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Projectile : IComponent
{

    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;

    private Vector2 position;
    private Vector2 velocity;
    private Texture2D texture;
    int projectileType, damage;

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
                velocity /= 2;
                //texture = GOManager.Instance.textureStorage.GetTexture("MegaBlastProjectile");
                break;
            case 2:
                //texture = GOManager.Instance.textureStorage.GetTexture("SpreadProjectile");
                break;
            case 3:
                //texture = GOManager.Instance.textureStorage.GetTexture("RoundaboutProjectile");
                velocity /= 2;
                break;
            case 4:
                //texture = GOManager.Instance.textureStorage.GetTexture("ChaserProjectile");
                break;
            case 5:
                velocity /= 1.5f;
                //texture = GOManager.Instance.textureStorage.GetTexture("LoberProjectile");
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
        return position.X < 0 || position.X > 1200;  // 0 and 800 are arbitrary for now
    }
}