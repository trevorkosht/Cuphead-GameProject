using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class AcornMaker : BaseEnemy
{
    private List<GameObject> acorns;  // List to hold spawned acorns
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
            GameObject newAcorn = EnemyFactory.CreateEnemy(EnemyType.AggravatingAcorn);
            // Set its position near the AcornMaker
            newAcorn.X = GameObject.X + 50;
            newAcorn.Y = GameObject.Y - 150;

            // Add the acorn to the list
            acorns.Add(newAcorn);

            // Reset spawn timer
            timeSinceLastSpawn = 0;
        }
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        // Initialize the base enemy with the starting position, hitpoints, and texture
        base.Initialize(texture, storage);
        sRend.setAnimation("acornMakerAnimation");
        acorns = new List<GameObject>();  // Initialize the list of acorns
        spawnCooldown = 1.5;
        timeSinceLastSpawn = 0;
    }

    public override void Update(GameTime gameTime)
    {
        // Call the base enemy update (handles movement, shooting, etc.)
        base.Update(gameTime);

        // Update each acorn in the list
        foreach (GameObject acorn in acorns)
        {
            acorn.Update(gameTime);
        }

        // Remove inactive acorns
        acorns.RemoveAll(a => a.destroyed);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Draw all active acorns
        foreach (GameObject acorn in acorns)
        {
            acorn.Draw(spriteBatch);
        }
    }
}
