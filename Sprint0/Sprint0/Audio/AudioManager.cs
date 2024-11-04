using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead
{
    public class AudioManager : IComponent
    {
        public GameObject GameObject { get; set; }
        public bool enabled { get; set; }
        private const float DEFAULT_VOLUME = 1.0f;
        private const float DEFAULT_PITCH = 0.0f;

        private Dictionary<string, SoundObject> soundObjects = new Dictionary<string, SoundObject>();
        public Song backgroundMusic { get; set; }

        public void addSoundObject(string soundObjectName, SoundEffect soundEffect)
        {
            soundObjects[soundObjectName] = new SoundObject(soundObjectName, soundEffect, DEFAULT_VOLUME, DEFAULT_PITCH);
        }

        public void addSoundObject(string soundObjectName, SoundEffect soundEffect, float pitch, float volume)
        {
            soundObjects[soundObjectName] = new SoundObject(soundObjectName, soundEffect, pitch, volume);
        }

        public void addSoundObject(Dictionary<string, SoundEffect> soundEffects)
        {
            foreach(string soundEffectName in soundEffects.Keys)
            {
                soundObjects[soundEffectName] = new SoundObject(soundEffectName, soundEffects[soundEffectName], DEFAULT_VOLUME, DEFAULT_PITCH);
            }
        }

        public SoundObject getSoundObject(string soundObjectName)
        {
            return soundObjects[soundObjectName];
        }

        public SoundEffectInstance getInstance(string soundObjectName)
        {
            return soundObjects[soundObjectName].instance;
        }

        public SoundEffectInstance getNewInstance(string soundObjectName)
        {
            SoundObject SO = new SoundObject(soundObjectName, soundObjects[soundObjectName].sound, soundObjects[soundObjectName].volume, soundObjects[soundObjectName].pitch);
            return SO.instance;
        }

        public void changeVolume(string soundObjectName, float volume)
        {
            soundObjects[soundObjectName].instance.Volume = volume;
            soundObjects[soundObjectName].volume = volume;
        }

        public void changePitch(string soundObjectName, float pitch)
        {
            soundObjects[soundObjectName].instance.Pitch = pitch;
            soundObjects[soundObjectName].pitch = pitch;
        }

        public void changeVolumePitch(string soundObjectName, float volume, float pitch)
        {
            changeVolume(soundObjectName, volume);
            changePitch(soundObjectName, pitch);
        }

        public void Dispose()
        {
            foreach (SoundObject SO in soundObjects.Values)
            {
                SO.instance.Dispose();
            }

            soundObjects = new Dictionary<string, SoundObject>();
        }

        public void Update(GameTime gameTime)
        {
            return;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            return;
        }
    }
}
