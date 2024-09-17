﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static IController;

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private Texture2D playerTexture;

        private IKeyboardController keyboardController;
        private IMouseController mouseController;

        //Example of how to make a GameObject
        GameObject player = new GameObject(new List<IComponent> { new Transform(new Vector2(100, 100)), new SpriteRenderer(null), new PlayerController() });


        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        protected override void Initialize()
        {
            base.Initialize();
            player.GetComponent<SpriteRenderer>().texture = playerTexture; //This is how to set the frame of the animation for the player
        }

        protected override void LoadContent() //Load sprites, fonts, etc. here
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("Font");
            playerTexture = Content.Load<Texture2D>("ch-jump"); //Replace by adding an animator component (holds animations and transition states)

        }

        protected override void Update(GameTime gameTime) //Update stuff here
        {
            player.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            _spriteBatch.Begin(); //Draw stuff here

            player.Draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }