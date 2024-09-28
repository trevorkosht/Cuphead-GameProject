using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Items
{
    internal class BluePotion : IComponent
    {

        // Reference to the parent GameObject
        public GameObject GameObject { get; set; }
        public bool enabled { get; set; }

        private Texture2D _texture;
        private Vector2 position;
        private Rectangle source;
        private string _itemName;

        public Texture2D itemTexture
        {
            get => _texture;
            set => _texture = value;
        }
        public string itemName
        {
            get => _itemName;
            set => _itemName = value;
        }
        public BluePotion(Vector2 itemPosition, Texture2D texture)
        {
            _itemName = "BluePotio";
            _texture = texture;
            position = itemPosition;
            source = new Rectangle(205, 1, 153, 255);
            enabled = true;
        }

        public void Update(GameTime gameTime)
        {
            //does not animate
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, this.position, source, Color.White, 0f, Vector2.Zero, .33f, SpriteEffects.None, 0f);
        }
    }
}
