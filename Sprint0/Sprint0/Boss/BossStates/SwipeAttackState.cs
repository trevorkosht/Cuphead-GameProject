using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


public class SwipeAttackState : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }
    private Boss boss;
    private bool highAttack, lowAttack;
    private GameObject hitbox;

    public SwipeAttackState(Boss boss)
    {
        this.boss = boss;
    }
    public void Update(GameTime gameTime)
    {
        if(boss.CurrentAnimation == "AttackHigh")
        {
            Rectangle destRect = GameObject.GetComponent<SpriteRenderer>().destRectangle;
            destRect.Width = 550;
            GameObject.GetComponent<SpriteRenderer>().destRectangle = destRect;

            if (boss.CurrentAnimationFrame == 12)
                SpawnHighHitBox();
            if(boss.CurrentAnimationFrame == 15)
                ClearHitBox();
        }
        else if(boss.CurrentAnimation == "AttackLow")
        {
            Rectangle destRect = GameObject.GetComponent<SpriteRenderer>().destRectangle;
            destRect.Width = 535;
            destRect.Height = 231;
            GameObject.GetComponent<SpriteRenderer>().destRectangle = destRect;

            if (boss.CurrentAnimationFrame == 12)
                SpawnLowHitBox();
            if (boss.CurrentAnimationFrame == 15)
                ClearHitBox();
        }
        else
        {
            boss.GetComponent<SpriteRenderer>().destRectangle = new Rectangle(boss.X, boss.Y, 256, 256);
        }
        if(boss.CurrentAnimation != "AttackHigh" && boss.CurrentAnimation != "AttackLow")
            ClearHitBox();
    }
    void SpawnHighHitBox()
    {
        hitbox = new GameObject(-1000, 200);
        hitbox.type = "EnemyBossHitBox";
        hitbox.AddComponent(new BoxCollider(new Vector2(3500, 200), Vector2.Zero, GOManager.Instance.GraphicsDevice));
        GOManager.Instance.allGOs.Add(hitbox);
    }
    void SpawnLowHitBox()
    {
        hitbox = new GameObject(-1000, 550);
        hitbox.type = "EnemyBossHitBox";
        hitbox.AddComponent(new BoxCollider(new Vector2(3500, 200), Vector2.Zero, GOManager.Instance.GraphicsDevice));
        GOManager.Instance.allGOs.Add(hitbox);
    }
    void ClearHitBox()
    {
        for(int i = 0; i < GOManager.Instance.allGOs.Count; i++)
        {
            if (GOManager.Instance.allGOs[i].type == "EnemyBossHitBox")
            {
                GOManager.Instance.allGOs[i].Destroy();
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
    }
}
