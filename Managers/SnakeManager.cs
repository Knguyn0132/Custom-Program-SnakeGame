using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.MovementStrategy;
using SnakeGame.State;
using SnakeGame.Utilities;

namespace SnakeGame
{
    public class SnakeManager
    {
        private Snake _snake;
        private SecondSnake _secondSnake;

        public SnakeManager(List<GameObject> gameObjects)
        {
            _snake = (Snake)GameObjectFactory.CreateGameObject("Snake");
            _secondSnake = (SecondSnake)GameObjectFactory.CreateGameObject("SecondSnake");
            gameObjects.Add(_snake);
        }


        public Snake Snake => _snake;
        public SecondSnake SecondSnake => _secondSnake;

        public void SnakeMovement(FoodManager foodManager)
        {
            // Determine the movement strategy for the first snake
            IMovementStrategy movementStrategy;
            if (foodManager.SnakeBoost)
            {
                movementStrategy = new SpeedBoostMovementStrategy();
            }
            else
            {
                movementStrategy = new NormalMovementStrategy();
            }

            //Move the first snake 
            movementStrategy.Move(_snake);

            // If multiplayer mode is active, determine the movement strategy for the second snake
            if (GameState.Instance.IsMultiplayer)
            {
                if (foodManager.SecondSnakeBoost)
                {
                    movementStrategy = new SpeedBoostMovementStrategy();
                }
                else
                {
                    movementStrategy = new NormalMovementStrategy();
                }

                // Move the second snake
                movementStrategy.Move(_secondSnake);
            }
        }



        public bool SnakeOneLose(Boss boss, bool bossActive, List<RaidMonster> additionalMonsters, List<Monster> monsters)
        {
            // Collide with the boss
            if (bossActive && boss != null && _snake.X >= boss.X && _snake.X < boss.X + boss.Size && _snake.Y >= boss.Y && _snake.Y < boss.Y + boss.Size)
            {
                GameState.Instance.IsFinished = true;
                return true;
            }
            
            //Collide with the monsters
            foreach (Monster monster in monsters)
            {
                if (_snake.X == monster.X && _snake.Y == monster.Y)
                {
                    GameState.Instance.IsFinished = true;
                    return true;
                }
            }

            //Collide with the raid monsters
            foreach (RaidMonster raidMonster in additionalMonsters)
            {
                if (_snake.X == raidMonster.X && _snake.Y == raidMonster.Y)
                {
                    GameState.Instance.IsFinished = true;
                    return true;
                }
            }

            //Collide with itself
            if (_snake.HitItself())
            {
                GameState.Instance.IsFinished = true;
                return true;
            }

            //Collide with the second snake
            if (GameState.Instance.IsMultiplayer && _snake.CollidesWith(_secondSnake))
            {
                GameState.Instance.IsFinished = true;
                return true;
            }

            return false;
        }

        public bool SnakeTwoLose(Boss boss, bool bossActive, List<RaidMonster> additionalMonsters, List<Monster>  monsters)
        {
            if (GameState.Instance.IsMultiplayer)
            {
                if (bossActive && boss != null && _secondSnake.X >= boss.X && _secondSnake.X < boss.X + boss.Size && _secondSnake.Y >= boss.Y && _secondSnake.Y < boss.Y + boss.Size) //6x6 Collision Detection
                {
                    GameState.Instance.IsFinished = true;
                    return true;
                }

                foreach (Monster monster in monsters)
                {
                    if (_secondSnake.X == monster.X && _secondSnake.Y == monster.Y)
                    {
                        GameState.Instance.IsFinished = true;
                        return true;
                    }
                }

                foreach (RaidMonster raidMonster in additionalMonsters)
                {
                    if (_secondSnake.X == raidMonster.X && _secondSnake.Y == raidMonster.Y)
                    {
                        GameState.Instance.IsFinished = true;
                        return true;
                    }
                }

                if (_secondSnake.HitItself())
                {
                    GameState.Instance.IsFinished = true;
                    return true;
                }

                if (_secondSnake.CollidesWith(_snake))
                {
                    GameState.Instance.IsFinished = true;
                    return true;
                }
            }

            return false;
        }
    }
}