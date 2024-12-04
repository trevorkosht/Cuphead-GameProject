using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class FlowerSeed : BaseEnemy
{
    public int GROUND_HEIGHT { get; set; }
    public int FALL_SPEED {  get; set; }
    private bool isFalling = true;
    private bool hasSpawned = false;

    public override void Move(GameTime gameTime)
    {
        if (isFalling)
        {
            GameObject.Y += FALL_SPEED;
            if (GameObject.Y >= GROUND_HEIGHT)
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

        if (!isFalling)
        {
            if (finishedAnim && currentAnim.Key.Equals("Plant"))
            {
                GameObject.GetComponent<SpriteRenderer>().spriteScale = 1.5f;
                GameObject.GetComponent<SpriteRenderer>().setAnimation("Sprout");
            }
            else if (finishedAnim && currentAnim.Key.Equals("Sprout")){
                GameObject.GetComponent<SpriteRenderer>().spriteScale = 2.0f;
                Rectangle scaledDest = GameObject.GetComponent<SpriteRenderer>().destRectangle;
                scaledDest.Width = (int)(0.5 * scaledDest.Width);
                GameObject.GetComponent<SpriteRenderer>().destRectangle = scaledDest;
                GameObject.GetComponent<SpriteRenderer>().setAnimation("Grow");
            }
            else if (finishedAnim && currentAnim.Key.Equals("Grow"))
            {
                GameObject.Destroy();
            }
            else if(currentAnim.Key.Equals("Grow") && currentAnim.Value.CurrentFrame == 15 && !hasSpawned)
            {
                bool flowerAlreadySpawned = false;
                foreach (GameObject GO in GOManager.Instance.allGOs)
                {
                    if (GO.GetComponent<FlyingFlower>() != null)
                    {
                        flowerAlreadySpawned = true;
                        break;
                    }
                }

                GameObject enemy;

                if (!flowerAlreadySpawned)
                {
                    enemy = BossEnemyFactory.CreateEnemy(BossEnemyType.FlyingFlower, GameObject.X - 20, GameObject.Y - 300);
                }
                else
                {
                    enemy = BossEnemyFactory.CreateEnemy(BossEnemyType.FollowingFlytrap, GameObject.X, GameObject.Y - 265);
                }
                
                GOManager.Instance.allGOs.Add(enemy);
                hasSpawned = true;
            }
        }


    }

    public override void Shoot(GameTime gameTime)
    {

    }
}