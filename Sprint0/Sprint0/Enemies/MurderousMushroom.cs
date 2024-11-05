using Microsoft.Xna.Framework;        
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

public class MurderousMushroom : BaseEnemy
{
    private bool isHidden;
    private double shootCooldown;
    private Texture2D purpleSporeTexture;
    private Texture2D pinkSporeTexture;
    private Texture2D attackVFX;
    private int closedHP;
    private float projectileScale = 0.5f;

    public override void Initialize(Texture2D texture, Texture2DStorage storage)
    {
        base.Initialize(texture, storage);
        sRend.setAnimation("murderousMushroomAnimation");
        sRend.isFacingRight = true;
        isHidden = false;
        shootCooldown = 2.0;

        purpleSporeTexture = storage.GetTexture("PurpleSpore");
        pinkSporeTexture = storage.GetTexture("PinkSpore");
        attackVFX = storage.GetTexture("MushroomAttackVFX");
    }

    public override void Move(GameTime gameTime)
    {
    }

    public override void Shoot(GameTime gameTime)
    {
        if(GameObject.X > GOManager.Instance.Camera.Position.X + 1200)
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
                if(sRend.currentAnimation.Value.CurrentFrame == 5) {
                    Vector2 playerPosition = new Vector2(player.X, player.Y);
                    bool shootPinkSpore = (new Random().Next(0, 2) == 0);

                    sRend.isFacingRight = player.X < GameObject.X;

                    Texture2D sporeTexture = shootPinkSpore ? pinkSporeTexture : purpleSporeTexture;
                    GameObject projectile = new GameObject(GameObject.X, GameObject.Y, new SporeProjectile(GameObject.position, playerPosition, sporeTexture, shootPinkSpore));
                    
                    SpriteRenderer projectileRenderer = new SpriteRenderer(new Rectangle(GameObject.X, GameObject.Y, (int)(144 * projectileScale), (int)(144 * projectileScale)), false);
                    projectile.AddComponent(projectileRenderer);

                    if (shootPinkSpore) {
                        projectileRenderer.addAnimation("pinkSpore", new Animation(sporeTexture, 3, 12, 144, 144));
                        projectileRenderer.setAnimation("pinkSpore");
                    }
                    else {
                        projectileRenderer.addAnimation("purpleSpore", new Animation(sporeTexture, 3, 12, 144, 144));
                        projectileRenderer.setAnimation("purpleSpore");
                    }


                    CircleCollider collider = new CircleCollider(30, new Vector2(-30, -35), GOManager.Instance.GraphicsDevice);
                    projectile.AddComponent(collider);

                    Rectangle effectPosition = new Rectangle();
                    if (sRend.isFacingRight) {
                        effectPosition = new Rectangle(GameObject.X - 7, GameObject.Y + 25, 72, 72);
                    }
                    else {
                        effectPosition = new Rectangle(GameObject.X + 45, GameObject.Y + 25, 72, 72);
                    }

                    VisualEffectFactory.createVisualEffect(effectPosition, attackVFX, 3, 5, 0.5f, sRend.isFacingRight);

                    GOManager.Instance.allGOs.Add(projectile);

                    shootCooldown = 4.0;
                }

            }
        }
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
