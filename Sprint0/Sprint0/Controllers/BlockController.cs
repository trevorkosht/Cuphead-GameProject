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

        public void Update(GameTime gameTime)
        {
            foreach (var (platform, start, x, y, speed, toward) in platformList.ToList())
            {
                Vector2 targetPosition = start + new Vector2(x, y);

                Vector2 direction = toward ? Vector2.Normalize(targetPosition - new Vector2(platform.X, platform.Y))
                                           : Vector2.Normalize(start - new Vector2(platform.X, platform.Y));

                int distanceToMove = speed * (int)gameTime.ElapsedGameTime.TotalSeconds;

                platform.X += (int)direction.X * distanceToMove;
                platform.Y += (int)direction.Y * distanceToMove;

                if (toward && Vector2.Distance(new Vector2(platform.X, platform.Y), targetPosition) < distanceToMove)
                {

                    platform.X = (int)targetPosition.X;
                    platform.Y = (int)targetPosition.Y;
                    platformList.Remove((platform, start, x, y, speed, toward));
                    platformList.Add((platform, start, x, y, speed, !toward));
                }
                else if (!toward && Vector2.Distance(new Vector2(platform.X, platform.Y), start) < distanceToMove)
                {

                    platform.X = (int)start.X; 
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
