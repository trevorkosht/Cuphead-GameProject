using Microsoft.Xna.Framework;          // For Vector2, GameTime
using Microsoft.Xna.Framework.Graphics; // For SpriteBatch, Texture2D
using System.Collections.Generic;



public class AcornMaker : BaseEnemy
{
    private List<AggravatingAcorn> acorns;
    private double spawnCooldown;

    public override void Move(GameTime gameTime)
    {
        // Acorn Maker doesn't move
    }

    public override void Shoot()
    {
        spawnCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
        if (spawnCooldown <= 0)
        {
            // Spawn new acorn logic
            acorns.Add(new AggravatingAcorn());
            spawnCooldown = 1.5; // Reset cooldown
        }
    }

    public override void Initialize(Vector2 startPosition, int hitPoints, Texture2D texture)
    {
        base.Initialize(startPosition, hitPoints, texture);
        acorns = new List<AggravatingAcorn>();
        spawnCooldown = 1.5;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        // Update acorns
        foreach (var acorn in acorns)
        {
            acorn.Update(gameTime);
        }
    }
}
