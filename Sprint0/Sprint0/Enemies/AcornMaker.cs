using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class AcornMaker : BaseEnemy
{
    private List<AggravatingAcorn> acorns;  // List to hold spawned acorns
    private double spawnCooldown;           // Cooldown between acorn spawns
    private double timeSinceLastSpawn;      // Tracks time since last spawn
    private Texture2DStorage storageObject;
    private float spriteScale = 2.0f;

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
            AggravatingAcorn newAcorn = (AggravatingAcorn)EnemyFactory.CreateEnemy(EnemyType.AggravatingAcorn, storageObject);

            // Set its position near the AcornMaker
            newAcorn.Initialize(new Vector2(position.X + 50 , position.Y - 150), 2, storageObject.GetTexture("AggravatingAcorn"), storageObject);

            // Add the acorn to the list
            acorns.Add(newAcorn);

            // Reset spawn timer
            timeSinceLastSpawn = 0;
        }
    }

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture, Texture2DStorage storage)
    {
        // Initialize the base enemy with the starting position, hitpoints, and texture
        base.Initialize(startPosition, hitPoints, texture, storage);
        base.setAnimation("acornMakerAnimation");
        storageObject = storage;
        acorns = new List<AggravatingAcorn>();  // Initialize the list of acorns
        spawnCooldown = 1.5;
        timeSinceLastSpawn = 0;
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

        // Remove inactive acorns
        acorns.RemoveAll(a => !a.IsActive);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        // Draw the Acorn Maker sprite with scaling
        if (IsActive)
        {
            // Adjust the destination rectangle based on the scale factor
            Rectangle scaledDestRectangle = new Rectangle(
                destRectangle.X,
                destRectangle.Y,
                (int)(destRectangle.Width * spriteScale),
                (int)(destRectangle.Height * spriteScale)
            );

            // Draw with the adjusted rectangle
            spriteAnimations[currentAnimation.Key].draw(spriteBatch, scaledDestRectangle, false);
        }

        // Draw all active acorns
        foreach (var acorn in acorns)
        {
            acorn.Draw(spriteBatch);
        }
    }
}
