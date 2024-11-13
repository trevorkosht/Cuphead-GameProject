using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Menu
{
    internal class LoadTransition
    {
        public void FadeIn()
        {
            Texture2D texture = GOManager.Instance.textureStorage.GetTexture("FadeIn");

            int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            VisualEffectFactory.createVisualEffect(new Rectangle(0, 0, width, height), texture, 1, 16, 1f, true);
        }

        public void FadeOut()
        {
            Texture2D texture = GOManager.Instance.textureStorage.GetTexture("FadeOut");

            int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            VisualEffectFactory.createVisualEffect(new Rectangle(0, 0, width, height), texture, 1, 16, 1f, true);
        }
    }
}
