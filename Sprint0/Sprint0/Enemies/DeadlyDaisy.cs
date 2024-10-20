using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class DeadlyDaisy : BaseEnemy
{
    private float speed;
    private float jumpHeight;
    private bool isJumping;
    private bool jumpRequested = false;
    private Vector2 velocity;
    private bool movingRight;
    private float turnDelay = -1.0f;
    private bool atEdge = false;
    private BoxCollider jumpCheckCollider;


    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("Spawn");
        speed = 300f;
        jumpHeight = 800f;
        isJumping = false;
        velocity = Vector2.Zero;
    }

    public override void Move(GameTime gameTime)
    {
        if(GameObject.Y < 0 && GameObject.X - player.X > 1500) {
            return;
        }

        sRend.isFacingRight = !movingRight;

        if (movingRight) {
            jumpCheckCollider = new BoxCollider(new Vector2(135, 155), new Vector2(215, -5), GOManager.Instance.GraphicsDevice);
        }
        else {
            jumpCheckCollider = new BoxCollider(new Vector2(135, 155), new Vector2(-215, -5), GOManager.Instance.GraphicsDevice);
        }
        jumpCheckCollider.GameObject = GameObject;
        GameObject.AddComponent(jumpCheckCollider);

        Vector2 playerPosition = new Vector2(player.X, player.Y);

        if (jumpRequested) {
            //set jump anim
            sRend.setAnimation("Jump");

            //check for frame where leaves ground, then call actual jump method


            //check for 

            Jump(jumpCheckCollider);
            jumpRequested = false;
        }
        else if (IsGrounded())
        {
            if (atEdge) {
                sRend.setAnimation("Turn");
                if (movingRight) {
                    GameObject.X += 1;
                }
                else {
                    GameObject.X -= 1;
                }
                if (sRend.currentAnimation.Value.CurrentFrame == 17) {
                    movingRight = !movingRight;
                    sRend.setAnimation("deadlyDaisyAnimation");
                    atEdge = false;
                    turnDelay = 0.5f;
                }
            }
            else {
                sRend.setAnimation("deadlyDaisyAnimation");
                if (movingRight)
                    GameObject.X += (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                else
                    GameObject.X -= (int)(speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

                if (turnDelay <= 0) {
                    atEdge = ReachedEdge();
                }
                turnDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            velocity.Y = 0;
        }
        if (!IsGrounded()) {
            {
                // Apply gravity if jumping
                velocity.Y += 100f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                GameObject.Y += (int)(velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
                movingRight = player.X > GameObject.X;

                //sRend.setAnimation("Spawn"); // Jumping animation
            }
        }

    }


    private bool ReachedEdge() {
        BoxCollider daisyCollider = GameObject.GetComponent<BoxCollider>();
        bool isOnPlatform = false;

        foreach (GameObject GO in GOManager.Instance.allGOs) {
            if (GO != null && GO.type != null) {
                BoxCollider platformCollider = GO.GetComponent<BoxCollider>();
                float leftEdge = daisyCollider.BoundingBox.Left;
                float rightEdge = daisyCollider.BoundingBox.Right;
                float topEdge = daisyCollider.BoundingBox.Top;

                if (platformCollider != null && platformCollider.Intersects(jumpCheckCollider)) {
                    if(topEdge <= platformCollider.BoundingBox.Top && leftEdge > platformCollider.BoundingBox.Left && rightEdge < platformCollider.BoundingBox.Right) {
                        jumpRequested = true;
                        return false;
                    }
                }

                if (platformCollider != null && GO.Y > GameObject.Y + 50 && (platformCollider.BoundingBox.Left <= leftEdge - 50 && platformCollider.BoundingBox.Right >= rightEdge + 50)) {
                    isOnPlatform = true;
                }
            }
        }

        return !isOnPlatform;
    }

    private void Jump(BoxCollider landingSpot) {


        
    }

    private bool IsGrounded()
    {
        // Check collision with blocks from BlockFactory to see if standing on the ground
        BoxCollider collider = GameObject.GetComponent<BoxCollider>();

        foreach (GameObject gameObject in GOManager.Instance.allGOs)
        {
            if (gameObject != null && gameObject.type != null)
            {
                if (gameObject.type.Contains("Platform") || gameObject.type.Contains("Hill"))
                {
                    if (collider.Intersects(gameObject.GetComponent<Collider>()))
                    {
                        return true;
                    }
                }
            }           
        }
        return false;
    }


    public override void Shoot(GameTime gameTime)
    {
        // Implement shooting logic if needed
    }
}
