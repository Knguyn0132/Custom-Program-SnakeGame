using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SnakeGame
{
    public class SoundManager
    {   
        private SoundEffect _eatSound;
        private SoundEffect _gameOverSound;
        private SoundEffect _startscreenSound;
        private SoundEffect _backgroundSound;



        private static SoundManager? _instance;

        public static SoundManager Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SoundManager();  // Create the instance if it doesn't exist.
                }
                return _instance;  // Return the single instance.
            }
        }



        private SoundManager()
        {
            _eatSound = SplashKit.LoadSoundEffect("Eat", "assets\\sounds\\eat.wav");
            _gameOverSound = SplashKit.LoadSoundEffect("GameOver", "assets\\sounds\\game_over.wav");
            _startscreenSound = SplashKit.LoadSoundEffect("StartScreenSound", "assets\\sounds\\startscreen_song.wav");
            _backgroundSound = SplashKit.LoadSoundEffect("Song", "assets\\sounds\\game_song.wav");
            
        }
        public void PlayEatSound()
        {
            _eatSound.Play();
        }
        public void PlayGameOverSound()
        {
            _gameOverSound.Play();
        }

        public void StopGameBackgroundSound()
        {
            _backgroundSound.Stop();
            
        }

        public void PlayBackgroundSound()
        {
            SplashKit.PlaySoundEffect(_backgroundSound, -1); // -1 for infinite loop
        }

        public void PlayStartScreenSound()
        {
            SplashKit.PlaySoundEffect(_startscreenSound, -1);
           
        }

        public void StopStartScreenSound()
        {
            _startscreenSound.Stop();
        }

       
    }
}
