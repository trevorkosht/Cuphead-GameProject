using Microsoft.Xna.Framework.Input;
using Sprint0;

public class MouseController : IController
{
    private MouseState currentMouseState;
    private int screenWidth;
    private int screenHeight;
    private Game1 game;

    public void Update()
    {
        currentMouseState = Mouse.GetState();
    }
    public MouseController(Game1 game, int screenWidth, int screenHeight)
    {
        this.game = game;
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
    }

    public void HandleInputs()
    {

        if (currentMouseState.LeftButton == ButtonState.Pressed)
        {
            if (currentMouseState.X < screenWidth / 2 && currentMouseState.Y < screenHeight / 2)
            {
                // Top-left: Display a non-moving, non-animated sprite
                game.SetCurrentSpriteToNonMovingNonAnimated();
            }
            else if (currentMouseState.X >= screenWidth / 2 && currentMouseState.Y < screenHeight / 2)
            {
                // Top-right: Display a non-moving, animated sprite
                game.SetCurrentSpriteToNonMovingAnimated();
            }
            else if (currentMouseState.X < screenWidth / 2 && currentMouseState.Y >= screenHeight / 2)
            {
                // Bottom-left: Display a moving, non-animated sprite
                game.SetCurrentSpriteToMovingNonAnimated();
            }
            else if (currentMouseState.X >= screenWidth / 2 && currentMouseState.Y >= screenHeight / 2)
            {
                // Bottom-right: Display a moving, animated sprite 
                game.SetCurrentSpriteToMovingAnimated();
            }
        }
        else if (currentMouseState.RightButton == ButtonState.Pressed)
        {
            game.Exit();
        }
    }

    public void HandleEvents()
    {

    }
}
