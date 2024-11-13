using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead
{
    internal class DelayGame
    {
        private bool delayFlag = false;
        private bool coolFlag = true;

        private double delayTime = 0;
        private double coolTime = 0;

        public bool Delay(GameTime gameTime, int milliSeconds)
        {
            if (delayFlag)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds >= delayTime)
                {
                    delayFlag = false;
                    return true;
                }
            }
            else
            {
                delayFlag = true;
                delayTime = gameTime.TotalGameTime.TotalMilliseconds + milliSeconds;
            }
            return false;
        }
        public bool Cooldown(GameTime gameTime, int milliSeconds)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= coolTime)
            {
                coolFlag = true;
            }
            if (coolFlag)
            {
                coolFlag = false;
                coolTime = gameTime.TotalGameTime.TotalMilliseconds + milliSeconds;
                return true;
            }
            return false;
        }

        public bool Cooldown(GameTime gameTime, float milliSeconds)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds >= coolTime)
            {
                coolFlag = true;
            }
            if (coolFlag)
            {
                coolFlag = false;
                coolTime = gameTime.TotalGameTime.TotalMilliseconds + ((int)milliSeconds)*1000;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            delayFlag = false;
            delayTime = 0;
            coolTime = 0;
            coolFlag = true;
        }
    }
}
