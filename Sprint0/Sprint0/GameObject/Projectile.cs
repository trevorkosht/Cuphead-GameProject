using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Projectile
{
    private Vector2 position;
    private Vector2 velocity;
    private Texture2D texture;

    public Projectile(float x, float y, Vector2 velocity)
    {
        this.position = new Vector2(x, y);
        this.velocity = velocity;

        // Load the texture (you should replace this with actual texture loading logic)
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