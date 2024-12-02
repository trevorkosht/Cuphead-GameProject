using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BossLogic : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    private bool setNewState = true, transforming = false; //True if in Idle
    private float timer = 2f, timerDuration = 1f;
    private int animationRepeatCount = 0; //If animation repeats
    private int attackChoice = 0;
    private int phase = 1;
    private readonly int maxHP;
    private readonly Boss boss;
    Random random = new Random();
    public BossLogic(int maxHP, Boss boss)
    {
        this.maxHP = maxHP;
        this.boss = boss;
    }

    public void Update(GameTime gameTime)
    {
        if (CaseAnimations())
            return;

        if (setNewState)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(timer <= 0)
            {
                setNewState = false;
                SetState();
            }
        }
        else
        {
            timer = timerDuration + (float)random.NextDouble() * 3;
            TrySetNeutralState();
        }
    }

    bool CaseAnimations()
    {
        SpriteRenderer sRend = GameObject.GetComponent<SpriteRenderer>();
        boss.CurrentAnimation = sRend.currentAnimation.Key;
        boss.CurrentAnimationFrame = sRend.currentAnimation.Value.CurrentFrame;
        boss.phase = phase;
        if (GameObject.GetComponent<HealthComponent>().isDead)
        {
            sRend.setAnimation("Death");
            return true;
        }
        if (GameObject.GetComponent<HealthComponent>().currentHealth < (4 * maxHP) / 5 && phase < 2)
        {
            phase = 2;
            sRend.setAnimation("ShootSeeds");
            setNewState = false;
        }
        if (GameObject.GetComponent<HealthComponent>().currentHealth < maxHP/2 && phase < 3)
        {
            phase = 3;
            sRend.setAnimation("Transform");
            transforming = true;
            setNewState = false;
        }
        if (transforming)
        {
            if (sRend.IsAnimationComplete())
                transforming = false;
            return true;
        }
        return false;
    }

    void TrySetNeutralState()
    {
        SpriteRenderer sRend = GameObject.GetComponent<SpriteRenderer>();
        if (sRend.IsAnimationComplete())
        {
            if (animationRepeatCount > 0)
            {
                animationRepeatCount--;
                return;
            }
            else
            {
                sRend.setAnimation(phase < 3 ? "Idle" : "FinalIdle");
                setNewState = true;
            }
        }
    }

    void SetState()
    {
        SpriteRenderer sRend = GameObject.GetComponent<SpriteRenderer>();
        if (phase < 3)
        {
            string[] animations = phase == 1
                ? new[] { "AttackHigh", "AttackLow" }
                : new[] { "CreateItem", "ShootSeeds", "AttackHigh", "AttackLow" };

            attackChoice = random.Next(animations.Length);
            sRend.setAnimation(animations[attackChoice]);
            animationRepeatCount = 0;
        }
        else
        {
            sRend.setAnimation(random.Next(0, 2) < 1 ? "FinalAttack" : "ShootSeeds");
            animationRepeatCount = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
    }
}
