using Cuphead.Interfaces;
using Cuphead.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Menu
{
    internal class LoadStart : IMenu
    {
        private MouseController mouseController;
        LoadTransition transition = new LoadTransition();
        private PlayerState player;
        private TextSprite start;

        private List<GameObject> list = new List<GameObject>();

        int offsetx;
        int offsety;

        private bool load = false;

        public bool loaded()
        {
            return load;
        }

        public LoadStart(PlayerState player,MouseController mouseController, SpriteFont font)
        {
            this.player = player;
            this.mouseController = mouseController;
            this.start = new TextSprite(font);

            GetOffset();
        }


        private void GetOffset()
        {
            offsetx = player.GameObject.X;
            offsety = player.GameObject.Y;
        }

        private void Addelement(string obj, Vector2 pos)
        {
            pos.X = pos.X + offsetx;
            pos.Y = pos.Y + offsety;
            GameObject gameObject = MenuFactory.CreateElement(obj, pos);
            GOManager.Instance.allGOs.Add(gameObject);
            list.Add(gameObject);
        }

        public void LoadScreen()
        {
            load = true;

            GetOffset();
            Addelement("Title1", new Vector2(-400, -300));
            Addelement("Title2", new Vector2 (-600, -500));
            //Addelement("GameStartText", new Vector2(-200, 0));

            LoadMenu();
        }

        public void Unload()
        {
            load = false;

            transition.FadeIn();

            foreach (GameObject gameObject in list)
            {
                gameObject.Destroy();
            }
        }

        public void LoadMenu()
        {
            start.UpdateText("Click here to start");
            start.UpdatePos(new Vector2(offsetx -100, offsety +50));
            start.UpdateColor(Color.Gold);
        }

        public string CheckAction()
        {
            if (mouseController.OnMouseClick(MouseButton.Left))
            {
                Point mousePosition = mouseController.GetMousePosition();

                if (start.GetBoundingBox().Contains(mousePosition))
                {
                    return "start";
                }
            }
            return null;
        }

        void IMenu.Draw(SpriteBatch _spriteBatch)
        {
            start.Draw(_spriteBatch);
        }
    }
}
