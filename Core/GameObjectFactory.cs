using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public static class GameObjectFactory
    {
        public static GameObject CreateGameObject(string type)
        {
            switch (type)
            {
                case "Snake":
                    return new Snake();
                case "Food":
                    return new Food();
                case "ScoreFood":
                    return new ScoreFood();
                case "SpeedBoostFood":
                    return new SpeedBoostFood();
                case "Monster":
                    return new Monster();
                case "Boss":
                    return new Boss(6);
                case "RaidMonster":
                    return new RaidMonster();
                case "SecondSnake":
                    return new SecondSnake();
                default:
                    throw new ArgumentException("Invalid type");
            }
        }
    }
}