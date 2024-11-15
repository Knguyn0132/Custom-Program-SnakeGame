using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Monster : GameObject
    {
        protected Random _random;
        private Bitmap _monsImage;

        // Constructor without generating a position automatically
        public Monster()
        {
            _random = new Random();
            _monsImage = SplashKit.LoadBitmap("Ghost", "assets\\images\\red_spider.png");
            GenerateNewPosition();
        }

        // Explicit method to initialize position
        
        public override void Draw()
        {
            SplashKit.DrawBitmap(_monsImage, _x * SQUARE_SIZE, _y * SQUARE_SIZE);
        }

        public virtual void Move()
        {
            // Simple random movement
            int direction = _random.Next(4); // 0: right, 1: left, 2: down, 3: up
            switch (direction)
            {
                case 0: _x = (_x + 1) % (800 / SQUARE_SIZE); break; // Move right
                case 1: _x = (_x - 1 + (800 / SQUARE_SIZE)) % (800 / SQUARE_SIZE); break; // Move left
                case 2: _y = (_y + 1) % (600 / SQUARE_SIZE); break; // Move down
                case 3: _y = (_y - 1 + (600 / SQUARE_SIZE)) % (600 / SQUARE_SIZE); break; // Move up
            }
        }

        public virtual void GenerateNewPosition()
        {
            _x = _random.Next(0, 800 / SQUARE_SIZE);
            _y = _random.Next(0, 600 / SQUARE_SIZE);
            
        }

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
    }


}
