using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static IController;

public class KeyboardController : IController
{
    private KeyboardState curKS, preKS;

    //contructor
    public KeyboardController()
    {
        curKS = Keyboard.GetState();
        preKS = curKS;
    }

    //update current and past keys
    public void Update()
    {
        preKS = curKS;
        curKS = Keyboard.GetState();
    }

    //return if a key is pressed
    public bool OnKeyDown(Keys key)
    {
        return curKS.IsKeyDown(key) && !preKS.IsKeyDown(key);
    }

    //return horizontal movementinput
    public Vector2 GetMovementInput()
    {
        float horizontal = 0;

        if (curKS.IsKeyDown(Keys.A) || curKS.IsKeyDown(Keys.Left)) horizontal = -1;
        if (curKS.IsKeyDown(Keys.D) || curKS.IsKeyDown(Keys.Right)) horizontal = (horizontal < 0) ? 0 : 1;

        return new Vector2(horizontal, 0);
    }

    //return if jump is requested
    public bool IsJumpRequested()
    {
        return curKS.IsKeyDown(Keys.W) || curKS.IsKeyDown(Keys.Up);
    }

    //return if duck is requested
    public bool IsDuckRequested()
    {
        return curKS.IsKeyDown(Keys.S) || curKS.IsKeyDown(Keys.Down);
    }

    //return if damage is requested (will prob take out?)
    public bool IsDamageRequested()
    {
        return curKS.IsKeyDown(Keys.E);
    }

    //return if player attack is requested
    public bool IsShootRequested()
    {
        return curKS.IsKeyDown(Keys.Z) || curKS.IsKeyDown(Keys.N);
    }

    //return if attack switch is requested
    public bool IsProjectileSwitchRequested(int projectileIndex)
    {
        return curKS.IsKeyDown((Keys)Enum.Parse(typeof(Keys), $"D{projectileIndex}"));
    }
}
