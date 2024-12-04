using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sprint0;

public class MainMenu : Menu
{
    private Rectangle MainMenuBGDestRect, MainMenuCharactersDestRect;
    private Vector2 MainMenuTextPosition;
    public MainMenu(Game1 game) : base(game){}

    public override void LoadContent(Texture2DStorage textureStorage)
    {
        textures["MainMenuCharacters"] = textureStorage.GetTexture("Title1");
        textures["MainMenuBG"] = textureStorage.GetTexture("Title2");

        MainMenuBGDestRect = new Rectangle(0, 0, 1820, 1000);
        MainMenuCharactersDestRect = new Rectangle(200, 150, 1920, 1055);
        MainMenuTextPosition = new Vector2(425, 575);
    }

    public override void Update(GameTime gameTime)
    {
        MediaPlayer.Pause();
        if (Keyboard.GetState().IsKeyDown(Keys.Space)) 
        {
            MediaPlayer.Resume();
            GOManager.Instance.audioManager.getInstance("Intro").Play();
            game.paused = false;
            game.gameState = GameState.Playing;
        }
    }

    public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        spriteBatch.Draw(textures["MainMenuBG"], MainMenuBGDestRect, Color.White);
        spriteBatch.Draw(textures["MainMenuCharacters"], MainMenuCharactersDestRect, Color.White);
        spriteBatch.DrawString(font, "Press Space", MainMenuTextPosition, Color.Goldenrod, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1);
    }
}