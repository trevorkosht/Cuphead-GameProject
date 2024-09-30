using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.BlocksObstacles;
using Sprint0.Interfaces;
using System.Collections.Generic;

namespace Sprint0.Controllers
{
    internal class ItemsController
    {
        private KeyboardController keyboardController;

        private List<GameObject> Items;
        private Vector2 itemPosition;

        private int currentItemIndex;

        public bool enabled { get; set; } = true;

        public GameObject currentItem { get; set; }

        public ItemsController(Texture2DStorage textureStorage)
        {
            itemPosition = new Vector2(400, 460);
            Items = new List<GameObject>()
            {
                new GameObject((int)itemPosition.X, (int)itemPosition.Y, new Cuphead.Items.YellowPotion(itemPosition, textureStorage.GetTexture("Item1_3"))),
                new GameObject((int)itemPosition.X, (int)itemPosition.Y, new Cuphead.Items.SkybluePotion(itemPosition, textureStorage.GetTexture("Item1_3"))),
                new GameObject((int)itemPosition.X, (int)itemPosition.Y, new Cuphead.Items.RedPotion(itemPosition, textureStorage.GetTexture("Item1_3"))),
                new GameObject((int)itemPosition.X, (int)itemPosition.Y, new Cuphead.Items.BluePotion(itemPosition, textureStorage.GetTexture("Item1_3"))),
                new GameObject((int)itemPosition.X, (int)itemPosition.Y, new Cuphead.Items.OceanbluePotion(itemPosition, textureStorage.GetTexture("Item1_3"))),
                new GameObject((int)itemPosition.X, (int)itemPosition.Y, new Cuphead.Items.RedKetchupPotion(itemPosition, textureStorage.GetTexture("Item1_3")))
            };
            currentItem = Items[0];

            this.keyboardController = new KeyboardController();
        }

        public void Update(GameTime gameTime)
        {
            keyboardController.Update();

            if (!enabled) return;

            if (keyboardController.OnKeyDown(Keys.U))
            {
                currentItemIndex--;
                if (currentItemIndex < 0)
                {
                    currentItemIndex = Items.Count - 1;
                }
            }
            if (keyboardController.OnKeyDown(Keys.I))
            {
                currentItemIndex++;
                if (currentItemIndex >= Items.Count)
                {
                    currentItemIndex = 0;
                }
            }

            currentItem = Items[currentItemIndex];
            currentItem.Update(gameTime);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            currentItem.Draw(spriteBatch);
        }
    }
}
