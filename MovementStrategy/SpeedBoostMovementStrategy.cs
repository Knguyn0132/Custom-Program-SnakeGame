using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.MovementStrategy
{
    public class SpeedBoostMovementStrategy : IMovementStrategy
    {
        private const int SpeedBoostMoveInterval = 40;

        public void Move(Snake snake)
        {
            if (snake is SecondSnake)
            {
                if (GameTimersManager.Instance.SecondSnakeTimer.Ticks > SpeedBoostMoveInterval)
                {
                    snake.Move();
                    GameTimersManager.Instance.SecondSnakeTimer.Stop();
                    GameTimersManager.Instance.SecondSnakeTimer.Start();
                }
            }

            else
            {
                // Use the first snake's timer
                if (GameTimersManager.Instance.Timer.Ticks > SpeedBoostMoveInterval)
                {
                    snake.Move();
                    GameTimersManager.Instance.Timer.Stop();
                    GameTimersManager.Instance.Timer.Start();
                }
            }

        }
    }

}
