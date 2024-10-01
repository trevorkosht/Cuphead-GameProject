using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Interfaces;

namespace Sprint0.BlocksObstacles
{
    internal class FloatingPlatformLg : IBlock
    {
        public bool IsActive { get; set; }
        protected Vector2 position;

        private Texture2D _blockTexture;
        public Texture2D blockTexture
        {
            get => _blockTexture;
            set => _blockTexture = value;
        }
        protected float spriteScale = 0.75f;
        protected Rectangle sourceRectangle;
        protected Vector2 origin;
        private string _blockName;
        public string blockName
        {
            get => _blockName;
            set => _blockName = value;
        }

        public FloatingPlatformLg(Vector2 blockPosition, Texture2D texture)
        {
            this.position = blockPosition;
            this.IsActive = true;
            this.blockName = "FloatingPlatformLg";
            this.blockTexture = texture;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_blockTexture != null)
            {
                sourceRectangle = new Rectangle(60, 60, 440, 120);
                origin = new Vector2(220, 60);

                if (IsActive)
                {
                    spriteBatch.Draw(
                        _blockTexture,
                        position,
                        sourceRectangle,
                        Color.White,
                        0f,
                        origin,
                        spriteScale,
                        SpriteEffects.None,
                        0f
                    );
                }
            }
        }

    }
}