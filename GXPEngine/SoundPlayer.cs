using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    //------------------------------------------------
    //           SoundPlayer : GameObject 
    //------------------------------------------------
    class SoundPlayer : GameObject
    {
        private Sound _gameMusic;
        private SoundChannel _gameMusicChannel;
        private Sound _bonk;
        private SoundChannel _bonkChannel;

        //------------------------------------------------
        //                    SoundPlayer()   
        //------------------------------------------------
        public SoundPlayer()
        {
            _gameMusic = new Sound("BlackNightTheme.mp3", true, false);
            _bonk = new Sound("Shield.wav", false, true);
        }

        //------------------------------------------------
        //                    music()   
        //------------------------------------------------
        public void gameMusic()
        {
            _gameMusicChannel = _gameMusic.Play();
            _gameMusicChannel.Volume = 0.4f;
        }

        //------------------------------------------------
        //                  Bonk()   
        //------------------------------------------------
        public void Bonk()
        {
            _bonkChannel = _bonk.Play();
            _bonkChannel.Volume = 0.5f;
        }

    }   
}
