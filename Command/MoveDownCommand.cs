using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Command
{
    public class MoveDownCommand : ICommand
    {
        private readonly Snake _snake;

        public MoveDownCommand(Snake snake)
        {
            _snake = snake;
        }

        public void Execute()
        {
            if (_snake.CanChangeDirectionTo(Direction.Down))
            {
                _snake.Direction = Direction.Down;
            }
        }
    }
}
