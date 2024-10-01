using Cuphead.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Controllers;
using System.Collections.Generic;
using static IController;

namespace Sprint0
{
    public class Game1 : Game
    {
        private static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2DStorage textureStorage;

        private KeyboardController keyboardController;

        private List<GameObject> gameObjects = new List<GameObject>();

        GameObject player = new GameObject(50, 50, new List<IComponent> { new PlayerController() });

        private EnemyController enemyController;
        private BlockController blockController;
        private ItemsController itemsControl;

        bool resetFrame;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            keyboardController = new KeyboardController();

        }

        protected override void Initialize()
        {
            base.Initialize();

            GOManager.Instance.Player = player;
            GOManager.Instance.allGOs = gameObjects;
            GOManager.Instance.textureStorage = textureStorage;
            enemyController = new EnemyController(keyboardController, textureStorage);
            blockController = new BlockController(textureStorage);
            itemsControl = new ItemsController(textureStorage);

            gameObjects.Add(player);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textureStorage = new Texture2DStorage();
            textureStorage.LoadContent(Content);

            SpriteRenderer playerSpriteRenderer = new SpriteRenderer(new Rectangle(player.X, player.Y, 144, 144), true);
            textureStorage.loadPlayerAnimations(playerSpriteRenderer);
            player.AddComponent(playerSpriteRenderer);
        }

        protected override void Update(GameTime gameTime)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject gameObject = gameObjects[i];
                if (gameObject.destroyed)
                {
                    gameObjects.RemoveAt(i);
                    i--;
                    continue;
                }
                gameObject.Update(gameTime);
            }

            enemyController.Update(gameTime);
            blockController.Update(gameTime);
            itemsControl.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.R))
                ResetGame();
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            base.Update(gameTime);
        }

        private void ResetGame()
        {
            enemyController.currentEnemyIndex = 0;
            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject gameObject = gameObjects[i];
                gameObject.Destroy();
                gameObjects.RemoveAt(i);
                i--;
            }
            resetFrame = true;
            player = new GameObject(50, 50, new List<IComponent> { new PlayerController() });
            Initialize();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);
            _spriteBatch.Begin();

            foreach (var gameObject in gameObjects)
            {
                if (resetFrame)
                {
                    resetFrame = false;
                    break;
                }
                gameObject.Draw(_spriteBatch);
            }
            blockController.Draw(_spriteBatch);
            itemsControl.Draw(gameTime, _spriteBatch);
            enemyController.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
