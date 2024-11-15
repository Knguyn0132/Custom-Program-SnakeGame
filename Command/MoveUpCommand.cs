using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Command
{
    public class MoveUpCommand : ICommand
    {
        private readonly Snake _snake;

        public MoveUpCommand(Snake snake)
        {
            _snake = snake;
        }

        public void Execute()
        {
            if (_snake.CanChangeDirectionTo(Direction.Up))
            {
                _snake.Direction = Direction.Up;
            }
        }
    }
}
