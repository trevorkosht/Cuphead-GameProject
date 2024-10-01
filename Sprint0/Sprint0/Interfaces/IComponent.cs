using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public interface IComponent
{
    GameObject GameObject { get; set; }
    bool enabled { get; set; }

    void Update(GameTime gameTime);

    void Draw(SpriteBatch spriteBatch);
}