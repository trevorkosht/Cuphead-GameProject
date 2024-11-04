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

    public void UpdateText(string text)
    {
        this.text = text;
    }

    public void UpdatePos(Vector2 position)
    {
        this.position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, text, position, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
    }

    //will not be used since gametime is useless
    public void Update(GameTime gameTime)
    {

    }
}
