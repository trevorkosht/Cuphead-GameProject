using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        GameObject player = new GameObject(50, 500, new List<IComponent> { new PlayerController() });

        private EnemyController enemyController;

        private Camera camera;
        private CameraController cameraController;
        Vector2 savedPlayerLoc;

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
            if (savedPlayerLoc != Vector2.Zero)
            {
                player.X = (int)savedPlayerLoc.X;
                player.Y = (int)savedPlayerLoc.Y;
            }
            GOManager.Instance.Player = player;
            GOManager.Instance.allGOs = gameObjects;
            GOManager.Instance.textureStorage = textureStorage;
            GOManager.Instance.GraphicsDevice = GraphicsDevice;
            enemyController = new EnemyController(keyboardController, textureStorage);

            // Initialize camera and controller
            camera = new Camera();
            GOManager.Instance.Camera = camera;

            List<Vector2> railPoints = new List<Vector2>()
            {
                new Vector2(0, 0), 
                new Vector2(500, 0),  
                new Vector2(1000, 0),  
                new Vector2(1200, -50),  
                new Vector2(1500, -50),  
                new Vector2(1700, -50), 
                new Vector2(2000, 0), 
                new Vector2(2200, 0),  
                new Vector2(2500, 0),  
                new Vector2(2700, 0),  
                new Vector2(3000, -25),
                new Vector2(3200, -25),
                new Vector2(3500, -25),
                new Vector2(3700, 0),
                new Vector2(4000, 0),
                new Vector2(4200, 0),
                new Vector2(4500, 0),
                new Vector2(4700, 0),
                new Vector2(5000, 0),
                new Vector2(5200, 0),
                new Vector2(5500, 0),
                new Vector2(5700, 0),
                new Vector2(6000, 0),
                new Vector2(6200, 0),
                new Vector2(6500, 0),
                new Vector2(6700, 0),
                new Vector2(7000, 0),
                new Vector2(7200, 0),
                new Vector2(7500, -50),
                new Vector2(7700, -50),
                new Vector2(8000, 0),
                new Vector2(8200, 0),
                new Vector2(8500, 0),
                new Vector2(8700, 0)
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
            playerSpriteRenderer.orderInLayer = .1f;
            textureStorage.loadPlayerAnimations(playerSpriteRenderer);
            player.AddComponent(playerSpriteRenderer);
            player.AddComponent(new BoxCollider(new Vector2(90, 144), new Vector2(25, 0), GraphicsDevice));
            player.type = "Player";
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
            savedPlayerLoc = player.position;

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
            _spriteBatch.Begin(SpriteSortMode.BackToFront, transformMatrix: camera.Transform);

            foreach (var gameObject in gameObjects)
            {
                if (resetFrame)
                {
                    resetFrame = false;
                    break;
                }
                gameObject.Draw(_spriteBatch);
            }
            enemyController.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
