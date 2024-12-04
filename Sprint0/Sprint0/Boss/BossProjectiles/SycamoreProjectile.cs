using Cuphead;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SycamoreProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 velocity;
    private bool returning;
    private float speed = 500f;

    public SycamoreProjectile(Vector2 startPosition)
    {
        // Start traveling to the left
        velocity = new Vector2(-speed, 0);
        returning = false;
    }

    public void Update(GameTime gameTime)
    {
        GOManager.Instance.audioManager.getInstance("BoomerangLoop").Play();
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!returning)
        {
            // Move left
            GameObject.Move((int)(velocity.X * delta), 0);

            // Check if it has reached X = -50
            if (GameObject.X <= -400)
            {
                // Switch to returning mode
                returning = true;

                // Update velocity to travel right and add +200 to Y
                velocity = new Vector2(speed, 0);
                GameObject.Y += 200;
            }
        }
        else
        {
            // Move right
            GameObject.Move((int)(velocity.X * delta), 0);

            // Destroy when off-screen to the right (optional threshold, e.g., screen width)
            if (GameObject.X > 800) // Replace 800 with screen width if dynamic
            {
                GameObject.Destroy();
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch) { }
}
