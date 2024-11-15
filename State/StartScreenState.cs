using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.State
{
    public class StartScreenState : IGameState
    {
        private Font _myFont;
        private Bitmap _startScreenImage;

        public StartScreenState()
        {
            _startScreenImage = SplashKit.LoadBitmap("Start Screen", "assets\\images\\background.png");
            _myFont = SplashKit.LoadFont("Arcade Font", "assets\\fonts\\pressstart2P.ttf")!;
        }

        // Called when entering the start screen state
        public void Enter(Game game)
        {
            SoundManager.Instance.PlayStartScreenSound();
        }

        // Called when exiting the start screen state
        public void Exit(Game game)
        {
            SoundManager.Instance.StopStartScreenSound();
        }

        // Updates the start screen state, handles user input
        public void Update(Game game)
        {
            if (SplashKit.KeyTyped(KeyCode.Num1Key))
            {
                game.TransitionToState(new PlayingState(false));
            }
            else if (SplashKit.KeyTyped(KeyCode.Num2Key))
            {
                game.TransitionToState(new PlayingState(true));
            }
            else if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                SplashKit.CloseWindow(SplashKit.CurrentWindow());
            }
        }

        // Draws the start screen
        public void Draw(Game game)
        {
            if (SplashKit.WindowCloseRequested(SplashKit.CurrentWindow()))
            {
                return;
            }

            SplashKit.ClearScreen();
            SplashKit.DrawBitmap(_startScreenImage, 0, 0);
            SplashKit.DrawText("SNAKE GAME", Color.DarkRed, _myFont, 50, 155, 150);
            SplashKit.DrawText("Press 1 for singleplayer", Color.DarkRed, _myFont, 18, 190, 300);
            SplashKit.DrawText("Press 2 for multiplayer", Color.DarkRed, _myFont, 18, 200, 360);
            SplashKit.DrawText("Press Esc to exit", Color.DarkRed, _myFont, 18, 250, 420);
            SplashKit.RefreshScreen();
        }
    }
}
