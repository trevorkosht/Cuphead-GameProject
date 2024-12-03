using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System.Collections.Generic;

public static class BossEnemyFactory {
    private const int GROUND_HEIGHT = 600;
    private const int FALL_SPEED = 5;
    public static GameObject CreateEnemy(BossEnemyType type, int posX = 300, int posY = 300) {
        GameObject enemy = new GameObject(posX, posY);

        Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
        BaseEnemy enemyLogic;
        SpriteRenderer spriteRenderer = new SpriteRenderer(new Rectangle(enemy.X, enemy.Y, 144, 144), false);
        spriteRenderer.orderInLayer = .2f;
        int enemyHP;
        enemy.AddComponent(spriteRenderer);

        switch (type) {
            case BossEnemyType.ToothyTerrorSeed:
                enemyLogic = new ToothyTerrorSeed();
                enemy.AddComponent(enemyLogic);
                Animation fallAnim = new Animation(textureStorage.GetTexture("PurpleSeed"), 1, 34, 60, 60);
                Animation plantAnim = new Animation(textureStorage.GetTexture("PurpleSeedPlant"), 5, 10, 60, 60);
                Animation sproutAnim = new Animation(textureStorage.GetTexture("VineGroundburst"), 5, 7, 422, 163, new Vector2(63, 275));
                spriteRenderer.addAnimation("Fall", fallAnim);
                spriteRenderer.addAnimation("Plant", plantAnim);
                spriteRenderer.addAnimation("Sprout", sproutAnim);
                spriteRenderer.setAnimation("Fall");
                spriteRenderer.spriteScale = 0.4f;
                enemy.GetComponent<ToothyTerrorSeed>().GROUND_HEIGHT = GROUND_HEIGHT;
                enemy.GetComponent<ToothyTerrorSeed>().FALL_SPEED = FALL_SPEED;
                enemyHP = 999;
                break;
            case BossEnemyType.FlowerSeed:
                enemyLogic = new FlowerSeed();
                enemy.AddComponent(enemyLogic);
                fallAnim = new Animation(textureStorage.GetTexture("PinkSeed"), 3, 34, 60, 60);
                plantAnim = new Animation(textureStorage.GetTexture("PinkSeedPlant"), 5, 10, 60, 60);
                Animation growAnim = new Animation(textureStorage.GetTexture("EnemySpawnVine"), 4, 39, 400, 120, new Vector2(60, 300));
                sproutAnim = new Animation(textureStorage.GetTexture("VineGroundburst"), 5, 7, 422, 163,new Vector2(63, 275));
                spriteRenderer.addAnimation("Fall", fallAnim);
                spriteRenderer.addAnimation("Plant", plantAnim);
                spriteRenderer.addAnimation("Grow", growAnim);
                spriteRenderer.addAnimation("Sprout", sproutAnim);
                spriteRenderer.setAnimation("Fall");
                spriteRenderer.spriteScale = 0.4f;
                enemy.GetComponent<FlowerSeed>().GROUND_HEIGHT = GROUND_HEIGHT;
                enemy.GetComponent<FlowerSeed>().FALL_SPEED = FALL_SPEED;
                enemyHP = 999;
                break;
            case BossEnemyType.FlytrapSeed:
                enemyLogic = new FlytrapSeed();
                enemy.AddComponent(enemyLogic);
                fallAnim = new Animation(textureStorage.GetTexture("BlueSeed"), 3, 34, 60, 60);
                plantAnim = new Animation(textureStorage.GetTexture("BlueSeedPlant"), 5, 10, 60, 60);
                growAnim = new Animation(textureStorage.GetTexture("EnemySpawnVine"), 4, 39, 400, 120, new Vector2(60, 300));
                sproutAnim = new Animation(textureStorage.GetTexture("VineGroundburst"), 5, 7, 422, 163, new Vector2(63, 275));
                spriteRenderer.addAnimation("Fall", fallAnim);
                spriteRenderer.addAnimation("Plant", plantAnim);
                spriteRenderer.addAnimation("Grow", growAnim);
                spriteRenderer.addAnimation("Sprout", sproutAnim);
                spriteRenderer.setAnimation("Fall");
                spriteRenderer.spriteScale = 0.4f;
                enemy.GetComponent<FlytrapSeed>().GROUND_HEIGHT = GROUND_HEIGHT;
                enemy.GetComponent<FlytrapSeed>().FALL_SPEED = FALL_SPEED;
                enemyHP = 999;
                break;
            case BossEnemyType.BabyToothyTerror:
                enemyLogic = new BabyToothyTerror();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new CircleCollider(25, new Vector2(-55, -45), GOManager.Instance.GraphicsDevice));
                Animation deathAnim = new Animation(textureStorage.GetTexture("ChomperDeath"), 3, 15, 288, 288);
                Animation attackAnim = new Animation(textureStorage.GetTexture("ChomperAttack"), 3,9, 128, 128);
                spriteRenderer.addAnimation("Death", deathAnim);
                spriteRenderer.addAnimation("Attack", attackAnim);
                spriteRenderer.setAnimation("Attack");
                spriteRenderer.spriteScale = 0.75f;
                enemyHP = 5;
                break;
            case BossEnemyType.FollowingFlytrap:
                enemyLogic = new FollowingFlytrap();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new CircleCollider(25, new Vector2(-38, -45), GOManager.Instance.GraphicsDevice));
                Animation spawnAnim = new Animation(textureStorage.GetTexture("FlytrapSpawn"), 4, 6, 68, 68);
                deathAnim = new Animation(textureStorage.GetTexture("FlytrapDeath"), 3, 10, 288, 288);
                attackAnim = new Animation(textureStorage.GetTexture("FlytrapAttack"), 3, 5, 116, 116);
                spriteRenderer.addAnimation("Spawn", spawnAnim);
                spriteRenderer.addAnimation("Death", deathAnim);
                spriteRenderer.addAnimation("Attack", attackAnim);
                spriteRenderer.setAnimation("Spawn");
                spriteRenderer.spriteScale = 0.5f;
                enemyHP = 4;
                break;
            case BossEnemyType.FlyingFlower:
                enemyLogic = new FlyingFlower();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new CircleCollider(40, new Vector2(-55, -65), GOManager.Instance.GraphicsDevice));
                Animation flightAnim = new Animation(textureStorage.GetTexture("MiniFlowerFly"), 3, 9, 118, 118);
                spawnAnim = new Animation(textureStorage.GetTexture("MiniFlowerSpawn"), 4, 4, 100, 100);
                deathAnim = new Animation(textureStorage.GetTexture("MiniFlowerDeath"), 3, 14, 432, 432);
                attackAnim = new Animation(textureStorage.GetTexture("MiniFlowerAttack"), 3, 26, 128, 128);
                spriteRenderer.addAnimation("Fly", flightAnim);
                spriteRenderer.addAnimation("Spawn", spawnAnim);
                spriteRenderer.addAnimation("Death", deathAnim);
                spriteRenderer.addAnimation("Attack", attackAnim);
                spriteRenderer.setAnimation("Spawn");
                spriteRenderer.spriteScale = 0.75f;
                enemyHP = 4;
                break;
            default:
                return null;

        }
        enemy.AddComponent(new HealthComponent(enemyHP, false));
        enemy.type = "Enemy" + type;
        return enemy;
    }
}

public enum BossEnemyType {
    BabyToothyTerror, 
    FlyingFlower,
    FollowingFlytrap,
    ToothyTerrorSeed,
    FlowerSeed,
    FlytrapSeed
}
