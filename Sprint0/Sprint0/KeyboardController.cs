using Microsoft.Xna.Framework.Input;
using Sprint0;

public class KeyboardController : IController
{
    private KeyboardState currentKeyState;
    private Game1 game;

    public KeyboardController(Game1 game)
    {
        this.game = game;
    }


    public void Update()
    {
        currentKeyState = Keyboard.GetState();
    }


    public void HandleInputs()
    {
        if (currentKeyState.IsKeyDown(Keys.D1))
        {
            game.SetCurrentSpriteToNonMovingNonAnimated();
        }

        if (currentKeyState.IsKeyDown(Keys.D2))
        {
            game.SetCurrentSpriteToNonMovingAnimated();
        }

        if (currentKeyState.IsKeyDown(Keys.D3))
        {
            game.SetCurrentSpriteToMovingNonAnimated();
        }

        if (currentKeyState.IsKeyDown(Keys.D4))
        {
            game.SetCurrentSpriteToMovingAnimated();
        }

        if (currentKeyState.IsKeyDown(Keys.Escape))
        {
            game.Exit();
        }
    }

    public void HandleEvents()
    {

    }
}
