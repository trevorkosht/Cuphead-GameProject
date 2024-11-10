using Cuphead.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Cuphead.Menu
{
    internal class LoadEnd
    {
        private PlayerState player;
        private TextSprite textSprite;
        private GameTime gameTime;

        int offsetx;
        int offsety;
        int coinCount;
        int parryCount;
        int health;

        double grade;

        string Text;
        string displayTime;
        String disGrade;


        public LoadEnd(GameTime time, PlayerState playerState, TextSprite text)
        { 
            this.player = playerState;
            this.textSprite = text;
            this.gameTime = time;
        }

        private void getTime(GameTime time)
        {
            int seconds = (int)time.TotalGameTime.TotalSeconds;
            int min = seconds / 60;
            seconds = seconds % 60;

            if (seconds < 10)
            {
                displayTime = min + ":0" + seconds;
            }
            else
            {
                displayTime = min + ":" + seconds;
            }
        }

        private void getHealth()
        {
            health = GOManager.Instance.Player.GetComponent<HealthComponent>().currentHealth / 100;
        }

        private void getParry()
        {
            
        }

        private void getCoin()
        {
            coinCount = player.coinCount;
        }

        private void getOffset()
        {
            offsetx = player.GameObject.X;
            offsety = player.GameObject.Y;
        }

        private void getGrade()
        {
            grade = (health + parryCount + coinCount) / 9;
            if (grade == 1.0)
            {
                disGrade = "S";
            }
            else if (grade > .9)
            {
                disGrade = "A";
            }
            else if (grade > .8)
            {
                disGrade = "B";
            }
            else if (grade > .7)
            {
                disGrade = "C";
            }
            else
            {
                disGrade = "D";
            }
        }

        public void loadScreen()
        {

            getHealth();
            getCoin();
            getParry();
            getTime(gameTime);
            getOffset();
            getGrade();


            Text = "TIME.............." + displayTime +
                "\nHP BONUS.........." + health + "/3" +
                "\nPARRY..............." + parryCount +
                "\nGOLD COINS  ...." + coinCount + "/5" +
                "\nSKILL LEVEL......" +
                "\n\n       GRADE....  " + disGrade;


            textSprite.UpdateText(Text);
            textSprite.UpdatePos(new Vector2(250 + offsetx, 250 + offsety));

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
            GOManager.Instance.allGOs.Add(MenuFactory.CreateElement(obj, pos));
        }

    }
}

