using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Controllers;
using System;
using System.Collections.Generic;

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

        private Camera camera;
        private CameraController cameraController;

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
            GOManager.Instance.GraphicsDevice = GraphicsDevice;
            enemyController = new EnemyController(keyboardController, textureStorage);
            blockController = new BlockController(textureStorage);
            itemsControl = new ItemsController(textureStorage);

            // Initialize camera and controller
            camera = new Camera();
            List<Vector2> railPoints = new List<Vector2>()
            {
                new Vector2(0, 0), 
                new Vector2(500, 0),  
                new Vector2(1000, 0),  
                new Vector2(1200, 0),  
                new Vector2(1500, 50),  
                new Vector2(1700, 100), 
                new Vector2(2000, 100), 
                new Vector2(2200, 150),  
                new Vector2(2500, 150),  
                new Vector2(2700, 50),  
                new Vector2(3000, 0)  
            };
            cameraController = new CameraController(camera, player, railPoints);
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            LevelLoader.LoadLevel(basePath + "\\..\\..\\.." + "\\GameObject\\FileData.txt");
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
            player.AddComponent(new BoxCollider(new Vector2(90, 144), new Vector2(25, 0), GraphicsDevice));
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

            // Update camera based on player's position and the rail
            cameraController.Update();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
                ResetGame();
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            if (keyboardController.OnKeyDown(Keys.L))
                GOManager.Instance.IsDebugging = !GOManager.Instance.IsDebugging;

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

            // Begin sprite batch with camera transformation matrix
            _spriteBatch.Begin(transformMatrix: camera.Transform);

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
