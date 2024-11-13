using Cuphead.Interfaces;
using Cuphead.Menu;
using Cuphead.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Collections.Generic;
using System.Data;
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
        private DelayGame delaygame;

        private IMenu menu;

        private LoadStart loadstart;
        private LoadPaused loadpaused;
        private LoadDeath loaddeath;
        private LoadEnd loadend;

        private LoadTransition loadtransition;

        private int playerX;
        private int playerY;

        public GameObject GameObject { get; set; }
        public bool enabled { get; set; }

        public MenuController(PlayerState player, SpriteFont font)
        {
            mouseController = new MouseController();
            delaygame = new DelayGame();
            this.playerState = player;

            this.loadstart = new LoadStart(player, mouseController, font);
            this.loadpaused = new LoadPaused();
            this.loaddeath = new LoadDeath();
            this.loadend = new LoadEnd(player, font);

            this.loadtransition = new LoadTransition(player);

            menu = loadstart;

        }

        public void Update(GameTime gameTime)
        {   
            UpdateSprite();
            mouseController.Update();

            if (mouseController.OnMouseClick(MouseButton.Right))
            {
                //loadtransition.LoadTransIn();
                menu.Unload();
                FadeIn();
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

        public void FadeIn()
        {
            Texture2D texture = GOManager.Instance.textureStorage.GetTexture("FadeIn");
            VisualEffectFactory.createVisualEffect(new Rectangle(00, 0, 500, 500), texture, 1, 16, 1f, true);
        }

        public void FadeOut()
        {
            Texture2D texture = GOManager.Instance.textureStorage.GetTexture("FadeIn");
            VisualEffectFactory.createVisualEffect(new Rectangle(00, 0, 500, 500), texture, 1, 16, 1f, true);
        }

    }
}
