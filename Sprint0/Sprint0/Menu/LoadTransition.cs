using Cuphead.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Menu
{
    internal class LoadTransition
    {
        private PlayerState player;

        private List<GameObject> list = new List<GameObject>();

        int offsetx;
        int offsety;

        public LoadTransition(PlayerState player) 
        {
            this.player = player;
        
        }

        private void GetOffset()
        {
            offsetx = player.GameObject.X;
            offsety = player.GameObject.Y;
        }

        private void Addelement(string obj, Vector2 pos)
        {
            pos.X = pos.X + offsetx;
            pos.Y = pos.Y + offsety;
            GameObject gameObject = MenuFactory.CreateElement(obj, pos);
            GOManager.Instance.allGOs.Add(gameObject);
            list.Add(gameObject);
        }

        public void LoadTrans()
        {
            GetOffset();

            Addelement("FadeIn", new Vector2(50, 50));
            Addelement("FadeOut", new Vector2(50, 50));
        }
    }
}
