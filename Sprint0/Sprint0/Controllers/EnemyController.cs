using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static IController;

public class EnemyController
{
    private List<BaseEnemy> enemies;
    private int currentEnemyIndex;
    private BaseEnemy currentEnemy;

    private IKeyboardController keyboardController;

    public EnemyController(IKeyboardController keyboardController)
    {
        this.keyboardController = keyboardController;
        currentEnemyIndex = 0;

        // Initialize the enemy list using the factory
        enemies = new List<BaseEnemy>
        {
            EnemyFactory.CreateEnemy(EnemyType.AggravatingAcorn),
            EnemyFactory.CreateEnemy(EnemyType.DeadlyDaisy),
            EnemyFactory.CreateEnemy(EnemyType.MurderousMushroom),
            EnemyFactory.CreateEnemy(EnemyType.TerribleTulip),
            EnemyFactory.CreateEnemy(EnemyType.AcornMaker)
            // Add more enemies here
        };

        currentEnemy = enemies[currentEnemyIndex];
        currentEnemy.Initialize(new Vector2(200, 200));
    }

    public void Update(GameTime gameTime)
    {
        // Update enemy based on player input
        keyboardController.Update();

        if (keyboardController.OnKeyDown(Keys.U))
        {
            CycleToPreviousEnemy();
        }
        else if (keyboardController.OnKeyDown(Keys.I))
        {
            CycleToNextEnemy();
        }

        currentEnemy.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        currentEnemy.Draw(spriteBatch);
    }

    private void CycleToNextEnemy()
    {
        currentEnemyIndex++;
        if (currentEnemyIndex >= enemies.Count)
        {
            currentEnemyIndex = 0;
        }
        currentEnemy = enemies[currentEnemyIndex];
        currentEnemy.Initialize(new Vector2(200, 200)); // Reset position or other parameters
    }

    private void CycleToPreviousEnemy()
    {
        currentEnemyIndex--;
        if (currentEnemyIndex < 0)
        {
            currentEnemyIndex = enemies.Count - 1;
        }
        currentEnemy = enemies[currentEnemyIndex];
        currentEnemy.Initialize(new Vector2(200, 200)); // Reset position or other parameters
    }
}
