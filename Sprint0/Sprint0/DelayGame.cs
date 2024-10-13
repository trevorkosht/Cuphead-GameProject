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
        //if delay is in progress
        private bool delayFlag = false;
        private bool coolFlag = true;

        //amount of time to delay
        private double delayTime = 0;
        private double coolTime = 0;

        //delay the game in milliseconds
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

        //reset game time
        public void Reset()
        {
            delayFlag = false;
            delayTime = 0;
            coolTime = 0;
            coolFlag = true;
        }
    }
}
