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
    internal class YellowPotion : IComponent
    {


        public GameObject GameObject { get; set; }
        public bool enabled { get; set; }

        private Texture2D _texture;
        BoxCollider collider;
        private Vector2 position;
        private Rectangle[] source;
        private string _itemName;
        private const int frameRate = 5;
        private int _frameIndex;
        private int counter = 0;
        private float scale = .33f;

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
        public YellowPotion(Vector2 itemPosition, Texture2D texture)
        {
            _itemName = "YellowPotion";
            _texture = texture;
            position = itemPosition;
            source = new Rectangle[]
                {
                new Rectangle(21, 175, 196, 235),
                new Rectangle(234, 175, 196, 235),
                new Rectangle(446, 175, 196, 235)
                };
            enabled = true;

            // Initialize collider
            Vector2 bounds = new Vector2(source[0].Width * scale, source[0].Width * scale); 
            collider = new BoxCollider(bounds, position, GOManager.Instance.GraphicsDevice);
            collider.GameObject = this.GameObject;
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
            collider.offset = position;
            collider.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, this.position, source[_frameIndex], Color.White, 0f, Vector2.Zero, .33f, SpriteEffects.None, 0f);

            collider.Draw(spriteBatch);
        }
    }
}
