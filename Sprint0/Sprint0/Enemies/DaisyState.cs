using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DaisyState : IComponent {
    public bool enabled { get; set; }
    public GameObject GameObject { get; set; }

    public DaisyState() { }

    //Booleans
    public bool Spawned = false;
    public bool isSpawning = true;
    public bool isWalking = false;
    public bool isGrounded = false;
    public bool atPlatformEdge = false;
    public bool isTurning = false;
    public bool jumpRequested = false;
    public bool isJumping = false;
    public bool foundAdjacentPlatform = false;
    //Other
    public GameObject currentPlatform { get; set; }
    public Rectangle landingSpot { get; set; }
    public int edgeCheckDistance = 35; 
    public int minJumpHeight = 35; 

    private float speed;
    private Vector2 airVelocity;
    private int airTime = 50;
    private float gravity = 0.5f;
    public bool movingRight { get; private set; }
    public float turnDelay { get; private set; } = -1.0f;



    public void Update(GameTime gameTime) {
        isWalking = isGrounded && !isTurning && !jumpRequested && !isJumping;
    }

    public void Draw(SpriteBatch spriteBatch) {
        /* Non-visual component. */
    }

}