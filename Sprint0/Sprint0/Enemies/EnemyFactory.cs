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
        spriteRenderer.addAnimation("Death",new Animation(textureStorage.GetTexture("EnemyDeath"), 5, 9, 144, 144));

        switch (type)
        {
            case EnemyType.AggravatingAcorn:
                enemyLogic = new AggravatingAcorn();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new CircleCollider(50, new Vector2(-72, -100), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("aggravatingAcornAnimation", new Animation(textureStorage.GetTexture("AggravatingAcorn"), 5, 20, 144, 144));
                spriteRenderer.addAnimation("AcornDrop", new Animation(textureStorage.GetTexture("AcornFall"), 3, 14, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("AggravatingAcorn"), textureStorage);
                enemyHP = 2;
                break;
            case EnemyType.DeadlyDaisy:
                enemyLogic = new DeadlyDaisy();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new BoxCollider(new Vector2(135, 155), new Vector2(5, -5), GOManager.Instance.GraphicsDevice));
                enemy.GetComponent<BoxCollider>().ChangeWidth(-30);
                enemy.GetComponent<BoxCollider>().ChangeHeight(-10);
                enemy.AddComponent(new DaisyState());
                enemy.AddComponent(new DaisyCollisionManager(enemy.GetComponent<BoxCollider>(), enemy.GetComponent<DaisyState>()));
                spriteRenderer.addAnimation("deadlyDaisyAnimation", new Animation(textureStorage.GetTexture("DeadlyDaisy"), 2, 16, 144, 144));
                spriteRenderer.addAnimation("Spawn", new Animation(textureStorage.GetTexture("DaisySpawn"), 5, 16, 144, 144));
                spriteRenderer.addAnimation("Turn", new Animation(textureStorage.GetTexture("DaisyTurn"), 1, 18, 144, 144));
                spriteRenderer.addAnimation("Jump", new Animation(textureStorage.GetTexture("DaisyJump"), 5, 9, 144, 144));
                spriteRenderer.addAnimation("Land", new Animation(textureStorage.GetTexture("DaisyLand"), 2, 11, 288, 288));
                enemyLogic.Initialize(textureStorage.GetTexture("DeadlyDaisy"), textureStorage);
                enemy.AddComponent(new DaisyAnimator(enemy.GetComponent<SpriteRenderer>(), enemy.GetComponent<DaisyState>()));
                enemyHP = 1;
                break;
            case EnemyType.MurderousMushroom:
                enemy.Y += 72;
                enemyLogic = new MurderousMushroom();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new BoxCollider(new Vector2(36, 36), Vector2.Zero, GOManager.Instance.GraphicsDevice));
                enemy.GetComponent<BoxCollider>().ChangeSize(-5);
                spriteRenderer.addAnimation("murderousMushroomAnimation", new Animation(textureStorage.GetTexture("MurderousMushroom"), 5, 8, 144, 144));
                spriteRenderer.addAnimation("Attack", new Animation(textureStorage.GetTexture("MushroomAttack"), 3, 15, 144, 144));
                spriteRenderer.addAnimation("Closed", new Animation(textureStorage.GetTexture("MushroomClosed"), 3, 3, 144, 144));
                spriteRenderer.addAnimation("Open", new Animation(textureStorage.GetTexture("MushroomOpening"), 3, 5, 144, 144));
                spriteRenderer.addAnimation("Closing", new Animation(textureStorage.GetTexture("MushroomClosing"), 3, 5, 144, 144));
                spriteRenderer.spriteScale = 0.5f;

                enemyLogic.Initialize(textureStorage.GetTexture("MurderousMushroom"), textureStorage);
                enemyHP = 2;
                break;
            case EnemyType.TerribleTulip:
                enemyLogic = new TerribleTulip();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new BoxCollider(new Vector2(115, 145), new Vector2(10, 0), GOManager.Instance.GraphicsDevice));
                enemy.GetComponent<BoxCollider>().ChangeWidth(-30);
                spriteRenderer.addAnimation("terribleTulipAnimation", new Animation(textureStorage.GetTexture("TerribleTulip"), 5, 15, 144, 144));
                spriteRenderer.addAnimation("Idle", new Animation(textureStorage.GetTexture("TulipIdle"), 2, 30, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("TerribleTulip"), textureStorage);
                enemyHP = 4;
                break;
            case EnemyType.AcornMaker:
                enemyLogic = new AcornMaker();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new BoxCollider(new Vector2(300, 450), new Vector2(90, 0), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("acornMakerAnimation", new Animation(textureStorage.GetTexture("AcornMaker"), 4, 20, 600, 600));
                spriteRenderer.spriteScale = 3;
                enemyLogic.Initialize(textureStorage.GetTexture("AcornMaker"), textureStorage);
                enemyHP = 75;
                break;
            case EnemyType.BothersomeBlueberry:
                enemy.Y += 65;
                enemyLogic = new BothersomeBlueberry();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new CircleCollider(20, new Vector2(-30,-30), GOManager.Instance.GraphicsDevice));
                enemy.GetComponent<CircleCollider>().ChangeSize(-10);
                spriteRenderer.addAnimation("bothersomeBlueberryAnimation", new Animation(textureStorage.GetTexture("BothersomeBlueberry"), 3, 12, 144, 144));
                spriteRenderer.addAnimation("Melt", new Animation(textureStorage.GetTexture("BlueberryMelt"), 5, 10, 144, 144));
                spriteRenderer.addAnimation("WaitForRespawn", new Animation(textureStorage.GetTexture("BlueberryWaitingToRespawn"), 5, 1, 144, 144));
                spriteRenderer.addAnimation("Respawn", new Animation(textureStorage.GetTexture("BlueberryRespawn"), 5, 14, 144, 144));
                spriteRenderer.addAnimation("Turn", new Animation(textureStorage.GetTexture("BlueberryTurn"), 2, 7, 144, 144));
                spriteRenderer.spriteScale = 0.5f;
                enemyLogic.Initialize(textureStorage.GetTexture("BothersomeBlueberry"), textureStorage);
                enemyHP = 51;
                break;
            case EnemyType.ToothyTerror:
                enemyLogic = new ToothyTerror();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new BoxCollider(new Vector2(135, 185), new Vector2(40, 0), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("toothyTerrorAnimation", new Animation(textureStorage.GetTexture("ToothyTerror"), 5, 4, 144, 144));
                spriteRenderer.addAnimation("Attack", new Animation(textureStorage.GetTexture("ToothyTerrorSpinAttack"), 5, 8, 144, 144));
                spriteRenderer.spriteScale = 1.5f;
                enemyLogic.Initialize(textureStorage.GetTexture("ToothyTerror"), textureStorage);
                enemyHP = 9999999;
                break;
            case EnemyType.SpikyBulb:
                enemyLogic = new SpikyBulb();
                enemy.AddComponent(enemyLogic);
                enemy.AddComponent(new CircleCollider(25, new Vector2(-35,-35), GOManager.Instance.GraphicsDevice));
                spriteRenderer.addAnimation("SpikyBulb", new Animation(textureStorage.GetTexture("SpikyBulb"), 1, 6, 100,100));
                spriteRenderer.spriteScale = 0.5f;
                enemyLogic.Initialize(textureStorage.GetTexture("SpikyBulb"), textureStorage);
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
    ToothyTerror,
    SpikyBulb
}
