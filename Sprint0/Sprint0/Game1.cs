﻿using Microsoft.Xna.Framework;
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
            

            //Animation setup testing:
            Texture2D playerJumpTexture = textureStorage.GetTexture("PlayerJump");
            Animation playerJumpAnimation = new Animation(playerJumpTexture, 5, 8, 81, 109);

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

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            _spriteBatch.Begin(); //Draw stuff here

            player.Draw(_spriteBatch);
            blockController.Draw(_spriteBatch);

            //enemyController.Draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
