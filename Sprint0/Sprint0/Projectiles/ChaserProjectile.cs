using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class ChaserProjectile : Projectile
{
    private float speed = 2f;

    public override void Update(GameTime gameTime)
    {
        if (IsActive && player != null)
        {
            // Calculate direction towards the player
            Vector2 direction = Vector2.Normalize(player.position - GameObject.position);
            GameObject.Move((int)(direction.X * speed), (int)(direction.Y * speed));

            // Deactivate if it goes off-screen
            if (GameObject.X > 800 || GameObject.Y > 600) // Assuming these are the screen boundaries
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
