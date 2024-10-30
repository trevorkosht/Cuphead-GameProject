using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cuphead.UI
{
    public class UI
    {
        private HealthComponent playerHealth;
        private Texture2D hp3Texture;
        private Texture2D hp2Texture;
        private Texture2D[] hp1FlashingTextures;
        private Texture2D deadTexture;
        private Vector2 uiPosition;
        private SpriteBatch spriteBatch;
        private double flashingTimer;
        private int flashingIndex;

        public UI(HealthComponent playerHealth, Texture2D hp3, Texture2D hp2, Texture2D[] hp1Flashing, Texture2D dead, Vector2 position, SpriteBatch spriteBatch)
        {
            this.playerHealth = playerHealth;
            hp3Texture = hp3;
            hp2Texture = hp2;
            hp1FlashingTextures = hp1Flashing;
            deadTexture = dead;
            uiPosition = position;
            this.spriteBatch = spriteBatch;
        }

        public void Update(GameTime gameTime)
        {
            if (playerHealth.currentHealth == playerHealth.maxHealth / 3)
            {
                flashingTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (flashingTimer >= 0.3)  // Adjust flashing speed as needed
                {
                    flashingTimer = 0;
                    flashingIndex = (flashingIndex + 1) % hp1FlashingTextures.Length;
                }
            }
        }

        public void Draw()
        {
            Texture2D textureToDraw;

            if (playerHealth.isDeadFull)
            {
                textureToDraw = deadTexture;
            }
            else if (playerHealth.currentHealth > playerHealth.maxHealth * 2 / 3)
            {
                textureToDraw = hp3Texture;
            }
            else if (playerHealth.currentHealth > playerHealth.maxHealth / 3)
            {
                textureToDraw = hp2Texture;
            }
            else if (playerHealth.currentHealth > 0)
            {
                textureToDraw = hp1FlashingTextures[flashingIndex];
            }
            else
            {
                textureToDraw = deadTexture;
            }

            spriteBatch.Draw(textureToDraw, uiPosition, Color.White);
        }
    }
}
