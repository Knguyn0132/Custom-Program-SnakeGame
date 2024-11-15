using SnakeGame.Command;
using SnakeGame.State;
using SnakeGame.Utilities;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Game
    {

        private IGameState _currentState;
        private EnemyManager _enemyManager;
        private SnakeManager _snakeManager;
        private FoodManager _foodManager;
        private GameTimersManager _timers;
        private SoundManager _sound;
        private InputHandler _inputHandler;
        private DrawManager _drawManager;

        private DrawManager _drawManager2;
        public IGameState CurrentState => _currentState;
        public EnemyManager EnemyManager => _enemyManager;
        public SnakeManager SnakeManager => _snakeManager;
        public FoodManager FoodManager => _foodManager;
        public InputHandler InputHandler => _inputHandler;


        public Game()
        {
            _drawManager = DrawManager.Instance;
            _enemyManager = new EnemyManager(_drawManager.GameObjects);
            _snakeManager = new SnakeManager(_drawManager.GameObjects);
            _timers = GameTimersManager.Instance;
            _foodManager = new FoodManager(_drawManager.GameObjects);
            _sound = SoundManager.Instance;
            _inputHandler = new InputHandler(this);
            _currentState = new StartScreenState();
            _drawManager2 = DrawManager.Instance;
        }

        // Transition to a new game state
        public void TransitionToState(IGameState newState)
        {
            _currentState?.Exit(this);
            _currentState = newState;
            _currentState.Enter(this);
        }

        // Run the game loop
        public void Run()
        {
            Window gameWindow = new Window("Snake Game", 800, 600);
            _currentState.Enter(this);

            while (!SplashKit.WindowCloseRequested(gameWindow))
            {
                SplashKit.ProcessEvents();
                _currentState.Update(this);
                _currentState.Draw(this);
            }

            _currentState.Exit(this);
        }

        // Reset the game to its initial state
        public void ResetGame()
        {
            // Clear game objects
            DrawManager.Instance.GameObjects.Clear();

            // Reinitialize private fields
            _drawManager = DrawManager.Instance;
            _snakeManager = new SnakeManager(DrawManager.Instance.GameObjects);
            _foodManager = new FoodManager(DrawManager.Instance.GameObjects);
            _enemyManager = new EnemyManager(DrawManager.Instance.GameObjects);

            // Add snake to game objects
            DrawManager.Instance.GameObjects.Add(_snakeManager.Snake);

            // Reset and start game timers
            GameTimersManager.Instance.ResetAll();
            GameTimersManager.Instance.StartAll();

            // Reset scores and game state
            DrawManager.Instance.SetScore(0);
            DrawManager.Instance.SetSecondSnakeScore(0);
            GameState.Instance.IsFinished = false;

            // Initialize input commands
            _inputHandler.InitializeCommands();
        }
    }
}