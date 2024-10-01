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
    internal class SkybluePotion : IComponent
    {

        public GameObject GameObject { get; set; }
        public bool enabled { get; set; }

        private Texture2D _texture;
        private Vector2 position;
        private Rectangle[] source;
        private string _itemName;
        private const int frameRate = 5;
        private int _frameIndex;
        private int counter = 0;

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
        public SkybluePotion(Vector2 itemPosition, Texture2D texture)
        {
            _itemName = "SkybluePotion";
            _texture = texture;
            position = itemPosition;
            source = new Rectangle[]
                {
                new Rectangle(21, 492, 153, 235),
                new Rectangle(190, 492, 152, 235),
                new Rectangle(358, 492, 153, 235)
                }; ;
            enabled = true;
        }

        public void Update(GameTime gameTime)
        {
            counter++;
            if (counter > frameRate)
            {
                _frameIndex++;
                counter = 0;
            }

            if (_frameIndex >= source.Length)
            {
                _frameIndex = 0;
            }

            if (_frameIndex >= source.Length)
            {
                _frameIndex = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, this.position, source[_frameIndex], Color.White, 0f, Vector2.Zero, .33f, SpriteEffects.None, 0f);
        }
    }
}
