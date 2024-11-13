using Cuphead;
using Cuphead.Controllers;
using Cuphead.Menu;
using Cuphead.Player;
using Cuphead.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;

namespace Sprint0
{
    public class Game1 : Game
    {
        private static GraphicsDeviceManager _graphics;


        private SpriteBatch _spriteBatch;
        private SpriteBatch _spriteBatch2;

        private Texture2DStorage textureStorage;
        private SoundEffectStorage audioStorage;
        private KeyboardController keyboardController;

        private List<GameObject> gameObjects = new List<GameObject>();
        GameObject player = new GameObject(50, 500);

        private EnemyController enemyController;
        private AudioManager audioManager;

        private Camera camera;
        private CameraController cameraController;
        private Vector2 savedPlayerLoc;
        private Vector2 startingPlayerLoc = new Vector2(0, 500);
        private bool saveLoc = false;
        internal PlayerState playerState;
        private MenuController menuController;
        private UI UI;

        string basePath;

        TextSprite texts;
        SpriteFont font;

        private bool resetFrame;
        private bool endGame = false;
        private int startGame = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            keyboardController = new KeyboardController();
            audioManager = new AudioManager();
        }

        protected override void Initialize()
        {
            base.Initialize();
            if (savedPlayerLoc != Vector2.Zero && saveLoc)
            {
                player.X = (int)savedPlayerLoc.X;
                player.Y = (int)savedPlayerLoc.Y;
            }
            else
            {
                player.X = (int)startingPlayerLoc.X;
                player.Y = (int)startingPlayerLoc.Y;
            }
            GOManager.Instance.Player = player;
            GOManager.Instance.allGOs = gameObjects;
            GOManager.Instance.textureStorage = textureStorage;
            GOManager.Instance.audioManager = audioManager;
            GOManager.Instance.GraphicsDevice = GraphicsDevice;
            enemyController = new EnemyController(keyboardController, textureStorage);

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
                new Vector2(4700, -50),
                new Vector2(5000, -100),
                new Vector2(5200, -100),
                new Vector2(5500, -100),
                new Vector2(5700, -100),
                new Vector2(6000, -100),
                new Vector2(6200, -100),
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
            basePath = AppDomain.CurrentDomain.BaseDirectory + "\\..\\..\\.." + "\\GameObjects\\";
            LevelLoader.LoadLevel(basePath + "FileData.txt");
            gameObjects.Add(player);
            GOManager.Instance.audioManager.getInstance("Intro").Play();
            MediaPlayer.Play(GOManager.Instance.audioManager.backgroundMusic);
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteBatch2 = new SpriteBatch(GraphicsDevice);
            textureStorage = new Texture2DStorage();
            audioStorage = new SoundEffectStorage();
            textureStorage.LoadContent(Content);
            audioStorage.LoadContent(Content);
            audioStorage.loadAudioManager(audioManager);

            //load player stuff
            SpriteRenderer playerSpriteRenderer = new SpriteRenderer(new Rectangle(player.X, player.Y, 144, 144), true);
            playerSpriteRenderer.orderInLayer = .3f;
            textureStorage.loadPlayerAnimations(playerSpriteRenderer);
            player.AddComponent(playerSpriteRenderer);
            player.AddComponent(new BoxCollider(new Vector2(90, 144), new Vector2(25, 0), GraphicsDevice));
            player.type = "Player";
            playerState = new PlayerState(player);
            player.AddComponent(new PlayerController2(playerState));
            ScoreComponent playerScore = new ScoreComponent();
            player.AddComponent(playerScore);

            Texture2D hp3Texture = textureStorage.GetTexture("hp3");
            Texture2D hp2Texture = textureStorage.GetTexture("hp2");
            Texture2D[] hp1FlashingTextures = {
                textureStorage.GetTexture("hp1-v1"),
                textureStorage.GetTexture("hp1-v2"),
                textureStorage.GetTexture("hp1-v3")
            };
            Texture2D deadTexture = textureStorage.GetTexture("hpDead");

            Texture2D cardBack = textureStorage.GetTexture("CardBack");
            Texture2D cardFront = textureStorage.GetTexture("CardFront");

            HealthComponent playerHealth = player.GetComponent<HealthComponent>();
            playerScore = player.GetComponent<ScoreComponent>();
            UI = new UI(playerHealth, playerScore, hp3Texture, hp2Texture, hp1FlashingTextures, deadTexture, cardBack, cardFront, new Vector2(50, 650), _spriteBatch2);

            font = Content.Load<SpriteFont>("Font/Winter");
            texts = new TextSprite(font, "",new Vector2(0, 0), Color.White);

            menuController = new MenuController(playerState, font);

        }

        protected override void Update(GameTime gameTime)
        {   
            menuController.Update(gameTime);
            RemoveDestroyedObjects();
            if (menuController.StopGame())
            {
                cameraController.Update();
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                    ResetGame();
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    Exit();
                for(int i = 0; i < GOManager.Instance.allGOs.Count; i++)
                {
                    if (GOManager.Instance.allGOs[i].type == "PlayerProjectile")
                    {
                        GOManager.Instance.allGOs[i].GetComponent<SpriteRenderer>().enabled = false;
                    }
                    if (GOManager.Instance.allGOs[i].type == "VFX")
                    {
                        GOManager.Instance.allGOs[i].GetComponent<VisualEffectRenderer>().enabled = false;
                    }
                }
            }
            else
            {
                
                UpdateGameObject(gameTime);

                enemyController.Update(gameTime);
                savedPlayerLoc = player.position;

                cameraController.Update();
                UI.Update(gameTime);

                if (Keyboard.GetState().IsKeyDown(Keys.D0))
                {
                    saveLoc = true;
                }

                if (player.GetComponent<HealthComponent>().isDeadFull)
                {
                    ResetGame();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.R))
                    ResetGame();
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    Exit();
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {

                }

                if (keyboardController.OnKeyDown(Keys.L))

                    GOManager.Instance.IsDebugging = !GOManager.Instance.IsDebugging;
            }
            base.Update(gameTime);

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

            //texts.Draw(_spriteBatch);

            menuController.Draw(_spriteBatch);

            _spriteBatch.End();

            _spriteBatch2.Begin();
            UI.Draw();
            _spriteBatch2.End();
            base.Draw(gameTime);
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
            SpriteRenderer playerSpriteRenderer = new SpriteRenderer(new Rectangle(player.X, player.Y, 144, 144), true);
            audioManager.stopAll();
            audioManager.Dispose();
            audioStorage.loadAudioManager(audioManager);
            playerSpriteRenderer.orderInLayer = .3f;
            textureStorage.loadPlayerAnimations(playerSpriteRenderer);
            player.AddComponent(playerSpriteRenderer);
            player.AddComponent(new BoxCollider(new Vector2(90, 144), new Vector2(25, 0), GraphicsDevice));
            player.AddComponent(audioManager);
            player.type = "Player";
            player = new GameObject(0, 500, new List<IComponent> { new PlayerController2(playerState) });
            Initialize();
        }

        private void UpdateGameObject(GameTime gameTime)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject gameObject = gameObjects[i];
                gameObject.Update(gameTime);
            }
        }

        private void RemoveDestroyedObjects()
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
            }
        }
    }
}
