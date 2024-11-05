using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.GameObjects
{
    internal class AutoMove : IComponent
    {
        public GameObject GameObject { get; set; }
        public bool enabled { get; set; }
        global::GameObject IComponent.GameObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
