using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;

public class VinesAttackState : IComponent
{
    Boss boss;
    Texture2DStorage storage;
    List<GameObject> vines = new List<GameObject>();
    bool horizAttack = true;
    float timeToAttack = 3;
    float attackTimeConst = 2;
    int attackPart = 0;
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    public VinesAttackState(Boss boss, Texture2DStorage storage)
	{
        this.boss = boss;
        this.storage = storage;
	}
    public void InitializeAnimations(int x, int y)
    {
        GameObject vine = new GameObject(x, y);
        vine.type = "AttackEnemyVine";
        vine.AddComponent(new SpriteRenderer(new Rectangle(x, y, 256, 256), true));
        SpriteRenderer sRend = vine.GetComponent<SpriteRenderer>();
        vines.Add(vine);
        if (horizAttack)
        {
            vine.Y += 525;
            sRend.destRectangle = new Rectangle(x, y, 946, 221);
            sRend.spriteScale = 1;
            Texture2D horizExtend = storage.GetTexture("HorizontalVineAttackExtend"); //946x221 23
            Texture2D horizRetract = storage.GetTexture("HorizontalVineAttackRetract"); //946x221 23
            sRend.addAnimation("horizExtend", new Animation(horizExtend, 3, 23, 221, 946));
            sRend.addAnimation("horizRetract", new Animation(horizRetract, 3, 23, 221, 946));
            sRend.setAnimation("horizExtend");
            vine.AddComponent(new BoxCollider(new Vector2(946, 150), new Vector2(0,71), GOManager.Instance.GraphicsDevice));
            vine.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            sRend.destRectangle = new Rectangle(x, y, 164, 485);
            Texture2D vertExtend = storage.GetTexture("VerticalVineAttackExtend"); //164x485 21
            Texture2D vertRetract = storage.GetTexture("VerticalVineAttackRetract"); //164x485 21
            sRend.addAnimation("vertExtend", new Animation(vertExtend, 3, 21, 485, 164));
            sRend.addAnimation("vertRetract", new Animation(vertRetract, 3, 21, 485, 164));
            sRend.setAnimation("vertExtend");
            vine.AddComponent(new BoxCollider(new Vector2(100, 328), new Vector2(30, 50), GOManager.Instance.GraphicsDevice));
            vine.GetComponent<BoxCollider>().enabled = true;
        }
        GOManager.Instance.allGOs.Add(vine);
    }

    public void Update(GameTime gameTime)
    {
        /*foreach (GameObject vine in vines)
        {
            vine.Update(gameTime);
        }*/
        if(boss.GetComponent<HealthComponent>().isDead)
        {
            DestroyVines();
            return;
        }
        if (boss.phase == 3)
        {
            if (attackPart == 0)
            {
                timeToAttack -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeToAttack <= 0)
                {
                    attackPart = 1;
                    horizAttack = !horizAttack;
                    timeToAttack = attackTimeConst;
                    if(horizAttack)
                    {
                        InitializeAnimations(GameObject.X - 700, GameObject.Y + -100);
                    }
                    else
                    {
                        GOManager.Instance.audioManager.getInstance("PlatformVineStart").Play();
                        for(int i = 0; i < 3; i++)
                        {
                            InitializeAnimations(GameObject.X - 60 - i * 300, GameObject.Y + 175);
                        }
                    }
                }
            }
            else
            {
                HandleAttack();
            }
        }
    }
    void HandleAttack()
    {
        if(attackPart == 1)
        {
            if (vines[0].GetComponent<SpriteRenderer>().currentAnimation.Value.CurrentFrame == 19)
            {
                foreach (GameObject vine in GOManager.Instance.allGOs)
                {
                    GOManager.Instance.audioManager.getInstance("PlatformVineGrow").Play();
                    if (vine.type == "AttackEnemyVine")
                    {
                        vine.GetComponent<BoxCollider>().enabled = true;
                    }
                }
            }
            if (vines[0].GetComponent<SpriteRenderer>().IsAnimationComplete())
            {
                if (horizAttack)
                {
                    vines[0].GetComponent<SpriteRenderer>().setAnimation("horizRetract");
                }
                else
                {
                    foreach (GameObject vine in vines)
                    {
                        vine.GetComponent<SpriteRenderer>().setAnimation("vertRetract");
                        GOManager.Instance.audioManager.getInstance("PlatformVineRetract").Play();
                    }
                }
                attackPart = 2;
            }
        }
        if (attackPart == 2)
        {
            if (vines[0].GetComponent<SpriteRenderer>().currentAnimation.Value.CurrentFrame == 5)
            {
                foreach (GameObject vine in GOManager.Instance.allGOs)
                {
                    if (vine.type == "AttackEnemyVine")
                    {
                        vine.GetComponent<BoxCollider>().enabled = false;
                    }
                }
            }
            if (vines[0].GetComponent<SpriteRenderer>().IsAnimationComplete())
            {
                attackPart = 0;
                DestroyVines();
            }
        }
    }

    void DestroyVines()
    {
        for (int i = 0; i < GOManager.Instance.allGOs.Count; i++)
        {
            if (GOManager.Instance.allGOs[i].type == "AttackEnemyVine")
            {
                GOManager.Instance.allGOs[i].Destroy();
            }
        }
        vines.Clear();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
    }
}
