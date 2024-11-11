using Cuphead.Interfaces;
using Cuphead.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Menu
{
    internal class LoadStart : IMenu
    {

        private PlayerState player;
        private TextSprite textSprite;

        private List<GameObject> list = new List<GameObject>();

        int offsetx;
        int offsety;

        public LoadStart(PlayerState player, SpriteFont font)
        {
            this.player = player;
            this.textSprite = new TextSprite(font);

            getOffset();
        }


        private void getOffset()
        {
            offsetx = player.GameObject.X;
            offsety = player.GameObject.Y;
        }

        private void addelement(string obj, Vector2 pos)
        {
            pos.X = pos.X + offsetx;
            pos.Y = pos.Y + offsety;
            GameObject gameObject = MenuFactory.CreateElement(obj, pos);
            GOManager.Instance.allGOs.Add(gameObject);
            list.Add(gameObject);
        }

        public void LoadScreen()
        {
            getOffset();
            addelement("Title1", new Vector2(-400, -300));
            addelement("Title2", new Vector2 (-600, -500));
            addelement("GameStartText", new Vector2(0, 300));
        }

        public void Unload()
        {
            foreach (GameObject gameObject in list)
            {
                gameObject.Destroy();
            }
        }

    }
}
