using Cuphead.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Controllers
{
    internal class MenuController : IComponent
    {
        private readonly MouseController mouseController;
        private readonly GameTime gameTime;
        private readonly TextSprite textSprite;
        
        private LoadStart loadstart;
        private LoadPaused loadpaused;
        private LoadDeath loaddeath;
        private LoadEnd loadend;

        private int playerX;
        private int playerY;

        private enum screens
        {
            none = 0,
            start = 1,
            paused = 2,
            death = 3,
            end = 4,
        };

        private screens screen;

        public GameObject GameObject { get; set; }
        public bool enabled { get; set; }

        public MenuController(GameTime gameTime, MouseController mouseController, TextSprite textSprite, int playerX, int playerY)
        {
            this.mouseController = mouseController;
            this.gameTime = gameTime;
            this.textSprite = textSprite;

            this.loadstart = new LoadStart();
            this.loadpaused = new LoadPaused();
            this.loaddeath = new LoadDeath();
            this.loadend = new LoadEnd(gameTime, this.textSprite, playerX, playerY);

            screen = 0;

        }

        public void Update(GameTime gameTime)
        {
            switch (screen)
            {
                case screens.none:
                    //nothing
                    break;
                case screens.start:
                    //loadstart.load();
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public bool StopGame()
        {
            if (screen == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
