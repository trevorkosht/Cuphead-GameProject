using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public interface IAnimation {
    static int updatesPerFrame;
    void load(ContentManager content);
    void update(GameTime gameTime, int x, int y);
    void draw(SpriteBatch spriteBatch);
}
