using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System.Collections.Generic;       // For List<T>

public class AcornMaker : BaseEnemy
{
    private List<AggravatingAcorn> acorns;  // List to hold spawned acorns
    private double spawnCooldown;           // Cooldown between acorn spawns

    public override void Move(GameTime gameTime)
    {
        // Acorn Maker doesn't move
    }

    public override void Shoot()
    {
        // Reduce the cooldown based on elapsed time
        spawnCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
        if (spawnCooldown <= 0)
        {
            // Spawn new acorn when cooldown is 0
            var acorn = new AggravatingAcorn();
            acorn.Initialize(new Vector2(position.X, position.Y), 1, spriteTexture); // Spawn at the AcornMaker's position
            acorns.Add(acorn);
            spawnCooldown = 1.5; // Reset spawn cooldown to 1.5 seconds
        }
    }

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture)
    {
        // Initialize the base enemy with the starting position, hitpoints, and texture
        base.Initialize(startPosition, hitPoints, texture);
        acorns = new List<AggravatingAcorn>();  // Initialize the list of acorns
        spawnCooldown = 1.5;                    // Set initial spawn cooldown to 1.5 seconds
    }

    public override void Update(GameTime gameTime)
    {
        // Call the base enemy update (handles movement, shooting, etc.)
        base.Update(gameTime);

        // Update each acorn in the list
        foreach (var acorn in acorns)
        {
            acorn.Update(gameTime);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Draw the Acorn Maker sprite
        base.Draw(spriteBatch);

        // Draw each acorn that has been spawned
        foreach (var acorn in acorns)
        {
            acorn.Draw(spriteBatch);
        }
    }
}
