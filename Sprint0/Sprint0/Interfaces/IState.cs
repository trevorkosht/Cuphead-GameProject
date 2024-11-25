using Microsoft.Xna.Framework;

public interface IState
{
    void Enter();
    void Exit();
    void Update(GameTime gameTime);
}
