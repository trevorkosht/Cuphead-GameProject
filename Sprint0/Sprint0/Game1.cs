using Cuphead.Controllers;
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
            enemyController = new EnemyController(keyboardController, textureStorage);
            blockController = new BlockController(textureStorage);
            itemControl = new ItemController();
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

            //Load player animations
            Animation playerDeathAnimation = new Animation(textureStorage.GetTexture("PlayerDeath"), 5, 16, 144, 144);
            Animation playerHitAirAnimation = new Animation(textureStorage.GetTexture("PlayerHitAir"), 5, 6, 144, 144);
            Animation playerHitGroundAnimation = new Animation(textureStorage.GetTexture("PlayerHitGround"), 5, 6, 144, 144);
            Animation playerIdleAnimation = new Animation(textureStorage.GetTexture("PlayerIdle"), 5, 5, 144, 144);
            Animation playerIntroAnimation = new Animation(textureStorage.GetTexture("PlayerIntro"), 5, 28, 144, 144);
            Animation playerJumpAnimation = new Animation(textureStorage.GetTexture("PlayerJump"), 5, 8, 144, 144);
            Animation playerRunAnimation = new Animation(textureStorage.GetTexture("PlayerRun"), 5, 16, 144, 144);
            Animation playerRunShootingDiagonalUpAnimation = new Animation(textureStorage.GetTexture("PlayerRunShootingDiagonalUp"), 5, 16, 144, 144);
            Animation playerRunShootingStraightAnimation = new Animation(textureStorage.GetTexture("PlayerRunShootingStraight"), 5, 16, 144, 144);
            Animation playerShootDiagonalDownAnimation = new Animation(textureStorage.GetTexture("PlayerShootDiagonalDown"), 5, 3, 144, 144);
            Animation playerShootDiagonalUpAnimation = new Animation(textureStorage.GetTexture("PlayerShootDiagonalUp"), 5, 3, 144, 144);
            Animation playerShootDownAnimation = new Animation(textureStorage.GetTexture("PlayerShootDown"), 5, 3, 144, 144);
            Animation playerShootStraightAnimation = new Animation(textureStorage.GetTexture("PlayerShootStraight"), 5, 3, 144, 144);
            Animation playerShootUpAnimation = new Animation(textureStorage.GetTexture("PlayerShootUp"), 5, 3, 144, 144);

            Animation seedAnimation = new Animation(textureStorage.GetTexture("Seed"), 5, 8, 144, 144);
            Animation purpleSporeAnimation = new Animation(textureStorage.GetTexture("PurpleSpore"), 5, 16, 144, 144);
            Animation pinkSporeAnimation = new Animation(textureStorage.GetTexture("PinkSpore"), 5, 8, 144, 144);

            SpriteRenderer playerSpriteRenderer = new SpriteRenderer(player, true, new Rectangle(player.X, player.Y, 144, 144), true);
            player.AddComponent(playerSpriteRenderer);

            playerSpriteRenderer.addAnimation("Death", playerDeathAnimation);
            playerSpriteRenderer.addAnimation("HitAir", playerHitAirAnimation);
            playerSpriteRenderer.addAnimation("HitGround", playerHitGroundAnimation);
            playerSpriteRenderer.addAnimation("Idle", playerIdleAnimation);
            playerSpriteRenderer.addAnimation("Intro", playerIntroAnimation);
            playerSpriteRenderer.addAnimation("Jump", playerJumpAnimation);
            playerSpriteRenderer.addAnimation("Run", playerRunAnimation);
            playerSpriteRenderer.addAnimation("RunShootingDiagonalUp", playerRunShootingDiagonalUpAnimation);
            playerSpriteRenderer.addAnimation("RunShootingStraight", playerRunShootingStraightAnimation);
            playerSpriteRenderer.addAnimation("ShootDiagonalDown", playerShootDiagonalDownAnimation);
            playerSpriteRenderer.addAnimation("ShootDiagonalUp", playerShootDiagonalUpAnimation);
            playerSpriteRenderer.addAnimation("ShootDown", playerShootDownAnimation);
            playerSpriteRenderer.addAnimation("ShootStraight", playerShootStraightAnimation);
            playerSpriteRenderer.addAnimation("ShootUp", playerShootUpAnimation);

            //Load spritesheets as individual animation frames
            playerSpriteRenderer.loadAllAnimations();

            //Sets the player's current animation 
            playerSpriteRenderer.setAnimation("Jump");
        }

        protected override void Update(GameTime gameTime) //Update stuff here
        {
            player.Update(gameTime);

            enemyController.Update(gameTime);
            blockController.Update(gameTime);
            items.update(gameTime, 580, 330);
            itemControl.Update(items);
            

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            _spriteBatch.Begin(); //Draw stuff here

            player.Draw(_spriteBatch);
            blockController.Draw(_spriteBatch);
            items.draw(_spriteBatch);

            enemyController.Draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
