using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cuphead.UI
{
    public class UI
    {
        private HealthComponent playerHealth;
        private ScoreComponent playerScore;
        private Texture2D hp3Texture;
        private Texture2D hp2Texture;
        private Texture2D[] hp1FlashingTextures;
        private Texture2D deadTexture;
        private Vector2 uiPosition;
        private SpriteBatch spriteBatch;
        private double flashingTimer;
        private int flashingIndex;

        private Texture2D cardBackTexture;
        private Texture2D cardFrontTexture;

        public UI(HealthComponent playerHealth, ScoreComponent scoreComponent, Texture2DStorage textureStorage, Vector2 position, SpriteBatch spriteBatch)
        {
            this.playerHealth = playerHealth;
            this.playerScore = scoreComponent;
            hp3Texture = textureStorage.GetTexture("hp3");
            hp2Texture = textureStorage.GetTexture("hp2");
            hp1FlashingTextures = new Texture2D[]
            {
                textureStorage.GetTexture("hp1-v1"),
                textureStorage.GetTexture("hp1-v2"),
                textureStorage.GetTexture("hp1-v3")
            };

            deadTexture = textureStorage.GetTexture("hpDead");
            uiPosition = position;
            this.spriteBatch = spriteBatch;


            cardBackTexture = textureStorage.GetTexture("CardBack");
            cardFrontTexture = textureStorage.GetTexture("CardFront");
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
            DrawHealthUI();
            DrawScoreUI(spriteBatch);
        }

        public void DrawHealthUI()
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

        private void DrawScoreUI(SpriteBatch spriteBatch)
        {
            float cardFillPercent = playerScore.GetCardFillPercent();

            // Define card positioning
            Vector2 cardPosition = uiPosition + new Vector2(hp3Texture.Width + 10, 0);  // Offset right from health UI
            float cardScale = 0.5f;  // Scale down the cards to half size
            int cardWidth = (int)(cardBackTexture.Width * cardScale);
            int cardHeight = (int)(cardBackTexture.Height * cardScale);

            // Draw the card back proportionately, growing from bottom
            int filledHeight = (int)(cardBackTexture.Height * cardFillPercent);
            Rectangle cardBackSource = new Rectangle(0, cardBackTexture.Height - filledHeight, cardBackTexture.Width, filledHeight);
            Vector2 cardBackPosition = cardPosition + new Vector2(0, cardHeight - filledHeight * cardScale);
            spriteBatch.Draw(cardBackTexture, cardBackPosition, cardBackSource, Color.White, 0, Vector2.Zero, cardScale, SpriteEffects.None, 0.3f);

            // Draw the front of each fully flipped card
            for (int i = 0; i < playerScore.CardFlips; i++)
            {
                Vector2 flippedCardPosition = cardPosition + new Vector2((i + 1) * (cardWidth + 5), 0);  // Adjust spacing between cards
                spriteBatch.Draw(cardFrontTexture, flippedCardPosition, null, Color.White, 0, Vector2.Zero, cardScale, SpriteEffects.None, 0.3f);
            }
        }

    }
}
