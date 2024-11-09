﻿using Cuphead.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Menu
{
    internal class LoadEnd
    {
        int offsetx;
        int offsety;

        string Text;
        string displayTime;

        public LoadEnd(GameTime time, TextSprite text, int offsetx, int offsety)
        {
            this.offsetx = offsetx;
            this.offsety = offsety;

            int seconds = (int)time.TotalGameTime.TotalSeconds;
            int min = seconds / 60;
            seconds = seconds % 60;
            string health = GOManager.Instance.Player.GetComponent<HealthComponent>().currentHealth / 100 + "/3";
            if (seconds < 10)
            {
                displayTime = min + ":0" + seconds;
            }
            else
            {
                displayTime = min + ":" + seconds;
            }
            Text = "TIME.............." + displayTime +
                "\nHP BONUS.........." + health +
                "\nPARRY..............." +
                "\nGOLD COINS  ...." + "5/5" +
                "\nSKILL LEVEL......" +
                "\n\n       GRADE....  A";


            text.UpdateText(Text);
            text.UpdatePos(new Vector2(250 + offsetx, 250 + offsety));

            addelement("WinScreenBackground", new Vector2(-500, -500));
            addelement("WinScreenBoard", new Vector2(150, 200));
            addelement("WinScreenResultsText", new Vector2(-300, 0));
            addelement("WinScreenCuphead", new Vector2(-400, 200));
            addelement("WinScreenLine", new Vector2(250, 500));


            addelement("WinScreenUnearnedStar", new Vector2(560, 445));
            addelement("WinScreenUnearnedStar", new Vector2(590, 445));

            addelement("WinScreenStarAppearAnimation", new Vector2(560, 445));
            addelement("WinScreenStarAppearAnimation", new Vector2(590, 445));

            addelement("WinScreenStar", new Vector2(560, 445));
            addelement("WinScreenStar", new Vector2(590, 445));

            addelement("WinScreenCircle", new Vector2(700, 400));

        }

        private void addelement(string obj, Vector2 pos)
        {
            pos.X = pos.X + offsetx;
            pos.Y = pos.Y + offsety;
            GOManager.Instance.allGOs.Add(EndingFactory.CreateElement(obj, pos));
        }

    }
}
