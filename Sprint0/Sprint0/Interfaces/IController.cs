using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public interface IController
{
    /// <summary>
    /// Updates the internal state of the mouse input.
    /// </summary>
    void Update();
}

public enum MouseButton
{
    Left,
    Right,
    Middle // Added middle button for more flexibility.
}
