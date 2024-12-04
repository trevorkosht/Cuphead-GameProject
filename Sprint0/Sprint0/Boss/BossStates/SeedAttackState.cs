using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class SeedAttackState : IComponent
{
    private readonly Boss boss;
    private double attackCooldown;
    private Random random;
    private const int NUM_TARGET_ZONES = 8;
    private const int LEFT_BOUND = -200;
    private const int RIGHT_BOUND = 600;
    private bool hasFired = false;

    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    public SeedAttackState(Boss boss)
    {
        this.boss = boss;
        random = new Random();

    }

    public void Update(GameTime gameTime)
    {
        if (boss.CurrentAnimation == "ShootSeeds")
        {
            if (!hasFired)
            {
                Rectangle vfxDest = new Rectangle(GameObject.X + 220, GameObject.Y - 85, 158, 158);
                Texture2D vfxTexture = GOManager.Instance.textureStorage.GetTexture("SeedMissileFireVFX");
                VisualEffectFactory.createVisualEffect(vfxDest, vfxTexture, 3, 12, 1.0f, true);
                Queue<int> seeds = new Queue<int>();

                int numShots = random.Next(3, 6);

                while (seeds.Count < numShots)
                {
                    int zone = random.Next(NUM_TARGET_ZONES);
                    if (!seeds.Contains(zone))
                    {
                        seeds.Enqueue(zone);
                    }
                }

                ShootSeeds(seeds);
                hasFired = true;
            }
        }
        else
        {
            hasFired = false;
        }
    }

    private void ShootSeeds(Queue<int> seeds)
    {
        while (seeds.Count > 0) { 
            CreateSeed(seeds.Dequeue());
        }
    }

    private void CreateSeed(int targetIndex)
    {
        int type = random.Next(0, 3);
        int X = LEFT_BOUND + targetIndex * (RIGHT_BOUND - LEFT_BOUND) / NUM_TARGET_ZONES;
        int Y = -1 * random.Next(200, 600);

        switch (type)
        {
            case 0:
                BossEnemyFactory.CreateEnemy(BossEnemyType.ToothyTerrorSeed, X, Y); break;
            case 1:
                BossEnemyFactory.CreateEnemy(BossEnemyType.FlowerSeed, X, Y); break;
            case 2:
                BossEnemyFactory.CreateEnemy(BossEnemyType.FlytrapSeed, X, Y); break;
            default:
                break;
        }
    }
    public void Draw(SpriteBatch spriteBatch) { }
}
