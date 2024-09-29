using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Timers;
using Sprint0;
using Sprint0.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Commands
{
    internal class UpdateGame : Interfaces.ICommand
    {

        private Game1 _game;
        private GameTime _gameTime;

        public UpdateGame(Game1 game)
        {
            _game = game;
            
        }

        public void Execute()
        {
            foreach (var gameObject in _game.gameObjects)
            {
                gameObject.Update(_gameTime);
            }

            _game.enemyController.Update(_gameTime);
            _game.blockController.Update(_gameTime);
            _game.itemsControl.Update(_gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.R))
                _game.cmd.GetCommand("Reset").Execute();
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                _game.cmd.GetCommand("Exit").Execute();
        }

        public void Execute(GameTime gameTime)
        {
            _gameTime = gameTime;
            Execute();
        }

    }
}
