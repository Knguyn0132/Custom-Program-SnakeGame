using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.State;
using SplashKitSDK;

namespace SnakeGame
{
    public class DrawManager
    {
        private Font _myFont;
        private int _score;
        private int _secondSnakeScore;
        private List<GameObject> _gameObjects;

        private static DrawManager? _instance;
        public static DrawManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DrawManager();
                }
                return _instance;
            }
        }

        private DrawManager()
        {
            _gameObjects = new List<GameObject> { };
            _myFont = SplashKit.LoadFont("Arcade Font", "assets\\fonts\\pressstart2P.ttf")!;
            _score = 0;
            _secondSnakeScore = 0;
        }

        public List<GameObject> GameObjects => _gameObjects;
        public int GetScore() => _score;
        public void SetScore(int value) => _score = value;

        public int GetSecondSnakeScore() => _secondSnakeScore;
        public void SetSecondSnakeScore(int value) => _secondSnakeScore = value;

        public void DrawGame(EnemyManager enemyManager, SnakeManager snakeManager)
        {
            if (SplashKit.WindowCloseRequested(SplashKit.CurrentWindow()))
            {
                return;
            }

            foreach (GameObject obj in _gameObjects)
            {
                obj.Draw();
            }


            if (enemyManager.BossWarningActive && GameTimersManager.Instance.BossWarningTimer.Ticks < 2000)
            {
                SplashKit.DrawText("Raid Incoming", Color.DarkRed, _myFont, 24, 245, 280);

            }

            SplashKit.DrawText($"Score: {_score}", Color.DarkRed, _myFont, 10, 10, 10);
            if (GameState.Instance.IsMultiplayer)
            {
                SplashKit.DrawText($"Score: {_secondSnakeScore}", Color.DarkRed, _myFont, 10, 10, 30);
            }

            SplashKit.DrawText($"Raid(s) Survived: {enemyManager.RaidsSurvived}", Color.DarkRed, _myFont, 10, 590, 10);
            if (GameState.Instance.IsFinished)
            {
                if (!GameState.Instance.IsMultiplayer) { SplashKit.DrawText($"Game over. Score: {_score}. Press 'R' to restart.", Color.DarkRed, _myFont, 15, 95, 265); }

                if (GameState.Instance.IsMultiplayer)
                {
                    if (snakeManager.SnakeOneLose(enemyManager.Boss!, enemyManager.BossActive, enemyManager.AdditionalMonsters, enemyManager.Monsters))
                    {
                        SplashKit.DrawText($"P1 Lose - P2 Win (P1 Score: {_score}, P2 Score: {_secondSnakeScore})", Color.DarkRed, _myFont, 15, 80, 270);
                        SplashKit.DrawText("Press 'R' to Restart", Color.DarkRed, _myFont, 15, 250, 300);
                    }
                    else if (snakeManager.SnakeTwoLose(enemyManager.Boss!, enemyManager.BossActive, enemyManager.AdditionalMonsters, enemyManager.Monsters))
                    {
                        SplashKit.DrawText($"P1 Win - P2 Lose (P1 Score: {_score}, P2 Score: {_secondSnakeScore})", Color.DarkRed, _myFont, 15, 80, 270);
                        SplashKit.DrawText("Press 'R' to Restart", Color.DarkRed, _myFont, 15, 250, 300);
                    }


                }
                SoundManager.Instance.StopGameBackgroundSound();
            }
        }
    }
}
