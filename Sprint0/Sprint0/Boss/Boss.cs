using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Boss : GameObject
{
    private Dictionary<string, IState> states;
    private IState currentState;

    public string CurrentAnimation { get; set; }
    public int CurrentAnimationFrame { get; set; }
    public bool IsFacingRight { get; set; }

    private SpriteRenderer sRend;
    private Texture2DStorage textureStorage;

    public int phase = 1;

    public Boss(int x, int y, Texture2DStorage textureStorage) : base(x, y)
    {
        this.textureStorage = textureStorage;
        sRend = new SpriteRenderer(new Rectangle(x, y, 256, 256), true);
        sRend.spriteScale = 3;
        AddComponent(sRend);

        states = new Dictionary<string, IState>
        {
            { "Idle", new IdleState(this) },
            { "MagicHandsAttack", new MagicHandsAttackState(this, textureStorage) }
        };

        currentState = states["Idle"];
        CurrentAnimation = "Idle";
        IsFacingRight = true;

    }

    public void InitializeAnimations()
    {
        if (sRend.getAnimationName() != null)
        {
            return;
        }
        Texture2D idleTexture = textureStorage.GetTexture("BossStageOneIdle");
        Texture2D magicHandsTexture = textureStorage.GetTexture("BossMagicHands");

        sRend.addAnimation("Idle", new Animation(idleTexture, 1, 24, 675, 510));
        sRend.addAnimation("MagicHands", new Animation(magicHandsTexture, 10, 10, 256, 256));
        sRend.setAnimation("Idle");
    }

    public void SetState(string stateName)
    {
        if (states.ContainsKey(stateName))
        {
            currentState.Exit();
            currentState = states[stateName];
            currentState.Enter();
        }
    }


}
