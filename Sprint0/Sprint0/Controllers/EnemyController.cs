using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static IController;

public class EnemyController
{
    private List<GameObject> enemies;
    public int currentEnemyIndex;
    public GameObject currentEnemy;
    private KeyboardController keyboardController = new KeyboardController();
    private Texture2DStorage textureStorage;

    public EnemyController(KeyboardController keyboardController, Texture2DStorage textureStorage)
    {
        this.keyboardController = keyboardController;
        this.textureStorage = textureStorage;
        currentEnemyIndex = 0;

        enemies = new List<GameObject>
        {
            EnemyFactory.CreateEnemy(EnemyType.AggravatingAcorn, 300, 100),
            EnemyFactory.CreateEnemy(EnemyType.DeadlyDaisy),
            EnemyFactory.CreateEnemy(EnemyType.MurderousMushroom),
            EnemyFactory.CreateEnemy(EnemyType.TerribleTulip),
            EnemyFactory.CreateEnemy(EnemyType.AcornMaker),
            EnemyFactory.CreateEnemy(EnemyType.BothersomeBlueberry),
            EnemyFactory.CreateEnemy(EnemyType.ToothyTerror)

        };

        currentEnemy = enemies[currentEnemyIndex];
        
    }

    public void Update(GameTime gameTime)
    {
        keyboardController.Update();

        //if (keyboardController.OnKeyDown(Keys.O))
        //{
        //    CycleToPreviousEnemy();
        //}
        //else if (keyboardController.OnKeyDown(Keys.P))
        //{
        //    CycleToNextEnemy();
        //}

        if (currentEnemy != null)
        {
            if (currentEnemy.destroyed)
                currentEnemy = null;
            currentEnemy?.Update(gameTime);
        }

        GOManager.Instance.currentEnemy = currentEnemy;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //if (currentEnemy != null)
        //    currentEnemy?.Draw(spriteBatch);
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
