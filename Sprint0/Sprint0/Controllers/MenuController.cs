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
        
        private bool start;
        private bool paused;
        private bool death;
        private bool end;

        private int playerX;
        private int playerY;

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

            this.start = true;
            this.paused = false;
            this.death = false;
            this.enabled = false;

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public bool StopGame()
        {
            return start || paused || death || end;
        }
    }
}
