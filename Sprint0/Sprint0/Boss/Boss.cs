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
        AddComponent(sRend);

        states = new Dictionary<string, IState>
        {
            { "Idle", new IdleState(this) },
            { "MagicHandsAttack", new MagicHandsAttackState(this, textureStorage) }
        };

        currentState = states["Idle"];
        CurrentAnimation = "Idle";
        IsFacingRight = true;

        InitializeAnimations();
    }

    private void InitializeAnimations()
    {
        Texture2D idleTexture = textureStorage.GetTexture("BossIdle");
        Texture2D magicHandsTexture = textureStorage.GetTexture("BossMagicHands");

        sRend.addAnimation("Idle", new Animation(idleTexture, 1, 1, 256, 256));
        sRend.addAnimation("MagicHands", new Animation(magicHandsTexture, 10, 10, 256, 256));
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
