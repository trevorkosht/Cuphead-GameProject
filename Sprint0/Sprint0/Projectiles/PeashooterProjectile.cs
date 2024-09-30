using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class PeashooterProjectile : Projectile
{
    private float speed = 5f;

    public override void Update(GameTime gameTime)
    {
        if (IsActive)
        {
            // Move straight in the X direction
            GameObject.Move((int)speed, 0);

            // Deactivate when it goes off-screen
            if (GameObject.X > 800) // Assuming 800 is the right boundary of the screen
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
