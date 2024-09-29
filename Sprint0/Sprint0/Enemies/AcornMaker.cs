using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class AcornMaker : BaseEnemy
{
    //private List<GameObject> acorns;  // List to hold spawned acorns
    private double spawnCooldown;           // Cooldown between acorn spawns
    private double timeSinceLastSpawn;      // Tracks time since last spawn

    public override void Move(GameTime gameTime)
    {
        // Acorn Maker doesn't move
    }

    public override void Shoot(GameTime gameTime)
    {
        // Spawn new acorns periodically
        timeSinceLastSpawn += gameTime.ElapsedGameTime.TotalSeconds;

        if (timeSinceLastSpawn >= spawnCooldown)
        {
            // Create a new AggravatingAcorn using the factory system
            GameObject newAcorn = EnemyFactory.CreateEnemy(EnemyType.AggravatingAcorn, GameObject.X + 50, GameObject.Y - 150);

            GOManager.Instance.allGOs.Add(newAcorn);

            // Add the acorn to the list
            //acorns.Add(newAcorn);

            // Reset spawn timer
            timeSinceLastSpawn = 0;
        }
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        // Initialize the base enemy with the starting position, hitpoints, and texture
        base.Initialize(texture, storage);
        sRend.setAnimation("acornMakerAnimation");
        spawnCooldown = 1.5;
        timeSinceLastSpawn = 0;
    }

    public override void Update(GameTime gameTime)
    {
        // Call the base enemy update (handles movement, shooting, etc.)
        base.Update(gameTime);
    }
}
