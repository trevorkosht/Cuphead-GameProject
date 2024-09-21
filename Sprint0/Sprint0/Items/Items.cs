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
        private int part = 1;

        public Items(Texture2D texture1, Texture2D texture2, int potionNum)
        {
            this.texture1 = texture1;
            this.texture2 = texture2;
            position = new Vector2(400, 480);
            sourcecorrection(potionNum);

        }

        private void sourcecorrection(int potionNum)
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
                new Rectangle(20, 175, 196, 235),
                new Rectangle(190, 175, 196, 235),
                new Rectangle(358, 175, 196, 235)
                };
            }
            if (potionNum == 2)
            {
                this.source = new Rectangle[]
                {
                new Rectangle(20, 490, 173, 740),
                new Rectangle(190, 490, 173, 740),
                new Rectangle(357, 490, 173, 740)
                };
            }
            if (potionNum == 3)
            {
                this.source = new Rectangle[]
                {
                new Rectangle(20, 820, 147, 230),
                new Rectangle(183, 820, 145, 230),
                new Rectangle(346, 820, 145, 230)
                };
            }
            if (potionNum == 4)
            {
                this.source = new Rectangle[]
                {
                new Rectangle(202, 1, 153, 235),
                new Rectangle(389, 1, 153, 235),
                 new Rectangle(571, 1, 153, 235)
                };
            }
            if (potionNum == 5)
            {
                this.source = new Rectangle[]
                {
                new Rectangle(210, 287, 371, 208),
                new Rectangle(395, 287, 371, 208),
                new Rectangle(585, 287, 371, 208)
                };
            }
            if (potionNum == 6)
            {
                this.source = new Rectangle[]
                {
                new Rectangle(205, 540, 315, 240),
                new Rectangle(370, 540, 315, 240),
                new Rectangle(536, 540, 315, 250)
                };
            }

        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (this.part == 1)
            {
                spriteBatch.Draw(this.texture1, this.position, source[frame], Microsoft.Xna.Framework.Color.White);
            }
            else
            {
                spriteBatch.Draw(this.texture2, this.position, source[frame], Microsoft.Xna.Framework.Color.White);
            }
            
        }

        public void load(ContentManager content)
        {
            //nothing since texture 2d got the loading
        }

        public void update(GameTime gameTime, int x, int y)
        {
            counter++;
            if (counter > 30)
            {
                if (frame > 1)
                {
                    frame = 0;
                }
                else
                {
                    frame++;
                }
                counter = 0;
            }
        }
    }
}
