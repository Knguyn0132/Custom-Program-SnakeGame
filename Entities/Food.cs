using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Food : GameObject
    {
        private Bitmap _appleImage;

        public Food()
        {
            _appleImage = SplashKit.LoadBitmap("Apple", "assets\\images\\apple.png");
            GenerateNewPosition();
        }

        public override void Draw()
        {
            SplashKit.DrawBitmap(_appleImage, _x * SQUARE_SIZE, _y * SQUARE_SIZE);            
        }

        public void GenerateNewPosition()
        {
            Random rand = new Random();
            _x = rand.Next(0, 800 / SQUARE_SIZE);
            _y = rand.Next(0, 600 / SQUARE_SIZE);
        }

        public int X => _x;
        public int Y => _y;
    }
}