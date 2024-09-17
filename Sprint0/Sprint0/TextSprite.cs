using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class TextSprite : ISprite
{
    private SpriteFont font;
    private string text;
    private Vector2 position;
    private Color color;

    public TextSprite(SpriteFont font, string text, Vector2 position, Color color)
    {
        this.font = font;
        this.text = text;
        this.position = position;
        this.color = color;
    }

    public void Update(GameTime gameTime)
    {
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, text, position, color);
    }
}
