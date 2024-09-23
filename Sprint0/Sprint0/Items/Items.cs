using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Items
{
    internal class Items : IAnimation
    {
        private Texture2D texture1;
        private Texture2D texture2;
        private Vector2 position;
        private Rectangle[] source;
        private int counter = 0;
        private int frame = 0;
        private int inc = 1;
        private int part = 1;

        public Items(Texture2D texture1, Texture2D texture2, int potionNum)
        {
            this.texture1 = texture1;
            this.texture2 = texture2;
            position = new Vector2(400, 360);
            Sourcecorrection(potionNum);

        }

        public void Sourcecorrection(int potionNum)
        {
            if (potionNum < 4)
            {
                part = 1;
            }
            else
            {
                part = 2;
            }
            if (potionNum == 1)
            {
                this.source = new Rectangle[]
                {
                new Rectangle(21, 175, 196, 235),
                new Rectangle(234, 175, 196, 235),
                new Rectangle(446, 175, 196, 235)
                };
            }
            if (potionNum == 2)
            {
                this.source = new Rectangle[]
                {
                new Rectangle(21, 492, 153, 235),
                new Rectangle(190, 492, 152, 235),
                new Rectangle(358, 492, 153, 235)
                };
            }
            if (potionNum == 3)
            {
                this.source = new Rectangle[]
                {
                new Rectangle(21, 820, 145, 228),
                new Rectangle(186, 820, 145, 229),
                new Rectangle(347, 820, 145, 229)
                };
            }
            if (potionNum == 4)
            {
                this.source = new Rectangle[]
                {
                new Rectangle(205, 1, 153, 255),
                new Rectangle(389, 1, 153, 255),
                 new Rectangle(571, 1, 153, 255)
                };
            }
            if (potionNum == 5)
            {
                this.source = new Rectangle[]
                {
                new Rectangle(210, 287, 155, 208),
                new Rectangle(395, 287, 155, 208),
                new Rectangle(585, 287, 155, 208)
                };
            }
            if (potionNum == 6)
            {
                this.source = new Rectangle[]
                {
                new Rectangle(205, 540, 140, 240),
                new Rectangle(370, 540, 140, 240),
                new Rectangle(536, 540, 140, 250)
                };
            }

        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (this.part == 1)
            {
                spriteBatch.Draw(this.texture1, this.position, source[frame], Color.White, 0f, Vector2.Zero, .33f, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(this.texture2, this.position, source[frame], Color.White, 0f, Vector2.Zero, .33f, SpriteEffects.None, 0f);
            }
            
        }

        public void load(ContentManager content)
        {
            //nothing since texture 2d got the loading
        }

        public void update(GameTime gameTime, int x, int y)
        {
            position = new Vector2(x, y);
            counter++;
            if (counter > 50)
            {
                frame = frame + inc;
                if (frame > 1)
                {
                    inc = -inc;
                }
                else if (frame < 1)
                {
                    inc = -inc;
                }
                counter = 0;
            }

        }
    }
}
