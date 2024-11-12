using Cuphead.Interfaces;
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

        private IMenu menu;

        private LoadStart loadstart;
        private LoadPaused loadpaused;
        private LoadDeath loaddeath;
        private LoadEnd loadend;

        private int playerX;
        private int playerY;

        public GameObject GameObject { get; set; }
        public bool enabled { get; set; }

        public MenuController(PlayerState player, SpriteFont font)
        {
            mouseController = new MouseController();
            this.playerState = player;

            this.loadstart = new LoadStart(player, mouseController, font);
            this.loadpaused = new LoadPaused();
            this.loaddeath = new LoadDeath();
            this.loadend = new LoadEnd(player, font);

            menu = loadstart;

        }

        public void Update(GameTime gameTime)
        {   
            UpdateSprite();
            mouseController.Update();

            if (mouseController.OnMouseClick(MouseButton.Right))
            {
                menu.Unload();
                menu = null;
            }

            if (menu != null)
            {
                CheckAction();
            }
            

            if(menu != null)
            {
                menu.LoadScreen();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public bool StopGame()
        {
            if (menu == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void UpdateSprite()
        {
            List<GameObject> gameObjects = GOManager.Instance.allGOs;

            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject go = gameObjects[i];
                if (go.type == "Menu")
                {
                    SpriteRenderer spriterender = go.GetComponent<SpriteRenderer>();
                    if (spriterender != null)
                    {
                        spriterender.setAnimation(spriterender.getAnimationName());
                        spriterender.Update();
                    }

                    VisualEffectRenderer visualEffectRenderer = go.GetComponent<VisualEffectRenderer>();
                    if(visualEffectRenderer != null)
                    {
                        visualEffectRenderer.Update();
                    }
                   
                }
            }
        }

        public void CheckAction()
        {
            String command = menu.CheckAction();
            if (command == "start")
            {
                menu.Unload();
                menu = null;
            }
        }
    }
}
