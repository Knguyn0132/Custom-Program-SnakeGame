using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SnakeGame.State
{
    public class GameState
    {


        private bool _finished;
        private bool _allowGameOverSound;
        private bool _isMultiplayer;
        private static GameState? _instance;


        public static GameState Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameState();
                }
                return _instance;
            }
        }


        public GameState()
        {
            _finished = false;
            _allowGameOverSound = true;
            _isMultiplayer = false;
        }
 
        public bool IsFinished
        {
            get { return _finished; }
            set { _finished = value; }
        }
        public bool IsMultiplayer
        {
            get { return _isMultiplayer; }
            set { _isMultiplayer = value; }
        }

        public void StartGame(bool isMultiplayer, List<GameObject> gameObjects, SecondSnake secondSnake)
        {
            GameTimersManager.Instance.StartAll();
            _isMultiplayer = isMultiplayer;
            _allowGameOverSound = true;

            if (isMultiplayer)
            {
                gameObjects.Add(secondSnake);
            }

        }

        public void HandleGameOver()
        {
            if (_allowGameOverSound)
            {
                SoundManager.Instance.PlayGameOverSound();
                Thread.Sleep(2000);
                _allowGameOverSound = false;
            }
        }


    }
}
