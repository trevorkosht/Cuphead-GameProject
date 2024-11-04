using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead
{
    internal class LoadEnd
    {
        int offsetx;
        int offsety;

        List<GameObject> all = new List<GameObject>();

        public LoadEnd(TextSprite text, int offsetx, int offsety)
        {
            this.offsetx = offsetx;
            this.offsety = offsety;

            text.UpdateText("TIME..............\nHP BONUS..........\nPARRY...............\nGOLD COINS  ....\nSKILL LEVEL......");
            text.UpdatePos(new Vector2(220, 220));

            addelement("WinScreenBackground", new Vector2(-500, -500));
            addelement("WinScreenBoard", new Vector2(200, 200));
            addelement("WinScreenResultsText", new Vector2(-300, 0));
            addelement("WinScreenCuphead", new Vector2(-400, 200));
            addelement("WinScreenLine", new Vector2(300, 500));


            all.Add(EndingFactory.CreateElement("WinScreenUnearnedStar", new Vector2(-300, 0)));

            all.Add(EndingFactory.CreateElement("WinScreenStarAppearAnimation", new Vector2(-300, 0)));

            all.Add(EndingFactory.CreateElement("WinScreenStar", new Vector2(-300, 0)));

            all.Add(EndingFactory.CreateElement("WinScreenCircle", new Vector2(-300, 0)));

            foreach (GameObject obj in all)
            {
                GOManager.Instance.allGOs.Add(obj);
            }


        }

        private void addelement(String obj, Vector2 pos)
        {
            pos.X = pos.X + offsetx;
            pos.Y = pos.Y + offsety;
            GOManager.Instance.allGOs.Add(EndingFactory.CreateElement(obj, pos));
        }

    }
}

