using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead
{
    public class SoundObject
    {
        public float volume { get; set; }
        public float pitch { get; set; } 
        public string name;
        public SoundEffect sound;
        public SoundEffectInstance instance;

        public SoundObject(string name, SoundEffect sound, float volume, float pitch)
        {
            this.name = name;
            this.volume = volume;
            this.pitch = pitch;
            this.sound = sound;
            CreateInstance();
        }

        public virtual void CreateInstance()
        {
            instance = sound.CreateInstance();
            instance.Pitch = pitch;
            instance.Volume = volume;
        }
    }
}
