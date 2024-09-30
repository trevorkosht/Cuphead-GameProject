using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class LobberProjectile : Projectile
{
    private float speed = 5f;
    private float gravity = 0.2f;
    private Vector2 velocity;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        velocity = new Vector2(speed, -speed); // Initial upward velocity
    }

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            velocity.Y += gravity; // Apply gravity
            GameObject.Move((int)velocity.X, (int)velocity.Y); // Update position

            // Deactivate after it goes below the screen or if it hits the ground (you might need additional bounce logic)
            if (GameObject.Y > 600) // Assuming this is the bottom of the screen
            {
                IsActive = false;
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // DO Nothing, Handled by Sprite Renderer
    }
}
