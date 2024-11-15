using SnakeGame.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Command
{
    public class ResetGameCommand : ICommand
    {
        private readonly Game _game;

        public ResetGameCommand(Game game)
        {
            _game = game;
        }

        public void Execute()
        {
            _game.ResetGame();
            _game.TransitionToState(new StartScreenState());
        }

    }
}
