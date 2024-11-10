using Cuphead.Menu;
using Cuphead.Player;
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
        private PlayerState playerState;
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

        public MenuController(PlayerState player, SpriteFont font)
        {
            mouseController = new MouseController();
            this.playerState = player;

            this.loadstart = new LoadStart(player, font);
            this.loadpaused = new LoadPaused();
            this.loaddeath = new LoadDeath();
            this.loadend = new LoadEnd(player, textSprite);

            screen = screens.none;

        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        private void loadScreen(GameTime gameTime)
        {
            switch (screen)
            {
                case screens.none:
                    //nothing
                    break;
                case screens.start:
                    //loadstart.load();
                    break;
                case screens.paused:
                    break;
                case screens.death:
                    break;
                case screens.end:
                    loadend.loadScreen(gameTime);
                    break;
                default:
                    break;
            }
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
