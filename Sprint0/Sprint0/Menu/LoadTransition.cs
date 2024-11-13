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

            int width = 1280;
            int height = 720;

            VisualEffectFactory.createVisualEffect(new Rectangle((int)GOManager.Instance.Camera.Position.X - 64, -36, width, height), new Vector2(515,290),texture, 5, 16, 1.1f, true);
        }

        public void FadeOut()
        {
            Texture2D texture = GOManager.Instance.textureStorage.GetTexture("FadeOut");

            int width = 1280;
            int height = 720;

            VisualEffectFactory.createVisualEffect(new Rectangle((int)GOManager.Instance.Camera.Position.X - 64, -36, width, height), new Vector2(515, 290), texture, 1, 16, 1.1f, true);
        }
    }
}
