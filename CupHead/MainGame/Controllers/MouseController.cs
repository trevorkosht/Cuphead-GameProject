using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using static IController;

public class MouseController : IMouseController
{
    MouseState curMS, preMS;
    public MouseController()
    {
        curMS = Mouse.GetState();
        preMS = curMS;
    }
    public void Update()
    {
        preMS = curMS;
        curMS = Mouse.GetState();
    }

    public bool OnMouseClick(MouseButton mouseButton)
    {
        bool mouseDown = false;
        if (mouseButton == MouseButton.Left)
            if (curMS.LeftButton == ButtonState.Pressed && preMS.LeftButton != ButtonState.Pressed)
                mouseDown = true;
        if (mouseButton == MouseButton.Right)
            if (curMS.RightButton == ButtonState.Pressed && preMS.RightButton != ButtonState.Pressed)
                mouseDown = true;
        return mouseDown;
    }

    public Point GetMousePosition()
    {
        return Mouse.GetState().Position;
    }
}
