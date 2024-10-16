using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System.Collections.Generic;

public static class EnemyFactory
{
    public static GameObject CreateEnemy(EnemyType type, int posX = 300, int posY = 300)
    {
        GameObject enemy = new GameObject(posX, posY);

        Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
        BaseEnemy enemyLogic;
        SpriteRenderer spriteRenderer = new SpriteRenderer(new Rectangle(enemy.X, enemy.Y, 144, 144), false);
        spriteRenderer.orderInLayer = .2f;
        int enemyHP;
        enemy.AddComponent(spriteRenderer);
        spriteRenderer.addAnimation("Death",new Animation(textureStorage.GetTexture("PlayerDeath"), 5, 16, 144, 144));

        switch (type)
        {
            case EnemyType.AggravatingAcorn:
                enemyLogic = new AggravatingAcorn();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new CircleCollider(80, new Vector2(-72, -60), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("aggravatingAcornAnimation", new Animation(textureStorage.GetTexture("AggravatingAcorn"), 5, 20, 144, 144));
                spriteRenderer.addAnimation("AcornDrop", new Animation(textureStorage.GetTexture("AcornFall"), 3, 14, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("AggravatingAcorn"), textureStorage);
                enemyHP = 50;
                break;
            case EnemyType.DeadlyDaisy:
                enemyLogic = new DeadlyDaisy();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new BoxCollider(new Vector2(135, 155), new Vector2(5, -5), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("deadlyDaisyAnimation", new Animation(textureStorage.GetTexture("DeadlyDaisy"), 3, 16, 144, 144));
                spriteRenderer.addAnimation("Spawn", new Animation(textureStorage.GetTexture("DaisySpawn"), 5, 16, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("DeadlyDaisy"), textureStorage);
                enemyHP = 50;
                break;
            case EnemyType.MurderousMushroom:
                enemyLogic = new MurderousMushroom();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new BoxCollider(new Vector2(145, 145), Vector2.Zero, GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("murderousMushroomAnimation", new Animation(textureStorage.GetTexture("MurderousMushroom"), 5, 8, 144, 144));
                spriteRenderer.addAnimation("Attack", new Animation(textureStorage.GetTexture("MushroomAttack"), 3, 15, 144, 144));
                spriteRenderer.addAnimation("Closed", new Animation(textureStorage.GetTexture("MushroomClosed"), 3, 3, 144, 144));
                spriteRenderer.addAnimation("Open", new Animation(textureStorage.GetTexture("MushroomOpening"), 3, 5, 144, 144));
                spriteRenderer.addAnimation("Closing", new Animation(textureStorage.GetTexture("MushroomClosing"), 3, 5, 144, 144));

                enemyLogic.Initialize(textureStorage.GetTexture("MurderousMushroom"), textureStorage);
                enemyHP = 50;
                break;
            case EnemyType.TerribleTulip:
                enemyLogic = new TerribleTulip();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new BoxCollider(new Vector2(115, 145), new Vector2(10, 0), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("terribleTulipAnimation", new Animation(textureStorage.GetTexture("TerribleTulip"), 5, 15, 144, 144));
                spriteRenderer.addAnimation("Idle", new Animation(textureStorage.GetTexture("TulipIdle"), 5, 30, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("TerribleTulip"), textureStorage);
                enemyHP = 50;
                break;
            case EnemyType.AcornMaker:
                enemyLogic = new AcornMaker();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new BoxCollider(new Vector2(200, 300), new Vector2(60, 0), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("acornMakerAnimation", new Animation(textureStorage.GetTexture("AcornMaker"), 5, 16, 144, 144));
                spriteRenderer.spriteScale = 2;
                enemyLogic.Initialize(textureStorage.GetTexture("AcornMaker"), textureStorage);
                enemyHP = 100;
                break;
            case EnemyType.BothersomeBlueberry:
                enemyLogic = new BothersomeBlueberry();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new CircleCollider(40, new Vector2(-72,-60), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("bothersomeBlueberryAnimation", new Animation(textureStorage.GetTexture("BothersomeBlueberry"), 5, 12, 144, 144));
                spriteRenderer.addAnimation("Melt", new Animation(textureStorage.GetTexture("BlueberryMelt"), 5, 10, 144, 144));
                spriteRenderer.addAnimation("WaitForRespawn", new Animation(textureStorage.GetTexture("BlueberryWaitingToRespawn"), 5, 1, 144, 144));
                spriteRenderer.addAnimation("Respawn", new Animation(textureStorage.GetTexture("BlueberryRespawn"), 5, 14, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("BothersomeBlueberry"), textureStorage);
                enemyHP = 100;
                break;
            case EnemyType.ToothyTerror:
                enemyLogic = new ToothyTerror();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new BoxCollider(new Vector2(125, 175), new Vector2(10, 0), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("toothyTerrorAnimation", new Animation(textureStorage.GetTexture("ToothyTerror"), 5, 4, 144, 144));
                spriteRenderer.addAnimation("Attack", new Animation(textureStorage.GetTexture("ToothyTerrorSpinAttack"), 5, 8, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("ToothyTerror"), textureStorage);
                enemyHP = 9999999;
                break;
            default:
                return null;
        }
        enemy.AddComponent(new HealthComponent(enemyHP, false));
        enemy.type = "Enemy" + type;
        return enemy;
    }
}

public enum EnemyType
{
    AggravatingAcorn,
    DeadlyDaisy,
    MurderousMushroom,
    TerribleTulip,
    AcornMaker,
    BothersomeBlueberry,
    ToothyTerror
}
