using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static IController;

public class KeyboardController : IController
{
    private KeyboardState curKS, preKS;

    // Define delegates for key actions
    public Action OnReset { get; set; }
    public Action OnExit { get; set; }
    public Action OnSaveLocation { get; set; }
    public Action OnDebugToggle { get; set; }

    public KeyboardController()
    {
        curKS = Keyboard.GetState();
        preKS = curKS;
    }

    public void Update()
    {
        preKS = curKS;
        curKS = Keyboard.GetState();

        // Call assigned actions if specific keys are pressed
        if (OnKeyDown(Keys.R)) OnReset?.Invoke();
        if (OnKeyDown(Keys.Q)) OnExit?.Invoke();
        if (OnKeyDown(Keys.D0)) OnSaveLocation?.Invoke();
        if (OnKeyDown(Keys.L)) OnDebugToggle?.Invoke();
    }

    public bool OnKeyDown(Keys key)
    {
        return curKS.IsKeyDown(key) && !preKS.IsKeyDown(key);
    }

    public KeyboardState GetPreKey()
    {
        return preKS;
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

    public bool IsDashRequested()
    {
        return OnKeyDown(Keys.LeftShift);
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
    public bool IsAimUp()
    {
        return curKS.IsKeyDown(Keys.Up);
    }
    public bool IsAimDown()
    {
        return curKS.IsKeyDown(Keys.Down);
    }

    public bool IsProjectileSwitchRequested(int projectileIndex)
    {
        return curKS.IsKeyDown((Keys)Enum.Parse(typeof(Keys), $"D{projectileIndex}"));
    }
}
