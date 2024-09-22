using Cuphead.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Controllers;
using Sprint0.Interfaces;
using System.Collections.Generic;
using static IController;


namespace Sprint0
{
    public class Game1 : Game
    {
        private static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private Texture2D playerTexture;
        private Texture2DStorage textureStorage;

        private KeyboardController keyboardController;
        private IMouseController mouseController;

        //Example of how to make a GameObject
        GameObject player = new GameObject(50, 50, new List<IComponent> { new PlayerController() });

        //controls the list of enemies and the cycle between them
        //private EnemyController enemyController;

        private BlockController blockController;
        private IAnimation items;


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
            //enemyController = new EnemyController(keyboardController, textureStorage);
            blockController = new BlockController(keyboardController, textureStorage);
        }

        protected override void LoadContent() //Load sprites, fonts, etc. here
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textureStorage = new Texture2DStorage();
            textureStorage.LoadContent(Content);

            _font = Content.Load<SpriteFont>("Font");
            //textureStorage.LoadContent(Content);


            //item animation
            Texture2D itemPart1 = textureStorage.GetTexture("Item1_3");
            Texture2D itemPart2 = textureStorage.GetTexture("Item4_6");
            items = new Items(itemPart1, itemPart2, 6);

            //Animation setup testing:
            Texture2D playerDeathTexture = textureStorage.GetTexture("PlayerDeath");
            Texture2D playerHitAirTexture = textureStorage.GetTexture("PlayerHitAir");
            Texture2D playerHitGroundTexture = textureStorage.GetTexture("PlayerHitGround");
            Texture2D playerIdleTexture = textureStorage.GetTexture("PlayerIdle");
            Texture2D playerIntroTexture = textureStorage.GetTexture("PlayerIntro");
            Texture2D playerJumpTexture = textureStorage.GetTexture("PlayerJump");
            Texture2D playerRunTexture = textureStorage.GetTexture("PlayerRun");
            Texture2D playerRunShootingDiagonalUpTexture = textureStorage.GetTexture("PlayerRunShootingDiagonalUp");
            Texture2D playerRunShootingStraightTexture = textureStorage.GetTexture("PlayerRunShootingStraight");
            Texture2D playerShootDiagonalDownTexture = textureStorage.GetTexture("PlayerShootDiagonalDown");
            Texture2D playerShootDiagonalUpTexture = textureStorage.GetTexture("PlayerShootDiagonalUp");
            Texture2D playerShootDownTexture = textureStorage.GetTexture("PlayerShootDown");
            Texture2D playerShootStraightTexture = textureStorage.GetTexture("PlayerShootStraight");
            Texture2D playerShootUpTexture = textureStorage.GetTexture("PlayerShootUp");

            Animation playerDeathAnimation = new Animation(playerDeathTexture, 5, 16, 144, 144);
            Animation playerHitAirAnimation = new Animation(playerHitAirTexture, 5, 6, 144, 144);
            Animation playerHitGroundAnimation = new Animation(playerHitGroundTexture, 5, 6, 144, 144);
            Animation playerIdleAnimation = new Animation(playerIdleTexture, 5, 5, 144, 144);
            Animation playerIntroAnimation = new Animation(playerIntroTexture, 5, 28, 144, 144);
            Animation playerJumpAnimation = new Animation(playerJumpTexture, 5, 8, 144, 144);
            Animation playerRunAnimation = new Animation(playerRunTexture, 5, 16, 144, 144);
            Animation playerRunShootingDiagonalUpAnimation = new Animation(playerRunShootingDiagonalUpTexture, 5, 16, 144, 144);
            Animation playerRunShootingStraightAnimation = new Animation(playerRunShootingStraightTexture, 5, 16, 144, 144);
            Animation playerShootDiagonalDownAnimation = new Animation(playerShootDiagonalDownTexture, 5, 3, 144, 144);
            Animation playerShootDiagonalUpAnimation = new Animation(playerShootDiagonalUpTexture, 5, 3, 144, 144);
            Animation playerShootDownAnimation = new Animation(playerShootDownTexture, 5, 3, 144, 144);
            Animation playerShootStraight = new Animation(playerShootStraightTexture, 5, 3, 144, 144);
            Animation playerShootUpAnimation = new Animation(playerShootUpTexture, 5, 3, 144, 144);


            SpriteRenderer playerSpriteRenderer = new SpriteRenderer(player, true, new Rectangle(player.X, player.Y, 81, 109), true);
            player.AddComponent(playerSpriteRenderer);
            playerSpriteRenderer.addAnimation("jump", playerJumpAnimation);
            playerSpriteRenderer.loadAllAnimations();

            playerSpriteRenderer.setAnimation("jump");
        }

        protected override void Update(GameTime gameTime) //Update stuff here
        {
            player.Update(gameTime);

            //enemyController.Update(gameTime);
            blockController.Update(gameTime);
            items.update(gameTime, 400, 460);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            _spriteBatch.Begin(); //Draw stuff here

            player.Draw(_spriteBatch);
            blockController.Draw(_spriteBatch);
            items.draw(_spriteBatch);

            //enemyController.Draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
