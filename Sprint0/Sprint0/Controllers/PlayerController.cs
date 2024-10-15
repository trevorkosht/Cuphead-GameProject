using Cuphead;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using System;
using System.Numerics;
using static IController;
using Vector2 = Microsoft.Xna.Framework.Vector2;

public class PlayerController : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; } = true;
    public float Speed { get; set; } = 700f;
    public float JumpForce { get; set; } = -1150f;
    public bool IsGrounded { get; set; } = false;
    public Vector2 velocity;
    public float GroundLevel { get; set; } = 500f; 
    public float Gravity { get; set; } = 2000f;
    public float timeTillNextBullet { get; set; } = .2f;
    public float timeTillNextHit { get; set; } = .4f;
    public float dashDuration = 0.5f;//about 1 second
    public float dashSpeed = 1500f;// about 750 pixel
    public int TimeTillNextDash { get; set; } = 500;
    public int height;
    public int playerHeight = 130;
    public int playerWidth = 100;

    public int Health { get; set; } = 100;
    public bool[] projectileUnlock = {true, true, false, false, false, false, false };
    public enum projectiletype{Peashooter = 1, Spreadshot = 2, Chaser = 3, Lobber = 4, Roundabout = 5}

    private float airTime = 0f, shootTime = 0f, hitTime = 0f, dashTime = 0f;
    private int floorY;
    private bool IsDucking, IsRunning, IsInvincible, isDuckingYAdjust, isShooting, IsDashing, IsDead = false;
    private bool IsSpawning = true;

    private readonly KeyboardController keyboardController = new KeyboardController();
    private readonly MouseController mouseController = new MouseController();

    private DelayGame gameDelay = new DelayGame();

    private const int DuckingYOffset = 50;
    private const float InvincibilityDuration = 0.5f;

    private ProjectileType currentProjectileType = ProjectileType.Peashooter;
    private BoxCollider Collider;

    float deltaTime;

    public PlayerController() 
    {

    }

    public void Update(GameTime gameTime)
    {
        if(IsSpawning)
        {
            HandleSpawnAnimation(gameTime);
            return;
        }

        if (!enabled) return;
        Collider = GameObject.GetComponent<BoxCollider>();

        keyboardController.Update();
        mouseController.Update();

        var state = Keyboard.GetState();
        var animator = GameObject.GetComponent<SpriteRenderer>();
        deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (IsDead)
        {
            if (animator.IsAnimationComplete())
            {
                GameObject.destroyed = true;
            }
            return;
        }
        
        UpdateTimers(deltaTime);
        CollisionCheck();
        HandleGroundCheck(animator);
        CollisionCheck();
        HandleMovementAndActions(gameTime, deltaTime);
        HandleShooting(state, animator);
        HandleProjectileSwitching(state);
        HandleDamageDetection();
        UpdateGravity(deltaTime);
        UpdateAnimationState(animator);
    }

    //check each collision type of the player
    public void CollisionCheck()
    {
        foreach(GameObject go in GOManager.Instance.allGOs)
        {
            if(go.type != null)
            {
                if (go.type.Contains("Platform"))
                {
                    HandlePlatformCollision(go);
                }
                if (go.type.Contains("Item"))
                {
                    HandleItemCollision(go);
                }
                if (go.type.Contains("Hill"))
                {
                    HandleHillCollision(go);
                }
                if (go.type.Contains("Log"))
                {
                    HandleLogCollision(go);
                }
                if (go.type.Contains("Stump"))
                {
                    HandleStumpCollision(go);
                }


            }
        }
    }

    //
    public void HandlePlatformCollision(GameObject platform)
    {
        if (Collider.Intersects(platform.GetComponent<Collider>()))
        {
            GroundLevel = (float)platform.Y;
            floorY = platform.Y;
            IsGrounded = true;
        }
        else
        {
            GroundLevel = 99999;
        }
        
    }

    public void HandleHillCollision(GameObject platform)
    {
        if (Collider.Intersects(platform.GetComponent<Collider>()))
        {
            GroundLevel = (float)platform.Y;
            floorY = platform.Y;
            IsGrounded = true;
        }
        else
        {
            GroundLevel = 99999;
        }

    }

    public void HandleLogCollision(GameObject platform)
    {
        if (Collider.Intersects(platform.GetComponent<Collider>()))
        {
            GroundLevel = (float)platform.Y;
            floorY = platform.Y;
            IsGrounded = true;
        }
        else
        {
            GroundLevel = 99999;
        }

    }

    public void HandleStumpCollision(GameObject platform)
    {
        if (Collider.Intersects(platform.GetComponent<Collider>()))
        {
            GroundLevel = (float)platform.Y;
            floorY = platform.Y;
            IsGrounded = true;
        }
        else
        {
            GroundLevel = 99999;
        }

    }

    public void HandleItemCollision(GameObject item)
    {
        if (Collider.Intersects(item.GetComponent<Collider>()))
        {
            item.Destroy();
            String itemName = item.type.Remove(0, 10);
            switch (itemName)
            {
                case "Spreadshot":
                    projectileUnlock[(int)projectiletype.Spreadshot] = true; break;
                case "Chaser":
                    projectileUnlock[(int)projectiletype.Chaser] = true; break;
                case "Lobber":
                    projectileUnlock[(int)projectiletype.Lobber] = true; break;
                case "Roundabout":
                    projectileUnlock[(int)projectiletype.Roundabout] = true; break;
            }
        }
    }

    private void HandleSpawnAnimation(GameTime gameTime)
    {
        var animator = GameObject.GetComponent<SpriteRenderer>();
        animator.setAnimation("Spawn");

        if (animator.IsAnimationComplete())
        {
            IsSpawning = false;
            animator.setAnimation("Idle");
        }
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
                if (!IsGrounded) {
                    CreateDustEffect();
                }

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
        else if (dashRequested && gameDelay.Cooldown(gameTime, TimeTillNextDash))
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
        if (dashTime > 0)
        {
            // Continue dashing within the duration
            float dashDistance = dashSpeed * deltaTime;


            GameObject.GetComponent<SpriteRenderer>().spriteScale = 1 + 7*(dashDuration - dashTime)/(12*dashDuration);

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
            GameObject.GetComponent<SpriteRenderer>().spriteScale = 1f;
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
            if (keyboardController.IsProjectileSwitchRequested(i) && projectileUnlock[i])
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
            GameObject newProjectile;
            if (GameObject.GetComponent<SpriteRenderer>().isFacingRight)
            {
                newProjectile = ProjectileFactory.CreateProjectile(currentProjectileType, GameObject.X, GameObject.Y, GameObject.GetComponent<SpriteRenderer>().isFacingRight);
                CreateShootingEffect(true);
            }
            else
            {
                newProjectile = ProjectileFactory.CreateProjectile(currentProjectileType, GameObject.X - 90, GameObject.Y, GameObject.GetComponent<SpriteRenderer>().isFacingRight);
                CreateShootingEffect(false);
            }
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
        if (keyboardController.IsDamageRequested() && !IsInvincible && !IsDead)
        {
            TakeDamage(20); // Example damage value
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        hitTime = InvincibilityDuration;
        IsInvincible = true;

        if (Health <= 0)
        {
            IsDead = true;
        } else
        {
            GameObject.GetComponent<SpriteRenderer>().setAnimation("HitGround");
        }
    }

    private void CreateDustEffect()
    {
        Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
        Rectangle dustPosition = new Rectangle(GameObject.X, GameObject.Y + 10, 144, 144); // Adjust Y position as needed
        Texture2D dustTexture = textureStorage.GetTexture("Dust");
        GameObject dustEffect = VisualEffectFactory.createVisualEffect(dustPosition, dustTexture, updatesPerFrame: 1, frameCount: 14, scale: 1f);
    }

    private void CreateShootingEffect(bool isFacingRight) {
        Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
        Rectangle effectPosition;
        if (isFacingRight) {
            effectPosition = new Rectangle(GameObject.X + 100, GameObject.Y + 25, 144, 144);
        }
        else {
            effectPosition = new Rectangle(GameObject.X - 25, GameObject.Y + 25, 144, 144);
        }

        Texture2D effectTexture;

        switch ((int)currentProjectileType) {
            case 0:
                effectTexture = textureStorage.GetTexture("PeashooterSpawn");
                break;
            case 1:
                effectTexture = textureStorage.GetTexture("SpreadSpawn");
                break;
            case 2:
                effectTexture = textureStorage.GetTexture("ChaserSpawn");
                break;
            case 3:
                effectTexture = textureStorage.GetTexture("LobberSpawn");
                break;
            case 4:
                effectTexture = textureStorage.GetTexture("RoundaboutSpawn");
                break;
            default:
                effectTexture = null;
                break;
        }
        VisualEffectFactory.createVisualEffect(effectPosition, effectTexture, updatesPerFrame: 2, frameCount: 4, scale: 0.5f);
    }



    private void UpdateGravity(float deltaTime)
    {
        if (!IsGrounded)
        {
            airTime += deltaTime;
            //velocity.Y += Gravity * deltaTime * airTime * 2;
            velocity.Y += Gravity * deltaTime;

            GameObject.Y += (int)(velocity.Y * deltaTime);
        }
    }

    private void UpdateAnimationState(SpriteRenderer animator)
    {
        if (IsDead)
        {
            animator.setAnimation("Death");
            return;
        }

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
            if(animator.currentAnimation.Value.CurrentFrame == 5 || animator.currentAnimation.Value.CurrentFrame == 12) {
                CreateDustEffect();
            }
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
