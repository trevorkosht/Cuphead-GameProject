using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class Boss : GameObject
{
    public string CurrentAnimation { get; set; }
    public int CurrentAnimationFrame { get; set; }
    public int phase { get; set; }

    public bool IsFacingRight { get; set; }

    private SpriteRenderer sRend;
    private Texture2DStorage textureStorage;
    public int maxHP = 200;

    public Boss(int x, int y, Texture2DStorage textureStorage) : base(x, y)
    {
        this.textureStorage = textureStorage;
        sRend = new SpriteRenderer(new Rectangle(x, y, 256, 256), true);
        sRend.spriteScale = 2.5f;
        AddComponent(sRend);
        AddComponent(new BossLogic(maxHP, this));
        AddComponent(new BoxCollider(new Vector2(400, 800), new Vector2(200, 0), GOManager.Instance.GraphicsDevice));
        AddComponent(new HealthComponent(maxHP, false, true));
        AddComponent(new MagicHandsAttackState(this, textureStorage));
        AddComponent(new SwipeAttackState(this));
        AddComponent(new SeedAttackState(this));
        AddComponent(new VinesAttackState(this, textureStorage));
        type = "BossEnemy";

        CurrentAnimation = "Idle";
        IsFacingRight = true;

    }

    public void InitializeAnimations()
    {
        if (sRend.getAnimationName() != null)
        {
            return;
        }
        Texture2D magicHands = textureStorage.GetTexture("BossMagicHands"); //606x670 26
        Texture2D idle = textureStorage.GetTexture("BossStageOneIdle");
        //Texture2D spawn = textureStorage.GetTexture("BossSpawn"); //
        Texture2D seeds = textureStorage.GetTexture("BossShootSeeds"); //600x600 22
        Texture2D attackHigh = textureStorage.GetTexture("BossFaceAttackHigh"); //1150x665 19
        Texture2D attackLow = textureStorage.GetTexture("BossFaceAttackLow"); //1245x608 20

        //Stage 3
        Texture2D death = textureStorage.GetTexture("BossDeath"); //492x628 12
        Texture2D transform = textureStorage.GetTexture("BossFinalTransformation"); //664x660 26
        Texture2D finalIdle = textureStorage.GetTexture("BossFinalStageIdle"); // ??
        Texture2D finalAttack = textureStorage.GetTexture("BossFinalStageAttack"); //621x653 22

        sRend.addAnimation("MagicHands", new Animation(magicHands, 3, 26, 670, 606));
        sRend.addAnimation("Idle", new Animation(idle, 3, 24, 675, 510));
        sRend.addAnimation("ShootSeeds", new Animation(seeds, 3, 22, 600, 600));
        sRend.addAnimation("AttackHigh", new Animation(attackHigh, 4, 19, 665, 1150, new Vector2(640,0)));
        sRend.addAnimation("AttackLow", new Animation(attackLow, 4, 20, 608, 1245, new Vector2(735,-100)));
        sRend.addAnimation("Death", new Animation(death, 3, 12, 628, 492));
        sRend.addAnimation("Transform", new Animation(transform, 3, 26, 660, 664));
        sRend.addAnimation("FinalIdle", new Animation(finalIdle, 3, 18, 710, 514));
        sRend.addAnimation("FinalAttack", new Animation(finalAttack, 3, 22, 653, 621));

        sRend.setAnimation("Idle");
    }
}
