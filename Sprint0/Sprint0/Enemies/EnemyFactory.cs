using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System.Collections.Generic;

public static class EnemyFactory
{
    public static GameObject CreateEnemy(EnemyType type)
    {
        GameObject enemy = new GameObject(100, 100);

        Texture2DStorage textureStorage = GOManager.Instance.textureStorage;
        BaseEnemy enemyLogic;

        switch (type)
        {
            case EnemyType.AggravatingAcorn:
                enemyLogic = new AggravatingAcorn();
                enemy.AddComponent(enemyLogic);
                enemyLogic.addAnimation("aggravatingAcornAnimation", new Animation(textureStorage.GetTexture("AggravatingAcorn"), 5, 20, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("AggravatingAcorn"), textureStorage);
                break;
            case EnemyType.DeadlyDaisy:
                enemyLogic = new DeadlyDaisy();
                enemy.AddComponent(enemyLogic);
                enemyLogic.addAnimation("deadlyDaisyAnimation", new Animation(textureStorage.GetTexture("DeadlyDaisy"), 3, 16, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("DeadlyDaisy"), textureStorage);
                break;
            case EnemyType.MurderousMushroom:
                enemyLogic = new MurderousMushroom();
                enemy.AddComponent(enemyLogic);
                enemyLogic.addAnimation("murderousMushroomAnimation", new Animation(textureStorage.GetTexture("MurderousMushroom"), 5, 8, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("MurderousMushroom"), textureStorage);
                break;
            case EnemyType.TerribleTulip:
                enemyLogic = new TerribleTulip();
                enemy.AddComponent(enemyLogic);
                enemyLogic.addAnimation("terribleTulipAnimation", new Animation(textureStorage.GetTexture("TerribleTulip"), 5, 15, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("TerribleTulip"), textureStorage);
                break;
            case EnemyType.AcornMaker:
                enemyLogic = new AcornMaker();
                enemy.AddComponent(enemyLogic);
                enemyLogic.addAnimation("acornMakerAnimation", new Animation(textureStorage.GetTexture("AcornMaker"), 5, 16, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("AcornMaker"), textureStorage);
                break;
            case EnemyType.BothersomeBlueberry:
                enemyLogic = new BothersomeBlueberry();
                enemy.AddComponent(enemyLogic);
                enemyLogic.addAnimation("bothersomeBlueberryAnimation", new Animation(textureStorage.GetTexture("BothersomeBlueberry"), 5, 12, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("BothersomeBlueberry"), textureStorage);
                break;
            case EnemyType.ToothyTerror:
                enemyLogic = new ToothyTerror();
                enemy.AddComponent(enemyLogic);
                enemyLogic.addAnimation("toothyTerrorAnimation", new Animation(textureStorage.GetTexture("ToothyTerror"), 5, 4, 144, 144));
                enemyLogic.Initialize(textureStorage.GetTexture("ToothyTerror"), textureStorage); // HP might not matter since it's invincible
                break;
            // Add other cases for different enemies here
            default:
                return null;
        }
        enemyLogic.loadAllAnimations();
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
    // Add other enemies here
}
