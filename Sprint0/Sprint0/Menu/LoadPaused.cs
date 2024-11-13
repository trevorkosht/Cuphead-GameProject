using Cuphead.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Menu
{
    internal class LoadPaused : IMenu
    {

        private bool load = false;

        public bool loaded()
        {
            return load;
        }
        public void LoadScreen()
        {
            throw new NotImplementedException();
        }

        public void Unload()
        {
            throw new NotImplementedException();
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
