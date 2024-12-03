using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class ToothyTerrorSeed : BaseEnemy
{
    public int GROUND_HEIGHT { get; set; }
    public int FALL_SPEED { get; set; }
    private bool isFalling = true;

    public override void Move(GameTime gameTime)
    {
        if (isFalling)
        {
            GameObject.Y += FALL_SPEED;
            if(GameObject.Y >= GROUND_HEIGHT)
            {
                isFalling = false;
                GameObject.GetComponent<SpriteRenderer>().setAnimation("Plant");
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        KeyValuePair<string, Animation> currentAnim = GameObject.GetComponent<SpriteRenderer>().currentAnimation;
        bool finishedAnim = GameObject.GetComponent<SpriteRenderer>().IsAnimationComplete();

        if (!isFalling && finishedAnim)
        {
            if (currentAnim.Key.Equals("Plant"))
            {
                GameObject.GetComponent<SpriteRenderer>().spriteScale = 1.5f;
                GameObject.GetComponent<SpriteRenderer>().setAnimation("Sprout");
            }
            else if (currentAnim.Key.Equals("Sprout")){
                GameObject enemy = BossEnemyFactory.CreateEnemy(BossEnemyType.BabyToothyTerror, GameObject.X - 30, GameObject.Y - 30);
                GOManager.Instance.allGOs.Add(enemy);
                GameObject.Destroy();
            }
        }


    }

    public override void Shoot(GameTime gameTime)
    {

    }
}