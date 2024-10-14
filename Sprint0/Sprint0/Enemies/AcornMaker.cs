using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class AcornMaker : BaseEnemy
{
    private double spawnCooldown;  
    private double timeSinceLastSpawn; 

    public override void Move(GameTime gameTime)
    {
    }

    public override void Shoot(GameTime gameTime)
    {
        if(GameObject.X > GOManager.Instance.Camera.Position.X + 1200)
        {
            return;
        }
        timeSinceLastSpawn += gameTime.ElapsedGameTime.TotalSeconds;

        if (timeSinceLastSpawn >= spawnCooldown && sRend.currentAnimation.Value.CurrentFrame == 8)
        {
            GameObject newAcorn = EnemyFactory.CreateEnemy(EnemyType.AggravatingAcorn, GameObject.X + 70, GameObject.Y - 150);

            GOManager.Instance.allGOs.Add(newAcorn);

            timeSinceLastSpawn = 0;
        }
    }

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("acornMakerAnimation");
        spawnCooldown = 1.5;
        timeSinceLastSpawn = 1.5;
        sRend.isFacingRight = true;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}
