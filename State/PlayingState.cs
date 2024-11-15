using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.State
{
    public class PlayingState : IGameState
    {
        private bool _isMultiplayer;
        private Bitmap _backgroundImage;

        public PlayingState(bool isMultiplayer)
        {
            _isMultiplayer = isMultiplayer;
            _backgroundImage = SplashKit.LoadBitmap("Background", "assets\\images\\background.png");
        }

        public void Enter(Game game)
        {
            // Start the game with the specified multiplayer mode and game objects
            GameState.Instance.StartGame(_isMultiplayer, DrawManager.Instance.GameObjects, game.SnakeManager.SecondSnake);
            SoundManager.Instance.PlayBackgroundSound();
        }

        public void Exit(Game game)
        {
            // Stop the background sound when exiting the playing state
            SoundManager.Instance.StopGameBackgroundSound();
        }

        public void Update(Game game)
        {
            if (SplashKit.WindowCloseRequested(SplashKit.CurrentWindow()))
            {
                return;
            }

            game.InputHandler.HandleSpecialKeys(game);

            if (!GameState.Instance.IsFinished)
            {
                UpdateGameLogic(game);
            }
            else
            {
                HandleGameOver(game);
            }
        }

        private void UpdateGameLogic(Game game)
        {
            // Get current scores
            int score = DrawManager.Instance.GetScore();
            int secondSnakeScore = DrawManager.Instance.GetSecondSnakeScore();

            // Perform food collision detection and update scores
            (score, secondSnakeScore) = game.FoodManager.FoodCollision(
                game.SnakeManager.Snake,
                game.SnakeManager.SecondSnake,
                DrawManager.Instance.GameObjects,
                score,
                secondSnakeScore,
                _isMultiplayer
            );

            // Spawn new food objects
            game.FoodManager.FoodSpawn(DrawManager.Instance.GameObjects);

            // Update the scores
            DrawManager.Instance.SetScore(score);
            DrawManager.Instance.SetSecondSnakeScore(secondSnakeScore);



            // Handle player input
            game.InputHandler.HandleInput(_isMultiplayer);

            // Move the snakes
            game.SnakeManager.SnakeMovement(game.FoodManager);

            // Check if the first snake loses
            game.SnakeManager.SnakeOneLose(
                game.EnemyManager.Boss!,
                game.EnemyManager.BossActive,
                game.EnemyManager.AdditionalMonsters,
                game.EnemyManager.Monsters
            );

            if (_isMultiplayer)
            {
                // Check if the second snake loses in multiplayer mode
                game.SnakeManager.SnakeTwoLose(
                    game.EnemyManager.Boss!,
                    game.EnemyManager.BossActive,
                    game.EnemyManager.AdditionalMonsters,
                    game.EnemyManager.Monsters
                );
            }

            // Spawn new enemies
            game.EnemyManager.EnemiesSpawn(DrawManager.Instance.GameObjects, game.SnakeManager);

            // Move the enemies
            game.EnemyManager.EnemiesMovement(game.SnakeManager, _isMultiplayer);
        }

        private void HandleGameOver(Game game)
        {
            // Handle game over state
            GameState.Instance.HandleGameOver();

            // Check if the player wants to restart the game
            if (SplashKit.KeyTyped(KeyCode.RKey))
            {
                game.ResetGame();
                game.TransitionToState(new StartScreenState());
            }
        }

        public void Draw(Game game)
        {
            SplashKit.ClearScreen();
            SplashKit.DrawBitmap(_backgroundImage, 0, 0);
            DrawManager.Instance.DrawGame(game.EnemyManager, game.SnakeManager);
            SplashKit.RefreshScreen();
        }
    }



}
