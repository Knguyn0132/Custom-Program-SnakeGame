using SnakeGame.State;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Command
{
    public class InputHandler
    {
        private Dictionary<KeyCode, ICommand> _keyBindings = new Dictionary<KeyCode, ICommand>();
        private Dictionary<KeyCode, ICommand> _multiplayerKeyBindings = new Dictionary<KeyCode, ICommand>();
        private readonly Game _game;

        public InputHandler(Game game)
        {
            _game = game;
            InitializeCommands();
        }

        // New method to initialize/reinitialize commands
        public void InitializeCommands()
        {
            // Initialize key bindings for single player
            _keyBindings = new Dictionary<KeyCode, ICommand>
                {
                    { KeyCode.WKey, new MoveUpCommand(_game.SnakeManager.Snake) },
                    { KeyCode.SKey, new MoveDownCommand(_game.SnakeManager.Snake) },
                    { KeyCode.AKey, new MoveLeftCommand(_game.SnakeManager.Snake) },
                    { KeyCode.DKey, new MoveRightCommand(_game.SnakeManager.Snake) },
                    { KeyCode.RKey, new ResetGameCommand(_game) }
                };

            // Initialize key bindings for multiplayer
            _multiplayerKeyBindings = new Dictionary<KeyCode, ICommand>
                {
                    { KeyCode.UpKey, new MoveUpCommand(_game.SnakeManager.SecondSnake) },
                    { KeyCode.DownKey, new MoveDownCommand(_game.SnakeManager.SecondSnake) },
                    { KeyCode.LeftKey, new MoveLeftCommand(_game.SnakeManager.SecondSnake) },
                    { KeyCode.RightKey, new MoveRightCommand(_game.SnakeManager.SecondSnake) }
                };
        }

        public void HandleInput(bool isMultiplayer)
        {
            // Handle single player commands
            foreach (var binding in _keyBindings)
            {
                if (SplashKit.KeyTyped(binding.Key))
                {
                    binding.Value.Execute();
                    break;
                }
            }

            // Handle multiplayer commands if in multiplayer mode
            if (isMultiplayer)
            {
                foreach (var binding in _multiplayerKeyBindings)
                {
                    if (SplashKit.KeyTyped(binding.Key))
                    {
                        binding.Value.Execute();
                        break;
                    }
                }
            }
        }

        public void HandleSpecialKeys(Game game)
        {
            if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                if (game.CurrentState is PlayingState) // Return to Startscreen if pressing escape during gameplay
                {
                    SoundManager.Instance.StopGameBackgroundSound();
                    ResetGameCommand resetCommand = new ResetGameCommand(game);
                    resetCommand.Execute();
                    SoundManager.Instance.PlayStartScreenSound();
                    DrawManager.Instance.GameObjects.Remove(game.SnakeManager.SecondSnake);
                }
                else
                {
                    SplashKit.CloseWindow(SplashKit.CurrentWindow());
                }
            }
        }
    }
}
