using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public interface IController
{

    public interface IKeyboardController
    {
        void Update();
        bool OnKeyDown(Keys key);
    }
    public interface IMouseController
    {
        void Update();
        Point GetMousePosition();
        bool OnMouseClick(MouseButton mouseButton);
    }

}

public enum MouseButton
{
    Left,
    Right
}