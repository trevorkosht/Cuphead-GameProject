using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BossLogic : IComponent
{
    public GameObject GameObject { get; set; }
    public bool enabled { get; set; }

    public BossLogic()
    {
    }

    public void Update(GameTime gameTime)
    {

    }

    public void Draw(SpriteBatch spriteBatch)
    {
    }
}
