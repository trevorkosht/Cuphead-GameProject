﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Interfaces;

namespace Sprint0.BlocksObstacles
{
    internal class FallenLog : IBlock
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

        public FallenLog(Vector2 blockPosition, Texture2D texture)
        {
            this.position = blockPosition;
            this.IsActive = true;
            this.blockName = "FallenLog";
            this.blockTexture = texture;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_blockTexture != null)
            {
                sourceRectangle = new Rectangle(1215, 0, 835, 195);
                origin = new Vector2(415, 98);

                if (IsActive)
                {
                    // Draw the sprite
                    spriteBatch.Draw(
                        _blockTexture,
                        position,                   // Position of the sprite
                        sourceRectangle,            // Part of the texture to draw (whole image in this case)
                        Color.White,                // Tint color
                        0f,                         // Rotation (0 = no rotation)
                        origin,                     // Origin point (center)
                        spriteScale,                // Scale of the sprite
                        SpriteEffects.None,         // Effects (flipping, etc.)
                        0f                          // Layer depth
                    );
                }
            }
        }

    }
}