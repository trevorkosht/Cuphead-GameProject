using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public interface IController
{ 
    void Update();
}

public enum MouseButton
{
    Left,
    Right,
    Middle
}
