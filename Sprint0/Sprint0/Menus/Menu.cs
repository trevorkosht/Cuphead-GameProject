using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;

public abstract class Menu
{
    protected Game1 game;
    public int totalGametimeSeconds;
    protected Dictionary<string, Texture2D> textures;

    public Menu(Game1 game){
        this.game = game;
        textures = new Dictionary<string, Texture2D>();
        totalGametimeSeconds = 0;
    }
    public abstract void LoadContent(Texture2DStorage textureStorage);
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch, SpriteFont font);
}
