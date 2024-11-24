using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AcornProjectile : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private Vector2 velocity;
    private int index;

    public AcornProjectile(Vector2 startPosition, int index)
    {
        this.index = index;
        float verticalOffset = 50f * index;
        velocity = new Vector2(-150f, 200f + verticalOffset);
    }

    public void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        GameObject.Move((int)(velocity.X * delta), (int)(velocity.Y * delta));

        if (GameObject.X < 0 || GameObject.Y > 600) // Replace 600 with screen height if dynamic.
        {
            GameObject.Destroy();
        }
    }

    public void Draw(SpriteBatch spriteBatch) { }
}
