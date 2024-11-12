using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class DaisyState : IComponent {
    public bool enabled { get; set; }
    public GameObject GameObject { get; set; }

    public DaisyState() { } 

    //Booleans
    public bool isGrounded { get; set; } = false;
    public bool jumpRequested { get; set; } = false;
    public bool atPlatformEdge { get; set; } = false;
    public bool isTurning { get; set; } = false;
    public bool isChargingJump { get; set; } = false;
    public bool isJumping { get; set; } = false;
    public bool foundAdjacentPlatform { get; set; } = false;

    //Other
    public GameObject currentPlatform { get; set; }
    public Rectangle landingSpot { get; set; }
    public int edgeCheckDistance = 50;
    public int minJumpHeight = 25;

    private float speed;
    private Vector2 airVelocity;
    private int airTime = 50;
    private float gravity = 0.5f;
    public bool movingRight { get; private set; }
    public float turnDelay { get; private set; } = -1.0f;



    public void Update(GameTime gameTime) {

    }

    public void Draw(SpriteBatch spriteBatch) {
        /* Non-visual component. */
    }

}