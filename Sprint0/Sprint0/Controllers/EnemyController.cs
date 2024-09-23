using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static IController;

public class EnemyController
{
    private List<BaseEnemy> enemies;
    private int currentEnemyIndex;
    private BaseEnemy currentEnemy;
    private IKeyboardController keyboardController;
    private Texture2DStorage textureStorage;

    public EnemyController(IKeyboardController keyboardController, Texture2DStorage textureStorage)
    {
        this.keyboardController = keyboardController;
        this.textureStorage = textureStorage;
        currentEnemyIndex = 0;

        // Initialize the enemy list using the factory
        enemies = new List<BaseEnemy>
        {
            EnemyFactory.CreateEnemy(EnemyType.AggravatingAcorn, textureStorage),
            EnemyFactory.CreateEnemy(EnemyType.DeadlyDaisy, textureStorage),
            EnemyFactory.CreateEnemy(EnemyType.MurderousMushroom, textureStorage),
            EnemyFactory.CreateEnemy(EnemyType.TerribleTulip, textureStorage),
            EnemyFactory.CreateEnemy(EnemyType.AcornMaker, textureStorage),
            EnemyFactory.CreateEnemy(EnemyType.BothersomeBlueberry, textureStorage),
            EnemyFactory.CreateEnemy(EnemyType.ToothyTerror, textureStorage)
        };

        currentEnemy = enemies[currentEnemyIndex];
    }

    public void Update(GameTime gameTime)
    {
        // Update enemy based on player input
        keyboardController.Update();

        if (keyboardController.OnKeyDown(Keys.O))
        {
            CycleToPreviousEnemy();
        }
        else if (keyboardController.OnKeyDown(Keys.P))
        {
            CycleToNextEnemy();
        }

        currentEnemy?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        currentEnemy?.Draw(spriteBatch);
    }

    private void CycleToNextEnemy()
    {
        currentEnemyIndex = (currentEnemyIndex + 1) % enemies.Count;
        currentEnemy = enemies[currentEnemyIndex];
    }

    private void CycleToPreviousEnemy()
    {
        currentEnemyIndex = (currentEnemyIndex - 1 + enemies.Count) % enemies.Count;
        currentEnemy = enemies[currentEnemyIndex];
    }
}
