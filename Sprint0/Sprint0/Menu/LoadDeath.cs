using Cuphead.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Menu
{
    internal class LoadDeath : IMenu
    {

        private bool load = false;

        public bool loaded()
        {
            return load;
        }

        public void LoadScreen()
        {
            //
        }

        public void Unload()
        {
            //
        }

        public string CheckAction()
        {
            return null;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            //
        }
    }
}
