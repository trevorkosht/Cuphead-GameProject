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

        //amount of time to delay
        private double delayTime = 0;

        private int delayCount = 0;

        private int delayFrames = 0;
        private int frameCount = 0;

        //delay the game in milliseconds
        public bool DelayTime(GameTime gameTime, int milliSeconds)
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

        // delay the game based on frames
        public bool DelayFrames(float frames)
        {
            if (delayFlag)
            {
                if (frameCount >= delayFrames)
                {
                    delayFlag = false;
                    return true; 
                }
                frameCount++; 
            }
            else
            {
                delayFlag = true;
                delayFrames = (int)frames; 
                frameCount = 0;
            }
            return false;
        }

        //reset game time
        public void Reset()
        {
            delayFlag = false;
            delayTime = 0;
        }
    }
}
