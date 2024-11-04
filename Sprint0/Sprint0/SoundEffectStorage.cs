using Cuphead;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SoundEffectStorage
{
    private Dictionary<string, SoundEffect> _audios = new Dictionary<string, SoundEffect>();

    public void LoadContent(ContentManager content)
    {
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
        audioManager.addSoundObject(_audios);
        audioManager.changeVolume("PlayerDeath", 0.5f);
        audioManager.changeVolume("PlayerWalk", 0.1f);
        audioManager.changePitch("PlayerWalk", -1.0f);
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
        _audios["RoundaboutShot"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerProjectileSFX\RoundaboutShot");
    }

    public void loadPlayerSounds(ContentManager content)
    {
        _audios["PlayerDash"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerDash");
        _audios["PlayerDeath"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerDeath");
        _audios["PlayerDamaged"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerDamaged");
        _audios["PlayerJump"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerJump");
        _audios["PlayerLanding"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerLanding");
        _audios["PlayerWalk"] = content.Load<SoundEffect>(@"SFX\PlayerSFX\PlayerMovementSFX\PlayerWalk");
    }

    public void loadEnemySounds(ContentManager content)
    {

    }

    public void loadItemSounds(ContentManager content)
    {

    }

    public void loadLevelSounds(ContentManager content)
    {

    }
}

