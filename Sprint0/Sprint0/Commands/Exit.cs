using Sprint0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Commands
{
    internal class Exit : Interfaces.ICommand
    {

        private Game1 _game;

        public Exit(Game1 game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.Exit();
        }

    }
}
