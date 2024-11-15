using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class GameTimersManager
    {
        public SplashKitSDK.Timer Timer { get; private set; }
        public SplashKitSDK.Timer SecondSnakeTimer { get; private set; }
        public SplashKitSDK.Timer MonsterTimer { get; private set; }
        public SplashKitSDK.Timer ScoreFoodTimer { get; private set; }
        public SplashKitSDK.Timer SpeedBoostTimer { get; private set; }
        public SplashKitSDK.Timer SpeedBoostFoodTimer { get; private set; }
        public SplashKitSDK.Timer MonsterSpawnTimer { get; private set; }
        public SplashKitSDK.Timer BossTimer { get; private set; }
        public SplashKitSDK.Timer BossMoveTimer { get; private set; }
        public SplashKitSDK.Timer BossWarningTimer { get; private set; }

        private static GameTimersManager? _instance;
        public static GameTimersManager Instance
            {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameTimersManager();
                }
                return _instance;
            }
        }

        private GameTimersManager()
        {
            Timer = new SplashKitSDK.Timer("Snake Timer");
            SecondSnakeTimer = new SplashKitSDK.Timer("Second Snake Timer");
            MonsterTimer = new SplashKitSDK.Timer("Monster Timer");
            ScoreFoodTimer = new SplashKitSDK.Timer("Score Food Timer");
            SpeedBoostTimer = new SplashKitSDK.Timer("Speed Boost Timer");
            SpeedBoostFoodTimer = new SplashKitSDK.Timer("Speed Boost Food Timer");
            MonsterSpawnTimer = new SplashKitSDK.Timer("Monster Spawn Timer");
            BossTimer = new SplashKitSDK.Timer("Boss Timer");
            BossMoveTimer = new SplashKitSDK.Timer("Boss Move Timer");
            BossWarningTimer = new SplashKitSDK.Timer("Boss Warning Timer");
        }
        public void StartAll()
        {
            Timer.Start();
            SecondSnakeTimer.Start();
            MonsterTimer.Start();
            ScoreFoodTimer.Start();
            SpeedBoostFoodTimer.Start();
            MonsterSpawnTimer.Start();
            BossTimer.Start();
            BossMoveTimer.Start();
            BossWarningTimer.Start();
        }

        public void ResetAll()
        {
            Timer.Reset();
            SecondSnakeTimer.Reset();
            MonsterTimer.Reset();
            ScoreFoodTimer.Reset();
            SpeedBoostFoodTimer.Reset();
            MonsterSpawnTimer.Reset();
            BossTimer.Reset();
            BossMoveTimer.Reset();
            BossWarningTimer.Reset();
        }
    }
}
