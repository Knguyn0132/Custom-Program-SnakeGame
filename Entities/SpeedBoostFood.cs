using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class SpeedBoostFood : Food
    {
        private Bitmap _speedBoostImage;

        public SpeedBoostFood()
        {
            _speedBoostImage = SplashKit.LoadBitmap("SpeedBoost", "assets\\images\\speedboost.png");
        }
        public override void Draw()
        {
            SplashKit.DrawBitmap(_speedBoostImage, _x * SQUARE_SIZE, _y * SQUARE_SIZE);
        }
    }
}
