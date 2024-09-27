using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    public float GroundLevel { get; set; } = 500f; // Arbitrary floor height
    public float Gravity { get; set; } = 1200f; // Constant downward force
    public float timeTillNextBullet { get; set; } = .2f;
    public float timeTillNextHit { get; set; } = .4f;

    public int Health { get; set; } = 100; 

    private float airTime = 0f, shootTime = 0f, hitTime = 0f;
    private int floorY;
    private bool IsDucking, IsRunning, IsInvincible, isDuckingYAdjust, isShooting;

    private readonly IKeyboardController keyboardController = new KeyboardController();
    private readonly IMouseController mouseController = new MouseController();

    private const int DuckingYOffset = 50; 
    private const float InvincibilityDuration = 2f; 

    public PlayerController() { }

    public void Update(GameTime gameTime)
    {
        if (!enabled) return;

        keyboardController.Update();
        mouseController.Update();

        var state = Keyboard.GetState();
        var animator = GameObject.GetComponent<SpriteRenderer>();
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        UpdateTimers(deltaTime);
        HandleGroundCheck(animator);
        HandleMovementAndActions(deltaTime);
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

        // Update invincibility state if needed
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
        if (!isDuckingYAdjust) // Prevent GroundCheck from interfering with ducking adjustment
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
            {
                animator.setAnimation("Jump");
                IsGrounded = false;
            }
        }
    }

    private void HandleMovementAndActions(float deltaTime)
    {
        Vector2 input = keyboardController.GetMovementInput();
        bool jumpRequested = keyboardController.IsJumpRequested();
        bool duckRequested = keyboardController.IsDuckRequested();

        // Update Facing Direction
        UpdateFacingDirection(input);

        HandleDucking(duckRequested);

        // Disable horizontal movement while ducking
        if (!IsDucking)
        {
            // Horizontal Movement
            if (input.X != 0 && IsGrounded)
                IsRunning = true;
            else
                IsRunning = false;

            GameObject.X += (int)(input.X * Speed * deltaTime);
        }

        // Handle Jump
        if (jumpRequested && IsGrounded && !IsDucking) // Prevent jumping while ducking
        {
            velocity.Y = JumpForce;
            IsGrounded = false;
        }
        
    }

    private void UpdateFacingDirection(Vector2 input)
    {
        if (input.X < 0)
        {
            GameObject.GetComponent<SpriteRenderer>().isFacingRight = false;
        }
        else if (input.X > 0)
        {
            GameObject.GetComponent<SpriteRenderer>().isFacingRight = true;
        }
    }

    private void HandleDucking(bool duckRequested)
    {
        if (duckRequested && IsGrounded)
        {
            if (!IsDucking)
            {
                GameObject.Y = floorY + DuckingYOffset;
                IsDucking = true;
                isDuckingYAdjust = true; // Set flag when ducking

            }
        }
        else
        {
            if (IsDucking)
            {
                GameObject.Y = floorY;
                IsDucking = false;
                isDuckingYAdjust = false; // Clear flag when not ducking

            }
        }
    }



    private void HandleShooting(KeyboardState state, SpriteRenderer animator)
    {
        if (keyboardController.IsShootRequested() && shootTime <= 0 && hitTime <= 0)
        {
            isShooting = true;
            shootTime = timeTillNextBullet;
            GameObject.GetComponent<ProjectileManager>().FireProjectile(GameObject.X, GameObject.Y, GameObject.GetComponent<SpriteRenderer>().isFacingRight);

            if (IsGrounded)
            {
                if (IsDucking) animator.setAnimation("DuckShoot");
                else if (IsRunning) animator.setAnimation("RunShootingStraight");
                else animator.setAnimation("ShootStraight");
            }
        }
        isShooting = false;
    }

    private void HandleProjectileSwitching(KeyboardState state)
    {
        for (int i = 0; i <= 5; i++)
        {
            if (keyboardController.IsProjectileSwitchRequested(i))
            {
                GameObject.GetComponent<ProjectileManager>().projectileType = i;
                timeTillNextBullet = GetBulletCooldown(i);
                break;
            }
        }
    }

    private float GetBulletCooldown(int projectileType)
    {
        return projectileType switch
        {
            0 => 1 / (25f / 8.3f), // Default
            1 => 1 / (35.38f / 26f), // Megablast
            2 => 1 / (41.33f / 6.2f), // Spread
            3 => 1 / (35.38f / 26f), // Roundabout
            4 => 1 / (17.1f / 2.85f), // Chaser
            5 => 1 / (33.14f / 11.6f), // Lobber
            _ => timeTillNextBullet
        };
    }

    private void HandleDamageDetection()
    {
        if(keyboardController.IsDamageRequested())
        //if (!IsInvincible && GameObject.GetComponent<CollisionHandler>().IsCollidingWith("EnemyProjectile"))
        //{
            TakeDamage(20); // Example damage value
        //}
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        hitTime = InvincibilityDuration;
        IsInvincible = true;
        GameObject.GetComponent<SpriteRenderer>().setAnimation("HitGround");
    }

    private void UpdateGravity(float deltaTime)
    {
        if (!IsGrounded)
        {
            airTime += deltaTime;
            velocity.Y += Gravity * deltaTime * airTime * 2; // Apply gravity

            GameObject.Y += (int)(velocity.Y * deltaTime);
        }
    }

    private void UpdateAnimationState(SpriteRenderer animator)
    {
        if (hitTime > 0)
        {
            animator.setAnimation(IsGrounded ? "HitGround" : "HitAir");
        }
        else if (IsDucking && !isShooting)
        {
            animator.setAnimation("Duck");
        }
        else if(IsDucking && isShooting)
        {
            animator.setAnimation("DuckShoot");
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
