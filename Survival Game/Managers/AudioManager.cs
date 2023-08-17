using System;
using SFML.Audio;

namespace The_Relic
{
    static class AudioManager
    {
        static Music mainMenuMusic;
        static Music inGameMusic;

        static SoundBuffer bufferDmgRecieved;
        static SoundBuffer bufferExplode;
        static SoundBuffer bufferShot;
        static SoundBuffer buttonBuffer;
        static SoundBuffer footStepsBuffer;

        static Sound soundDmgRecieved;
        static Sound soundExplode;
        static Sound soundShot;
        static Sound buttonSound;
        static Sound footStepsSound;

        static float musicVol;
        static float gfxVol;

        static public Music MainMenuMusic { get => mainMenuMusic; set => mainMenuMusic = value; }
        static public Music InGameMusic { get => inGameMusic; set => inGameMusic = value; }

        static public SoundBuffer DmgRecievedBuffer { get => bufferDmgRecieved; set => bufferDmgRecieved = value; }
        static public SoundBuffer ExplodeBuffer { get => bufferExplode; set => bufferExplode = value; }
        static public SoundBuffer ShotBuffer { get => bufferShot; set => bufferShot = value; }
        static public SoundBuffer ButtonBuffer { get => buttonBuffer; set => buttonBuffer = value; }
        static public SoundBuffer FootStepsBuffer { get => footStepsBuffer; set => footStepsBuffer = value; }


        static public Sound DmgRecievedSound { get => soundDmgRecieved; set => soundDmgRecieved = value; }
        static public Sound ExplodeSound { get => soundExplode; set => soundExplode = value; }
        static public Sound ShotSound { get => soundShot; set => soundShot = value; }
        static public Sound ButtonSound { get => buttonSound; set => buttonSound = value; }
        static public Sound FootStepsSound { get => footStepsSound; set => footStepsSound = value; }

        static public float MusicVol { get => musicVol; }
        static public float GFXVol { get => gfxVol; }

        static private void ActualizarVol()
        {
            if (mainMenuMusic != null) mainMenuMusic.Volume = musicVol;
            if (inGameMusic != null) inGameMusic.Volume = musicVol;

            if (soundDmgRecieved != null) DmgRecievedSound.Volume = gfxVol;
            if (soundExplode != null) ExplodeSound.Volume = gfxVol;
            if (soundShot != null) ShotSound.Volume = gfxVol;
            if (buttonSound != null) ButtonSound.Volume = gfxVol;
            if (footStepsSound != null) FootStepsSound.Volume = gfxVol;
        }

        static public void AudioInit()
        {
            if (mainMenuMusic != null)
                mainMenuMusic.Loop = true;
            if (inGameMusic != null)
                inGameMusic.Loop = true;

            if (footStepsSound != null)
                footStepsSound.Loop = true;

            if (bufferDmgRecieved != null)
                soundDmgRecieved = new Sound(bufferDmgRecieved);
            if (bufferExplode != null)
                soundExplode = new Sound(bufferExplode);
            if (bufferShot != null)
                soundShot = new Sound(bufferShot);
            if (buttonBuffer != null)
                buttonSound = new Sound(buttonBuffer);
            if (footStepsBuffer != null)
                footStepsSound = new Sound(footStepsBuffer);

            musicVol = 100f;
            gfxVol = 100f;
        }

        static public void SetMusicVol(float vol)
        {
            musicVol = (vol > 100) ? 100 : (vol < 0) ? 0 : musicVol = vol;
            ActualizarVol();
        }
        static public void SetGFXVol(float vol)
        {
            gfxVol = (vol > 100) ? 100 : (vol < 0) ? 0 : gfxVol = vol;
            ActualizarVol();
        }

        static public void AddMusicVol(float vol)
        {
            musicVol += vol;
            musicVol = Math.Clamp(musicVol, 0, 100);
            ActualizarVol();
        }
        static public void AddGFXVol(float vol)
        {
            gfxVol += vol;
            gfxVol = Math.Clamp(gfxVol, 0, 100);
            ActualizarVol();
        }

    }
}
