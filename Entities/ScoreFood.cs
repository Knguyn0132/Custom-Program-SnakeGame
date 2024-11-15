using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class ScoreFood : Food
    {
        private Bitmap _pizzaImage;

        public ScoreFood()
        {
            _pizzaImage = SplashKit.LoadBitmap("Pizza", "assets\\images\\Pizza.png");
        }

        public override void Draw()
        {
            
            SplashKit.DrawBitmap(_pizzaImage, _x * SQUARE_SIZE, _y * SQUARE_SIZE);
        }
    }
}
