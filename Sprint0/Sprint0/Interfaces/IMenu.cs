using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Interfaces
{
    internal interface IMenu
    {
        public bool loaded();
        public void LoadScreen();
        public void Unload();

        public String CheckAction();

        public void Draw(SpriteBatch _spriteBatch);
    }
}
