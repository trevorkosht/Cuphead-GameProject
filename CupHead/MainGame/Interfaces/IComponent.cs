using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public interface IComponent
{
    // Reference to the parent GameObject
    GameObject GameObject { get; set; }
    bool enabled { get; set; }

    // Called during the Update loop
    void Update(GameTime gameTime);

    // Called during the Draw loop (for visual components)
    void Draw(SpriteBatch spriteBatch);
}