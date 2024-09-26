using Cuphead.Controllers;
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
        private SpriteFont _font;

        private Texture2DStorage textureStorage;

        private KeyboardController keyboardController;
        private IMouseController mouseController;

        private List<GameObject> gameObjects = new List<GameObject>();

        //Example of how to make a GameObject
        GameObject player = new GameObject(50, 50, new List<IComponent> { new PlayerController(), new ProjectileManager() });

        private EnemyController enemyController;

        private BlockController blockController;
        private Cuphead.Items.Items items;
        private ItemController itemControl;


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
            GOManager.Instance.textureStorage = textureStorage;
            enemyController = new EnemyController(keyboardController, textureStorage);
            blockController = new BlockController(textureStorage);
            itemControl = new ItemController();

            gameObjects.Add(player);
        }

        protected override void LoadContent() //Load sprites, fonts, etc. here
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textureStorage = new Texture2DStorage();
            textureStorage.LoadContent(Content);

            _font = Content.Load<SpriteFont>("Font");


            //item animation
            Texture2D itemPart1 = textureStorage.GetTexture("Item1_3");
            Texture2D itemPart2 = textureStorage.GetTexture("Item4_6");
            items = new Items(itemPart1, itemPart2, 2);

            Animation seedAnimation = new Animation(textureStorage.GetTexture("Seed"), 5, 8, 144, 144);
            Animation purpleSporeAnimation = new Animation(textureStorage.GetTexture("PurpleSpore"), 5, 16, 144, 144);
            Animation pinkSporeAnimation = new Animation(textureStorage.GetTexture("PinkSpore"), 5, 8, 144, 144);

            SpriteRenderer playerSpriteRenderer = new SpriteRenderer(player, true, new Rectangle(player.X, player.Y, 144, 144), true);
            textureStorage.loadPlayerAnimations(playerSpriteRenderer);
            player.AddComponent(playerSpriteRenderer);
        }

        protected override void Update(GameTime gameTime) //Update stuff here
        {
            foreach(var gameObject in gameObjects) {
                gameObject.Update(gameTime);
            }

            enemyController.Update(gameTime);
            blockController.Update(gameTime);
            items.update(gameTime, 580, 330);
            itemControl.Update(items);

            if (Keyboard.GetState().IsKeyDown(Keys.R))
                ResetGame();
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                Exit();

            base.Update(gameTime);
        }

        private void ResetGame()    
        {
            gameObjects.Clear();
            player = new GameObject(50, 50, new List<IComponent> { new PlayerController(), new ProjectileManager() });
            Initialize();
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);
            _spriteBatch.Begin(); //Draw stuff here

            foreach (var gameObject in gameObjects) {
                gameObject.Draw(_spriteBatch);
            }
            blockController.Draw(_spriteBatch);
            items.draw(_spriteBatch);
            enemyController.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
