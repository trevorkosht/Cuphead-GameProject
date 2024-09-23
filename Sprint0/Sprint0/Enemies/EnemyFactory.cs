using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System.Collections.Generic;

public static class EnemyFactory
{
    public static BaseEnemy CreateEnemy(EnemyType type, Texture2DStorage textureStorage)
    {
        BaseEnemy enemy = null;

        switch (type)
        {
            case EnemyType.AggravatingAcorn:
                enemy = new AggravatingAcorn();
                enemy.addAnimation("aggravatingAcornAnimation", new Animation(textureStorage.GetTexture("AggravatingAcorn"), 5, 8, 144, 144));
                enemy.Initialize(new Vector2(100, 100), 2, textureStorage.GetTexture("AggravatingAcorn"), textureStorage);
                break;
            case EnemyType.DeadlyDaisy:
                enemy = new DeadlyDaisy();
                enemy.addAnimation("deadlyDaisyAnimation", new Animation(textureStorage.GetTexture("DeadlyDaisy"), 5, 16, 144, 144));
                enemy.Initialize(new Vector2(150, 200), 1, textureStorage.GetTexture("DeadlyDaisy"), textureStorage);
                break;
            case EnemyType.MurderousMushroom:
                enemy = new MurderousMushroom();
                enemy.addAnimation("murderousMushroomAnimation", new Animation(textureStorage.GetTexture("MurderousMushroom"), 5, 6, 144, 144));
                enemy.Initialize(new Vector2(200, 150), 2, textureStorage.GetTexture("MurderousMushroom"), textureStorage);
                break;
            case EnemyType.TerribleTulip:
                enemy = new TerribleTulip();
                enemy.addAnimation("terribleTulipAnimation", new Animation(textureStorage.GetTexture("TerribleTulip"), 5, 6, 144, 144));
                enemy.Initialize(new Vector2(250, 300), 4, textureStorage.GetTexture("TerribleTulip"), textureStorage);
                break;
            case EnemyType.AcornMaker:
                enemy = new AcornMaker();
                enemy.addAnimation("acornMakerAnimation", new Animation(textureStorage.GetTexture("AcornMaker"), 5, 16, 144, 144));
                enemy.Initialize(new Vector2(300, 350), 125, textureStorage.GetTexture("AcornMaker"), textureStorage);
                break;
            case EnemyType.BothersomeBlueberry:
                enemy = new BothersomeBlueberry();
                enemy.addAnimation("bothersomeBlueberryAnimation", new Animation(textureStorage.GetTexture("BothersomeBlueberry"), 5, 28, 144, 144));
                enemy.Initialize(new Vector2(120, 180), 1, textureStorage.GetTexture("BothersomeBlueberry"), textureStorage);
                break;
            case EnemyType.ToothyTerror:
                enemy = new ToothyTerror();
                enemy.addAnimation("toothyTerrorAnimation", new Animation(textureStorage.GetTexture("ToothyTerror"), 5, 8, 144, 144));
                enemy.Initialize(new Vector2(400, 400), 1, textureStorage.GetTexture("ToothyTerror"), textureStorage); // HP might not matter since it's invincible
                break;
            // Add other cases for different enemies here
            default:
                return null;
        }
        enemy.loadAllAnimations();
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
