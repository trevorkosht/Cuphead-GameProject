using Microsoft.Xna.Framework;        
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

public class MurderousMushroom : BaseEnemy
{
    private bool isHidden;
    private double shootCooldown;
    private int closedHP;
    private float projectileScale = 0.5f;
    private HealthComponent mushroomHealthComponent => GameObject.GetComponent<HealthComponent>();
    private BoxCollider mushroomCollider => GameObject.GetComponent<BoxCollider>();

    private MurderousMushroomProjectile mushroomProjectile;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("murderousMushroomAnimation");
        sRend.isFacingRight = true;
        isHidden = false;
        shootCooldown = 2.0;

        Texture2D purpleSporeTexture = storage.GetTexture("PurpleSpore");
        Texture2D pinkSporeTexture = storage.GetTexture("PinkSpore");
        Texture2D attackVFX = storage.GetTexture("MushroomAttackVFX");

        mushroomProjectile = new MurderousMushroomProjectile(purpleSporeTexture, pinkSporeTexture, attackVFX, projectileScale);
    }

    public override void Shoot(GameTime gameTime)
    {
        if (GameObject.X > GOManager.Instance.Camera.Position.X + 1200)
        {
            return;
        }

        if (!isHidden)
        {
            shootCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
            if (shootCooldown <= 0)
            {
                GOManager.Instance.audioManager.getInstance("MurderousMushroomShoot").Play();
                sRend.setAnimation("Attack");
                if (sRend.currentAnimation.Value.CurrentFrame == 5)
                {
                    Vector2 playerPosition = new Vector2(player.X, player.Y);
                    mushroomProjectile.SpawnProjectile(GameObject.position, playerPosition, sRend);
                    shootCooldown = 4.0;
                }
            }
        }
    }

    public override void Move(GameTime gameTime)
    {
    }

    public void HideUnderCap()
    {
        if (!sRend.getAnimationName().Equals("Attack")) {
            if (!sRend.getAnimationName().Equals("Closed")) {
                closedHP = GameObject.GetComponent<HealthComponent>().currentHealth;
            }
            closedHP = GameObject.GetComponent<HealthComponent>().currentHealth = closedHP;

            if (!isHidden) {
                sRend.setAnimation("Closing");
                GameObject.GetComponent<BoxCollider>().bounds = new Vector2(72, 47);
                GameObject.GetComponent<BoxCollider>().offset = new Vector2(0, 25);
            }
            else if (sRend.currentAnimation.Value.CurrentFrame >= 4) {
                sRend.setAnimation("Closed");
            }

            isHidden = true;
        }

    }

    public void EmergeFromCap()
    {
        sRend.setAnimation("Open");
        GOManager.Instance.audioManager.getInstance("MurderousMushroomUp").Play();

        if (sRend.currentAnimation.Value.CurrentFrame >= 4) {
            isHidden = false;
            sRend.setAnimation("murderousMushroomAnimation");
            GameObject.GetComponent<BoxCollider>().bounds = new Vector2(72, 72);
            GameObject.GetComponent<BoxCollider>().offset = new Vector2(0, 0);
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if(Math.Abs((float)(GameObject.X - player.X)) >= 600) {
            HideUnderCap();
        }
        else if(isHidden) {
            EmergeFromCap();
        }

        sRend.isFacingRight = player.X <= GameObject.X;

        Shoot(gameTime);
        if (sRend.getAnimationName().Equals("Attack") && sRend.currentAnimation.Value.CurrentFrame == 14) {
            sRend.setAnimation("murderousMushroomAnimation");
        } 
    }
}
