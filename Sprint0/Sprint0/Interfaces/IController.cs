using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public interface IController
{
    public interface IKeyboardController
    {
        /// <summary>
        /// Updates the internal state of the keyboard input.
        /// </summary>
        void Update();

        /// <summary>
        /// Checks if a specified key was pressed in the current frame.
        /// </summary>
        bool OnKeyDown(Keys key);

        Vector2 GetMovementInput();

        /// <summary>
        /// Checks if the jump key is pressed.
        /// </summary>
        bool IsJumpRequested();

        /// <summary>
        /// Checks if the duck key is pressed.
        /// </summary>
        bool IsDuckRequested();

        bool IsDamageRequested();

        /// <summary>
        /// Checks if the shoot key is pressed.
        /// </summary>
        bool IsShootRequested();

        /// <summary>
        /// Checks if a key to switch projectiles is pressed.
        /// </summary>
        bool IsProjectileSwitchRequested(int projectileIndex);
    }

    public interface IMouseController
    {
        /// <summary>
        /// Updates the internal state of the mouse input.
        /// </summary>
        void Update();

        /// <summary>
        /// Gets the current position of the mouse as a Point.
        /// </summary>
        Point GetMousePosition();

        /// <summary>
        /// Checks if a specified mouse button is clicked.
        /// </summary>
        bool OnMouseClick(MouseButton mouseButton);

    }
}

public enum MouseButton
{
    Left,
    Right,
    Middle // Added middle button for more flexibility.
}
