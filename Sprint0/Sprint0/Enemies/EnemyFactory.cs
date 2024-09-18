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
                enemy.Initialize(new Vector2(100, 100), 2, textureStorage.GetTexture("AggravatingAcorn"));
                break;
            case EnemyType.DeadlyDaisy:
                enemy = new DeadlyDaisy();
                enemy.Initialize(new Vector2(150, 200), 1, textureStorage.GetTexture("DeadlyDaisy"));
                break;
            case EnemyType.MurderousMushroom:
                enemy = new MurderousMushroom();
                enemy.Initialize(new Vector2(200, 150), 2, textureStorage.GetTexture("MurderousMushroom"));
                break;
            case EnemyType.TerribleTulip:
                enemy = new TerribleTulip();
                enemy.Initialize(new Vector2(250, 300), 4, textureStorage.GetTexture("TerribleTulip"));
                break;
            case EnemyType.AcornMaker:
                enemy = new AcornMaker();
                enemy.Initialize(new Vector2(300, 350), 125, textureStorage.GetTexture("AcornMaker"));
                break;
            case EnemyType.BothersomeBlueberry:
                enemy = new BothersomeBlueberry();
                enemy.Initialize(new Vector2(120, 180), 1, textureStorage.GetTexture("BothersomeBlueberry"));
                break;
            case EnemyType.ToothyTerror:
                enemy = new ToothyTerror();
                enemy.Initialize(new Vector2(400, 400), 1, textureStorage.GetTexture("ToothyTerror")); // HP might not matter since it's invincible
                break;
            // Add other cases for different enemies here
            default:
                return null;
        }

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
