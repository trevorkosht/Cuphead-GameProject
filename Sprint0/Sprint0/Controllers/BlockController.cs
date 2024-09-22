using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.BlocksObstacles;
using Sprint0.Interfaces;
using System.Collections.Generic;

namespace Sprint0.Controllers
{
    internal class BlockController
    {
        private KeyboardController keyboardController;

        private List<IBlock> blocks;
        private Vector2 blockPosition;
        
        private int currentBlockIndex;
        public IBlock currentBlock { get; set; }

        public bool enabled { get; set; } = true;

        public BlockController(KeyboardController keyboardController, Texture2DStorage textureStorage)
        {
            blockPosition = new Vector2(625, 600);
            blocks = new List<IBlock>() 
            { 
                new ForestStump(blockPosition, textureStorage.GetTexture("TreeStump")),
                new FallenLog(blockPosition, textureStorage.GetTexture("FallenLog")),
                new FloatingPlatformSm(blockPosition, textureStorage.GetTexture("FloatingPlatformSm")),
                new FloatingPlatformLg(blockPosition, textureStorage.GetTexture("FloatingPlatformLg")),
                new PlatformMd(blockPosition, textureStorage.GetTexture("PlatformMd")),
                new PlatformLg(blockPosition, textureStorage.GetTexture("PlatformLg")),
            };
            currentBlock = blocks[0];

            this.keyboardController = keyboardController;
        }

        public void Update(GameTime gameTime)
        {
            keyboardController.Update();

            if (!enabled) return;

            if(keyboardController.OnKeyDown(Keys.T))
            {
                currentBlockIndex--;
                if(currentBlockIndex < 0)
                {
                    currentBlockIndex = blocks.Count - 1;
                }
            }
            if (keyboardController.OnKeyDown(Keys.Y))
            {
                currentBlockIndex++;
                if (currentBlockIndex >= blocks.Count)
                {
                    currentBlockIndex = 0;
                }
            }

            currentBlock = blocks[currentBlockIndex];
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (enabled && currentBlock != null)
                currentBlock.Draw(spriteBatch);
        }
    }
}
