using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

public class MagicHandsAttackState : IState
{
    private Boss boss;
    private double attackCooldown;
    private Texture2D sycamoreTexture;
    private Texture2D acornTexture;
    private Texture2D pollenTexture;
    private Texture2D pollenPinkTexture;
    private Random random;

    public MagicHandsAttackState(Boss boss, Texture2DStorage storage)
    {
        this.boss = boss;
        attackCooldown = 0.0;
        sycamoreTexture = storage.GetTexture("BoomerangProjectile");
        acornTexture = storage.GetTexture("SmallAcornProjectile");
        pollenTexture = storage.GetTexture("PollenProjectileWhite");
        pollenPinkTexture = storage.GetTexture("PollenProjectilePink");
        random = new Random();
    }

    public void Enter()
    {
        attackCooldown = 0.0;
    }

    public void Exit() { }

    public void Update(GameTime gameTime)
    {
        attackCooldown -= gameTime.ElapsedGameTime.TotalSeconds;

        if (attackCooldown <= 0)
        {
            if (boss.CurrentAnimation == "MagicHands") //&& boss.phase == 2)
            {
                if (boss.CurrentAnimationFrame == 7)
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
                    attackCooldown = 3.0;
                }
            }
            if (boss.CurrentAnimation == "MagicHands") //&& boss.phase == 3)
            {
                if (boss.CurrentAnimationFrame == 7)
                {
                    SpawnPollen();
                    attackCooldown = 1.5;
                }
            }
        }
    }

    private void SpawnSycamore()
    {
        GameObject sycamore = new GameObject(boss.X, boss.Y - 50, new SycamoreProjectile(new Vector2(boss.X, boss.Y - 50)));
        SpriteRenderer sr = new SpriteRenderer(new Rectangle(sycamore.X, sycamore.Y, 64, 64), boss.IsFacingRight);
        CircleCollider collider = new CircleCollider(30, Vector2.Zero, GOManager.Instance.GraphicsDevice);
        sycamore.AddComponent(collider);
        sycamore.AddComponent(sr);
        sr.addAnimation("boomerang", new Animation(sycamoreTexture, 1, 1, 64, 64));
        sr.setAnimation("boomerang");
        GOManager.Instance.allGOs.Add(sycamore);
    }

    private void SpawnAcorns()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 spawnPosition = new Vector2(boss.X, boss.Y - 50);
            GameObject acorn = new GameObject((int)spawnPosition.X, (int)spawnPosition.Y, new AcornProjectile(spawnPosition, i));
            SpriteRenderer sr = new SpriteRenderer(new Rectangle(acorn.X, acorn.Y, 48, 48), boss.IsFacingRight);
            CircleCollider collider = new CircleCollider(20, Vector2.Zero, GOManager.Instance.GraphicsDevice);
            acorn.AddComponent(collider);
            acorn.AddComponent(sr);
            sr.addAnimation("acorn", new Animation(acornTexture, 1, 1, 48, 48));
            sr.setAnimation("acorn");
            GOManager.Instance.allGOs.Add(acorn);
        }
    }

    private void SpawnPollen()
    {
        bool spawnPink = random.Next(0, 2) == 0; // 50% chance for pink pollen
        Texture2D texture = spawnPink ? pollenPinkTexture : pollenTexture;

        Vector2 spawnPosition = new Vector2(boss.X, boss.Y - 50);
        GameObject pollen = new GameObject((int)spawnPosition.X, (int)spawnPosition.Y, new PollenProjectile(spawnPosition, texture, spawnPink, 3.0f));
        SpriteRenderer sr = new SpriteRenderer(new Rectangle(pollen.X, pollen.Y, 48, 48), boss.IsFacingRight);
        CircleCollider collider = new CircleCollider(20, Vector2.Zero, GOManager.Instance.GraphicsDevice);

        pollen.AddComponent(collider);
        pollen.AddComponent(sr);

        string animationName = spawnPink ? "PinkPollen" : "WhitePollen";
        sr.addAnimation(animationName, new Animation(texture, 2, 12, 48, 48));
        sr.setAnimation(animationName);

        GOManager.Instance.allGOs.Add(pollen);
    }

    public void Draw(SpriteBatch spriteBatch) { }
}
