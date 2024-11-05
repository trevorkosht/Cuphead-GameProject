using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.GameObjects
{
    internal class BlockController : IComponent
    {
        public GameObject GameObject { get; set; }
        public bool enabled { get; set; }

        List<(GameObject platform, Vector2 start,int x, int y,int speed, bool toward)> platformList = new List<(GameObject, Vector2 start, int x, int y, int speed, bool toward)>();

        public BlockController()
        {

        }

        //chatgpt wrote this idk if it right lol
        public void Update(GameTime gameTime)
        {
            foreach (var (platform, start, x, y, speed, toward) in platformList.ToList())
            {
                // Calculate target position based on start position and x, y offsets
                Vector2 targetPosition = start + new Vector2(x, y);

                // Determine the direction based on `toward` property
                Vector2 direction = toward ? Vector2.Normalize(targetPosition - new Vector2(platform.X, platform.Y))
                                           : Vector2.Normalize(start - new Vector2(platform.X, platform.Y));

                // Calculate movement amount based on speed and elapsed time
                int distanceToMove = speed * (int)gameTime.ElapsedGameTime.TotalSeconds;

                // Move the platform's X and Y toward the target or start position
                platform.X += (int)direction.X * distanceToMove;
                platform.Y += (int)direction.Y * distanceToMove;

                // Check if the platform has reached the target position
                if (toward && Vector2.Distance(new Vector2(platform.X, platform.Y), targetPosition) < distanceToMove)
                {
                    // Platform reached target position, change direction
                    platform.X = (int)targetPosition.X; // Snap to target to avoid overshoot
                    platform.Y = (int)targetPosition.Y;
                    platformList.Remove((platform, start, x, y, speed, toward));
                    platformList.Add((platform, start, x, y, speed, !toward));
                }
                else if (!toward && Vector2.Distance(new Vector2(platform.X, platform.Y), start) < distanceToMove)
                {
                    // Platform returned to start position, change direction
                    platform.X = (int)start.X; // Snap to start to avoid overshoot
                    platform.Y = (int)start.Y;
                    platformList.Remove((platform, start, x, y, speed, toward));
                    platformList.Add((platform, start, x, y, speed, !toward));
                }
            }
        }



        public void add(GameObject platform, int x, int y, int speed)
        {
            platformList.Add((platform, platform.position,x, y,speed, true));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //does nothing
        }
    }
}
