using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BossLogic : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    bool enableTimer = true, transform = false; //True if in Idle
    float timer = 2f, timerDuration = 1f;
    int animationRepeatCount = 0; //If animation repeats
    int attackChoice = 0;
    int phase = 1;
    int maxHP;
    bool lowFace, highFace, vines;
    float lowFaceTimer, highFaceTimer, vinesTimer;
    public BossLogic(int maxHP)
    {
        this.maxHP = maxHP;
    }

    public void Update(GameTime gameTime)
    {
        bool willReturn = CaseAnimations();
        if (willReturn)
            return;
        HandleFaceAttacks();
        HandleVines();

        if (enableTimer)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(timer <= 0)
            {
                enableTimer = false;
                ChooseAttack();
            }
        }
        else
        {
            Random random = new Random();
            timer = timerDuration + (float)random.NextDouble() * 3;
            SwitchAnimation();
        }
    }

    void HandleFaceAttacks()
    {
        if(phase < 3)
        {

        }
    }

    void HandleVines()
    {
        if(phase == 3)
        {

        }
    }

    bool CaseAnimations()
    {
        SpriteRenderer sRend = GameObject.GetComponent<SpriteRenderer>();
        if (GameObject.GetComponent<HealthComponent>().isDead)
        {
            sRend.setAnimation("Death");
            return true;
        }
        if (GameObject.GetComponent<HealthComponent>().currentHealth < (4 * maxHP) / 5 && phase < 2)
            phase = 2;
        if (GameObject.GetComponent<HealthComponent>().currentHealth < maxHP/2 && phase < 3)
        {
            phase = 3;
            sRend.setAnimation("Transform");
            transform = true;
            enableTimer = false;
        }
        if (transform)
        {
            if (sRend.IsAnimationComplete())
                transform = false;
            return true;
        }
        return false;
    }

    void SwitchAnimation()
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
                lowFace = false;
                highFace = false;
                if (phase < 3)
                    sRend.setAnimation("Idle");
                else
                    sRend.setAnimation("FinalIdle");
                enableTimer = true;
            }
        }
    }

    void ChooseAttack()
    {
        Random random = new Random();
        SpriteRenderer sRend = GameObject.GetComponent<SpriteRenderer>();
        if(phase == 1)
        {
            int attChoices = 2;
            attackChoice = random.Next(0, attChoices);
            if (attackChoice == 0)
            {
                sRend.setAnimation("AttackHigh");
                animationRepeatCount = 0;
            }
            if (attackChoice == 1)
            {
                sRend.setAnimation("AttackLow");
                animationRepeatCount = 0;
            }
        }
        else if (phase == 2)
        {
            int attChoices = 4;
            attackChoice = random.Next(0, attChoices);
            if (attackChoice == 0)
            {
                sRend.setAnimation("CreateItem");
                animationRepeatCount = 0;
            }
            if (attackChoice == 1)
            {
                sRend.setAnimation("ShootSeeds");
                animationRepeatCount = 0;
            }
            if (attackChoice == 2)
            {
                sRend.setAnimation("AttackHigh");
                animationRepeatCount = 0;
            }
            if (attackChoice == 3)
            {
                sRend.setAnimation("AttackLow");
                animationRepeatCount = 0;
            }
        }
        else
        {
            sRend.setAnimation("FinalAttack");
            animationRepeatCount = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
    }
}
