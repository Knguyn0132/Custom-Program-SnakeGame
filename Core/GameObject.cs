using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SnakeGame
{
    public abstract class GameObject 
    {
        protected int _x;
        protected int _y;
        protected const int SQUARE_SIZE = 20;

        public GameObject() { }

        public abstract void Draw(); // gives common properties to all game objects.
    }
}
