using Cuphead.Player;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Menu
{
    internal class LoadStart
    {

        private PlayerState player;

        int offsetx;
        int offsety;

        public LoadStart(PlayerState player)
        {
            this.player = player;
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
            GOManager.Instance.allGOs.Add(MenuFactory.CreateElement(obj, pos));
        }

        public void loadScreen()
        {
            addelement("Title1", new Vector2(0, 0));
            addelement("Title2", new Vector2 (0, 0));
        }
    }
}
