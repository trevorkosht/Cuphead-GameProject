using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Collider : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;

    public abstract bool Intersects(Collider other);
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);

    public abstract void ChangeSize(int Change);
}