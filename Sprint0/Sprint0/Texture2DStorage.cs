using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Texture2DStorage
{
    private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

    // Initialize the texture storage with the ContentManager
    public void LoadContent(ContentManager content)
    {
        //Enemy textures
        _textures["DeadlyDaisy"] = content.Load<Texture2D>(@"EnemyTextures\DeadlyDaisySprite");
        _textures["MurderousMushroom"] = content.Load<Texture2D>(@"EnemyTextures\MurderousMushroomSprite");
        _textures["TerribleTulip"] = content.Load<Texture2D>(@"EnemyTextures\TerribleTulipSprite");
        _textures["ToothyTerror"] = content.Load<Texture2D>(@"EnemyTextures\ToothyTerrorSprite");
        _textures["BothersomeBlueberry"] = content.Load<Texture2D>(@"EnemyTextures\BothersomeBlueberrySprite");
        _textures["AggravatingAcorn"] = content.Load<Texture2D>(@"EnemyTextures\AggravatingAcornSprite");
        _textures["AcornMaker"] = content.Load<Texture2D>(@"EnemyTextures\AcornMakerSprite");
        _textures["Seed"] = content.Load<Texture2D>(@"EnemyTextures\LoberSeed");
        _textures["PurpleSpore"] = content.Load<Texture2D>(@"EnemyTextures\mushroom_poison_cloud_0001");
        _textures["PinkSpore"] = content.Load<Texture2D>(@"EnemyTextures\mushroom_poison_cloud_pink_0003");

        // Block/Obstacle Textures
        _textures["TreeStump"] = content.Load<Texture2D>(@"BlockTextures\ForestStumps");
        _textures["FallenLog"] = content.Load<Texture2D>(@"BlockTextures\ForestBackground-6");
        _textures["PlatformMd"] = content.Load<Texture2D>(@"BlockTextures\ForestBackground-2");
        _textures["PlatformLg"] = content.Load<Texture2D>(@"BlockTextures\ForestBackground-1");
        _textures["FloatingPlatformSm"] = content.Load<Texture2D>(@"BlockTextures\ForestBackground-5");
        _textures["FloatingPlatformLg"] = content.Load<Texture2D>(@"BlockTextures\ForestBackground-5");

        //Player Animation Textures
        _textures["PlayerDeath"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerDeath");
        _textures["PlayerDuck"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerDuck");
        _textures["PlayerDuckShoot"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerDuckShoot");
        _textures["PlayerHitAir"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerHitAir");
        _textures["PlayerHitGround"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerHitGround");
        _textures["PlayerIdle"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerIdle");
        _textures["PlayerIntro"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerIntro");
        _textures["PlayerJump"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerJump");
        _textures["PlayerRun"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerRun");
        _textures["PlayerRunShootingDiagonalUp"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerRunShootingDiagonalUp");
        _textures["PlayerRunShootingStraight"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerRunShootingStraight");
        _textures["PlayerShootDiagonalDown"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerShootDiagonalDown");
        _textures["PlayerShootDiagonalUp"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerShootDiagonalUp");
        _textures["PlayerShootDown"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerShootDown");
        _textures["PlayerShootStraight"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerShootStraight");
        _textures["PlayerShootUp"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerShootUp");

        //Projectile Textures
        _textures["Chaser"] = content.Load<Texture2D>(@"ProjectileTextures\Chaser");


        //item texture
        _textures["Item1_3"] = content.Load<Texture2D>("items/Items1");
        _textures["Item4_6"] = content.Load<Texture2D>("items/Items2");

        // Add more textures as needed
    }

    // Method to retrieve a texture
    public Texture2D GetTexture(string textureName)
    {
        if (_textures.ContainsKey(textureName))
            return _textures[textureName];

        return null; // Handle missing textures if necessary
    }

    public void loadPlayerAnimations(SpriteRenderer spriteRenderer) {

        //Load player animations
        Animation playerDeathAnimation = new Animation(GetTexture("PlayerDeath"), 5, 16, 144, 144);
        Animation playerDuckAnimation = new Animation(GetTexture("PlayerDuck"), 5, 8, 144, 144);
        Animation playerDuckShootAnimation = new Animation(GetTexture("PlayerDuckShoot"), 5, 3, 144, 144);
        Animation playerHitAirAnimation = new Animation(GetTexture("PlayerHitAir"), 5, 6, 144, 144);
        Animation playerHitGroundAnimation = new Animation(GetTexture("PlayerHitGround"), 5, 6, 144, 144);
        Animation playerIdleAnimation = new Animation(GetTexture("PlayerIdle"), 5, 8, 144, 144);
        Animation playerIntroAnimation = new Animation(GetTexture("PlayerIntro"), 5, 28, 144, 144);
        Animation playerJumpAnimation = new Animation(GetTexture("PlayerJump"), 2, 8, 144, 144);
        Animation playerRunAnimation = new Animation(GetTexture("PlayerRun"),1, 16, 144, 144);
        Animation playerRunShootingDiagonalUpAnimation = new Animation(GetTexture("PlayerRunShootingDiagonalUp"), 5, 16, 144, 144);
        Animation playerRunShootingStraightAnimation = new Animation(GetTexture("PlayerRunShootingStraight"), 1, 16, 144, 144);
        Animation playerShootDiagonalDownAnimation = new Animation(GetTexture("PlayerShootDiagonalDown"), 5, 3, 144, 144);
        Animation playerShootDiagonalUpAnimation = new Animation(GetTexture("PlayerShootDiagonalUp"), 5, 3, 144, 144);
        Animation playerShootDownAnimation = new Animation(GetTexture("PlayerShootDown"), 5, 3, 144, 144);
        Animation playerShootStraightAnimation = new Animation(GetTexture("PlayerShootStraight"), 5, 3, 144, 144);
        Animation playerShootUpAnimation = new Animation(GetTexture("PlayerShootUp"), 5, 3, 144, 144);

        spriteRenderer.addAnimation("Death", playerDeathAnimation);
        spriteRenderer.addAnimation("Duck", playerDuckAnimation);
        spriteRenderer.addAnimation("DuckShoot", playerDuckShootAnimation);
        spriteRenderer.addAnimation("HitAir", playerHitAirAnimation);
        spriteRenderer.addAnimation("HitGround", playerHitGroundAnimation);
        spriteRenderer.addAnimation("Idle", playerIdleAnimation);
        spriteRenderer.addAnimation("Intro", playerIntroAnimation);
        spriteRenderer.addAnimation("Jump", playerJumpAnimation);
        spriteRenderer.addAnimation("Run", playerRunAnimation);
        spriteRenderer.addAnimation("RunShootingDiagonalUp", playerRunShootingDiagonalUpAnimation);
        spriteRenderer.addAnimation("RunShootingStraight", playerRunShootingStraightAnimation);
        spriteRenderer.addAnimation("ShootDiagonalDown", playerShootDiagonalDownAnimation);
        spriteRenderer.addAnimation("ShootDiagonalUp", playerShootDiagonalUpAnimation);
        spriteRenderer.addAnimation("ShootDown", playerShootDownAnimation);
        spriteRenderer.addAnimation("ShootStraight", playerShootStraightAnimation);
        spriteRenderer.addAnimation("ShootUp", playerShootUpAnimation);

        spriteRenderer.loadAllAnimations();


    }
}
