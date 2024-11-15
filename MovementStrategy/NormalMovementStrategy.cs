using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.MovementStrategy
{
    public class NormalMovementStrategy : IMovementStrategy
    {
        private const int NormalMoveInterval = 120;

        public void Move(Snake snake)
        {
            if (snake is SecondSnake)
            {
                if (GameTimersManager.Instance.SecondSnakeTimer.Ticks > NormalMoveInterval)
                {
                    snake.Move();
                    GameTimersManager.Instance.SecondSnakeTimer.Stop();
                    GameTimersManager.Instance.SecondSnakeTimer.Start();
                }
            }
            else
            {
                if (GameTimersManager.Instance.Timer.Ticks > NormalMoveInterval)
                {
                    snake.Move();
                    GameTimersManager.Instance.Timer.Stop();
                    GameTimersManager.Instance.Timer.Start();
                }
            }
        }
    }
}
