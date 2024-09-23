using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System.Collections.Generic;       // For List<T>

public class AcornMaker : BaseEnemy
{
    private List<AggravatingAcorn> acorns;  // List to hold spawned acorns
    private double spawnCooldown;           // Cooldown between acorn spawns
    private Texture2DStorage storageObject;

    public override void Move(GameTime gameTime)
    {
        // Acorn Maker doesn't move
    }

    public override void Shoot(GameTime gameTime)
    {
        // TO-DO Add behavior
    }

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture, Texture2DStorage storage)
    {
        // Initialize the base enemy with the starting position, hitpoints, and texture
        base.Initialize(startPosition, hitPoints, texture, storage);
        base.setAnimation("acornMakerAnimation");
        storageObject = storage;
        acorns = new List<AggravatingAcorn>();  // Initialize the list of acorns
        spawnCooldown = 1.5;      
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
        // TO-DO make sure enemy acorns get spawned In
    }
}
