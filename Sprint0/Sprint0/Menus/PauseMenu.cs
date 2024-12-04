using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sprint0;

public class PauseMenu : Menu
{
    private Rectangle pauseMenuBGDestRect, resumeHitbox, restartHitbox, quitHitbox;
    private Vector2 resumeTextPosition, restartTextPosition, quitTextPosition;
    public PauseMenu(Game1 game) : base(game){}

    public override void LoadContent(Texture2DStorage textureStorage)
    {
        textures["PauseMenuBG"] = textureStorage.GetTexture("Title2");

        pauseMenuBGDestRect = new Rectangle(0, 0, 1820, 1000);

        resumeHitbox = new Rectangle(425, 265, 435, 100);
        restartHitbox = new Rectangle(450, 415, 390, 100);
        quitHitbox = new Rectangle(530, 565, 220, 100);

        resumeTextPosition = new Vector2(425, 250);
        restartTextPosition = new Vector2(450, 400);
        quitTextPosition = new Vector2(530, 550);
    }

    public override void Update(GameTime gameTime)
    {
        MediaPlayer.Pause();
        if ((resumeHitbox.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed) || Keyboard.GetState().IsKeyDown(Keys.Space)) 
        {
            MediaPlayer.Resume();
            game.paused = false;
            game.gameState = GameState.Playing;
        }

        if(restartHitbox.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed) {
            game.gameState = GameState.MainMenu;
        }

        if(quitHitbox.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed) {
            game.Exit();
        }
    }

    public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        spriteBatch.Draw(textures["PauseMenuBG"], pauseMenuBGDestRect, Color.White);
        spriteBatch.DrawString(font, "Resume", resumeTextPosition, Color.Goldenrod, 0f, Vector2.Zero, 3f, SpriteEffects.None, 1);
        spriteBatch.DrawString(font, "Restart", restartTextPosition, Color.Goldenrod, 0f, Vector2.Zero, 3f, SpriteEffects.None, 1);
        spriteBatch.DrawString(font, "Quit", quitTextPosition, Color.Goldenrod, 0f, Vector2.Zero, 3f, SpriteEffects.None, 1);
    }
}