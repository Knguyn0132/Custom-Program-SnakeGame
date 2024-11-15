using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Command
{
    public class MoveLeftCommand : ICommand
    {
        private readonly Snake _snake;

        public MoveLeftCommand(Snake snake)
        {
            _snake = snake;
        }

        public void Execute()
        {
            if (_snake.CanChangeDirectionTo(Direction.Left))
            {
                _snake.Direction = Direction.Left;
            }
        }
    }
}
