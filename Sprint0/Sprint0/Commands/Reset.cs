using Sprint0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Commands
{
    internal class Reset : Interfaces.ICommand
    {

        private Game1 _game;

        public Reset(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.ResetGame();
        }

    }
}
