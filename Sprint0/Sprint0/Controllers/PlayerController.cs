using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static IController;

public class PlayerController : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;

    //private Rigidbody rigidbody;

    public float Speed { get; set; } = 300f;
    public float JumpForce { get; set; } = -350f;
    public bool IsGrounded { get; set; } = false;
    public Vector2 velocity;
    public float GroundLevel { get; set; } = 300f; // Arbitrary floor height
    public float Gravity { get; set; } = 500f;     // Constant downward force
    float airTime = 0f;




    public PlayerController() { }

    private IKeyboardController keyboardController = new KeyboardController();
    private IMouseController mouseController = new MouseController();

    public void Update(GameTime gameTime)
    {
        if (!enabled) return;

        keyboardController.Update();
        mouseController.Update();

        //transform = GameObject.GetComponent<Transform>();
        //rigidbody = GameObject.GetComponent<RigidbodyComponent>();

        if (true) //temporary since I removed transform, && rigidbody != null)
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

            // Movement
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left)) 
            {
                input.X = -1;
                GameObject.GetComponent<SpriteRenderer>().isFacingRight = false;
            }    

            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                if (input.X < 0) //No input if both left/right are pressed
                    input.X = 0;
                else {
                    input.X = 1;
                    GameObject.GetComponent<SpriteRenderer>().isFacingRight = true;
                }
            }

            if ((state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down)) && IsGrounded) // Duck
            {
                input.X = 0;
                //Ducking logic
            }
            else if ((state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up)) && IsGrounded) // Jump
            {
                velocity.Y = JumpForce;
                IsGrounded = false;
            }

            if (GameObject.Y >= GroundLevel)
            {
                airTime = 1;
                IsGrounded = true;
                if(velocity.Y > 0)
                    velocity.Y = 0;
            }

            // Apply gravity if not grounded
            if (!IsGrounded)
            {
                airTime += deltaTime;
                velocity.Y += Gravity * deltaTime * airTime;  // Gravity pulls down
            }

            if (state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.N)) // Shoot logic
            {
                GameObject.GetComponent<ProjectileManager>().FireProjectile();
            }

            for (int i = 1; i <= 5; i++)
            {
                if (state.IsKeyDown((Keys)Enum.Parse(typeof(Keys), $"D{i}"))) // Handle the key press for D1, D2, D3, etc. (switch projectile based off of i)
                {
                    GameObject.GetComponent<ProjectileManager>().projectileType = i;
                    break;
                }
            }

            GameObject.X += (int)(input.X * Speed * deltaTime);
            GameObject.Y += (int)(velocity.Y * deltaTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch) { /* Non-visual */ }
}