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

        private List<GameObject> blocks = new List<GameObject>();
        private Vector2 blockPosition;
        
        private int currentBlockIndex;
        public IBlock currentBlock { get; set; }

        public bool enabled { get; set; } = true;

        public BlockController(Texture2DStorage textureStorage)
        {
            blockPosition = new Vector2(625, 600);
            //GameObject temp = BlockFactory.createBlock(new Rectangle(400, 550, 144, 144), textureStorage.GetTexture("TreeStump_1"));
            //GameObject temp2 = BlockFactory.createBlock(new Rectangle(550, 550, 144, 144), textureStorage.GetTexture("TreeStump_2"));
            //GameObject temp3 = BlockFactory.createBlock(new Rectangle(700, 550, 144, 144), textureStorage.GetTexture("TreeStump_3"));
            //blocks.Add(temp);
            //blocks.Add(temp2);
            //blocks.Add(temp3);

            //currentBlock = blocks[0];

            this.keyboardController = new KeyboardController();
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

            //currentBlock = blocks[currentBlockIndex];
            foreach (GameObject block in blocks) {
                block.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (enabled && currentBlock != null)
                currentBlock.Draw(spriteBatch);
        }
    }
}
