using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame;
using static System.Formats.Asn1.AsnWriter;

namespace SnakeGame
{
    public class FoodManager
    {
        private Food _food;
        private ScoreFood _scoreFood;
        private SpeedBoostFood _speedBoostFood;
        private bool _scoreFoodActive;
        private bool _speedBoostActive;
        private bool _speedBoostFoodActive;
        private bool _snakeBoost;
        private bool _secondSnakeBoost;

        public bool SnakeBoost => _snakeBoost;
        public bool SecondSnakeBoost => _secondSnakeBoost;
        public bool SpeedBoostActive => _speedBoostActive;

        public FoodManager(List<GameObject> gameObjects)
        {
            _food = (Food)GameObjectFactory.CreateGameObject("Food");
            _scoreFood = (ScoreFood)GameObjectFactory.CreateGameObject("ScoreFood");
            _speedBoostFood = (SpeedBoostFood)GameObjectFactory.CreateGameObject("SpeedBoostFood");
            _scoreFoodActive = false;
            _speedBoostActive = false;
            _speedBoostFoodActive = false;
            _snakeBoost = false;
            _secondSnakeBoost = false;

            gameObjects.Add(_food);
        }

        // Update both scores right in place
        public (int score, int secondSnakeScore) FoodCollision(Snake snake, SecondSnake secondSnake, List<GameObject> gameObjects, int score, int secondSnakeScore, bool isMultiplayer)
        {
            // Check for collision with the first snake
            if (snake.X == _food.X && snake.Y == _food.Y)
            {
                score++;
                snake.Grow();
                _food.GenerateNewPosition();
                SoundManager.Instance.PlayEatSound();
            }

            // Check for collision with the second snake
            if (isMultiplayer && secondSnake.X == _food.X && secondSnake.Y == _food.Y)
            {
                secondSnakeScore++;
                secondSnake.Grow();
                _food.GenerateNewPosition();
                SoundManager.Instance.PlayEatSound();
            }

            // ScoreFood collision for first snake
            if (_scoreFoodActive && snake.X == _scoreFood.X && snake.Y == _scoreFood.Y)
            {
                score += 2;
                snake.Grow();
                snake.Grow();
                _scoreFoodActive = false;
                gameObjects.Remove(_scoreFood);
                SoundManager.Instance.PlayEatSound();
            }

            // ScoreFood collision for second snake
            if (_scoreFoodActive && secondSnake.X == _scoreFood.X && secondSnake.Y == _scoreFood.Y)
            {
                secondSnakeScore += 2;
                secondSnake.Grow();
                secondSnake.Grow();
                _scoreFoodActive = false;
                gameObjects.Remove(_scoreFood);
                SoundManager.Instance.PlayEatSound();
            }

            // SpeedBoostFood collision for first snake
            if (_speedBoostFoodActive && snake.X == _speedBoostFood.X && snake.Y == _speedBoostFood.Y)
            {
                _speedBoostActive = true;
                GameTimersManager.Instance.SpeedBoostTimer.Reset();
                GameTimersManager.Instance.SpeedBoostTimer.Start();
                _speedBoostFoodActive = false;
                gameObjects.Remove(_speedBoostFood);
                _snakeBoost = true;
                SoundManager.Instance.PlayEatSound();
            }

            // SpeedBoostFood collision for second snake
            if (_speedBoostFoodActive && isMultiplayer && secondSnake.X == _speedBoostFood.X && secondSnake.Y == _speedBoostFood.Y)
            {
                _speedBoostActive = true;
                GameTimersManager.Instance.SpeedBoostTimer.Reset();
                GameTimersManager.Instance.SpeedBoostTimer.Start();
                _speedBoostFoodActive = false;
                gameObjects.Remove(_speedBoostFood);
                _secondSnakeBoost = true;
                SoundManager.Instance.PlayEatSound();
            }

            // Deactivate boost after time limit
            if (_speedBoostActive && GameTimersManager.Instance.SpeedBoostTimer.Ticks > 5000)
            {
                _speedBoostActive = false;
                GameTimersManager.Instance.SpeedBoostTimer.Stop();
                GameTimersManager.Instance.SpeedBoostTimer.Reset();
                _snakeBoost = false;
                _secondSnakeBoost = false;
            }

            // Return the updated scores
            return (score, secondSnakeScore);
        }

        public void FoodSpawn(List<GameObject> gameObjects)
        {
            if (GameTimersManager.Instance.ScoreFoodTimer.Ticks > 15000)
            {
                _scoreFoodActive = !_scoreFoodActive;
                GameTimersManager.Instance.ScoreFoodTimer.Reset();

                if (_scoreFoodActive)
                {
                    _scoreFood.GenerateNewPosition();
                    gameObjects.Add(_scoreFood);
                }
                else
                {
                    gameObjects.Remove(_scoreFood);
                }
            }


            if (GameTimersManager.Instance.SpeedBoostFoodTimer.Ticks > 20000)
            {
                _speedBoostFoodActive = !_speedBoostFoodActive;
                GameTimersManager.Instance.SpeedBoostFoodTimer.Reset();

                if (_speedBoostFoodActive)
                {
                    _speedBoostFood.GenerateNewPosition();
                    gameObjects.Add(_speedBoostFood);
                }
                else
                {
                    gameObjects.Remove(_speedBoostFood);
                }
            }
        }
    }
}
