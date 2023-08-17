using SFML.Audio;
using SFML.Window;
using SFML.Graphics;

namespace The_Relic
{
    class Program
    {
        static void Main()
        {
            VideoMode videoMode = new VideoMode(1360, 768);
            string title = "The Relic";

            RenderWindow renderWindow = new RenderWindow(videoMode, title, Styles.Titlebar);

            AudioManager.MainMenuMusic = new Music("Assets/Audio/xhale303_MenuMusic.wav");
            AudioManager.InGameMusic = new Music("Assets/Audio/sami-hiltunen_battle-music.wav");

            AudioManager.ButtonBuffer = new SoundBuffer("Assets/Audio/mixkit-ButtonPress.wav");
            AudioManager.DmgRecievedBuffer = new SoundBuffer("Assets/Audio/mixkit-DmgRecieved.wav");
            AudioManager.ExplodeBuffer = new SoundBuffer("Assets/Audio/mixkit-Explosion.wav");
            AudioManager.ShotBuffer = new SoundBuffer("Assets/Audio/mixkit-GunShot.wav");
            AudioManager.FootStepsBuffer = new SoundBuffer("Assets/Audio/FootSteps.wav");

            AudioManager.AudioInit();

            Cursor.Init();
            //Cursor.Rainbow(true);

            Input.defaultInputs();
            TextureManager.SaveSprites("Assets/Sprites/Textures.png");

            StatesController statesController = new StatesController(renderWindow);
        }
    }
}