using Microsoft.Xna.Framework.Input;
using System;
using static IController;

public class KeyboardController : IKeyboardController
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
        bool keyDown = false;
        if (curKS.IsKeyDown(key) && !preKS.IsKeyDown(key))
            keyDown = true;
        return keyDown;
    }
}

