using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class RaidMonster : Monster
    {
        private Bitmap _raidMonsterImage;

        public RaidMonster() 
        {
            _raidMonsterImage = SplashKit.LoadBitmap("RaidMonster", "assets\\images\\small_spider.png");
        }

        public override void Draw()
        {
            SplashKit.DrawBitmap(_raidMonsterImage, _x * SQUARE_SIZE, _y * SQUARE_SIZE);
        }
    }

}
