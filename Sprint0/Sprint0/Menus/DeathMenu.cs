using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sprint0;

public class DeathMenu : Menu
{
    private Rectangle deathMessageDestRect, deathMessageSrcRect, lossScreenDestRect, lossScreenSrcRect, lossScreenIconDestRect, lossScreenIconSrcRect, retryHitbox, quitHitbox;
    private bool lossScreenDisplayed;
    private float deathMessageTime;
    private const float DEATH_MESSAGE_DURATION = 1.0f;

    public DeathMenu(Game1 game) : base(game)
    {
        deathMessageTime = 0;
        lossScreenDisplayed = false;
    }

    public override void LoadContent(Texture2DStorage textureStorage)
    {
        textures["DeathMessage"] = textureStorage.GetTexture("DeathMessage");
        textures["LossScreen"] = textureStorage.GetTexture("LossScreen");
        textures["LossScreenIcon"] = textureStorage.GetTexture("LossScreenIcon");

        deathMessageDestRect = new Rectangle(150, 200, 1000, 250);
        deathMessageSrcRect = new Rectangle(0, 0, 1070, 220);
        
        lossScreenDestRect = new Rectangle(400, 90, 450, 600);
        lossScreenSrcRect = new Rectangle(0, 0, 554, 632);

        lossScreenIconDestRect = new Rectangle(555, 368, 92, 81);
        lossScreenIconSrcRect = new Rectangle(0, 0, 92, 81);

        retryHitbox = new Rectangle(620, 470, 90, 40);
        quitHitbox = new Rectangle(596, 550, 165, 40);
    }

    public override void Update(GameTime gameTime)
    {
        deathMessageTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (deathMessageTime < DEATH_MESSAGE_DURATION)
        {
            game.paused = false;
        } else {
            if(!lossScreenDisplayed) {
                lossScreenDisplayed = true;
                GOManager.Instance.audioManager.stopAll();
            } else {
                MediaPlayer.Stop();
                if(deathMessageTime >= DEATH_MESSAGE_DURATION + 0.05f) {
                    game.gameState = GameState.DeathMenu;
                    game.paused = true;
                } else {
                    GOManager.Instance.audioManager.getInstance("VinylScratch").Play();
                }

                if(retryHitbox.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed) {
                    game.paused = false;
                    game.gameState = GameState.MainMenu;
                }

                if(quitHitbox.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed) {
                    game.Exit();
                }

            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        if(lossScreenDisplayed) {
            spriteBatch.Draw(textures["LossScreen"], lossScreenDestRect, lossScreenSrcRect, Color.White, -0.1f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.Draw(textures["LossScreenIcon"], lossScreenIconDestRect, lossScreenIconSrcRect, Color.White, -0.1f, Vector2.Zero, SpriteEffects.None, 0f);
        } else {
            spriteBatch.Draw(textures["DeathMessage"], deathMessageDestRect, deathMessageSrcRect, Color.White);
        }
    }
}
