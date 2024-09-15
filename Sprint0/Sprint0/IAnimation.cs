using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public interface IAnimation {
    static int updatesPerFrame;
    void load(ContentManager content);
    void update(GameTime gameTime, int x, int y);
    void draw(SpriteBatch spriteBatch);
}
