using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Transform : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;

    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;

    public Transform(Vector2 position)
    {
        Position = position;
    }

    public void Update(GameTime gameTime) { /* Typically no logic needed */ }

    public void Draw(SpriteBatch spriteBatch) { /* Transform is non-visual */ }
}