using Cuphead;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

public class SoundEffectStorage
{
    private Dictionary<string, SoundEffect> _audios = new Dictionary<string, SoundEffect>();
    private Song backgroundMusic, bossFightMusic, victoryMusic;

    public void LoadContent(ContentManager content)
    {
        backgroundMusic = content.Load<Song>(@"SFX\LevelSFX\ForestFolliesBackgroundMusic");
        bossFightMusic = content.Load<Song>(@"SFX\LevelSFX\FloralFuryBackgroundMusic");
        victoryMusic = content.Load<Song>(@"SFX\LevelSFX\VictoryScreenBackgroundMusic");
        loadProjectileSounds(content);
        loadPlayerSounds(content);
        loadEnemySounds(content);
        loadItemSounds(content);
        loadLevelSounds(content);
    }

    public SoundEffect getAudio(string audioName)
    {
        if (_audios.ContainsKey(audioName))
            return _audios[audioName];

        return null;
    }

    public void loadAudioManager(AudioManager audioManager)
    {
        audioManager.backgroundMusic = backgroundMusic;
        audioManager.bossFightMusic = bossFightMusic;
        audioManager.victoryMusic = victoryMusic;
        audioManager.addSoundObject(_audios);

        // PlayerProjectileSFX
        audioManager.changeVolume("PeashooterShotLoop", 0.2f);
        audioManager.changeVolume("PeashooterShotImpact", 0.1f);

        audioManager.changeVolume("SpreadshotShotLoop", 0.2f);
        audioManager.changeVolume("SpreadshotShotImpact", 0.1f);

        audioManager.changeVolume("ChaserShotLoop", 0.4f);
        audioManager.changeVolume("ChaserShotImpact", 0.3f);

        audioManager.changeVolume("RoundaboutShot", 0.2f);
        audioManager.changeVolume("RoundaboutShotImpact", 0.1f);

        audioManager.changeVolume("LobberShot", 0.2f);
        audioManager.changeVolume("LobberShotImpact", 0.1f);

        // PlayerMovementSFX
        audioManager.changeVolumePitch("PlayerWalk", 0.2f, -1.0f);
        audioManager.changeVolume("PlayerDamaged", 0.3f);
        audioManager.changeVolume("PlayerJump", 0.2f);
        audioManager.changeVolume("PlayerLanding", 0.2f);
        audioManager.changeVolume("PlayerDash", 0.3f);
        audioManager.changeVolume("PlayerDeath", 0.3f);
        audioManager.changeVolume("PlayerParry", 0.8f);

        // EnemySFX
        //      AcornMakerSFX
        audioManager.changeVolume("AcornMakerDeath", 0.3f);
        audioManager.changeVolume("AcornMakerIdle", 0.2f);

        //      AggravatingAcornSFX
        audioManager.changeVolume("AggravatingAcornDeath", 0.2f);
        audioManager.changeVolume("AggravatingAcornIdle", 0.2f);
        audioManager.changeVolume("AggravatingAcornDrop", 0.3f);

        //      BothersomeBlueberrySFX
        audioManager.changeVolume("BothersomeBlueberryDeath", 0.3f);
        audioManager.changeVolume("BothersomeBlueberryRevive", 0.2f);
        audioManager.changeVolume("BothersomeBlueberryIdle", 0.05f);

        //      DeadlyDaisySFX
        audioManager.changeVolume("DeadlyDaisyDeath", 0.3f);
        audioManager.changeVolume("DeadlyDaisyFloat", 0.5f);
        audioManager.changeVolume("DeadlyDaisyLanding", 0.3f);

        //      MurderousMushroomSFX
        audioManager.changeVolume("MurderousMushroomDeath", 0.3f);
        audioManager.changeVolume("MurderousMushroomUp", 0.7f);
        audioManager.changeVolume("MurderousMushroomShoot", 0.4f);

        //      SpikyBulbSFX
        audioManager.changeVolume("SpikyBulbDeath", 0.4f);

        //      TerribleTulipSFX
        audioManager.changeVolume("TerribleTulipDeath", 0.3f);
        audioManager.changeVolume("TerribleTulipShoot", 0.4f);
        audioManager.changeVolume("TerribleTulipProjectileExplosion", 0.4f);

        //      ToothyTerrorSFX
        audioManager.changeVolume("ToothyTerrorDeath", 0.4f);
        audioManager.changeVolume("ToothyTerrorUp", 0.1f);
        audioManager.changeVolume("ToothyTerrorBite", 0.1f);

        // ItemSFX
        audioManager.changeVolume("CoinPickup", 0.5f);
        audioManager.changeVolume("ProjectileItemPickup", 0.5f);

        // LevelSFX
        audioManager.changeVolume("Intro", 0.3f);

    }

    public void loadProjectileSounds(ContentManager content)
    {
        _audios["PeashooterShotLoop"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerProjectileSFX\PeashooterShotLoop");
        _audios["PeashooterShotImpact"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerProjectileSFX\PeashooterShotImpact");
       
        _audios["SpreadshotShotLoop"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerProjectileSFX\SpreadshotShotLoop");
        _audios["SpreadshotShotImpact"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerProjectileSFX\SpreadshotShotImpact");
        
        _audios["ChaserShotLoop"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerProjectileSFX\ChaserShotLoop");
        _audios["ChaserShotImpact"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerProjectileSFX\ChaserShotImpact");
        
        _audios["LobberShot"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerProjectileSFX\LobberShot");
        _audios["LobberShotImpact"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerProjectileSFX\LobberShotImpact");
        
        _audios["RoundaboutShot"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerProjectileSFX\RoundaboutShot");
        _audios["RoundaboutShotImpact"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerProjectileSFX\RoundaboutShotImpact");
    }

    public void loadPlayerSounds(ContentManager content)
    {
        _audios["PlayerDash"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerDash");
        _audios["PlayerDeath"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerDeath");
        _audios["PlayerDamaged"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerDamaged");
        _audios["PlayerJump"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerJump");
        _audios["PlayerLanding"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerLanding");
        _audios["PlayerWalk"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerWalk");
        _audios["PlayerParry"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerParry");
    }

    public void loadEnemySounds(ContentManager content)
    {
        _audios["AcornMakerDeath"] = content.Load<SoundEffect>(@"SFX\EnemySFX\AcornMakerSFX\AcornMakerDeath");
        _audios["AcornMakerIdle"] = content.Load<SoundEffect>(@"SFX\EnemySFX\AcornMakerSFX\AcornMakerIdle");

        _audios["AggravatingAcornDrop"] = content.Load<SoundEffect>(@"SFX\EnemySFX\AggravatingAcornSFX\AggravatingAcornDrop");
        _audios["AggravatingAcornIdle"] = content.Load<SoundEffect>(@"SFX\EnemySFX\AggravatingAcornSFX\AggravatingAcornIdle");
        _audios["AggravatingAcornDeath"] = content.Load<SoundEffect>(@"SFX\EnemySFX\AggravatingAcornSFX\AggravatingAcornDeath");

        _audios["BothersomeBlueberryDeath"] = content.Load<SoundEffect>(@"SFX\EnemySFX\BothersomeBlueberrySFX\BothersomeBlueberryDeath");
        _audios["BothersomeBlueberryIdle"] = content.Load<SoundEffect>(@"SFX\EnemySFX\BothersomeBlueberrySFX\BothersomeBlueberryIdle");
        _audios["BothersomeBlueberryRevive"] = content.Load<SoundEffect>(@"SFX\EnemySFX\BothersomeBlueberrySFX\BothersomeBlueberryRevive");
        
        _audios["DeadlyDaisyDeath"] = content.Load<SoundEffect>(@"SFX\EnemySFX\DeadlyDaisySFX\DeadlyDaisyDeath");
        _audios["DeadlyDaisyFloat"] = content.Load<SoundEffect>(@"SFX\EnemySFX\DeadlyDaisySFX\DeadlyDaisyFloat");
        _audios["DeadlyDaisyLanding"] = content.Load<SoundEffect>(@"SFX\EnemySFX\DeadlyDaisySFX\DeadlyDaisyLanding");
        
        _audios["MurderousMushroomUp"] = content.Load<SoundEffect>(@"SFX\EnemySFX\MurderousMushroomSFX\MurderousMushroomUp");
        _audios["MurderousMushroomShoot"] = content.Load<SoundEffect>(@"SFX\EnemySFX\MurderousMushroomSFX\MurderousMushroomShoot");
        _audios["MurderousMushroomDeath"] = content.Load<SoundEffect>(@"SFX\EnemySFX\MurderousMushroomSFX\MurderousMushroomDeath");
        
        _audios["SpikyBulbDeath"] = content.Load<SoundEffect>(@"SFX\EnemySFX\SpikyBulbSFX\SpikyBulbDeath");
        
        _audios["TerribleTulipDeath"] = content.Load<SoundEffect>(@"SFX\EnemySFX\TerribleTulipSFX\TerribleTulipDeath");
        _audios["TerribleTulipProjectileExplosion"] = content.Load<SoundEffect>(@"SFX\EnemySFX\TerribleTulipSFX\TerribleTulipProjectileExplosion");
        _audios["TerribleTulipShoot"] = content.Load<SoundEffect>(@"SFX\EnemySFX\TerribleTulipSFX\TerribleTulipShoot");

        _audios["ToothyTerrorBite"] = content.Load<SoundEffect>(@"SFX\EnemySFX\ToothyTerrorSFX\ToothyTerrorBite");
        _audios["ToothyTerrorUp"] = content.Load<SoundEffect>(@"SFX\EnemySFX\ToothyTerrorSFX\ToothyTerrorUp");
        _audios["ToothyTerrorDeath"] = content.Load<SoundEffect>(@"SFX\EnemySFX\ToothyTerrorSFX\ToothyTerrorDeath");
    }

    public void loadItemSounds(ContentManager content)
    {
        _audios["CoinPickup"] = content.Load<SoundEffect>(@"SFX\ItemSFX\CoinPickup");
        _audios["ProjectileItemPickup"] = content.Load<SoundEffect>(@"SFX\ItemSFX\ProjectileItemPickup");
    }

    public void loadLevelSounds(ContentManager content)
    {
        _audios["Intro"] = content.Load<SoundEffect>(@"SFX\LevelSFX\Intro");
    }
}

