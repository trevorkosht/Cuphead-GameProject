using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using static IController;

public class MouseController : IController
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

    //return if a mouse button is clicked
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

    //return the mouse position
    public Point GetMousePosition()
    {
        return Mouse.GetState().Position;
    }
}
