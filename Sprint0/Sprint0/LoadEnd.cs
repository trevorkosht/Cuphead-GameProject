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
        public LoadEnd(TextSprite text)
        {
            List<GameObject> all = new List<GameObject>();
            text.UpdateText("TIME..............\nHP BONUS..........\nPARRY...............\nGOLD COINS  ....SKILL LEVEL......");
            text.UpdatePos(new Vector2(220, 220));

            all.Add(EndingFactory.CreateElement("WinScreenBackground", new Vector2(-500, -500)));
            all.Add(EndingFactory.CreateElement("WinScreenBoard", new Vector2(200, 200)));
            all.Add(EndingFactory.CreateElement("WinScreenResultsText", new Vector2(-300, 0)));
            all.Add(EndingFactory.CreateElement("WinScreenCuphead", new Vector2(-400, 200)));
            all.Add(EndingFactory.CreateElement("WinScreenUnearnedStar", new Vector2(-300, 0)));

            all.Add(EndingFactory.CreateElement("WinScreenStarAppearAnimation", new Vector2(-300, 0)));

            all.Add(EndingFactory.CreateElement("WinScreenLine", new Vector2(-300, 0)));

            all.Add(EndingFactory.CreateElement("WinScreenStar", new Vector2(-300, 0)));

            all.Add(EndingFactory.CreateElement("WinScreenCircle", new Vector2(-300, 0)));


            foreach(GameObject obj in all)
            {
                GOManager.Instance.allGOs.Add(obj);
            }

        }

    }
}

