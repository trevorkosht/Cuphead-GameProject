using Cuphead;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using System;
using static IController;

public class PlayerController : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;

    public float Speed { get; set; } = 700f;
    public float JumpForce { get; set; } = -1150f;
    public bool IsGrounded { get; set; } = false;
    public Vector2 velocity;
    public float GroundLevel { get; set; } = 500f; 
    public float Gravity { get; set; } = 1200f;
    public float timeTillNextBullet { get; set; } = .2f;
    public float timeTillNextHit { get; set; } = .4f;
    public float dashDuration = 1f;//about 1 second
    public float dashSpeed = 600f;// about 600 pixel
    public int timeTillNextDash { get; set; } = 1000;
    public int height;

    public int Health { get; set; } = 100;

    private float airTime = 0f, shootTime = 0f, hitTime = 0f, dashTime = 0f;
    private int floorY;
    private bool IsDucking, IsRunning, IsInvincible, isDuckingYAdjust, isShooting, IsDashing = false;

    private readonly KeyboardController keyboardController = new KeyboardController();
    private readonly MouseController mouseController = new MouseController();

    private DelayGame gameDelay = new DelayGame();

    private const int DuckingYOffset = 50;
    private const float InvincibilityDuration = 0.5f;

    private ProjectileType currentProjectileType = ProjectileType.Peashooter;
    BoxCollider Collider;

    float deltaTime;

    public PlayerController() 
    {
    }

    public void Update(GameTime gameTime)
    {
        if (!enabled) return;
        Collider = GameObject.GetComponent<BoxCollider>();

        keyboardController.Update();
        mouseController.Update();

        var state = Keyboard.GetState();
        var animator = GameObject.GetComponent<SpriteRenderer>();
        deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        UpdateTimers(deltaTime);
        HandleGroundCheck(animator);
        HandleMovementAndActions(gameTime, deltaTime);
        HandleShooting(state, animator);
        HandleProjectileSwitching(state);
        HandleDamageDetection();
        UpdateGravity(deltaTime);
        UpdateAnimationState(animator);
    }

    private void UpdateTimers(float deltaTime)
    {
        shootTime -= deltaTime;
        hitTime -= deltaTime;
        dashTime -= deltaTime;
        dashTime -= deltaTime;

        if (IsInvincible)
        {
            hitTime -= deltaTime;
            if (hitTime <= 0)
            {
                IsInvincible = false;
            }
        }
    }

    private void HandleGroundCheck(SpriteRenderer animator)
    {
        if (!isDuckingYAdjust)
        {
            if (GameObject.Y >= GroundLevel)
            {
                IsGrounded = true;
                floorY = (int)GroundLevel;
                airTime = 1;

                if (velocity.Y > 0) velocity.Y = 0;
                GameObject.Y = floorY;
            }
            else
                IsGrounded = false;
        }
    }

    private void HandleMovementAndActions(GameTime gameTime, float deltaTime)
    {
        Vector2 input = keyboardController.GetMovementInput();
        bool jumpRequested = keyboardController.IsJumpRequested();
        bool duckRequested = keyboardController.IsDuckRequested();
        bool dashRequested = keyboardController.IsDashRequested();

        UpdateFacingDirection(input);

        HandleDucking(duckRequested);

        if (!IsDucking)
        {
            if (input.X != 0 && IsGrounded)
            {
                IsRunning = true;
            }
            else
            {
                IsRunning = false;
            }
            GameObject.X += (int)(input.X * Speed * deltaTime);
            
            HandleDash(dashRequested, gameTime);
        }

        if (jumpRequested && IsGrounded && !IsDucking)
        {
            velocity.Y = JumpForce;
            IsGrounded = false;
        }

    }

    private void UpdateFacingDirection(Vector2 input)
    {
        if (input.X < 0 && !IsDashing)
        {
            GameObject.GetComponent<SpriteRenderer>().isFacingRight = false;
        }
        else if (input.X > 0 && !IsDashing)
        {
            GameObject.GetComponent<SpriteRenderer>().isFacingRight = true;
        }
    }

    private void HandleDash(bool dashRequested, GameTime gameTime)
    {
        if (IsDashing)
        {
            // Continue dashing
            PerformDash(gameTime, height);
        }
        else if (dashRequested && gameDelay.Cooldown(gameTime, 1500))
        {
            // Start dash
            IsDashing = true;
            dashTime = dashDuration; 
            height = GameObject.Y;
            Gravity = 0;
            Speed = 0;
            CreateDustEffect();
            UpdateAnimationState(GameObject.GetComponent<SpriteRenderer>());
            PerformDash(gameTime, height);
        }
    }

    private void PerformDash(GameTime gameTime, int height)
    {   
        qif (dashTime > 0)
        {
            // Continue dashing within the duration
            float dashDistance = dashSpeed * deltaTime;

            if (GameObject.GetComponent<SpriteRenderer>().isFacingRight)
            {
                GameObject.X += (int)dashDistance;
            }
            else
            {
                GameObject.X -= (int)dashDistance;
            }
            GameObject.Y = height;
        }
        else
        {
            // Dash duration is over, stop dashing
            IsDashing = false;
            Gravity = 1200f;
            Speed = 700f;
        }
    }


    private void HandleDucking(bool duckRequested)
    {
        var animator = GameObject.GetComponent<SpriteRenderer>();
        if (hitTime > 0) return;

        if (duckRequested && IsGrounded)
        {
            if (!IsDucking)
            {
                GameObject.Y = floorY + DuckingYOffset;
                IsDucking = true;
                isDuckingYAdjust = true;

                Collider.bounds = new Vector2(144, 70);
                Collider.offset = new Vector2(0, 30);
            }
        }
        else
        {
            if (IsDucking)
            {
                GameObject.Y = floorY;
                IsDucking = false;
                isDuckingYAdjust = false;
                Collider.bounds = new Vector2(90, 144);
                Collider.offset = new Vector2(25, 0);
            }
        }
    }

    private void HandleProjectileSwitching(KeyboardState state)
    {
        for (int i = 1; i <= 5; i++)
        {
            if (keyboardController.IsProjectileSwitchRequested(i))
            {
                currentProjectileType = (ProjectileType)(i - 1);
                timeTillNextBullet = GetBulletCooldown(i - 1); 
                break;
            }
        }
    }

    private void HandleShooting(KeyboardState state, SpriteRenderer animator)
    {
        if (keyboardController.IsShootRequested() && shootTime <= 0 && hitTime <= 0)
        {
            isShooting = true;
            shootTime = timeTillNextBullet;
            GameObject newProjectile = ProjectileFactory.CreateProjectile(currentProjectileType, GameObject.X, GameObject.Y, GameObject.GetComponent<SpriteRenderer>().isFacingRight);
            GOManager.Instance.allGOs.Add(newProjectile);

        }
    }

    private float GetBulletCooldown(int projectileType)
    {
        return projectileType switch
        {
            0 => 1 / (25f / 8.3f), // Default
            1 => 1 / (41.33f / 6.2f), // Spread
            2 => 1 / (25.1f / 13.85f), // Chaser
            3 => 1 / (33.14f / 11.6f), // Lobber
            4 => 1 / (20.38f / 8f), // Roundabout
            _ => timeTillNextBullet
        };
    }

    private void HandleDamageDetection()
    {
        if (keyboardController.IsDamageRequested())
        {
            TakeDamage(20); // Example damage value
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        hitTime = InvincibilityDuration;
        IsInvincible = true;
        GameObject.GetComponent<SpriteRenderer>().setAnimation("HitGround");
    }

    private void CreateDustEffect()
    {
        Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
        Rectangle dustPosition = new Rectangle(GameObject.X, GameObject.Y + 10, 144, 144); // Adjust Y position as needed
        Texture2D dustTexture = textureStorage.GetTexture("Dust");
        GameObject dustEffect = VisualEffectFactory.createVisualEffect(dustPosition, dustTexture, updatesPerFrame: 1, frameCount: 14, scale: 1f);

        // Add the dust effect to your game object manager
        //GOManager.Instance.allGOs.Add(dustEffect);
    }

    private void UpdateGravity(float deltaTime)
    {
        if (!IsGrounded)
        {
            airTime += deltaTime;
            velocity.Y += Gravity * deltaTime * airTime * 2;

            GameObject.Y += (int)(velocity.Y * deltaTime);
        }
    }

    private void UpdateAnimationState(SpriteRenderer animator)
    {
        if (hitTime > 0)
        {
            animator.setAnimation(IsGrounded ? "HitGround" : "HitAir");
        }
        else if (IsDucking)
        {
            animator.setAnimation(shootTime > 0 ? "DuckShoot" : "Duck");
        }
        else if (IsDashing)
        {
            animator.setAnimation(IsGrounded ? "DashGround" : "DashAir");
            
        }
        else if (!IsGrounded)
        {
            animator.setAnimation("Jump");
        }
        else if (IsRunning)
        {
            animator.setAnimation(shootTime > 0 ? "RunShootingStraight" : "Run");
        }
        else if (shootTime > 0)
        {
            animator.setAnimation("ShootStraight");
        }
        else 
        {
            animator.setAnimation("Idle");
        }
    }

    public void Draw(SpriteBatch spriteBatch) { /* Non-visual, no changes here */ }
}
