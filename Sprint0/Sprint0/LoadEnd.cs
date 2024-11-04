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

        public LoadEnd(TextSprite text, int offsetx, int offsety)
        {
            this.offsetx = offsetx;
            this.offsety = offsety;

            text.UpdateText("TIME..............\nHP BONUS..........\nPARRY...............\nGOLD COINS  ....\nSKILL LEVEL......");
            text.UpdatePos(new Vector2(300+offsetx, 300+offsety));

            addelement("WinScreenBackground", new Vector2(-500, -500));
            addelement("WinScreenBoard", new Vector2(200, 200));
            addelement("WinScreenResultsText", new Vector2(-300, 0));
            addelement("WinScreenCuphead", new Vector2(-400, 200));
            addelement("WinScreenLine", new Vector2(300, 500));


            addelement("WinScreenUnearnedStar", new Vector2(-300, 0));

            addelement("WinScreenStarAppearAnimation", new Vector2(-300, 0));

            addelement("WinScreenStar", new Vector2(-300, 0));

            addelement("WinScreenCircle", new Vector2(-300, 0));

        }

        private void addelement(String obj, Vector2 pos)
        {
            pos.X = pos.X + offsetx;
            pos.Y = pos.Y + offsety;
            GOManager.Instance.allGOs.Add(EndingFactory.CreateElement(obj, pos));
        }

    }
}

