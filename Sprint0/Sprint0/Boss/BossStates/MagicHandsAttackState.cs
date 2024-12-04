using Cuphead;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class MagicHandsAttackState : IComponent
{
    private readonly Boss boss;
    private double attackCooldown;
    private Texture2D sycamoreTexture;
    private Texture2D acornTexture;
    private Texture2D pollenTexture;
    private Texture2D pollenPinkTexture;
    private Random random;
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    public MagicHandsAttackState(Boss boss, Texture2DStorage storage)
    {
        this.boss = boss;
        sycamoreTexture = storage.GetTexture("BoomerangProjectile");
        acornTexture = storage.GetTexture("SmallAcornProjectile");
        pollenTexture = storage.GetTexture("PollenProjectileWhite");
        pollenPinkTexture = storage.GetTexture("PollenProjectilePink");
        random = new Random();
    }

    public void Update(GameTime gameTime)
    {
        if (boss.CurrentAnimation == "MagicHands")
        {
            if (boss.phase == 2)
            {
                if(boss.CurrentAnimationFrame == 1) {
                    GOManager.Instance.audioManager.getInstance("HandSpawnStart").Play();
                } 
                if(boss.CurrentAnimationFrame == 20) {
                    GOManager.Instance.audioManager.getInstance("HandSpawnOpen").Play();
                    if (attackCooldown <= 0)
                    {
                        int attackType = random.Next(0, 2);
                        if (attackType == 0)
                        {
                            SpawnSycamore();
                        }
                        else
                        {
                            SpawnAcorns();
                        }
                        attackCooldown = 2.0; // Cooldown in seconds (adjust as needed)
                    }
                    GOManager.Instance.audioManager.getInstance("HandSpawnEnd").Play();
                }
            }
        }
        if (boss.CurrentAnimation == "FinalAttack")
        {
            if (boss.phase == 3 && boss.CurrentAnimationFrame == 15)
            {
                if (attackCooldown <= 0)
                {
                    SpawnPollen();
                    attackCooldown = 2.0; // Cooldown in seconds
                }
            }
        }

        // Update the attack cooldown
        if (attackCooldown > 0)
        {
            attackCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
        }
    }


    private void SpawnSycamore()
    {
        GameObject sycamore = new GameObject(boss.X, boss.Y +300, new SycamoreProjectile(new Vector2(boss.X, boss.Y +300)));
        SpriteRenderer sr = new SpriteRenderer(new Rectangle(sycamore.X, sycamore.Y, 212, 212), boss.IsFacingRight);
        CircleCollider collider = new CircleCollider(40, new Vector2(-100, -60), GOManager.Instance.GraphicsDevice);
        sycamore.type = "sycamoreEnemy";
        sycamore.AddComponent(collider);
        sycamore.AddComponent(sr);
        sr.addAnimation("boomerang", new Animation(sycamoreTexture, 1, 8, 212, 212));
        sr.setAnimation("boomerang");
        GOManager.Instance.allGOs.Add(sycamore);
    }

    private void SpawnAcorns()
    {
        GameObject player = GOManager.Instance.Player; // Assuming there's a way to access the player object

        for (int i = 0; i < 3; i++)
        {
            // Adjust vertical position for each acorn
            float verticalOffset = i * 50; // Each acorn spawns 50 units apart vertically
            Vector2 spawnPosition = new Vector2(boss.X, boss.Y + 300 + verticalOffset);

            // Calculate the direction vector towards the player
            Vector2 direction = Vector2.Normalize(player.position - spawnPosition);
            float rotation = (float)Math.Atan2(direction.Y, direction.X); // Calculate the angle in radians

            // Reverse the rotation direction
            rotation += MathHelper.Pi; // Add 180 degrees (π radians)

            // Create the acorn game object
            GameObject acorn = new GameObject((int)spawnPosition.X, (int)spawnPosition.Y, new AcornProjectile(spawnPosition, player));
            SpriteRenderer sr = new SpriteRenderer(new Rectangle(acorn.X, acorn.Y, 90, 90), boss.IsFacingRight, MathHelper.ToDegrees(rotation));
            CircleCollider collider = new CircleCollider(20, new Vector2(-25, -20), GOManager.Instance.GraphicsDevice);
            acorn.type = "acornEnemy";
            acorn.AddComponent(collider);
            acorn.AddComponent(sr);

            // Set up the animation for the acorn
            sr.addAnimation("acorn", new Animation(acornTexture, 1, 4, 90, 90));
            sr.setAnimation("acorn");

            // Add the acorn to the game objects list
            GOManager.Instance.allGOs.Add(acorn);
        }
    }





    private void SpawnPollen()
    {
        GOManager.Instance.audioManager.getNewInstance("ProjectileSpit").Play();
        bool spawnPink = random.Next(0, 2) == 0; // 50% chance for pink pollen
        Texture2D texture = spawnPink ? pollenPinkTexture : pollenTexture;

        Vector2 spawnPosition = new Vector2(boss.X, boss.Y + 300);
        GameObject pollen = new GameObject((int)spawnPosition.X, (int)spawnPosition.Y, new PollenProjectile(spawnPosition, texture, spawnPink, 3.0f));
        SpriteRenderer sr = new SpriteRenderer(new Rectangle(pollen.X, pollen.Y, 56, 56), boss.IsFacingRight);
        CircleCollider collider = new CircleCollider(20, new Vector2(-28, -28), GOManager.Instance.GraphicsDevice);
        pollen.type = "pollenEnemy";
        pollen.AddComponent(collider);
        pollen.AddComponent(sr);

        string animationName = spawnPink ? "PinkPollen" : "WhitePollen";
        sr.addAnimation(animationName, new Animation(texture, 2, 5, 56, 56));
        sr.setAnimation(animationName);

        GOManager.Instance.allGOs.Add(pollen);
    }

    public void Draw(SpriteBatch spriteBatch) { }
}
