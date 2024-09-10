using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Sprint0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private IController keyboardController;
        private IController mouseController;
        private ISprite currentSprite;
        private TextSprite textSprite;



        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            keyboardController = new KeyboardController(this);
            mouseController = new MouseController(this, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("Font");
            textSprite = new TextSprite(_font, "Credits\nProgram Made By: Trevor Kosht\nSprites From URL https://www.mariouniverse.com/sprites-gbc-smbd/", new Vector2(800, 650), Color.Black);

            SetCurrentSpriteToNonMovingNonAnimated();


        }

        protected override void Update(GameTime gameTime)
        {

            keyboardController.Update();
            mouseController.Update();

            keyboardController.HandleInputs();
            mouseController.HandleInputs();

            currentSprite.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            _spriteBatch.Begin();

            currentSprite.Draw(_spriteBatch);
            textSprite.Draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }


        public void SetCurrentSpriteToNonMovingNonAnimated()
        {
            Texture2D texture = Content.Load<Texture2D>("characters");
            currentSprite = new NonMovingNonAnimatedSprite(texture, new Vector2(200, 100));
        }

        public void SetCurrentSpriteToNonMovingAnimated()
        {
            Texture2D texture = Content.Load<Texture2D>("characters");
            currentSprite = new NonMovingAnimatedSprite(texture, new Vector2(800, 100));
        }

        public void SetCurrentSpriteToMovingNonAnimated()
        {
            Texture2D texture = Content.Load<Texture2D>("characters");
            currentSprite = new MovingNonAnimatedSprite(texture, new Vector2(200, 500));
        }

        public void SetCurrentSpriteToMovingAnimated()
        {
            Texture2D texture = Content.Load<Texture2D>("characters");
            currentSprite = new MovingAnimatedSprite(texture, new Vector2(800, 500));
        }

    }
}
