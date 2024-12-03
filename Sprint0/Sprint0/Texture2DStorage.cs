using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class Texture2DStorage
{
    private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

    public void LoadContent(ContentManager content)
    {
        //Enemy textures
        _textures["DeadlyDaisy"] = content.Load<Texture2D>(@"EnemyTextures\DeadlyDaisySprite");
        _textures["MurderousMushroom"] = content.Load<Texture2D>(@"EnemyTextures\MurderousMushroomSprite");
        _textures["TerribleTulip"] = content.Load<Texture2D>(@"EnemyTextures\TerribleTulipSprite");
        _textures["ToothyTerror"] = content.Load<Texture2D>(@"EnemyTextures\ToothyTerrorSprite");
        _textures["BothersomeBlueberry"] = content.Load<Texture2D>(@"EnemyTextures\BothersomeBlueberrySprite");
        _textures["AggravatingAcorn"] = content.Load<Texture2D>(@"EnemyTextures\AggravatingAcornSprite");
        _textures["AcornMaker"] = content.Load<Texture2D>(@"EnemyTextures\acorn-maker");
        _textures["SpikyBulb"] = content.Load<Texture2D>(@"EnemyTextures\spike-ball");
        _textures["Seed"] = content.Load<Texture2D>(@"EnemyTextures\LoberSeed");
        _textures["PurpleSpore"] = content.Load<Texture2D>(@"EnemyTextures\purple_spore");
        _textures["PinkSpore"] = content.Load<Texture2D>(@"EnemyTextures\pink_spore");

        _textures["AcornFall"] = content.Load<Texture2D>(@"EnemyTextures\acorn_fall");
        _textures["BlueberryMelt"] = content.Load<Texture2D>(@"EnemyTextures\blueberry_melt");
        _textures["BlueberryRespawn"] = content.Load<Texture2D>(@"EnemyTextures\blueberry_respawn");
        _textures["BlueberryWaitingToRespawn"] = content.Load<Texture2D>(@"EnemyTextures\waiting_for_respawn");
        _textures["BlueberryTurn"] = content.Load<Texture2D>(@"EnemyTextures\blueberry-turn");
        _textures["DaisySpawn"] = content.Load<Texture2D>(@"EnemyTextures\daisy_spawn");
        _textures["MushroomAttack"] = content.Load<Texture2D>(@"EnemyTextures\mushroom_attack");
        _textures["ToothyTerrorSpinAttack"] = content.Load<Texture2D>(@"EnemyTextures\terror_spin");
        _textures["TulipIdle"] = content.Load<Texture2D>(@"EnemyTextures\tulip_idle");
        _textures["DaisyTurn"] = content.Load<Texture2D>(@"EnemyTextures\daisy-turn");
        _textures["DaisyJump"] = content.Load<Texture2D>(@"EnemyTextures\daisy-jump");

        _textures["MushroomClosed"] = content.Load<Texture2D>(@"EnemyTextures\mushroom_closed");
        _textures["MushroomClosing"] = content.Load<Texture2D>(@"EnemyTextures\mushroom_closing");
        _textures["MushroomOpening"] = content.Load<Texture2D>(@"EnemyTextures\mushroom_opening");
        _textures["MushroomAttackVFX"] = content.Load<Texture2D>(@"EnemyTextures\mushroom_shoot_vfx");
        _textures["AcornMakerDeath"] = content.Load<Texture2D>(@"EnemyTextures\acorn-maker-death");



        //Level Textures

        //Background
        _textures["LevelBackground"] = content.Load<Texture2D>(@"BlockTextures\Background\background-1");
        _textures["HillBackground1"] = content.Load<Texture2D>(@"BlockTextures\Background\hill-background-1");

        _textures["TreeBackground1"] = content.Load<Texture2D>(@"BlockTextures\Background\tree-background-1");
        _textures["TreeBackground2"] = content.Load<Texture2D>(@"BlockTextures\Background\tree-background-2");
        _textures["TreeBackground3"] = content.Load<Texture2D>(@"BlockTextures\Background\tree-background-3");
        _textures["TreeBackground4"] = content.Load<Texture2D>(@"BlockTextures\Background\tree-background-4");
        _textures["TreeBackground5"] = content.Load<Texture2D>(@"BlockTextures\Background\tree-background-5");
        _textures["TreeBackground6"] = content.Load<Texture2D>(@"BlockTextures\Background\tree-background-6");
        _textures["TreeBackground7"] = content.Load<Texture2D>(@"BlockTextures\Background\tree-background-7");
        _textures["TreeBackground8"] = content.Load<Texture2D>(@"BlockTextures\Background\tree-background-8");

        _textures["Clouds"] = content.Load<Texture2D>(@"BlockTextures\Background\clouds-1");
        _textures["SkyBackground1"] = content.Load<Texture2D>(@"BlockTextures\Background\sky-background-1");

        _textures["Grass1"] = content.Load<Texture2D>(@"BlockTextures\Background\grass-1");
        _textures["Grass2"] = content.Load<Texture2D>(@"BlockTextures\Background\grass-2");


        //Bushes
        _textures["Bush1"] = content.Load<Texture2D>(@"BlockTextures\Bushes\bush-1");
        _textures["Bush2"] = content.Load<Texture2D>(@"BlockTextures\Bushes\bush-2");

        //Hills
        _textures["BigHill1"] = content.Load<Texture2D>(@"BlockTextures\Hills\big-hill-1");
        _textures["BigHill2"] = content.Load<Texture2D>(@"BlockTextures\Hills\big-hill-2");
        _textures["BigHill3"] = content.Load<Texture2D>(@"BlockTextures\Hills\big-hill-3");
        _textures["BigHill4"] = content.Load<Texture2D>(@"BlockTextures\Hills\big-hill-4");
        _textures["Hill1"] = content.Load<Texture2D>(@"BlockTextures\Hills\hill-1");
        _textures["Hill2"] = content.Load<Texture2D>(@"BlockTextures\Hills\hill-2");
        _textures["Hill3"] = content.Load<Texture2D>(@"BlockTextures\Hills\hill-3");
        _textures["Hill4"] = content.Load<Texture2D>(@"BlockTextures\Hills\hill-4");
        _textures["Hill5"] = content.Load<Texture2D>(@"BlockTextures\Hills\hill-5");

        //Logs
        _textures["Log1"] = content.Load<Texture2D>(@"BlockTextures\FallenTrees\log-1");
        _textures["Log2"] = content.Load<Texture2D>(@"BlockTextures\FallenTrees\log-2");
        _textures["Stump1"] = content.Load<Texture2D>(@"BlockTextures\FallenTrees\Stump1");
        _textures["Stump2"] = content.Load<Texture2D>(@"BlockTextures\FallenTrees\Stump2");
        _textures["Stump3"] = content.Load<Texture2D>(@"BlockTextures\FallenTrees\Stump3");

        //Platforms
        _textures["Platform1"] = content.Load<Texture2D>(@"BlockTextures\Platforms\platform-1");
        _textures["Platform2"] = content.Load<Texture2D>(@"BlockTextures\Platforms\platform-2");

        //Rocks
        _textures["Rock1"] = content.Load<Texture2D>(@"BlockTextures\Rocks\rock-1");
        _textures["Rock2"] = content.Load<Texture2D>(@"BlockTextures\Rocks\rock-2");
        _textures["Rock3"] = content.Load<Texture2D>(@"BlockTextures\Rocks\rock-3");
        _textures["Rock4"] = content.Load<Texture2D>(@"BlockTextures\Rocks\rock-4");
        _textures["Rock5"] = content.Load<Texture2D>(@"BlockTextures\Rocks\rock-5");
        _textures["Rock6"] = content.Load<Texture2D>(@"BlockTextures\Rocks\rock-6");

        //Trees
        _textures["Leaves1"] = content.Load<Texture2D>(@"BlockTextures\Trees\leaves-1");
        _textures["Leaves2"] = content.Load<Texture2D>(@"BlockTextures\Trees\leaves-2");
        _textures["Leaves3"] = content.Load<Texture2D>(@"BlockTextures\Trees\leaves-3");
        _textures["Tree1"] = content.Load<Texture2D>(@"BlockTextures\Trees\tree-1");
        _textures["Tree2"] = content.Load<Texture2D>(@"BlockTextures\Trees\tree-2");
        _textures["Tree3"] = content.Load<Texture2D>(@"BlockTextures\Trees\tree-3");
        _textures["Tree4"] = content.Load<Texture2D>(@"BlockTextures\Trees\tree-4");
        _textures["Tree5"] = content.Load<Texture2D>(@"BlockTextures\Trees\tree-5");
        _textures["Tree6"] = content.Load<Texture2D>(@"BlockTextures\Trees\tree-6");

        //Title Screen
        _textures["Title1"] = content.Load<Texture2D>(@"TitleScreen\title-1");
        _textures["Title2"] = content.Load<Texture2D>(@"TitleScreen\title-2");

        //Game Start
        _textures["GameStartText"] = content.Load<Texture2D>(@"GameStart\game-start-text-animation");

        //End Game Screen
        _textures["DeathMessage"] = content.Load<Texture2D>(@"EndGameScreen\death-message");
        _textures["WinScreenBackground"] = content.Load<Texture2D>(@"EndGameScreen\winscreen_background");
        _textures["WinScreenBoard"] = content.Load<Texture2D>(@"EndGameScreen\winscreen_board");
        _textures["WinScreenLine"] = content.Load<Texture2D>(@"EndGameScreen\winscreen_line");
        _textures["WinScreenStar"] = content.Load<Texture2D>(@"EndGameScreen\winscreen_star");
        _textures["WinScreenUnearnedStar"] = content.Load<Texture2D>(@"EndGameScreen\winscreen_star_unearned");
        _textures["WinScreenCircle"] = content.Load<Texture2D>(@"EndGameScreen\winscreen-circle");
        _textures["WinScreenResultsText"] = content.Load<Texture2D>(@"EndGameScreen\winscreen-results-text");
        _textures["WinScreenStarAppearAnimation"] = content.Load<Texture2D>(@"EndGameScreen\winscreen-star-appear");
        _textures["WinScreenCuphead"] = content.Load<Texture2D>(@"EndGameScreen\winscren-cuphead-idle");
        _textures["LossScreen"] = content.Load<Texture2D>(@"EndGameScreen\forest-follies-endgame");
        _textures["LossScreenIcon"] = content.Load<Texture2D>(@"EndgameScreen\cuphead-icon-retry-screen");
        _textures["FadeIn"] = content.Load<Texture2D>(@"EndgameScreen\iris-fade-in");
        _textures["FadeOut"] = content.Load<Texture2D>(@"EndGameScreen\iris-fade-out");
        _textures["Victory"] = content.Load<Texture2D>(@"TextAnimations\victory-text-animation");
 

        //Player Animation Textures
        _textures["PlayerDashAir"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerDashAir");
        _textures["PlayerDashGround"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerDashGround");
        _textures["PlayerDeath"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerDeath");
        _textures["PlayerDuck"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerDuck");
        _textures["PlayerDuckShoot"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerDuckShoot");
        _textures["PlayerHitAir"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerHitAir");
        _textures["PlayerHitGround"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerHitGround");
        _textures["PlayerIdle"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerIdle");
        _textures["PlayerIntro"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerIntro");
        _textures["PlayerJump"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerJump");
        _textures["PlayerParry"] = content.Load<Texture2D>(@"PlayerAnimationTextures\player-parry-pink");
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
        _textures["Lobber"] = content.Load<Texture2D>(@"ProjectileTextures\Lobber");
        _textures["Peashooter"] = content.Load<Texture2D>(@"ProjectileTextures\Peashooter");
        _textures["Roundabout"] = content.Load<Texture2D>(@"ProjectileTextures\Roundabout");
        _textures["Spread"] = content.Load<Texture2D>(@"ProjectileTextures\Spread");

        //Projectile Explosion Textures
        _textures["ChaserExplosion"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\chaser_hit_vfx");
        _textures["LobberExplosion"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\lobber_hit_vfx");
        _textures["PeashooterExplosion"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\peashooter_hit_vfx");
        _textures["RoundaboutExplosion"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\roundabout_hit_vfx");
        _textures["SpreadExplosion"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\spread_hit_vfx");

        //Projectile Spawning Textures
        _textures["ChaserSpawn"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\chaser_spawn_vfx");
        _textures["LobberSpawn"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\lobber_spawn_vfx");
        _textures["PeashooterSpawn"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\peashooter_spawn_vfx");
        _textures["RoundaboutSpawn"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\roundabout_spawn_vfx");
        _textures["SpreadSpawn"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\spread_spawn_vfx");

        //Visual Effect Textures
        _textures["Dust"] = content.Load<Texture2D>(@"VisualEffectTextures\DustEffect");
        _textures["EnemyDeath"] = content.Load<Texture2D>(@"VisualEffectTextures\enemy_death");
        _textures["TulipAttackVFX"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\tulip-attack-vfx");
        _textures["TulipHitVFX"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\tulip-hit-vfx");
        _textures["SporeTrailVFX"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\spore-trail-vfx");
        _textures["SporeExplosionVFX"] = content.Load<Texture2D>(@"VisualEffectTextures\ProjectileVFX\spore-explosion-vfx");
        _textures["ParryVFX"] = content.Load<Texture2D>(@"VisualEffectTextures\parry-vfx");
        _textures["CoinVFX"] = content.Load<Texture2D>(@"VisualEffectTextures\coin-vfx");
        _textures["PropVFX"] = content.Load<Texture2D>(@"VisualEffectTextures\acorn-prop-vfx");
        _textures["PlayerDamageVFX"] = content.Load<Texture2D>(@"VisualEffectTextures\dmg-vfx");


        //item texture
        _textures["ChaserItem"] = content.Load<Texture2D>(@"Items\ChaserItem");
        _textures["LobberItem"] = content.Load<Texture2D>(@"Items\LobberItem");
        _textures["RoundaboutItem"] = content.Load<Texture2D>(@"Items\RoundaboutItem");
        _textures["SpreadshotItem"] = content.Load<Texture2D>(@"Items\SpreadshotItem");
        _textures["Coin"] = content.Load<Texture2D>(@"Items\coin");

        //User interface textures
        _textures["CardBack"] = content.Load<Texture2D>(@"UI\red-card-back");
        _textures["CardFront"] = content.Load<Texture2D>(@"UI\red-ace-card");
        _textures["hp1-v1"] = content.Load<Texture2D>(@"UI\hp-1-v1");
        _textures["hp1-v2"] = content.Load<Texture2D>(@"UI\hp-1-v2");
        _textures["hp1-v3"] = content.Load<Texture2D>(@"UI\hp-1-v3");
        _textures["hp2"] = content.Load<Texture2D>(@"UI\hp-2");
        _textures["hp3"] = content.Load<Texture2D>(@"UI\hp-3");
        _textures["hp4"] = content.Load<Texture2D>(@"UI\hp-4");
        _textures["hp5"] = content.Load<Texture2D>(@"UI\hp-5");
        _textures["hp6"] = content.Load<Texture2D>(@"UI\hp-6");
        _textures["hp7"] = content.Load<Texture2D>(@"UI\hp-7");
        _textures["hp8"] = content.Load<Texture2D>(@"UI\hp-8");
        _textures["hpDead"] = content.Load<Texture2D>(@"UI\hp-dead");


        //Boss level
        //Stage 1
        _textures["BossMagicHands"] = content.Load<Texture2D>(@"BossLevel\Stage1\boss-create-item");
        _textures["BossStageOneIdle"] = content.Load<Texture2D>(@"BossLevel\Stage1\boss-idle");
        _textures["BossSpawn"] = content.Load<Texture2D>(@"BossLevel\Stage1\boss-stage1-intro-temp");
        // ^ I might redo this texture at some point, I think some of the frames might be weird but not sure how it'll actually look in game
        _textures["BossShootSeeds"] = content.Load<Texture2D>(@"BossLevel\Stage1\boss-shoot-seeds");
        _textures["BossFaceAttackHigh"] = content.Load<Texture2D>(@"BossLevel\Stage1\face-attack-high");
        _textures["BossFaceAttackLow"] = content.Load<Texture2D>(@"BossLevel\Stage1\face-attack-low");


        //Stage 2

        //Stage 3
        _textures["BossDeath"] = content.Load<Texture2D>(@"BossLevel\Stage3\boss-death");
        _textures["BossFinalTransformation"] = content.Load<Texture2D>(@"BossLevel\Stage3\boss-final-stage-transformation");
        _textures["BossFinalStageIdle"] = content.Load<Texture2D>(@"BossLevel\Stage3\boss-final-stage-idle");
        _textures["BossFinalStageAttack"] = content.Load<Texture2D>(@"BossLevel\Stage3\boss-final-stage-attack");
        _textures["HorizontalVineAttackExtend"] = content.Load<Texture2D>(@"BossLevel\Stage3\vine-attack-horizontal-extend");
        _textures["HorizontalVineAttackRetract"] = content.Load<Texture2D>(@"BossLevel\Stage3\vine-attack-horizontal-retract");
        _textures["VerticalVineAttackExtend"] = content.Load<Texture2D>(@"BossLevel\Stage3\vine-attack-vertical-extend");
        _textures["VerticalVineAttackRetract"] = content.Load<Texture2D>(@"BossLevel\Stage3\vine-attack-vertical-retract");

        //Boss level enemy textures
        _textures["VineGroundburst"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\flower-vine-groundburst");
        _textures["EnemySpawnVine"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\enemy-spawn-vine");

        _textures["FlytrapDeath"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\flytrap-minion-death");
        _textures["FlytrapSpawn"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\flytrap-spawn");
        _textures["FlytrapAttack"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\flytrap-attack");
        _textures["FlytrapVine"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\flytrap-minion-vine");

        _textures["ChomperDeath"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\chomper-minion-death");
        _textures["ChomperAttack"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\chomper-attack");

        _textures["MiniFlowerAttack"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\mini-flower-attack-orange");
        _textures["MiniFlowerDeath"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\mini-flower-death");
        _textures["MiniFlowerFly"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\mini-flower-fly-orange");
        _textures["MiniFlowerProjectile"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\mini-flower-projectile");
        _textures["MiniFlowerHitVFX"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\mini-flower-hit-vfx");
        _textures["MiniFlowerSpawn"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\mini-flower-spawn-orange");


        //Seeds
        _textures["BlueSeed"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\Seeds\boss-blue-seed");
        _textures["BlueSeedPlant"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\Seeds\blue-seed-plant");
        _textures["PinkSeed"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\Seeds\boss-pink-seed");
        _textures["PinkSeedPlant"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\Seeds\pink-seed-plant");
        _textures["PurpleSeed"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\Seeds\boss-purple-seed");
        _textures["PurpleSeedPlant"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\Seeds\purple-seed-plant");
        _textures["SeedMissileFireVFX"] = content.Load<Texture2D>(@"BossLevel\SpawnedEnemies\seed-missile-fire-vfx");


        //Boss level environment
        _textures["BossLevelFloatingPlatform"] = content.Load<Texture2D>(@"BossLevel\Environment\boss-level-floating-platform");
        _textures["BossLevelBush"] = content.Load<Texture2D>(@"BossLevel\Environment\boss-level-bush");
        _textures["BossLevelClouds"] = content.Load<Texture2D>(@"BossLevel\Environment\boss-level-clouds");
        _textures["BossLevelGround"] = content.Load<Texture2D>(@"BossLevel\Environment\boss-level-ground");
        _textures["BossLevelHills"] = content.Load<Texture2D>(@"BossLevel\Environment\boss-level-hills");
        _textures["BossLevelSky"] = content.Load<Texture2D>(@"BossLevel\Environment\boss-level-sky-background");

        //Boss projectiles
        _textures["SmallAcornProjectile"] = content.Load<Texture2D>(@"BossLevel\BossProjectiles\small-acorn-projectile");
        _textures["BoomerangProjectile"] = content.Load<Texture2D>(@"BossLevel\BossProjectiles\boomerang-projectile");
        _textures["PollenProjectilePink"] = content.Load<Texture2D>(@"BossLevel\BossProjectiles\pollen-projectile-pink");
        _textures["PollenProjectileWhite"] = content.Load<Texture2D>(@"BossLevel\BossProjectiles\pollen-projectile-white");


        // Add more textures as needed
    }

    public Texture2D GetTexture(string textureName)
    {
        if (_textures.ContainsKey(textureName))
        {
            return _textures[textureName];
        }
        else
        {
            Console.WriteLine(textureName + " does not exist");
            return null;
        }
            

        
    }

    public void loadPlayerAnimations(SpriteRenderer spriteRenderer) {
        Animation playerDashAirAnimation = new Animation(GetTexture("PlayerDashAir"), 2, 5, 144, 144);
        Animation playerDashGroundAnimation = new Animation(GetTexture("PlayerDashGround"), 2, 5, 144, 144);
        Animation playerDeathAnimation = new Animation(GetTexture("PlayerDeath"), 5, 16, 144, 144);
        Animation playerDuckAnimation = new Animation(GetTexture("PlayerDuck"), 5, 8, 144, 144);
        Animation playerDuckShootAnimation = new Animation(GetTexture("PlayerDuckShoot"), 5, 3, 144, 144);
        Animation playerHitAirAnimation = new Animation(GetTexture("PlayerHitAir"), 3, 6, 144, 144);
        Animation playerHitGroundAnimation = new Animation(GetTexture("PlayerHitGround"), 3, 6, 144, 144);
        Animation playerIdleAnimation = new Animation(GetTexture("PlayerIdle"), 5, 8, 144, 144);
        Animation playerSpawnAnimation = new Animation(GetTexture("PlayerIntro"), 3, 28, 144, 144);
        Animation playerJumpAnimation = new Animation(GetTexture("PlayerJump"), 2, 8, 144, 144);
        Animation playerParryAnimation = new Animation(GetTexture("PlayerParry"), 2, 8, 144, 144);
        Animation playerRunAnimation = new Animation(GetTexture("PlayerRun"),2, 16, 144, 144);
        Animation playerRunShootingDiagonalUpAnimation = new Animation(GetTexture("PlayerRunShootingDiagonalUp"), 2, 16, 144, 144);
        Animation playerRunShootingStraightAnimation = new Animation(GetTexture("PlayerRunShootingStraight"), 2, 16, 144, 144);
        Animation playerShootDiagonalDownAnimation = new Animation(GetTexture("PlayerShootDiagonalDown"), 5, 3, 144, 144);
        Animation playerShootDiagonalUpAnimation = new Animation(GetTexture("PlayerShootDiagonalUp"), 5, 3, 144, 144);
        Animation playerShootDownAnimation = new Animation(GetTexture("PlayerShootDown"), 5, 3, 144, 144);
        Animation playerShootStraightAnimation = new Animation(GetTexture("PlayerShootStraight"), 5, 3, 144, 144);
        Animation playerShootUpAnimation = new Animation(GetTexture("PlayerShootUp"), 5, 3, 144, 144);

        spriteRenderer.addAnimation("DashAir", playerDashAirAnimation);
        spriteRenderer.addAnimation("DashGround", playerDashGroundAnimation);
        spriteRenderer.addAnimation("Death", playerDeathAnimation);
        spriteRenderer.addAnimation("Duck", playerDuckAnimation);
        spriteRenderer.addAnimation("DuckShoot", playerDuckShootAnimation);
        spriteRenderer.addAnimation("HitAir", playerHitAirAnimation);
        spriteRenderer.addAnimation("HitGround", playerHitGroundAnimation);
        spriteRenderer.addAnimation("Idle", playerIdleAnimation);
        spriteRenderer.addAnimation("Spawn", playerSpawnAnimation);
        spriteRenderer.addAnimation("Jump", playerJumpAnimation);
        spriteRenderer.addAnimation("Parry", playerParryAnimation);
        spriteRenderer.addAnimation("Run", playerRunAnimation);
        spriteRenderer.addAnimation("RunShootingDiagonalUp", playerRunShootingDiagonalUpAnimation);
        spriteRenderer.addAnimation("RunShootingStraight", playerRunShootingStraightAnimation);
        spriteRenderer.addAnimation("ShootDiagonalDown", playerShootDiagonalDownAnimation);
        spriteRenderer.addAnimation("ShootDiagonalUp", playerShootDiagonalUpAnimation);
        spriteRenderer.addAnimation("ShootDown", playerShootDownAnimation);
        spriteRenderer.addAnimation("ShootStraight", playerShootStraightAnimation);
        spriteRenderer.addAnimation("ShootUp", playerShootUpAnimation);
    }
}
