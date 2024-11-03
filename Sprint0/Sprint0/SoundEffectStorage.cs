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
        _audios["PeashooterShotLoop"] = content.Load<SoundEffect>(@"ProjectileSFX\PeashooterShotLoop");
        _audios["PeashooterShotImpact"] = content.Load<SoundEffect>(@"ProjectileSFX\PeashooterShotImpact");
        _audios["SpreadshotShotLoop"] = content.Load<SoundEffect>(@"ProjectileSFX\SpreadshotShotLoop");
        _audios["SpreadshotShotImpact"] = content.Load<SoundEffect>(@"ProjectileSFX\SpreadshotShotImpact");
        _audios["ChaserShotLoop"] = content.Load<SoundEffect>(@"ProjectileSFX\ChaserShotLoop");
        _audios["ChaserShotImpact"] = content.Load<SoundEffect>(@"ProjectileSFX\ChaserShotImpact");
        _audios["LobberShot"] = content.Load<SoundEffect>(@"ProjectileSFX\LobberShot");
        _audios["RoundaboutShot"] = content.Load<SoundEffect>(@"ProjectileSFX\RoundaboutShot");
    }

    public void loadPlayerSounds(ContentManager content)
    {
        _audios["PlayerDash"] = content.Load<SoundEffect>(@"PlayerSFX\PlayerDash");
        _audios["PlayerDeath"] = content.Load<SoundEffect>(@"PlayerSFX\PlayerDeath");
        _audios["PlayerDamaged"] = content.Load<SoundEffect>(@"PlayerSFX\PlayerDamaged");
        _audios["PlayerJump"] = content.Load<SoundEffect>(@"PlayerSFX\PlayerJump");
        _audios["PlayerLanding"] = content.Load<SoundEffect>(@"PlayerSFX\PlayerLanding");
        _audios["PlayerWalk"] = content.Load<SoundEffect>(@"PlayerSFX\PlayerWalk");
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

