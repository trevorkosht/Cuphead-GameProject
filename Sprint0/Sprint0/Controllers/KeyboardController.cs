using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static IController;

public class KeyboardController : IController
{
    private KeyboardState curKS, preKS;

    public KeyboardController()
    {
        curKS = Keyboard.GetState();
        preKS = curKS;
    }

    public void Update()
    {
        preKS = curKS;
        curKS = Keyboard.GetState();
    }

    public bool OnKeyDown(Keys key)
    {
        return curKS.IsKeyDown(key) && !preKS.IsKeyDown(key);
    }

    public Vector2 GetMovementInput()
    {
        float horizontal = 0;

        if (curKS.IsKeyDown(Keys.Left)) horizontal = -1;
        if (curKS.IsKeyDown(Keys.Right)) horizontal = (horizontal < 0) ? 0 : 1;

        return new Vector2(horizontal, 0);
    }

    public bool IsJumpRequested()
    {
        return curKS.IsKeyDown(Keys.Z);
    }

    public bool IsDuckRequested()
    {
        return curKS.IsKeyDown(Keys.Down);
    }

    public bool IsDamageRequested()
    {
        return curKS.IsKeyDown(Keys.E);
    }

    public bool IsShootRequested()
    {
        return curKS.IsKeyDown(Keys.X);
    }

    public bool IsProjectileSwitchRequested(int projectileIndex)
    {
        return curKS.IsKeyDown((Keys)Enum.Parse(typeof(Keys), $"D{projectileIndex}"));
    }
}
