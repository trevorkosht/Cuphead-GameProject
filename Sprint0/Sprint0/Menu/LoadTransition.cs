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
            Texture2D fadeTexture = GOManager.Instance.textureStorage.GetTexture("FadeIn");

            int width = 1280;
            int height = 720;



            Vector2 cameraPosition = GOManager.Instance.Camera.Position;
            Texture2D GameStartText = GOManager.Instance.textureStorage.GetTexture("GameStartText");
            VisualEffectFactory.createVisualEffect(new Rectangle((int)cameraPosition.X - 64, (int)cameraPosition.Y - 36, width, height), new Rectangle(0, 0, 1285, 720), GameStartText, 4, 17, 1.1f, true);


            VisualEffectFactory.createVisualEffect(new Rectangle((int)cameraPosition.X - 64, (int)cameraPosition.Y - 36, width, height), new Rectangle(0,0,515,290), fadeTexture, 3, 16, 1.1f, true);
        }

        public void FadeOut()
        {
            Texture2D fadeTexture = GOManager.Instance.textureStorage.GetTexture("FadeOut");

            int width = 1280;
            int height = 720;

            VisualEffectFactory.createVisualEffect(new Rectangle((int)GOManager.Instance.Camera.Position.X - 64, -36, width, height), new Rectangle(0, 0, 515, 290), fadeTexture, 5, 16, 1.1f, true);
        }
    }
}
