using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SycamoreProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 velocity;
    private bool returning;
    private float speed = 200f;

    public SycamoreProjectile(Vector2 startPosition)
    {
        velocity = new Vector2(speed, 0);
        returning = false;
    }

    public void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (!returning)
        {
            GameObject.Move((int)(velocity.X * delta), 0);
            if (GameObject.X > 800) returning = true; // Replace 800 with screen width if dynamic.
        }
        else
        {
            GameObject.Move((int)(-velocity.X * delta), 0);
            if (GameObject.X < 0) GameObject.Destroy();
        }
    }

    public void Draw(SpriteBatch spriteBatch) { }
}
