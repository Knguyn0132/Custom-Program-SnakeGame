using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Command
{
    public class MoveRightCommand : ICommand
    {
        private readonly Snake _snake;

        public MoveRightCommand(Snake snake)
        {
            _snake = snake;
        }

        public void Execute()
        {
            if (_snake.CanChangeDirectionTo(Direction.Right))
            {
                _snake.Direction = Direction.Right;
            }
        }

    }

}
