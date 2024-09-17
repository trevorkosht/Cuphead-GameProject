﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static IController;

public class PlayerController : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;

    private Transform transform;
    //private Rigidbody rigidbody;

    public float Speed { get; set; } = 150f;
    public float JumpForce { get; set; } = -350f;
    //public bool IsGrounded { get; set; } = false;

    public PlayerController() { }

    private IKeyboardController keyboardController = new KeyboardController();
    private IMouseController mouseController = new MouseController();

    public void Update(GameTime gameTime)
    {
        if (!enabled) return;

        keyboardController.Update();
        mouseController.Update();

        transform = GameObject.GetComponent<Transform>();
        //rigidbody = GameObject.GetComponent<RigidbodyComponent>();

        if (transform != null) //&& rigidbody != null)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 input = new Vector2(0, 0);

            //if (IsGrounded && InputHelper.IsKeyDown(Keys.Space)) // Jump
            //{
            //rigidbody.Velocity = new Vector2(rigidbody.Velocity.X, JumpForce);
            //IsGrounded = false;
            //}

            KeyboardState state = Keyboard.GetState();
            input = new Vector2(0, 0);

            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) 
                input.X = -1;
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                if(input.X < 0) //No input if both left/right are pressed
                    input.X = 0;
                else
                    input.X = 1;
            }

            transform.Position += input * Speed * deltaTime;
        }
    }

    public void Draw(SpriteBatch spriteBatch) { /* Non-visual */ }
}